using System;
using Boo.Lang.Runtime;
using UnityEngine;

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

    // 新增：防止碰墙后连续来回抽搐
    private int WallTurnCooldown;

    public RatAI()
    {
        SeeDistance = 3f;
        JumpSpeed = 8f;
        XSpeedOnJump = 13f;
        FarFromStart = 2f;
        ShowTimer = 50;
        ShellSpeed = 10f;
        WallTurnCooldown = 0;
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

        float z = -0.9f;
        Vector3 position = transform.position;
        position.z = z;
        transform.position = position;

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
                core.LookTo(core.target.position, -1);
                break;
            case 1:
                core.LookTo(core.target.position, 1);
                break;
            default:
                core.LookRnd();
                break;
        }

        core.Coords = transform.position;
        gravity = core.gravity;

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

        if (MoveToWall)
        {
            core.checkWalls = 10;
            core.NewAI("moveWall", "idle", 10, 10);

            // 开局补一个速度，避免站着不动
            core.Speed.x = core.Direction * core.MaxSpeed;
        }
        else
        {
            Idle(10, 55);
        }
    }

    public virtual void HideNow()
    {
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

        if (WallTurnCooldown > 0)
        {
            WallTurnCooldown--;
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
                if (!ForceSleep)
                {
                    Idle(50, 55);
                }
                break;

            case "show":
                if (core.timer <= 0)
                {
                    coll.enabled = true;
                    core.gravity = gravity;
                    SendMessage("OrderOn", null, SendMessageOptions.DontRequireReceiver);
                    MoveInit();
                }
                break;

            case "hide":
                if (core.timer <= 0)
                {
                    UnityEngine.Object.Destroy(gameObject);
                }
                break;

            case "jump":
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
                        core.NewAI("moveWall", "idle", 10, 10);
                        core.Speed.x = core.Direction * core.MaxSpeed;
                    }
                }
                break;

            case "moveWall":
                // 一直按当前方向移动
                core.Move(core.Direction, 0f);
                core.Walk();

                // 关键修复：墙在一边，就转向另一边
                if (WallTurnCooldown <= 0)
                {
                    if (core.ThereIsWall > 0)
                    {
                        core.Look(-1);
                        core.Speed.x = -core.MaxSpeed;
                        core.ThereIsWall = 0;
                        WallTurnCooldown = 8;
                    }
                    else if (core.ThereIsWall < 0)
                    {
                        core.Look(1);
                        core.Speed.x = core.MaxSpeed;
                        core.ThereIsWall = 0;
                        WallTurnCooldown = 8;
                    }
                }
                else
                {
                    // 冷却期间不重复转向，但要把墙标记清掉
                    core.ThereIsWall = 0;
                }
                break;

            case "idle":
                if (core.target != null)
                {
                    core.LookTo(core.target.position, 1);
                }

                core.MoveToX(rnd.x, core.distance);
                core.Walk();

                if (core.timer <= 0 || core.HurtTimer > 0)
                {
                    if (core.land && JumpAfterIdle && core.HurtTimer <= 0)
                    {
                        Jump();
                        return;
                    }
                    Idle(25, 100);
                }
                break;

            case "shell":
                if (!ShellRun)
                {
                    break;
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

                core.Move(core.Direction, 0f);

                if (WallTurnCooldown <= 0)
                {
                    if (core.ThereIsWall > 0)
                    {
                        core.Look(-1);
                        core.Speed.x = -core.MaxSpeed;
                        core.ThereIsWall = 0;
                        WallTurnCooldown = 8;
                    }
                    else if (core.ThereIsWall < 0)
                    {
                        core.Look(1);
                        core.Speed.x = core.MaxSpeed;
                        core.ThereIsWall = 0;
                        WallTurnCooldown = 8;
                    }
                }
                else
                {
                    core.ThereIsWall = 0;
                }
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
            else if (JumpWhenISeeYou && core.target != null && core.ISeeYou(2f, 2f) && !(core.Distance2D(core.trans.position.x, core.target.position.x) >= SeeDistance))
            {
                Jump();
            }
        }
    }

    public virtual void DISAPPEAR()
    {
        if (ShellAfterDead)
        {
            core.MarkDeathHandled();
            core.checkWalls = 1;
            core.Layer("plat");
            core.NewAI("shell", string.Empty, 10, 10);
            core.Invincible = true;
            core.BlockUp = false;
        }
    }

    public virtual void Idle(object min, object max)
    {
        core.NewAI("idle", string.Empty, RuntimeServices.UnboxInt32(min), RuntimeServices.UnboxInt32(max));
        rnd.x = core.Coords.x + UnityEngine.Random.Range(0f - FarFromStart, FarFromStart);
    }

    public virtual void Jump()
    {
        core.DontMoveTimer = 0;
        core.JumpOn(JumpSpeed, 10);

        int num = core.Direction;
        if (RandomDirJump)
        {
            num = (int)Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
            if (num == 0)
            {
                num = 1;
            }
        }

        if (JumpOtherDir)
        {
            num = -num;
        }

        if (XSpeedOnJump != 0f)
        {
            core.Speed.x = (float)num * XSpeedOnJump;
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
                    else
                    {
                        core.LookRnd();
                    }

                    ShellRun = false;
                    core.MaxSpeed = 0f;
                    core.Speed.x = 0f;
                    core.HP = 0f;
                }
                else
                {
                    ShellNoTurnTimer = 25;

                    if (core.target != null)
                    {
                        core.LookTo(core.target.position, -1);
                    }
                    else
                    {
                        core.LookRnd();
                    }

                    ShellRun = true;
                    core.MaxSpeed = ShellSpeed;
                    core.Speed.x = ShellSpeed * (float)core.Direction;
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
}