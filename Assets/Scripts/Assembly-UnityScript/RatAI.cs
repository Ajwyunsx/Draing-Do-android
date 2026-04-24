using System;
using Boo.Lang.Runtime;
using UnityEngine;

[DefaultExecutionOrder(100)]
[Serializable]
public class RatAI : MonoBehaviour
{
    public bool ForceSleep;
    private AI core;
    private Collider coll;
    private Vector3 rnd;

    public bool MoveToWall;
    public bool JumpWithHero;
    public bool JumpByCrush;
    public bool JumpAfterIdle;
    public bool JumpWhenISeeYou;

    public float SeeDistance;
    public float JumpSpeed;
    public float XSpeedOnJump;
    public bool RandomDirJump;
    public bool JumpOtherDir;
    public float FarFromStart;

    public bool ShowOnStart;
    public int ShowTimer;
    public int HideTimer;

    private float gravity;

    public int LookSet;
    public bool HideByCrush;
    public bool ShellAfterDead;
    public float ShellSpeed;

    private bool ShellRun;
    public bool AlwaysBlockAtUp;
    private bool Checked;
    private bool JumpNow;
    private int ShellDangerTimer;
    private int ShellNoTurnTimer;
    private int patrolPauseTimer;
    private int stuckFrames;
    private int wallTurnCooldown;
    private float lastX;
    private int lastPatrolSign;
    private bool horizontalRigidPrepared;

    private const int PatrolMoveMin = 90;
    private const int PatrolMoveMax = 170;
    private const int PatrolPauseMin = 20;
    private const int PatrolPauseMax = 45;
    private const int StuckFrameLimit = 20;
    private const float RatZ = -0.9f;

    public RatAI()
    {
        SeeDistance = 3f;
        JumpSpeed = 8f;
        XSpeedOnJump = 13f;
        FarFromStart = 2f;
        ShowTimer = 50;
        ShellSpeed = 10f;
    }

