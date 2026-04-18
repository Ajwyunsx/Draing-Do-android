using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class AI : MonoBehaviour
{
    public float Level;

    public float HP;

    [HideInInspector]
    public float MaxHP;

    public float POWER;

    public float Defense;

    [HideInInspector]
    public float DefenseFactor;

    public int EXP;

    public float PowFactor;

    public float BlockFactor;

    [HideInInspector]
    public bool NoTextBlock;

    public float maxHurt;

    public string DeadLevel;

    [HideInInspector]
    public bool BlockUp;

    [HideInInspector]
    public bool BlockLeft;

    [HideInInspector]
    public bool BlockRight;

    public bool VelocityBySpeed;

    public float gravity;

    public float MaxFallSpeed;

    [HideInInspector]
    public bool land;

    [HideInInspector]
    public int landTimer;

    [HideInInspector]
    public string WalkAnimName;

    [HideInInspector]
    public string ai;

    [HideInInspector]
    public int timer;

    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public Animation animOld;

    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public int HurtTimer;

    [HideInInspector]
    public int HurtJump;

    [HideInInspector]
    public int OuchTimer;

    public float distance;

    public float AnimSpeed;

    public bool WalkAnim;

    public bool DeadRightNow;

    [HideInInspector]
    public int ItemHP;

    [HideInInspector]
    public int ItemPower;

    public bool NearEvader;

    [HideInInspector]
    public float FactorNear;

    public bool CorrectHorizon;

    [HideInInspector]
    public float HorizonY;

    [HideInInspector]
    public bool Invincible;

    public float MaxSpeed;

    [HideInInspector]
    public Vector3 Speed;

    private Vector3 NeedSpeed;

    public float FactorStop;

    [HideInInspector]
    public int DontMoveTimer;

    public bool NoDontMove;

    [HideInInspector]
    public int WindWaker;

    [HideInInspector]
    public Rigidbody rigid;

    [HideInInspector]
    public Transform trans;

    public int Direction;

    private int oldDirection;

    private Vector3 StartScale;

    [HideInInspector]
    public Vector3 Coords;

    public AudioClip SFXHit;

    public GameObject[] PartsByStrike;

    public float PartHigh;

    public int lookCorr;

    public bool NoStomp;

    [HideInInspector]
    public int InvincibleTimer;

    private bool onceDead;

    private bool deathSequenceActive;

    private bool deathHandled;

    private bool fadeOutActive;

    private int fadeOutTimer;

    private int fadeOutTimerMax;

    private Renderer[] fadeRenderers;

    private tk2dBaseSprite[] fadeSprites;

    [HideInInspector]
    public int oldWalk;

    private int noTrigColl;

    private bool heroIsHigher;

    [HideInInspector]
    public int checkWalls;

    [HideInInspector]
    public int ThereIsWall;

    private int FromUp;

    public int POISON;

    public string EnemyName;

    // ===== 修复移动卡住新增 =====
    private Vector3 lastPos;
    private int stuckTimer;
    // ==========================

    public AI()
    {
        Level = 10f;
        DefenseFactor = 1f;
        PowFactor = 1f;
        BlockFactor = 0.1f;
        maxHurt = 1.75f;
        VelocityBySpeed = true;
        MaxFallSpeed = 10f;
        distance = 3.5f;
        AnimSpeed = 1f;
        FactorNear = 0.35f;
        MaxSpeed = 3f;
        FactorStop = 0.975f;
        NoDontMove = true;
        Direction = 1;
        PartHigh = 0.75f;
        lookCorr = 1;
        oldWalk = 2;
        EnemyName = "unknown enemy";
    }

    public virtual void Awake()
    {
        if (HP == 0f)
        {
            HP = Level;
        }
        if (POWER == 0f)
        {
            POWER = Level;
        }
        if (Defense == 0f)
        {
            Defense = Level * 0.25f;
        }
        if (EXP == 0)
        {
            EXP = (int)Level;
        }
        InvincibleTimer = 20;
        anim = GetComponent<Animator>();
        if ((bool)anim)
        {
            anim.speed = AnimSpeed * UnityEngine.Random.Range(0.95f, 1.05f);
        }
        animOld = gameObject.GetComponent<Animation>();
        distance = UnityEngine.Random.Range(distance * 0.9f, distance * 1.2f);
        rigid = GetComponent<Rigidbody>();
        trans = transform;
        StartScale = trans.localScale;
        MaxHP = HP;
        if (target == null)
        {
            target = Global.CurrentPlayerObject;
        }

        // 修复移动卡住初始化
        lastPos = trans.position;
        stuckTimer = 0;
    }

    public virtual void Start()
    {
        target = Global.CurrentPlayerObject;
    }

    public virtual void FixedUpdate()
    {
        if (target == null && Global.CurrentPlayerObject != null)
        {
            target = Global.CurrentPlayerObject;
        }
        if (!TalkPause.IsGameplayBlocked())
        {
            if (timer > 0)
            {
                timer--;
            }
            if (InvincibleTimer > 0)
            {
                InvincibleTimer--;
            }
            if (FromUp > 0)
            {
                FromUp--;
            }
            if (noTrigColl > 0)
            {
                noTrigColl--;
            }
            if (HurtJump > 0)
            {
                HurtJump--;
            }
            if (OuchTimer > 0)
            {
                OuchTimer--;
            }
            if (HurtTimer > 0)
            {
                HurtTimer--;
            }
            if (DontMoveTimer > 0)
            {
                DontMoveTimer--;
                Speed.x = 0f;
            }
            if (landTimer > 0)
            {
                landTimer--;
            }
            if (deathSequenceActive && timer <= 0 && !fadeOutActive)
            {
                StartFadeOut(25);
            }
            if (fadeOutActive)
            {
                UpdateFadeOut();
            }
        }
        if (deathSequenceActive)
        {
            Speed.x *= 0.9f;
            NeedSpeed.x *= 0.9f;
            return;
        }
        if (!VelocityBySpeed)
        {
            return;
        }
        if (!(gravity <= 0f))
        {
            if (!(Speed.y <= 0f - MaxFallSpeed))
            {
                Speed.y -= gravity;
            }
            else
            {
                Speed.y = 0f - MaxFallSpeed;
            }
        }

        // ===== 修复移动卡住逻辑 =====
        Vector3 currentPos = trans.position;
        bool tryingMoveX = Mathf.Abs(Speed.x + NeedSpeed.x) > 0.1f;
        bool barelyMovedX = Mathf.Abs(currentPos.x - lastPos.x) < 0.005f;

        // 只在落地状态检测水平卡住，避免跳跃/击退时误判
        if (tryingMoveX && barelyMovedX && land && DontMoveTimer <= 0)
        {
            stuckTimer++;
        }
        else
        {
            stuckTimer = 0;
        }

        // 连续几帧没动，判定为卡住，自动反向脱困
        if (stuckTimer >= 8)
        {
            stuckTimer = 0;

            Direction = -Direction;
            if (Direction == 0)
            {
                Direction = 1;
            }
            Look(Direction);

            Speed.x = Direction * MaxSpeed;
            NeedSpeed.x = Direction * 0.5f;

            ThereIsWall = 0;
            DontMoveTimer = 0;
        }
        // ==========================

        rigid.velocity = Speed + NeedSpeed;
        lastPos = currentPos;

        Speed *= 0.9f;
        NeedSpeed *= FactorStop;
    }

    public virtual GameObject FindTag(string name, bool random)
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag(name);
        return array[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)array))];
    }

    public virtual bool TrySetTrigger(string parameterName)
    {
        if (!HasAnimatorParameter(parameterName, AnimatorControllerParameterType.Trigger))
        {
            return false;
        }
        anim.SetTrigger(parameterName);
        return true;
    }

    public virtual bool TrySetInteger(string parameterName, int value)
    {
        if (!HasAnimatorParameter(parameterName, AnimatorControllerParameterType.Int))
        {
            return false;
        }
        anim.SetInteger(parameterName, value);
        return true;
    }

    public virtual void Look(int direction)
    {
        if (oldDirection != direction)
        {
            if (direction == 0)
            {
                direction = 1;
            }
            Direction = direction;
            oldDirection = Direction;
            float x = StartScale.x * (float)Direction;
            Vector3 localScale = trans.localScale;
            float num = (localScale.x = x);
            Vector3 vector = (trans.localScale = localScale);
        }
    }

    public virtual void LookTo(Vector3 targ, int corr)
    {
        if (!(targ.x <= trans.position.x))
        {
            Direction = 1 * corr;
        }
        else
        {
            Direction = -1 * corr;
        }
        if (oldDirection != Direction)
        {
            oldDirection = Direction;
            float x = StartScale.x * (float)Direction;
            Vector3 localScale = trans.localScale;
            float num = (localScale.x = x);
            Vector3 vector = (trans.localScale = localScale);
        }
    }

    public virtual void LookRnd()
    {
        Direction = (int)Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
        if (Direction == 0)
        {
            Direction = 1;
        }
        if (oldDirection != Direction)
        {
            oldDirection = Direction;
            float x = StartScale.x * (float)Direction;
            Vector3 localScale = trans.localScale;
            float num = (localScale.x = x);
            Vector3 vector = (trans.localScale = localScale);
        }
    }

    public virtual void LookBySpeed(int factor)
    {
        if (factor == 0)
        {
            factor = 1;
        }
        if (!(Speed.x <= 0f))
        {
            Direction = 1 * factor;
        }
        else
        {
            Direction = -1 * factor;
        }
        if (oldDirection != Direction)
        {
            oldDirection = Direction;
            float x = StartScale.x * (float)Direction;
            Vector3 localScale = trans.localScale;
            float num = (localScale.x = x);
            Vector3 vector = (trans.localScale = localScale);
        }
    }

    public virtual void Move(float xx, float yy)
    {
        if (xx != 0f)
        {
            Speed.x = xx * MaxSpeed;
        }
        if (yy != 0f)
        {
            Speed.y = yy * MaxSpeed;
        }
    }

    public virtual void MoveTo(Vector3 targ, float dist)
    {
        float num = Distance(trans.position, targ);
        Vector3 vector = default(Vector3);
        vector = (targ - trans.position).normalized;
        if (CorrectHorizon)
        {
            float num2 = targ.y + HorizonY;
            if (!(Distance2D(num2, trans.position.y) <= 0.4f))
            {
                Speed.y += Mathf.Sign(num2 - trans.position.y) * MaxSpeed * 0.125f;
                if (!(num * 1.01f > dist))
                {
                    return;
                }
            }
        }
        if (!(num <= dist))
        {
            Speed.x = vector.x * MaxSpeed;
            Speed.y = vector.y * MaxSpeed;
        }
        else if (NearEvader && !(num >= dist * FactorNear))
        {
            Speed.x = (0f - vector.x) * MaxSpeed;
            Speed.y = (0f - vector.y) * MaxSpeed;
        }
    }

    public virtual void MoveToX(float targ, float dist)
    {
        float num = Distance2D(trans.position.x, targ);
        int num2 = (int)Mathf.Sign(targ - trans.position.x);

        if (num2 == 0)
        {
            num2 = (Direction != 0) ? Direction : 1;
        }

        if (num > dist)
        {
            Speed.x = (float)num2 * MaxSpeed;
        }
        else if (NearEvader && num < dist * FactorNear)
        {
            Speed.x = (float)(-num2) * MaxSpeed;
        }
        else
        {
            // 到达范围后直接停下，避免贴墙抖动/来回抽搐
            Speed.x = 0f;
        }
    }

    public virtual void MoveToY(float targ, float dist)
    {
        float num = Distance2D(trans.position.y, targ);
        int num2 = default(int);
        num2 = (int)Mathf.Sign(targ - trans.position.y);
        if (!(num <= dist))
        {
            Speed.y = (float)num2 * MaxSpeed;
        }
        else if (NearEvader && !(num >= dist * FactorNear))
        {
            Speed.y = (float)(-num2) * MaxSpeed;
        }
    }

    public virtual float Distance2D(float x1, float x2)
    {
        return Mathf.Abs(x1 - x2);
    }

    public virtual void DashTarget(Vector3 targ, float speed)
    {
        Vector3 normalized = (targ - trans.position).normalized;
        Speed.x = normalized.x * speed;
        Speed.y = normalized.y * speed;
        DontMoveTimer = 0;
    }

    public virtual void DashTargetX(float targ, float speed)
    {
        int num = (int)Mathf.Sign(targ - trans.position.x);
        if (num == 0)
        {
            num = 1;
        }
        Speed.x = (float)num * speed;
        DontMoveTimer = 0;
    }

    public virtual void DashTargetY(float targ, float speed)
    {
        int num = (int)Mathf.Sign(targ - trans.position.y);
        if (num == 0)
        {
            num = 1;
        }
        Speed.y = (float)num * speed;
        DontMoveTimer = 0;
    }

    public virtual void DashX(float speed)
    {
        Speed.x = speed;
        DontMoveTimer = 0;
    }

    public virtual void DashY(float speed)
    {
        Speed.y = speed;
        DontMoveTimer = 0;
    }

    public virtual float Distance(Vector3 trans1, Vector3 trans2)
    {
        return Vector2.Distance(new Vector2(trans1.x, trans1.y), new Vector2(trans2.x, trans2.y));
    }

    public virtual void Shock(Vector3 targ, float power)
    {
        HP -= power;
        Vector3 normalized = (targ - trans.position).normalized;
        if (!NoDontMove)
        {
            DontMoveTimer = 20;
        }
        InvincibleTimer = 20;
        NeedSpeed.x = (0f - normalized.x) * 1.5f;
        NeedSpeed.y = (0f - normalized.y) * 1.5f;
        Global.GetEnemyHit(new Vector3(HP, MaxHP, 0f), this);
        if ((bool)SFXHit)
        {
            AudioSource.PlayClipAtPoint(SFXHit, trans.position);
        }
        if (!(HP > 0f) && !onceDead)
        {
            onceDead = true;
            int num = (int)((float)EXP + UnityEngine.Random.Range((float)EXP * -0.1f, (float)EXP * 0.1f));
            Global.Experience += num;
            gameObject.layer = 30;
            deathHandled = false;
            gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
            if (!deathHandled && !deathSequenceActive && !fadeOutActive)
            {
                StartDeathSequence(100);
            }
            if (num > 0)
            {
                Global.CreateText("+ " + num + " Exp", trans.position + new Vector3(0f, 0f, -2f), new Color(1f, 1f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
            }
            if (DeadRightNow)
            {
                UnityEngine.Object.Destroy(gameObject);
            }
        }
    }

    public virtual void ShockIt(int power)
    {
        HurtMessage(power);
        Shock(trans.position, power);
    }

    public virtual void StrikeShot(GameObject @object, Vector3 targ, int shotPower, float shotSpeed, int shotTimer, Transform WeaponPoint)
    {
        if ((bool)@object)
        {
            Global.LastCreatedObject = UnityEngine.Object.Instantiate(@object, WeaponPoint.position, Quaternion.identity) as GameObject;
            float z = Mathf.Atan2(targ.y - trans.position.y, targ.x - trans.position.x) * 57.29578f + 90f;
            Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
            float num = (eulerAngles.z = z);
            Vector3 vector = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
            if ((bool)Global.LastCreatedObject.GetComponent<ShotObject>())
            {
                Global.LastCreatedObject.GetComponent<ShotObject>().timer = shotTimer;
                Global.LastCreatedObject.GetComponent<ShotObject>().ShotPower(shotPower);
            }
            Vector3 vector3 = Quaternion.Euler(0f, 0f, Global.LastCreatedObject.transform.eulerAngles.z) * Vector3.down;
            if ((bool)Global.LastCreatedObject.GetComponent<Rigidbody>())
            {
                Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = vector3 * shotSpeed;
            }
        }
    }

    public virtual void CreatePartsByStrike()
    {
        for (int i = 0; i < Extensions.get_length((System.Array)PartsByStrike); i++)
        {
            if ((bool)PartsByStrike[i])
            {
                Global.LastCreatedObject = UnityEngine.Object.Instantiate(PartsByStrike[i], trans.position + new Vector3(0f, PartHigh, -1f), Quaternion.identity) as GameObject;
            }
        }
        Global.LastCreatedObject = null;
    }

    public virtual void Walk()
    {
        if (!WalkAnim)
        {
            return;
        }
        if (!(Mathf.Abs(Speed.x) <= 0.35f))
        {
            if (Mathf.Sign(Speed.x) != Mathf.Sign(Direction))
            {
                if (oldWalk != -1)
                {
                    oldWalk = -1;
                    if ((bool)anim)
                    {
                        TrySetInteger("walk", -1);
                        TrySetTrigger("idle");
                    }
                    else if ((bool)animOld)
                    {
                        animOld.CrossFade(WalkAnimName, 0.2f);
                    }
                }
            }
            else if (oldWalk != 1)
            {
                oldWalk = 1;
                if ((bool)anim)
                {
                    TrySetInteger("walk", 1);
                    TrySetTrigger("idle");
                }
                else if ((bool)animOld)
                {
                    animOld.CrossFade(WalkAnimName, 0.2f);
                }
            }
        }
        else if (oldWalk != 0)
        {
            oldWalk = 0;
            if ((bool)anim)
            {
                TrySetInteger("walk", 0);
                TrySetTrigger("idle");
            }
            else if ((bool)animOld)
            {
                animOld.CrossFade("Pony Idle", 0.2f);
            }
        }
    }

    public virtual void CrushHP()
    {
        if (Invincible || Global.LastStrike.clan == "foe" || ai == "dead" || InvincibleTimer > 0)
        {
            return;
        }
        bool flag = false;
        if ((bool)Global.LastStrike.trans && FromUp <= 0)
        {
            if (BlockLeft && !(Global.LastStrike.trans.position.x >= trans.position.x))
            {
                MonoBehaviour.print("BlockLeft");
                Blocked();
                flag = true;
            }
            else if (BlockRight && !(Global.LastStrike.trans.position.x <= trans.position.x))
            {
                MonoBehaviour.print("BlockRight");
                Blocked();
                flag = true;
            }
        }
        FromUp = 0;
        float num = Defense * 0.25f * DefenseFactor;
        float num2 = Global.LastStrike.pow - num;
        num2 *= Global.LastStrike.multy * 2f;
        num2 += UnityEngine.Random.Range(num2 * -0.1f, num2 * 0.1f);
        if (!(num2 >= 0.1f))
        {
            num2 = 0.1f;
        }
        if (!flag)
        {
            CreatePartsByStrike();
            HurtTimer = 2;
        }
        if (!(num2 <= 0f) && !(maxHurt <= 0f) && !(num2 <= MaxHP / maxHurt))
        {
            num2 = MaxHP / maxHurt;
        }
        HurtMessage(num2);
        Vector3 position = trans.position;
        if ((bool)Global.LastStrike.trans)
        {
            position = Global.LastStrike.trans.position;
        }
        Shock(position, num2);
    }

    public virtual void HurtMessage(float showPow)
    {
        Global.CreateText("- " + FloatText(showPow), trans.position + new Vector3(0f, 1f, 0f), new Color(0f, 1f, 0.25f, 1f), UnityEngine.Random.Range(-25, 25));
    }

    public virtual void BlockByLook(int fact)
    {
        if (Direction >= 0)
        {
            if (fact < 0)
            {
                BlockLeft = true;
                BlockRight = false;
            }
            else
            {
                BlockLeft = false;
                BlockRight = true;
            }
        }
        else if (fact > 0)
        {
            BlockLeft = true;
            BlockRight = false;
        }
        else
        {
            BlockLeft = false;
            BlockRight = true;
        }
    }

    public virtual void BlockOff()
    {
        BlockLeft = false;
        BlockRight = false;
        BlockUp = false;
    }

    public virtual string FloatText(float f)
    {
        string text = string.Empty + f;
        if (text.IndexOf(".") > -1)
        {
            string[] array = text.Split("."[0]);
            text = array[0] + "." + array[1].Substring(0, 1);
        }
        return text;
    }

    public virtual bool NewAI(string aiName, string trigS, int timeMin, int timeMax)
    {
        ai = aiName;
        timer = UnityEngine.Random.Range(timeMin, timeMax);
        int result;
        if ((bool)anim)
        {
            if (trigS != string.Empty)
            {
                TrySetTrigger(trigS);
            }
            else
            {
                TrySetTrigger(aiName);
            }
        }
        else if ((bool)animOld)
        {
            if (trigS != string.Empty)
            {
                animOld.CrossFade(trigS, 0.2f);
            }
            else
            {
                animOld.CrossFade(aiName, 0.2f);
            }
            result = 1;
            goto IL_0106;
        }
        if (WalkAnim)
        {
            oldWalk = 2;
            if ((bool)anim)
            {
                TrySetInteger("walk", 2);
            }
            else if ((bool)animOld)
            {
                animOld.CrossFade(WalkAnimName, 0.2f);
            }
        }
        result = 1;
        goto IL_0106;
    IL_0106:
        return (byte)result != 0;
    }

    public virtual void DestroyIt()
    {
        UnityEngine.Object.Destroy(gameObject);
    }

    public virtual void StartFadeOut(int frames)
    {
        if (fadeOutActive)
        {
            return;
        }
        fadeOutActive = true;
        deathHandled = true;
        fadeOutTimer = Mathf.Max(1, frames);
        fadeOutTimerMax = fadeOutTimer;
        ai = "dead";
        timer = Mathf.Max(timer, frames);
        DisableCollisionsForFade();
        CacheFadeTargets();
        ApplyFade(1f);
    }

    public virtual void MarkDeathHandled()
    {
        deathHandled = true;
    }

    public virtual void StartDeathSequence(int frames)
    {
        if (deathSequenceActive)
        {
            return;
        }
        deathHandled = true;
        deathSequenceActive = true;
        ai = "dead";
        timer = Mathf.Max(1, frames);
        Invincible = true;
        NoStomp = true;
        BlockOff();
        LookBySpeed(1);
        PlayDeathAnimation();
        Layer("plat");
        if ((bool)rigid)
        {
            rigid.useGravity = true;
            Vector3 velocity = rigid.velocity;
            if (velocity.y > 0f)
            {
                velocity.y = 0f;
            }
            rigid.velocity = velocity;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (!TalkPause.IsGameplayBlocked() && noTrigColl <= 0 && !(other.gameObject.tag != "Player"))
        {
            noTrigColl = 20;
            heroIsHigher = HeroIsHigher(other.transform);
            CheckColl(other.gameObject);
        }
    }

    public virtual void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Danger")
        {
            UnityEngine.Object.Destroy(gameObject);
            return;
        }
        if (landTimer <= 0 && !(gravity <= 0f) && !land)
        {
            int i = 0;
            ContactPoint[] contacts = other.contacts;
            for (int length = contacts.Length; i < length; i++)
            {
                if (!(contacts[i].normal.y < 0.4f))
                {
                    land = true;
                }
            }
        }
        if (checkWalls > 0)
        {
            int j = 0;
            ContactPoint[] contacts2 = other.contacts;
            for (int length2 = contacts2.Length; j < length2; j++)
            {
                if (!(contacts2[j].normal.x < 0.8f))
                {
                    ThereIsWall = 1;
                }
                if (!(contacts2[j].normal.x > -0.8f))
                {
                    ThereIsWall = -1;
                }
            }
        }
        if (!TalkPause.IsGameplayBlocked() && other.gameObject.tag == "Player")
        {
            int k = 0;
            ContactPoint[] contacts3 = other.contacts;
            for (int length3 = contacts3.Length; k < length3; k++)
            {
                heroIsHigher = contacts3[k].normal.y < -0.25f;
            }
            CheckColl(other.gameObject);
        }
    }

    public virtual void CheckColl(GameObject GO)
    {
        if (!(HP > 0f))
        {
            return;
        }
        if (Global.Stomp && !NoStomp && heroIsHigher)
        {
            if (InvincibleTimer <= 0 && Global.InvincibleTimer <= 0)
            {
                bool flag = false;
                if (BlockUp)
                {
                    MonoBehaviour.print("up");
                    Blocked();
                    flag = true;
                    HurtTimer = 2;
                    InvincibleTimer = 10;
                    GO.SendMessage("FromEnemyJump", trans.position.y, SendMessageOptions.DontRequireReceiver);
                }
                Global.LastStrike = new SendStrike();
                Global.LastStrike.trans = GO.transform;
                Global.LastStrike.pow = Perks.GetPOWER() * (float)Global.DashStomp;
                Global.LastStrike.multy = 0.5f;
                Global.LastStrike.clan = "hero";
                FromUp = 2;
                SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
                if (!flag)
                {
                    HurtJump = 2;
                }
                GO.SendMessage("FromEnemyJump", trans.position.y, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if (NoStomp && Global.Stomp && heroIsHigher && OuchTimer <= 0)
            {
                OuchTimer = 50;
                Global.CreateText("Ouch!", Global.CurrentPlayerObject.position + new Vector3(0f, 0.5f, -3f), new Color(1f, 0.75f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
            }
            if (PowFactor > 0.01f)
            {
                StrikeIt(GO);
            }
        }
    }

    public virtual void StrikeIt(GameObject go)
    {
        Global.LastStrike = new SendStrike();
        if (!string.IsNullOrEmpty(DeadLevel))
        {
            Global.DeadLevel = DeadLevel;
        }
        else
        {
            Global.DeadLevel = null;
        }
        Global.LastStrike.trans = trans;
        Global.LastStrike.pow = POWER * PowFactor;
        Global.LastStrike.clan = "foe";
        Global.LastStrike.poison = POISON;
        go.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
    }

    public virtual void Blocked()
    {
        if (!NoTextBlock)
        {
            Global.CreateText("Blocked!", trans.position + new Vector3(0f, 0.75f, -3f), new Color(0f, 0.8f, 1f, 1f), UnityEngine.Random.Range(-25, 25));
        }
        if (Global.LastStrike != null)
        {
            Global.LastStrike.pow = Global.LastStrike.pow * BlockFactor;
        }
        else
        {
            MonoBehaviour.print("strange error!");
        }
    }

    public virtual bool HeroIsHigher(Transform t)
    {
        return !(PonyControl.SpeedFall > 0f);
    }

    public virtual void Layer(string word)
    {
        switch (word)
        {
            case "plat":
                gameObject.layer = 27;
                break;
            case "fly":
                gameObject.layer = 17;
                break;
            case "shift":
                gameObject.layer = 29;
                break;
        }
    }

    public virtual void JumpOn(float JumpSpeed, int t)
    {
        Speed.y = JumpSpeed;
        landTimer = t;
        land = false;
    }

    public virtual bool ISeeYou(float yMin, float yMax)
    {
        bool flag = false;
        if (Direction > 0 && !(target.position.x <= trans.position.x))
        {
            flag = true;
        }
        if (Direction < 0 && !(target.position.x >= trans.position.x))
        {
            flag = true;
        }
        return (flag && !(target.position.y < trans.position.y - yMin) && !(target.position.y > trans.position.y + yMax)) ? true : false;
    }

    public virtual void OnMouseOver()
    {
        Monitor.TextA = EnemyName;
        Monitor.TextB = "Level: " + Level + "  HP: " + HP;
        Monitor.MouseNo = false;
        Global.MouseMode = "use";
    }

    public virtual void Regeneration(float Regenerate)
    {
        if (!(Regenerate <= 0f) && !(HP >= MaxHP))
        {
            HP += Regenerate;
            if (!(HP <= MaxHP))
            {
                HP = MaxHP;
            }
        }
    }

    public virtual void SetDeadLevel(string text)
    {
        DeadLevel = text;
    }

    public virtual void EyeSprite()
    {
    }

    public virtual void Main()
    {
    }

    private bool HasAnimatorParameter(string parameterName, AnimatorControllerParameterType parameterType)
    {
        if (!(bool)anim || string.IsNullOrEmpty(parameterName))
        {
            return false;
        }
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (parameter.name == parameterName && parameter.type == parameterType)
            {
                return true;
            }
        }
        return false;
    }

    private void CacheFadeTargets()
    {
        if (fadeRenderers == null)
        {
            fadeRenderers = GetComponentsInChildren<Renderer>(true);
        }
        if (fadeSprites == null)
        {
            fadeSprites = GetComponentsInChildren<tk2dBaseSprite>(true);
        }
    }

    private void DisableCollisionsForFade()
    {
        Collider[] components = GetComponents<Collider>();
        int i = 0;
        for (int length = components.Length; i < length; i++)
        {
            components[i].enabled = false;
        }
        Collider2D[] components2 = GetComponents<Collider2D>();
        int j = 0;
        for (int length2 = components2.Length; j < length2; j++)
        {
            components2[j].enabled = false;
        }
    }

    private void UpdateFadeOut()
    {
        if (fadeOutTimer > 0)
        {
            fadeOutTimer--;
        }
        ApplyFade((float)fadeOutTimer / (float)fadeOutTimerMax);
        if (fadeOutTimer <= 0)
        {
            DestroyIt();
        }
    }

    private void ApplyFade(float alpha)
    {
        float num = Mathf.Clamp01(alpha);
        CacheFadeTargets();
        if (fadeSprites != null)
        {
            int i = 0;
            for (int length = fadeSprites.Length; i < length; i++)
            {
                if (!fadeSprites[i])
                {
                    continue;
                }
                Color color = fadeSprites[i].color;
                color.a = num;
                fadeSprites[i].color = color;
            }
        }
        if (fadeRenderers == null)
        {
            return;
        }
        int j = 0;
        for (int length2 = fadeRenderers.Length; j < length2; j++)
        {
            if (!fadeRenderers[j] || !(fadeRenderers[j].material != null))
            {
                continue;
            }
            Material material = fadeRenderers[j].material;
            if (material.HasProperty("_Color"))
            {
                Color color2 = material.color;
                color2.a = num;
                material.color = color2;
            }
            if (material.HasProperty("_TintColor"))
            {
                Color color3 = material.GetColor("_TintColor");
                color3.a = num;
                material.SetColor("_TintColor", color3);
            }
        }
    }

    private void PlayDeathAnimation()
    {
        if ((bool)anim)
        {
            if (TrySetTrigger("death") || TrySetTrigger("die") || TrySetTrigger("fall") || TrySetTrigger("hide") || TrySetTrigger("shock") || TrySetTrigger("hurt") || TrySetTrigger("dead"))
            {
                return;
            }
        }
        if (!(bool)animOld)
        {
            return;
        }
        string text = FindLegacyClip(new string[7] { "death", "die", "fall", "hide", "shock", "hurt", "dead" });
        if (!string.IsNullOrEmpty(text))
        {
            animOld.CrossFade(text, 0.15f);
        }
    }

    private string FindLegacyClip(string[] preferredNames)
    {
        AnimationState animationState = null;
        foreach (AnimationState item in animOld)
        {
            if (item == null || string.IsNullOrEmpty(item.name))
            {
                continue;
            }
            string lower = item.name.ToLowerInvariant();
            int i = 0;
            for (int length = preferredNames.Length; i < length; i++)
            {
                if (lower.Contains(preferredNames[i]))
                {
                    return item.name;
                }
            }
            if (animationState == null)
            {
                animationState = item;
            }
        }
        return (animationState != null) ? animationState.name : string.Empty;
    }
}