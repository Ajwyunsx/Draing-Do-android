using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class Vehicle : MonoBehaviour
{
	public AudioClip SFXEnter;

	public AudioClip SFXOuch;

	public AudioClip SFXFall;

	public AudioClip SFXMotor;

	public AudioClip SFXShot;

	public AudioClip SFXWater;

	public GameObject WaterSplash;

	public bool CanExitFromLevel;

	public float jumpHeight;

	public float gravity;

	public float speed;

	public string VEHICLE_NAME;

	public int TurnSpeedWithMove;

	public bool CheckXSpeedGround;

	public bool SET_HERO_HP;

	public int HP;

	private int MaxHP;

	public bool CheckGround;

	public int DangerRotate;

	public int RotateBarrier;

	public bool CanChangeDirection;

	public bool CanFly;

	public bool CanSwim;

	public Vector3 AutoSpeed;

	public Vector3 AutoDirection;

	public bool CanHorizontal;

	public bool CanVertical;

	public bool CanRotate;

	public bool CanFastSlow;

	public bool CanChangeGravity;

	public bool AnimationWalker;

	private int block_animation;

	private string action;

	private bool StayOnGround;

	public bool ControlVelocityY;

	public AnimationClip AnimationStop;

	public AnimationClip AnimationFall;

	public AnimationClip AnimationWalk;

	public bool CANT_ESCAPE;

	public bool NoGas;

	public Vector3 Direction;

	private GameObject Passager;

	public bool ON_TRUE;

	[HideInInspector]
	public Joystick moveTouchPad;

	[HideInInspector]
	public Joystick jumpTouchPad;

	[HideInInspector]
	public Joystick actionTouchPad;

	[HideInInspector]
	public bool InTheWater;

	[HideInInspector]
	public bool OldInTheWater;

	[HideInInspector]
	public bool BeenInWater;

	[HideInInspector]
	public Vector3 NeedSpeed;

	[HideInInspector]
	public Vector3 OldNeedSpeed;

	[HideInInspector]
	public Vector3 Speed;

	public Vector3 MaxSpeed;

	[HideInInspector]
	public int PushTimer;

	[HideInInspector]
	public object IsNoFalling;

	[HideInInspector]
	public Vector3 StartScale;

	private int DelayShot;

	private float passagerOLD_Z;

	private int timerEscape;

	private Vector3 OFF_Position;

	public string ShotMODE;

	public Vector3 ShotSpeed;

	public Vector3 ShotPosition;

	public GameObject ShotGameObject;

	public Vector3 ShotRotation;

	public int ShotDelay;

	public bool passager_OFF;

	private int OLD_HP;

	public bool FastMoveCamera;

	public bool RESTORE;

	private Vector3 startPosition;

	public bool DeleteIFNOControl;

	private Transform myTransform;

	public GameObject particleStrike;

	private bool CollisionWithSensore;

	private GameObject lastSensore;

	private int ActTimer;

	private float SetHorizontal;

	private float SetVertical;

	private int w_timer;

	public int w_DelayTime;

	public GameObject Wave_Object;

	public Vehicle()
	{
		jumpHeight = 2f;
		gravity = 0.14f;
		speed = 0.35f;
		VEHICLE_NAME = "any_vehicle";
		HP = 4;
		CanChangeDirection = true;
		CanFly = true;
		CanHorizontal = true;
		CanVertical = true;
		ControlVelocityY = true;
		NoGas = true;
		Direction = new Vector3(1f, 0f, 0f);
		MaxSpeed = new Vector3(5f, 5f, 0f);
		w_DelayTime = 200;
	}

	public virtual void Awake()
	{
		MaxHP = HP;
		if (ON_TRUE)
		{
			Global.CurrentPlayerObject = gameObject.transform;
			EnterVehicle(false);
		}
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		myTransform = transform;
		StartScale = myTransform.localScale;
		Direction.y = 1f;
		startPosition = myTransform.position;
		if ((bool)AnimationWalk)
		{
			GetComponent<Animation>()[AnimationWalk.name].speed = 3f;
		}
	}

	public virtual void Start()
	{
		moveTouchPad = (Joystick)GameObject.Find("MoveJoystick").GetComponent(typeof(Joystick));
		jumpTouchPad = (Joystick)GameObject.Find("JumpJoystick").GetComponent(typeof(Joystick));
		actionTouchPad = (Joystick)GameObject.Find("ActionJoystick").GetComponent(typeof(Joystick));
	}

	public virtual void Update()
	{
		if (ON_TRUE && CanChangeDirection)
		{
			float x = Direction.x * StartScale.x;
			Vector3 localScale = myTransform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (myTransform.localScale = localScale);
			float y = Direction.y * StartScale.y;
			Vector3 localScale2 = myTransform.localScale;
			float num2 = (localScale2.y = y);
			Vector3 vector3 = (myTransform.localScale = localScale2);
		}
	}

	public virtual void FixedUpdate()
	{
		if (DeleteIFNOControl && !ON_TRUE)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		if (ON_TRUE)
		{
			if ((bool)SFXMotor && !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = SFXMotor;
				GetComponent<AudioSource>().Play();
			}
			Global.Stomp = false;
			Global.Oxygen = Global.MaxOxygen;
		}
		CheckMouseControl();
		if (timerEscape > 0)
		{
			timerEscape--;
		}
		if (ON_TRUE)
		{
			if (Global.BlockMouseTime <= 0)
			{
				if (Global.CheatGodMode == 1)
				{
					Global.InvincibleTimer = 100;
				}
				if (DelayShot < 1)
				{
					if (Input.GetButton("Strike") || actionTouchPad.IsFingerDown() || Input.GetButton("Jump"))
					{
						MonoBehaviour.print("FIRE!");
						Shot_Control();
					}
				}
				else
				{
					DelayShot--;
				}
			}
		}
		else
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			Speed = Vector3.zero;
			NeedSpeed = Vector3.zero;
		}
		ControlWave();
		Speed.x *= 0.925f;
		Speed.y *= 0.925f;
		if (ON_TRUE && Global.BlockControl <= 0)
		{
			float num = 0f;
			if (CanHorizontal)
			{
				num = ((Mathf.Abs(moveTouchPad.position.x) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.x) : 0f);
				float num2 = default(float);
				if (!CheckXSpeedGround)
				{
					num2 = (Input.GetAxis("Horizontal") + num + SetHorizontal) * speed;
				}
				else
				{
					MonoBehaviour.print("StayOnGround: " + StayOnGround);
					if (StayOnGround)
					{
						num2 = (Input.GetAxis("Horizontal") + num + SetHorizontal) * speed;
					}
				}
				if (TurnSpeedWithMove != 0)
				{
					float z = num2 * (float)(-TurnSpeedWithMove);
					Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
					float num3 = (angularVelocity.z = z);
					Vector3 vector = (GetComponent<Rigidbody>().angularVelocity = angularVelocity);
				}
				if (AnimationWalker)
				{
					if (!(Mathf.Abs(num2) <= 0.1f))
					{
						if (StayOnGround)
						{
							Animate(AnimationWalk, 0.15f);
						}
						else
						{
							Animate(AnimationFall, 0.15f);
						}
					}
					else if (StayOnGround)
					{
						Animate(AnimationStop, 0.15f);
					}
					else
					{
						Animate(AnimationFall, 0.15f);
					}
				}
				if (PushTimer == 0)
				{
					Speed.x += num2;
				}
				else
				{
					Speed.x += num2 * 0.75f;
				}
				if (!(Mathf.Abs(num2) <= 0.1f))
				{
					Direction.x = Mathf.Sign(num2);
				}
			}
			if (CanVertical)
			{
				if (!CanChangeGravity)
				{
					VerticalSpeed();
				}
				else
				{
					ChangeGravity();
				}
			}
			if (AutoSpeed.x != 0f)
			{
				Speed.x = AutoSpeed.x;
			}
			if (AutoSpeed.y != 0f)
			{
				Speed.y = AutoSpeed.y;
			}
			if (AutoDirection.x != 0f)
			{
				Direction.x = AutoDirection.x;
			}
		}
		float num4 = 0f;
		if (ON_TRUE)
		{
			if (!InTheWater)
			{
				if (!RuntimeServices.ToBool(IsNoFalling))
				{
					num4 -= gravity * 2f;
				}
				num4 -= gravity;
			}
			else
			{
				if (!CanSwim && !CanFly)
				{
					num4 = (0f - gravity) * 1.5f;
					Speed.y = 0f;
					Speed.x *= 0.25f;
				}
				if (!CanSwim && CanFly)
				{
					Speed *= 0.25f;
					Speed.x *= 0.25f;
					num4 = MaxSpeed.y * 1.1f;
				}
			}
			Speed.y += num4;
		}
		if (StayOnGround)
		{
			num4 = -0.1f;
			Speed.y = -0.1f;
			NeedSpeed.y = -0.1f;
		}
		if (!ON_TRUE)
		{
			Speed = Vector3.zero;
			NeedSpeed = Vector3.zero;
		}
		NeedSpeed.x *= 0.95f;
		NeedSpeed.y *= 0.95f;
		if (ON_TRUE)
		{
			if (!CanFly && !StayOnGround)
			{
				if (!(gravity <= 0f) && !InTheWater && !(NeedSpeed.y <= -15f))
				{
					NeedSpeed.y -= 0.25f;
				}
				if (!(gravity >= 0f) && !InTheWater && !(NeedSpeed.y >= 15f))
				{
					NeedSpeed.y += 0.25f;
				}
			}
			OldNeedSpeed = NeedSpeed;
		}
		if (!InTheWater)
		{
			if (PushTimer > 0)
			{
				PushTimer--;
			}
			if (PushTimer < 0)
			{
				PushTimer++;
			}
		}
		OldInTheWater = InTheWater;
		InTheWater = false;
		if (BeenInWater && !InTheWater && !OldInTheWater)
		{
			BeenInWater = false;
			if ((bool)SFXWater)
			{
				AudioSource.PlayClipAtPoint(SFXWater, myTransform.position);
			}
			UnityEngine.Object.Instantiate(WaterSplash, myTransform.position + new Vector3(0f, 0f, -0.2f), Quaternion.identity);
		}
		float num5 = Mathf.DeltaAngle(myTransform.eulerAngles.z, 0f);
		if (RotateBarrier != 0)
		{
		}
		if (ON_TRUE && DangerRotate != 0)
		{
			if (!(num5 <= (float)DangerRotate))
			{
				InitBoom();
			}
			if (!(num5 >= (float)(-DangerRotate)))
			{
				InitBoom();
			}
		}
		if (!(Mathf.Abs(Speed.x) <= MaxSpeed.x))
		{
			Speed.x = MaxSpeed.x * Mathf.Sign(Speed.x);
		}
		if (!(Mathf.Abs(Speed.y) <= MaxSpeed.y))
		{
			Speed.y = MaxSpeed.y * Mathf.Sign(Speed.y);
		}
		float x = Speed.x + NeedSpeed.x;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num6 = (velocity.x = x);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity);
		if (ControlVelocityY)
		{
			float y = Speed.y + NeedSpeed.y;
			Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
			float num7 = (velocity2.y = y);
			Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity2);
		}
		if (ON_TRUE)
		{
			PassagerControl();
			if (AnimationWalker && !StayOnGround && timerEscape <= 0)
			{
				timerEscape = 1;
			}
		}
		StayOnGround = false;
		CollisionWithSensore = false;
		if (ActTimer > 0)
		{
			ActTimer--;
		}
	}

	public virtual void VerticalSpeed()
	{
		if (ON_TRUE && (InTheWater || CanFly))
		{
			float num = 0f;
			num = ((Mathf.Abs(moveTouchPad.position.y) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.y) : 0f);
			if (PushTimer == 0)
			{
				Speed.y += (Input.GetAxis("Vertical") + num + SetVertical) * speed;
			}
			else
			{
				Speed.y += (Input.GetAxis("Vertical") + num + SetVertical) * speed * 0.75f;
			}
		}
	}

	public virtual void ChangeGravity()
	{
		if (ON_TRUE)
		{
			float num = 0f;
			num = ((Mathf.Abs(moveTouchPad.position.y) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.y) : 0f);
			if (!(Input.GetAxis("Vertical") + num + SetVertical <= 0.25f))
			{
				gravity = Mathf.Abs(gravity) * -1f;
				Direction.y = -1f;
			}
			if (!(Input.GetAxis("Vertical") + num + SetVertical >= -0.25f))
			{
				gravity = Mathf.Abs(gravity);
				Direction.y = 1f;
			}
		}
	}

	public virtual void StreamSpeed(Vector3 needspeed)
	{
		NeedSpeed = Vector3.Lerp(NeedSpeed, needspeed, 0.25f);
	}

	public virtual void InWater()
	{
		IsNoFalling = true;
		BeenInWater = true;
		InTheWater = true;
	}

	public virtual float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2f * jumpHeight * 16f);
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Danger")
		{
			Dangerz();
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Danger")
		{
			Dangerz();
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (!CheckGround)
		{
			return;
		}
		int i = 0;
		ContactPoint[] contacts = other.contacts;
		for (int length = contacts.Length; i < length; i++)
		{
			if (!(Direction.y <= 0f))
			{
				if (!(contacts[i].normal.y < 0.5f))
				{
					StayOnGround = true;
					break;
				}
			}
			else if (!(contacts[i].normal.y > -0.5f))
			{
				StayOnGround = true;
				break;
			}
		}
	}

	public virtual void TouchDanger(Vector3 loc_data)
	{
		OnEnemyCollision((int)loc_data.z);
		if (!(myTransform.position.x <= loc_data.x))
		{
			Speed.x += 4f;
		}
		else
		{
			Speed.x -= 4f;
		}
		if (!(myTransform.position.y <= loc_data.y))
		{
			Speed.y += 4f;
		}
		else
		{
			Speed.y -= 4f;
		}
	}

	public virtual void GasTouchDanger(Vector3 loc_data)
	{
		if (!passager_OFF && !NoGas)
		{
			OnEnemyCollision((int)loc_data.z);
			if (!(myTransform.position.x <= loc_data.x))
			{
				Speed.x += 4f;
			}
			else
			{
				Speed.x -= 4f;
			}
			if (!(myTransform.position.y <= loc_data.y))
			{
				Speed.y += 4f;
			}
			else
			{
				Speed.y -= 4f;
			}
		}
	}

	public virtual void OnEnemyCollision(int hp)
	{
		if (Global.InvincibleTimer > 0)
		{
			return;
		}
		if (!(GetComponent<AudioSource>().clip == SFXOuch) || !GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().PlayOneShot(SFXOuch);
		}
		if ((bool)particleStrike)
		{
			UnityEngine.Object.Instantiate(particleStrike, myTransform.position + new Vector3(0f, 0.1f, -0.5f), Quaternion.identity);
		}
		if (hp > 1)
		{
			hp--;
		}
		Global.HP -= hp;
		Global.QuakeStart(75, 30f);
		Global.InvincibleTimer = 75;
		int num = 0;
		if (Global.HP > 0f)
		{
			return;
		}
		if ((bool)SFXFall)
		{
			AudioSource.PlayClipAtPoint(SFXFall, transform.position);
		}
		if (!passager_OFF)
		{
			num = 1000;
			return;
		}
		if (!SET_HERO_HP && Global.CorrectMaxHP != 0)
		{
			Global.MaxHP = Global.CorrectMaxHP;
			Global.CorrectMaxHP = 0;
		}
		EscapeVehicle();
	}

	public virtual void Dangerz()
	{
		if (passager_OFF)
		{
			Global.HP = 0f;
			EscapeVehicle();
			if (!SET_HERO_HP && Global.CorrectMaxHP != 0)
			{
				Global.MaxHP = Global.CorrectMaxHP;
				Global.CorrectMaxHP = 0;
			}
		}
		else
		{
			Global.HP = 0f;
			Global.BlockControl = 30;
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			CollisionWithSensore = true;
			lastSensore = other.gameObject;
		}
	}

	public virtual void Shot_Control()
	{
		if (CollisionWithSensore)
		{
			if (ActTimer <= 0)
			{
				ActTimer = 50;
				MonoBehaviour.print("OKA1");
				if ((bool)lastSensore)
				{
					lastSensore.SendMessage("ActON", null, SendMessageOptions.DontRequireReceiver);
				}
			}
			return;
		}
		DelayShot = ShotDelay;
		if ((bool)SFXShot)
		{
			AudioSource.PlayClipAtPoint(SFXShot, transform.position);
		}
		switch (ShotMODE)
		{
		case "Free":
		case "free":
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ShotGameObject, myTransform.position + ShotPosition, Quaternion.identity) as GameObject;
			Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = ShotSpeed;
			Global.LastCreatedObject.transform.eulerAngles = ShotRotation;
			break;
		case "Dir":
		case "dir":
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ShotGameObject, myTransform.position, Quaternion.identity) as GameObject;
			float x = Global.LastCreatedObject.transform.position.x + ShotPosition.x * Direction.x;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.x = x);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			float x2 = ShotSpeed.x * Direction.x;
			Vector3 velocity = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity;
			float num2 = (velocity.x = x2);
			Vector3 vector3 = (Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = velocity);
			Global.LastCreatedObject.transform.eulerAngles = ShotRotation;
			break;
		}
		case "Angle":
			break;
		case "angle":
			break;
		case "Action":
			break;
		case "action":
			break;
		default:
			EscapeVehicle();
			break;
		}
	}

	public virtual void EscapeVehicle()
	{
		if (CANT_ESCAPE || (!(Global.HP <= 0f) && !StayOnGround && CheckGround) || !ON_TRUE || timerEscape > 0)
		{
			return;
		}
		if (!SET_HERO_HP && Global.CorrectMaxHP != 0)
		{
			Global.MaxHP = Global.CorrectMaxHP;
			Global.CorrectMaxHP = 0;
		}
		if ((bool)SFXEnter)
		{
			AudioSource.PlayClipAtPoint(SFXEnter, transform.position);
		}
		if ((bool)SFXMotor)
		{
			GetComponent<AudioSource>().clip = SFXMotor;
			GetComponent<AudioSource>().Stop();
		}
		if (AnimationWalker)
		{
			Animate(AnimationStop, 0.15f);
		}
		Global.CanExitFromLevel = true;
		if (passager_OFF)
		{
			HP = (int)Global.HP;
			if (!(Global.HP > 0f))
			{
				HP = MaxHP;
				if (!RESTORE && !SET_HERO_HP)
				{
					Global.HP = OLD_HP;
					Delete();
				}
			}
			if (!SET_HERO_HP)
			{
				Global.HP = OLD_HP;
			}
		}
		if (!passager_OFF && !SET_HERO_HP)
		{
			HP = (int)Global.HP;
			Global.HP = OLD_HP;
		}
		Global.CurrentPlayerObject = Passager.transform;
		Global.TimerNoVehicle = 30;
		int num = 0;
		Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
		float num2 = (angularVelocity.z = num);
		Vector3 vector = (GetComponent<Rigidbody>().angularVelocity = angularVelocity);
		if (FastMoveCamera)
		{
			float x = Global.CurrentPlayerObject.position.x;
			Vector3 position = Camera.main.transform.position;
			float num3 = (position.x = x);
			Vector3 vector3 = (Camera.main.transform.position = position);
			float y = Global.CurrentPlayerObject.position.y;
			Vector3 position2 = Camera.main.transform.position;
			float num4 = (position2.y = y);
			Vector3 vector5 = (Camera.main.transform.position = position2);
		}
		if (!passager_OFF)
		{
			Passager.SetActiveRecursively(true);
			if ((bool)Global.InTheHandObject)
			{
				Global.InTheHandObject.SetActiveRecursively(true);
			}
		}
		if (!ControlVelocityY)
		{
			GetComponent<Rigidbody>().useGravity = false;
		}
		Global.VehicleName = string.Empty;
		Global.Passager_OFF = false;
		Passager.SendMessage("ONTRUE", null, SendMessageOptions.DontRequireReceiver);
		Global.CurrentPlayerObject.GetComponent<Collider>().enabled = true;
		Passager = null;
		float z = passagerOLD_Z;
		Vector3 position3 = Global.CurrentPlayerObject.position;
		float num5 = (position3.z = z);
		Vector3 vector7 = (Global.CurrentPlayerObject.position = position3);
		float y2 = 0f;
		Vector3 velocity = Global.CurrentPlayerObject.GetComponent<Rigidbody>().velocity;
		float num6 = (velocity.y = y2);
		Vector3 vector9 = (Global.CurrentPlayerObject.GetComponent<Rigidbody>().velocity = velocity);
		ON_TRUE = false;
		gameObject.layer = 11;
		GetComponent<Collider>().isTrigger = true;
		Global.CurrentPlayerObject.SendMessage("StartGetWithTheTool", null, SendMessageOptions.DontRequireReceiver);
		if (RESTORE)
		{
			myTransform.position = startPosition;
		}
	}

	public virtual void EnterVehicle(bool pc)
	{
		if ((!pc && passager_OFF) || ON_TRUE)
		{
			return;
		}
		if ((bool)SFXEnter)
		{
			AudioSource.PlayClipAtPoint(SFXEnter, transform.position);
		}
		if ((bool)SFXMotor)
		{
			GetComponent<AudioSource>().clip = SFXMotor;
			GetComponent<AudioSource>().Play();
		}
		if (FastMoveCamera)
		{
			float x = myTransform.position.x;
			Vector3 position = Camera.main.transform.position;
			float num = (position.x = x);
			Vector3 vector = (Camera.main.transform.position = position);
			float y = myTransform.position.y;
			Vector3 position2 = Camera.main.transform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (Camera.main.transform.position = position2);
		}
		Global.CanExitFromLevel = CanExitFromLevel;
		timerEscape = 40;
		DelayShot = 20;
		if (!ControlVelocityY)
		{
			GetComponent<Rigidbody>().useGravity = true;
		}
		if (!SET_HERO_HP)
		{
			Global.CorrectMaxHP = (int)Global.MaxHP;
			Global.MaxHP = MaxHP;
			OLD_HP = (int)Global.HP;
			Global.HP = HP;
		}
		else if (!SET_HERO_HP)
		{
			OLD_HP = (int)Global.HP;
		}
		if (Global.VehicleName == string.Empty)
		{
			Global.VehicleName = VEHICLE_NAME;
		}
		Global.Passager_OFF = passager_OFF;
		Global.CurrentPlayerObject.GetComponent<Collider>().enabled = false;
		if (!passager_OFF)
		{
			Global.CurrentPlayerObject.gameObject.SetActiveRecursively(false);
			if ((bool)Global.InTheHandObject)
			{
				Global.InTheHandObject.SetActiveRecursively(false);
			}
		}
		else
		{
			OFF_Position = Global.CurrentPlayerObject.position;
		}
		Passager = Global.CurrentPlayerObject.gameObject;
		passagerOLD_Z = Global.CurrentPlayerObject.position.z;
		Passager.SendMessage("OFFTRUE", null, SendMessageOptions.DontRequireReceiver);
		Global.CurrentPlayerObject = myTransform;
		ON_TRUE = true;
		gameObject.layer = 8;
		GetComponent<Collider>().isTrigger = false;
	}

	public virtual void PassagerControl()
	{
		if (!(Passager == null))
		{
			if (!passager_OFF)
			{
				Passager.transform.position = myTransform.position;
				float y = myTransform.position.y;
				Vector3 position = Passager.transform.position;
				float num = (position.y = y);
				Vector3 vector = (Passager.transform.position = position);
				float z = myTransform.position.z + 0.1f;
				Vector3 position2 = Passager.transform.position;
				float num2 = (position2.z = z);
				Vector3 vector3 = (Passager.transform.position = position2);
			}
			else
			{
				Passager.transform.position = OFF_Position;
			}
		}
	}

	public virtual void ActON()
	{
		if (!ON_TRUE)
		{
			EnterVehicle(false);
		}
	}

	public virtual void InitBoom()
	{
		DangerRotate = 0;
		Global.BlockControl = 100;
		Global.HP = 0f;
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("explosion")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
	}

	public virtual void Delete()
	{
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("explosion")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Directions(int dir)
	{
		Direction.x = dir;
	}

	public virtual void Animate(AnimationClip anim, float time)
	{
		if (anim == null)
		{
			return;
		}
		string text = anim.name;
		if (!(action == text))
		{
			action = text;
			if (time == 0f)
			{
				time = 0.15f;
			}
			GetComponent<Animation>().CrossFade(action, 0.25f);
		}
	}

	public virtual void CheckMouseControl()
	{
		SetHorizontal = 0f;
		SetVertical = 0f;
		if (Global.MouseTrig)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f));
			float num = Mathf.Abs(vector.x - myTransform.position.x);
			if (!(num <= 1.5f))
			{
				SetHorizontal = Mathf.Sign(vector.x - myTransform.position.x);
			}
			else if (!(num <= 0.5f))
			{
				SetHorizontal = Mathf.Sign(vector.x - myTransform.position.x) * 0.55f;
			}
			num = Mathf.Abs(vector.y - myTransform.position.y);
			if (!(num <= 1.5f))
			{
				SetVertical = Mathf.Sign(vector.y - myTransform.position.y);
			}
			else if (!(num <= 0.5f))
			{
				SetVertical = Mathf.Sign(vector.y - myTransform.position.y) * 0.55f;
			}
		}
	}

	public virtual void ControlWave()
	{
		if (!(Wave_Object == null))
		{
			w_timer++;
			if (w_timer > w_DelayTime)
			{
				w_timer = 0;
				UnityEngine.Object.Instantiate(Wave_Object, myTransform.position, Quaternion.identity);
			}
		}
	}

	public virtual void Main()
	{
	}
}