    public virtual void Awake()
    {
        if (ShowOnStart)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    public virtual void Start()
    {
        core = GetComponent<AI>();
        coll = GetComponent<Collider>();

        Vector3 pos = transform.position;
        pos.z = RatZ;
        transform.position = pos;

        if (AlwaysBlockAtUp)
        {
            core.BlockUp = true;
        }

        if (!core.target)
        {
            LookSet = 0;
        }

        switch (LookSet)
        {
            case -1:
                if ((bool)core.target)
                {
                    core.LookTo(core.target.position, -1);
                }
                break;
            case 1:
                if ((bool)core.target)
                {
                    core.LookTo(core.target.position, 1);
                }
                break;
            default:
                core.LookRnd();
                break;
        }

        core.Coords = transform.position;
        gravity = core.gravity;
        PrepareHorizontalRigidbody();
        lastX = transform.position.x;

        if (ShowOnStart)
        {
            coll.enabled = false;
            core.NewAI("show", string.Empty, ShowTimer, ShowTimer);
            core.gravity = 0f;
        }
        else
        {
            MoveInit();
        }

        CheckForceSleep();
    }

    public virtual void CheckForceSleep()
    {
        if (!Checked && (bool)core.target && ForceSleep)
        {
            Checked = true;
            core.NewAI("forceSleep", "idle", 10, 10);
            core.LookTo(core.target.position, 1);
        }
    }

    public virtual void MoveInit()
    {
        core.Layer("plat");
        patrolPauseTimer = 0;
        stuckFrames = 0;
        core.ThereIsWall = 0;

        if (MoveToWall)
        {
            core.checkWalls = 10;
            EnsureDirection();
            core.NewAI("moveWall", "idle", PatrolMoveMin, PatrolMoveMax);
            CommitRatVelocity((float)core.Direction * core.MaxSpeed);
        }
        else
        {
            Idle(PatrolMoveMin, PatrolMoveMax);
        }
    }

    public virtual void HideNow()
    {
        CommitRatVelocity(0f);
        core.NewAI("hide", string.Empty, 50, 50);
        core.gravity = 0f;
        SendMessage("OrderOff", 0, SendMessageOptions.DontRequireReceiver);
    }

    public virtual void FixedUpdate()
    {
        if (core.target == null && Global.CurrentPlayerObject != null)
        {
            core.target = Global.CurrentPlayerObject;
        }

        CheckForceSleep();

        if (TalkPause.IsGameplayBlocked())
        {
            return;
        }

        if (HideTimer > 0 && core.ai != "show")
        {
            HideTimer--;
            if (HideTimer <= 0)
            {
                HideNow();
                return;
            }
        }

        switch (core.ai)
        {
            case "forceSleep":
                CommitRatVelocity(0f);
                if (!ForceSleep)
                {
                    Idle(50, 55);
                }
                break;

            case "show":
                CommitRatVelocity(0f);
                if (core.timer <= 0)
                {
                    coll.enabled = true;
                    core.gravity = gravity;
                    SendMessage("OrderOn", null, SendMessageOptions.DontRequireReceiver);
                    MoveInit();
                }
                break;

            case "hide":
                CommitRatVelocity(0f);
                if (core.timer <= 0)
                {
                    UnityEngine.Object.Destroy(gameObject);
                }
                break;

            case "jump":
                UpdateJumpState();
                break;

            case "moveWall":
                UpdateMoveWall();
                break;

            case "idle":
                UpdatePatrolIdle();
                break;

            case "shell":
                UpdateShell();
                break;
        }

        if (core.ai != "dead" && core.land)
        {
            if (JumpWithHero && Global.Stomp)
            {
                Jump();
            }
            else if (JumpByCrush && core.HurtTimer > 0)
            {
                Jump();
            }
            else if (JumpWhenISeeYou && (bool)core.target && core.ISeeYou(2f, 2f) &&
                     !(core.Distance2D(core.trans.position.x, core.target.position.x) >= SeeDistance))
            {
                Jump();
            }
        }
    }

    private void UpdateJumpState()
    {
        if (JumpNow && !(core.Speed.y >= 0f))
        {
            JumpNow = false;
            core.Layer("plat");
        }

        if (core.timer <= 0 && core.land)
        {
            core.Layer("plat");
            if (!MoveToWall)
            {
                Idle(25, 100);
            }
            else
            {
                core.NewAI("moveWall", "idle", PatrolMoveMin, PatrolMoveMax);
                EnsureDirection();
                CommitRatVelocity((float)core.Direction * core.MaxSpeed);
            }
        }
    }

    private void UpdateMoveWall()
    {
        if (HandlePatrolPause())
        {
            return;
        }

        if (core.timer <= 0)
        {
            StartPatrolPause();
            core.NewAI("moveWall", "idle", PatrolMoveMin, PatrolMoveMax);
            return;
        }

        EnsureDirection();
        if (TurnFromWall())
        {
            StartPatrolPause();
            return;
        }

        float desiredX = (float)core.Direction * core.MaxSpeed;
        CommitRatVelocity(desiredX);
        core.Walk();
        CheckStuck(desiredX, false);
    }

    private void UpdatePatrolIdle()
    {
        if (HandlePatrolPause())
        {
            return;
        }

        if (core.timer <= 0 || core.HurtTimer > 0)
        {
            if (core.land && JumpAfterIdle && core.HurtTimer <= 0)
            {
                Jump();
                return;
            }
            StartPatrolPause();
            return;
        }

        float distanceToTarget = core.Distance2D(core.trans.position.x, rnd.x);
        if (distanceToTarget <= core.distance + 0.05f)
        {
            StartPatrolPause();
            return;
        }

        int direction = DirectionTo(rnd.x);
        core.Look(direction);
        float desiredX = (float)direction * core.MaxSpeed;
        CommitRatVelocity(desiredX);
        core.Walk();
        CheckStuck(desiredX, true);
    }

    private void UpdateShell()
    {
        if (!ShellRun)
        {
            CommitRatVelocity(0f);
            return;
        }

        if (ShellNoTurnTimer > 0)
        {
            ShellNoTurnTimer--;
        }

        if (ShellDangerTimer > 0)
        {
            ShellDangerTimer--;
            if (ShellDangerTimer <= 0)
            {
                core.HP = 1f;
            }
        }

        EnsureDirection();
        if (TurnFromWall())
        {
            return;
        }

        CommitRatVelocity((float)core.Direction * ShellSpeed);
    }

    public virtual void DISAPPEAR()
    {
        if (ShellAfterDead)
        {
            core.checkWalls = 1;
            core.Layer("plat");
            core.NewAI("shell", string.Empty, 10, 10);
            core.Invincible = true;
            core.BlockUp = false;
        }
    }

    public virtual void Idle(object min, object max)
    {
        patrolPauseTimer = 0;
        stuckFrames = 0;
        core.NewAI("idle", string.Empty, RuntimeServices.UnboxInt32(min), RuntimeServices.UnboxInt32(max));
        PickPatrolTarget(0);
    }

    public virtual void Jump()
    {
        core.DontMoveTimer = 0;
        core.JumpOn(JumpSpeed, 10);

        int dir = core.Direction;

        if (RandomDirJump)
        {
            dir = (UnityEngine.Random.value > 0.5f) ? 1 : -1;
        }

        if (JumpOtherDir)
        {
            dir = -dir;
        }

        if (XSpeedOnJump != 0f)
        {
            core.Speed.x = (float)dir * XSpeedOnJump;
        }

        if (MoveToWall)
        {
            core.LookBySpeed(1);
        }

        core.Layer("fly");
        JumpNow = true;
        core.NewAI("jump", string.Empty, 10, 10);
    }

    public virtual void CrushHP()
    {
        if (core.ai == "shell")
        {
            if (ShellNoTurnTimer <= 0)
            {
                if (ShellRun)
                {
                    if (core.target != null)
                    {
                        core.LookTo(core.target.position, -1);
                    }

                    ShellRun = false;
                    core.MaxSpeed = 0f;
                    CommitRatVelocity(0f);
                    core.HP = 0f;
                }
                else
                {
                    ShellNoTurnTimer = 25;

                    if (core.target != null)
                    {
                        core.LookTo(core.target.position, -1);
                    }

                    ShellRun = true;
                    core.MaxSpeed = ShellSpeed;
                    CommitRatVelocity(ShellSpeed * (float)core.Direction);
                    ShellDangerTimer = 3;
                }

                core.ThereIsWall = 0;
            }
        }
        else if (!(core.ai == "hide") && HideByCrush)
        {
            HideNow();
        }
    }

    public virtual void Custom(string text)
    {
        ForceSleep = false;
    }

    public virtual void Main()
    {
    }

    private bool HandlePatrolPause()
    {
        if (patrolPauseTimer <= 0)
        {
            return false;
        }

        patrolPauseTimer--;
        CommitRatVelocity(0f);
        core.Walk();

        if (patrolPauseTimer <= 0)
        {
            if (MoveToWall)
            {
                core.NewAI("moveWall", "idle", PatrolMoveMin, PatrolMoveMax);
                EnsureDirection();
            }
            else
            {
                Idle(PatrolMoveMin, PatrolMoveMax);
            }
        }
        return true;
    }

    private void StartPatrolPause()
    {
        patrolPauseTimer = UnityEngine.Random.Range(PatrolPauseMin, PatrolPauseMax + 1);
        stuckFrames = 0;
        core.ThereIsWall = 0;
        CommitRatVelocity(0f);
        core.Walk();
    }

    private void PickPatrolTarget(int preferredSign)
    {
        float patrolRange = Mathf.Max(FarFromStart, core.distance + 0.75f);
        float offset = UnityEngine.Random.Range(core.distance + 0.35f, patrolRange);
        int sign = preferredSign;

        if (sign == 0)
        {
            sign = (lastPatrolSign == 0) ? ((UnityEngine.Random.value > 0.5f) ? 1 : -1) : -lastPatrolSign;
        }

        if (sign == 0)
        {
            sign = 1;
        }

        lastPatrolSign = sign;
        rnd.x = core.Coords.x + offset * (float)sign;
    }

    private void PrepareHorizontalRigidbody()
    {
        if (core == null)
        {
            return;
        }
        if (horizontalRigidPrepared && (bool)core.rigid)
        {
            return;
        }
        if (!(bool)core.rigid)
        {
            core.rigid = GetComponent<Rigidbody>();
        }
        if (!(bool)core.rigid)
        {
            return;
        }

        core.VelocityBySpeed = true;
        core.rigid.isKinematic = false;
        core.rigid.useGravity = false;
        core.rigid.interpolation = RigidbodyInterpolation.Interpolate;
        RigidbodyConstraints constraints = core.rigid.constraints;
        constraints &= ~RigidbodyConstraints.FreezePositionX;
        constraints &= ~RigidbodyConstraints.FreezePositionY;
        constraints |= RigidbodyConstraints.FreezePositionZ;
        constraints |= RigidbodyConstraints.FreezeRotationX;
        constraints |= RigidbodyConstraints.FreezeRotationY;
        constraints |= RigidbodyConstraints.FreezeRotationZ;
        core.rigid.constraints = constraints;
        horizontalRigidPrepared = true;
        core.rigid.WakeUp();
    }

    private void CommitRatVelocity(float desiredX)
    {
        PrepareHorizontalRigidbody();
        core.DontMoveTimer = 0;
        core.Speed.x = desiredX;

        if (!(bool)core.rigid)
        {
            DriveTransformPosition(desiredX);
            return;
        }

        Vector3 velocity = core.rigid.velocity;
        velocity.x = 0f;
        velocity.z = 0f;
        core.rigid.velocity = velocity;

        // Keep horizontal movement on one path. Mixing velocity and MovePosition caused visible jitter.
        DriveRigidbodyPosition(desiredX);
        core.rigid.WakeUp();
    }

    private void DriveRigidbodyPosition(float desiredX)
    {
        if (Mathf.Abs(desiredX) <= 0.01f || !(bool)core.rigid)
        {
            return;
        }

        Vector3 position = core.rigid.position;
        position.x += desiredX * Time.fixedDeltaTime;
        position.z = RatZ;
        core.rigid.MovePosition(position);
    }

    private void DriveTransformPosition(float desiredX)
    {
        if (Mathf.Abs(desiredX) <= 0.01f)
        {
            return;
        }

        Vector3 position = transform.position;
        position.x += desiredX * Time.fixedDeltaTime;
        position.z = RatZ;
        transform.position = position;
    }

    private bool TurnFromWall()
    {
        if (wallTurnCooldown > 0)
        {
            wallTurnCooldown--;
            core.ThereIsWall = 0;
            return false;
        }

        if (core.ThereIsWall > 0)
        {
            core.Look(1);
            wallTurnCooldown = 8;
            core.ThereIsWall = 0;
            return true;
        }

        if (core.ThereIsWall < 0)
        {
            core.Look(-1);
            wallTurnCooldown = 8;
            core.ThereIsWall = 0;
            return true;
        }

        return false;
    }

    private void CheckStuck(float desiredX, bool repickTarget)
    {
        if (Mathf.Abs(desiredX) <= 0.01f)
        {
            stuckFrames = 0;
            lastX = transform.position.x;
            return;
        }

        if (Mathf.Abs(transform.position.x - lastX) <= 0.0025f)
        {
            stuckFrames++;
        }
        else
        {
            stuckFrames = 0;
        }

        if (stuckFrames >= StuckFrameLimit)
        {
            int newDirection = -core.Direction;
            if (newDirection == 0)
            {
                newDirection = (desiredX > 0f) ? -1 : 1;
            }
            core.Look(newDirection);
            if (repickTarget)
            {
                PickPatrolTarget(newDirection);
            }
            core.ThereIsWall = 0;
            wallTurnCooldown = 8;
            StartPatrolPause();
        }

        lastX = transform.position.x;
    }

    private void EnsureDirection()
    {
        if (core.Direction == 0)
        {
            core.LookRnd();
        }
    }

    private int DirectionTo(float targetX)
    {
        int direction = (int)Mathf.Sign(targetX - core.trans.position.x);
        if (direction == 0)
        {
            direction = (core.Direction == 0) ? 1 : core.Direction;
        }
        return direction;
    }
}
