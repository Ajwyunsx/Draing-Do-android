using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class PonyAI : MonoBehaviour
{
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

    public string WalkAnimName;

    private bool JumpNow;

    private int ShellDangerTimer;

    private int ShellNoTurnTimer;

    public PonyAI()
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
        float z = -0.9f;
        Vector3 position = transform.position;
        float num = (position.z = z);
        Vector3 vector = (transform.position = position);
        core.WalkAnimName = WalkAnimName;
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
        if (ShowOnStart)
        {
            coll.enabled = false;
            core.NewAI("show", "Pony Show", ShowTimer, ShowTimer);
            core.gravity = 0f;
        }
        else
        {
            MoveInit();
        }
    }

    public virtual void MoveInit()
    {
        core.Layer("plat");
        if (MoveToWall)
        {
            core.checkWalls = 10;
            core.NewAI("moveWall", "Pony Idle", 10, 10);
        }
        else
        {
            core.NewAI("idle", "Pony Idle", 10, 55);
        }
    }

    public virtual void HideNow()
    {
        core.NewAI("hide", "Pony Hide", 50, 50);
        core.gravity = 0f;
        SendMessage("OrderOff", 0, SendMessageOptions.DontRequireReceiver);
    }

    public virtual void FixedUpdate()
    {
        if (core.target == null && Global.CurrentPlayerObject != null)
        {
            core.target = Global.CurrentPlayerObject;
        }
        if (core.target == null)
        {
            return;
        }
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
                        core.NewAI("moveWall", "Pony Idle", 10, 10);
                    }
                }
                break;
            case "moveWall":
                core.Move(core.Direction, 0f);
                core.Walk();
                if (core.ThereIsWall > 0)
                {
                    core.Look(1);
                    core.ThereIsWall = 0;
                }
                if (core.ThereIsWall < 0)
                {
                    core.Look(-1);
                    core.ThereIsWall = 0;
                }
                break;
            case "idle":
                core.LookTo(core.target.position, 1);
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
                if (core.ThereIsWall > 0)
                {
                    core.Look(1);
                    core.ThereIsWall = 0;
                }
                if (core.ThereIsWall < 0)
                {
                    core.Look(-1);
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
            else if (JumpWhenISeeYou && core.ISeeYou(2f, 2f) && !(core.Distance2D(core.trans.position.x, core.target.position.x) >= SeeDistance))
            {
                Jump();
            }
        }
    }

    public virtual void DISAPPEAR()
    {
        if (ShellAfterDead)
        {
            core.checkWalls = 1;
            core.Layer("plat");
            core.NewAI("shell", "Pony Idle", 10, 10);
            core.Invincible = true;
            core.BlockUp = false;
        }
    }

    public virtual void Idle(object min, object max)
    {
        core.NewAI("idle", "Pony Idle", RuntimeServices.UnboxInt32(min), RuntimeServices.UnboxInt32(max));
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
        core.NewAI("jump", "Pony Jump", 10, 10);
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

    public virtual void Main()
    {
    }
}
