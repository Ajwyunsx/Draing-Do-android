using System;
using UnityEngine;

[Serializable]
public class MonkeyAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public string Type;

	public int OnlyDir;

	public Transform WeaponPoint;

	public GameObject ShotObject;

	public float ShotPowerFactor;

	public Vector2 IdleTimer;

	public Vector2 ActTimer;

	public float JumpSpeed;

	public float XSpeedOnJump;

	public bool isSleepOnCeil;

	private float saveGravity;

	public bool isSleeping;

	public float WakeDistance;

	public Vector2 BlockTime;

	public bool BlockUp;

	public float BlockUpDist;

	public bool BlockUpAfterHit;

	public bool toShotUp;

	public float Regen;

	public string ToJumpAnim;

	public bool AngrySalto;

	public Vector2 preWallTimer;

	public Vector2 AngrySaltoTimer;

	public Vector2 AngryJumpSpeed;

	private bool JumpNow;

	public MonkeyAI()
	{
		Type = "jumper";
		ShotPowerFactor = 1f;
		IdleTimer = new Vector2(50f, 100f);
		ActTimer = new Vector2(60f, 60f);
		JumpSpeed = 20f;
		XSpeedOnJump = 25f;
		isSleeping = true;
		WakeDistance = 8f;
		BlockTime = new Vector2(80f, 100f);
		BlockUpDist = 0.6f;
		Regen = 0.01f;
		ToJumpAnim = "toJump";
		preWallTimer = new Vector2(50f, 60f);
		AngrySaltoTimer = new Vector2(80f, 100f);
		AngryJumpSpeed = new Vector2(15f, 30f);
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		if (core.target == null)
		{
			core.target = Global.CurrentPlayerObject;
		}
		if (OnlyDir == 0)
		{
			if (core.target != null)
			{
				core.LookTo(core.target.position, 1);
			}
			else
			{
				core.LookRnd();
			}
		}
		else
		{
			core.Look(OnlyDir);
		}
		if (AngrySalto)
		{
			core.checkWalls = 1;
		}
		if (!isSleepOnCeil)
		{
			if (!isSleeping)
			{
				WakeFunction();
			}
			else
			{
				core.NewAI("sleep", string.Empty, 100, 105);
			}
		}
		else
		{
			saveGravity = core.gravity;
			core.gravity = -0.025f;
			core.NewAI("ceil", string.Empty, 100, 105);
		}
		if (Type == "shoterV")
		{
			core.TrySetTrigger("toShotV");
		}
	}

	public virtual void WakeFunction()
	{
		core.BlockOff();
		core.Invincible = false;
		core.NoStomp = false;
		switch (Type)
		{
		case "block":
			SetBlock();
			break;
		case "jumper":
			if (OnlyDir == 0)
			{
				if (core.target != null)
				{
					core.LookTo(core.target.position, 1);
				}
				else
				{
					core.LookRnd();
				}
			}
			core.NewAI("toJump", ToJumpAnim, 50, 55);
			break;
		case "shoter":
			core.NewAI("toShot", string.Empty, 50, 55);
			break;
		case "shoterV":
			core.NewAI("toShotV", string.Empty, 50, 55);
			break;
		case "shoterU":
			core.NewAI("toShotU", string.Empty, 50, 55);
			break;
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
		if (core.ai != "dead")
		{
			core.Regeneration(Regen);
		}
		bool flag = false;
		switch (core.ai)
		{
		case "ceil":
			if (core.HurtTimer > 0)
			{
				core.gravity = saveGravity;
				WakeFunction();
			}
			else if (!(core.target.position.y >= core.trans.position.y) && !(core.Distance2D(core.target.position.x, core.trans.position.x) >= WakeDistance))
			{
				core.gravity = saveGravity;
				WakeFunction();
			}
			break;
		case "preWall":
			if (core.timer <= 0)
			{
				core.ThereIsWall = 0;
				core.NoStomp = true;
				core.NewAI("moveWall", "salto", (int)AngrySaltoTimer.x, (int)AngrySaltoTimer.y);
				core.LookRnd();
				core.Speed.y = UnityEngine.Random.Range(AngryJumpSpeed.x, AngryJumpSpeed.y);
				core.Invincible = true;
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
			if (core.timer <= 0)
			{
				WakeFunction();
			}
			break;
		case "blockUp":
			if (AngrySalto && core.HurtTimer > 0)
			{
				core.NewAI("preWall", "shock", (int)preWallTimer.x, (int)preWallTimer.y);
				break;
			}
			if (core.HurtJump > 0)
			{
				if (BlockUpAfterHit)
				{
					SetBlockUp();
					flag = true;
				}
				if (toShotUp)
				{
					core.NewAI("toShotU", string.Empty, 25, 25);
					flag = true;
				}
				if (flag)
				{
					break;
				}
			}
			if (core.timer <= 0)
			{
				if (BlockUp && !(core.target.position.y - 0.5f <= core.trans.position.y) && !(core.Distance2D(core.target.position.x, core.trans.position.x) >= BlockUpDist))
				{
					SetBlockUp();
				}
				else
				{
					WakeFunction();
				}
			}
			break;
		case "block":
			if (core.HurtJump > 0)
			{
				if (BlockUpAfterHit)
				{
					SetBlockUp();
					flag = true;
				}
				if (toShotUp)
				{
					core.NewAI("toShotU", string.Empty, 25, 25);
					flag = true;
				}
				if (flag)
				{
					break;
				}
			}
			if (AngrySalto && core.HurtTimer > 0)
			{
				core.NewAI("preWall", "shock", (int)preWallTimer.x, (int)preWallTimer.y);
			}
			else if (BlockUp && !(core.target.position.y - 0.5f <= core.trans.position.y) && !(core.Distance2D(core.target.position.x, core.trans.position.x) >= BlockUpDist))
			{
				SetBlockUp();
			}
			else if (core.timer <= 0)
			{
				WakeFunction();
			}
			break;
		case "sleep":
			if (core.Distance(core.trans.position, core.target.position) < WakeDistance || core.HurtTimer > 0)
			{
				WakeFunction();
			}
			break;
		case "toShotU":
			if (core.timer <= 0)
			{
				core.NewAI("shotU", string.Empty, (int)ActTimer.x, (int)ActTimer.y);
			}
			break;
		case "shotU":
			if (core.timer <= 0)
			{
				WakeFunction();
			}
			break;
		case "toShotV":
			if (core.timer <= 0)
			{
				core.NewAI("shotV", string.Empty, (int)ActTimer.x, (int)ActTimer.y);
			}
			break;
		case "shotV":
			if (core.timer <= 0)
			{
				core.NewAI("toShotV", string.Empty, (int)IdleTimer.x, (int)IdleTimer.y);
			}
			break;
		case "toShot":
			if (core.HurtJump > 0)
			{
				if (BlockUpAfterHit)
				{
					SetBlockUp();
					flag = true;
				}
				if (toShotUp)
				{
					core.NewAI("toShotU", string.Empty, 25, 25);
					flag = true;
				}
				if (flag)
				{
					break;
				}
			}
			if (BlockUp && !(core.target.position.y - 0.5f <= core.trans.position.y) && !(core.Distance2D(core.target.position.x, core.trans.position.x) >= BlockUpDist))
			{
				SetBlockUp();
				break;
			}
			if (OnlyDir == 0)
			{
				core.LookTo(core.target.position, 1);
			}
			if (core.timer <= 0)
			{
				core.NewAI("shot", string.Empty, (int)ActTimer.x, (int)ActTimer.y);
			}
			break;
		case "shot":
			if (core.HurtJump > 0)
			{
				if (BlockUpAfterHit)
				{
					SetBlockUp();
					flag = true;
				}
				if (toShotUp)
				{
					core.NewAI("toShotU", string.Empty, 25, 25);
					flag = true;
				}
				if (flag)
				{
					break;
				}
			}
			if (core.timer <= 0)
			{
				core.NewAI("toShot", string.Empty, (int)IdleTimer.x, (int)IdleTimer.y);
			}
			break;
		case "toJump":
			if (AngrySalto && core.HurtTimer > 0)
			{
				core.NewAI("preWall", "shock", (int)preWallTimer.x, (int)preWallTimer.y);
				break;
			}
			if (core.timer == 30)
			{
				core.TrySetTrigger("charge");
			}
			if (core.timer <= 0)
			{
				Jump();
				core.NewAI("jump", string.Empty, 10, 10);
			}
			break;
		case "jump":
			if (AngrySalto && core.HurtTimer > 0)
			{
				core.NewAI("preWall", "shock", (int)preWallTimer.x, (int)preWallTimer.y);
				break;
			}
			if (JumpNow && !(core.rigid.velocity.y >= 0f))
			{
				JumpNow = false;
				core.Layer("plat");
			}
			if (core.timer <= 0)
			{
				core.Layer("plat");
				if (OnlyDir == 0)
				{
					core.LookTo(core.target.position, 1);
				}
				core.NewAI("toJump", ToJumpAnim, (int)IdleTimer.x, (int)IdleTimer.y);
			}
			break;
		case "idle":
			if (core.timer <= 0)
			{
				core.NewAI("idle", string.Empty, 150, 200);
			}
			break;
		}
	}

	public virtual void CreateShot()
	{
		switch (Type)
		{
		case "block":
		case "shoter":
			if (core.ai != "shotU")
			{
				core.StrikeShot(ShotObject, core.trans.position + new Vector3(core.Direction * 10, UnityEngine.Random.Range(-1.5f, 1.5f), 0f), (int)(ShotPowerFactor * core.POWER), 5f, 50, WeaponPoint);
			}
			else
			{
				core.StrikeShot(ShotObject, core.trans.position + new Vector3(UnityEngine.Random.Range(-5, 5), 10f, 0f), (int)(ShotPowerFactor * core.POWER), 5f, 50, WeaponPoint);
			}
			break;
		case "shoterV":
			core.StrikeShot(ShotObject, core.trans.position + new Vector3(UnityEngine.Random.Range(-3, 3), -10f, 0f), (int)(ShotPowerFactor * core.POWER), 5f, 50, WeaponPoint);
			break;
		case "shoterU":
			core.StrikeShot(ShotObject, core.trans.position + new Vector3(UnityEngine.Random.Range(-3, 3), 10f, 0f), (int)(ShotPowerFactor * core.POWER), 5f, 50, WeaponPoint);
			break;
		}
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

	public virtual void DISAPPEAR()
	{
		core.NewAI("dead", string.Empty, 100, 100);
	}

	public virtual void SetBlock()
	{
		core.BlockOff();
		if (OnlyDir == 0 && core.target != null)
		{
			core.LookTo(core.target.position, 1);
		}
		else if (OnlyDir == 0)
		{
			core.LookRnd();
		}
		core.BlockByLook(1);
		core.NewAI("block", string.Empty, (int)BlockTime.x, (int)BlockTime.y);
	}

	public virtual void SetBlockUp()
	{
		core.BlockOff();
		if (OnlyDir == 0 && core.target != null)
		{
			core.LookTo(core.target.position, 1);
		}
		else if (OnlyDir == 0)
		{
			core.LookRnd();
		}
		core.BlockUp = true;
		core.NewAI("blockUp", string.Empty, (int)(BlockTime.x * 0.5f), (int)(BlockTime.y * 0.5f));
	}

	public virtual void Main()
	{
	}
}
