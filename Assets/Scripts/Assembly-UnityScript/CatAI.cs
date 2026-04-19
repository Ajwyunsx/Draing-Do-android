using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class CatAI : MonoBehaviour
{
    public bool ForceSleep;

    private AI core;

    private Collider coll;

    private Vector3 rnd;

    public float FarFromStart;

    public float JumpSpeed;

    public float XSpeedOnJump;

    public Vector2 dashSpeed;

    public int dashTime;

    public Vector2 BlockTime;

    private bool JumpNow;

    public CatAI()
    {
        FarFromStart = 2f;
        JumpSpeed = 25f;
        XSpeedOnJump = 25f;
        dashSpeed = new Vector2(25f, 4f);
        dashTime = 10;
        BlockTime = new Vector2(140f, 160f);
    }

    public virtual void Start()
    {
        core = GetComponent<AI>();
        coll = GetComponent<Collider>();
        core.Coords = transform.position;
        if (!core.target)
        {
            core.target = Global.CurrentPlayerObject;
        }
        if (core.target != null)
        {
            core.LookTo(core.target.position, 1);
        }
        else
        {
            core.LookRnd();
        }
        Idle(50, 55);
        if (ForceSleep)
        {
            core.NewAI("forceSleep", "idle", 10, 10);
        }
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
        switch (core.ai)
        {
            case "forceSleep":
                if (!ForceSleep)
                {
                    Idle(50, 55);
                }
                break;
            case "block":
                if (core.timer <= 0)
                {
                    core.BlockOff();
                    Idle(5, 10);
                }
                if (core.HurtTimer > 0)
                {
                    Dash();
                }
                break;
            case "shift":
                if (core.timer <= 0)
                {
                    core.Layer("plat");
                    Idle(5, 10);
                    rnd.x = core.Coords.x;
                }
                break;
            case "toJump":
                if (core.HurtJump > 0)
                {
                    Dash();
                }
                else if (core.timer <= 0)
                {
                    Jump();
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
                    Idle(5, 10);
                }
                break;
            case "near":
                core.MoveToX(core.target.position.x, core.distance);
                core.LookTo(core.target.position, 1);
                if (core.HurtTimer > 0)
                {
                    Dash();
                    break;
                }
                core.Walk();
                if (core.timer <= 0)
                {
                    Idle(5, 10);
                }
                break;
            case "idle":
                core.LookTo(core.target.position, 1);
                core.MoveToX(rnd.x, core.distance);
                core.Walk();
                if (core.HurtTimer > 0)
                {
                    Dash();
                }
                else if (core.timer <= 0)
                {
                    switch (UnityEngine.Random.Range(0, 4))
                    {
                        case 0:
                            Idle(50, 100);
                            break;
                        case 1:
                            SetBlock();
                            break;
                        case 2:
                            core.NewAI("near", "idle", 50, 100);
                            break;
                        case 3:
                            core.NewAI("toJump", "charge", 50, 55);
                            break;
                    }
                }
                break;
        }
    }

    public virtual void SetBlock()
    {
        core.LookTo(core.target.position, 1);
        core.BlockUp = true;
        core.BlockByLook(1);
        core.NewAI("block", string.Empty, (int)BlockTime.x, (int)BlockTime.y);
    }

    public virtual void Idle(object min, object max)
    {
        core.BlockOff();
        core.InvincibleTimer = 0;
        core.NewAI("idle", string.Empty, RuntimeServices.UnboxInt32(min), RuntimeServices.UnboxInt32(max));
        core.Coords = transform.position;
        rnd.x = core.Coords.x + UnityEngine.Random.Range(0f - FarFromStart, FarFromStart);
    }

    public virtual void Jump()
    {
        core.JumpOn(JumpSpeed, 10);
        int direction = core.Direction;
        if (XSpeedOnJump != 0f)
        {
            core.Speed.x = (float)direction * XSpeedOnJump;
        }
        core.Layer("fly");
        JumpNow = true;
        core.NewAI("jump", string.Empty, 10, 10);
    }

    public virtual void Dash()
    {
        if (core.HurtTimer > 0)
        {
            core.Layer("shift");
            core.NewAI("shift", "jump", dashTime, dashTime);
            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                core.DashTargetX(core.target.position.x, 0f - dashSpeed.x);
                core.DashY(dashSpeed.y);
                core.LookBySpeed(1);
            }
            else
            {
                core.DashTargetX(core.target.position.x, dashSpeed.x);
                core.DashY(dashSpeed.y);
                core.LookBySpeed(1);
            }
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
