using System;
using Boo.Lang.Runtime;
using UnityEngine;
using GUITexture = LegacyCompat.GUITexture;

[Serializable]
public class PonyControl : MonoBehaviour
{
	public AudioClip SFXJump;

	public AudioClip SFXJumpEnemy;

	public AudioClip SFXOuch;

	public AudioClip SFXFall;

	public AudioClip SFXGet;

	public AudioClip SFXWater;

	public AudioClip SFXBulp;

	public AudioClip SFXCrush;

	public AudioClip SFXWhip;

	public AudioClip SFXStrike;

	private tk2dSpriteAnimator animatedSprite;

	private Animation animatedObject;

	public int NoWaterFlashTimer;

	public GameObject WaterSplash;

	public GameObject SFXFail;

	private int oldContact;

	private float lastVELOCITY;

	public GameObject VEHICLE_AT_START;

	public float speed;

	[HideInInspector]
	public Vector3 NeedSpeed;

	[HideInInspector]
	public Vector3 OldNeedSpeed;

	public float maxVelocityChange;

	public float jumpSpeed;

	public float jumpHeight;

	public float gravity;

	public string action;

	public string old_action;

	public int block_animation;

	public float local_scale;

	private int NoActionTimer;

	[HideInInspector]
	public CurrentDropBox dropBox;

	[HideInInspector]
	public CurrentTool tool;

	public GameObject _hand;

	public GameObject _sens;

	public GameObject _arm;

	private bool DoubleJump;

	private bool Dash;

	public Joystick moveTouchPad;

	public Joystick jumpTouchPad;

	public Joystick actionTouchPad;

	public Joystick rollTouchPad;

	private bool oldRollTouchState;

	private bool oldActionTouchState;

	private bool oldMoveDownTouchState;

	[HideInInspector]
	public float restmove_x;

	[HideInInspector]
	public float SpeedY;

	[HideInInspector]
	public bool Zahvat;

	[HideInInspector]
	public int ZahvatDelay;

	[HideInInspector]
	public bool InTheWater;

	[HideInInspector]
	public bool OldInTheWater;

	[HideInInspector]
	public bool BeenInWater;

	[HideInInspector]
	public int RopeDelay;

	[HideInInspector]
	public int EscapeFromRopeTimer;

	[HideInInspector]
	public Transform oldRopeParent;

	[HideInInspector]
	public GameObject RopeObject;

	[HideInInspector]
	public bool IsHorizontal;

	[HideInInspector]
	public int RightJumpIH;

	[HideInInspector]
	public int LeftJumpIH;

	[HideInInspector]
	public object CollideWithEnemy;

	[HideInInspector]
	public bool IsNoFalling;

	[HideInInspector]
	public bool grounded;

	[HideInInspector]
	public int NoGroundedTimer;

	[HideInInspector]
	public int PushTimer;

	[HideInInspector]
	public bool SendAction;

	[HideInInspector]
	public bool SendActionToFalse;

	[HideInInspector]
	public int DontDropBoxTimer;

	[HideInInspector]
	public bool ToDropBox;

	[HideInInspector]
	public bool IsAction;

	[HideInInspector]
	public bool ToUseTheTools;

	[HideInInspector]
	public Vector3 StartScale;

	[HideInInspector]
	public int layerMask;

	private int noJumpTime;

	private Vector3 moveDirection;

	private bool ON_TRUE;

	private int _CorrectTimer;

	private int _DropTheObject;

	public int Direction;

	private Transform myTransform;

	private Rigidbody rigid;

	public GameObject particleStrike;

	public GameObject GuardianObject;

	private bool NOWALK;

	private bool OnceDead;

	private int BeforeSaveTimer;

	private int edgeTimer;

	private int NoStompCorrectionTimer;

	private int tiredTimer;

	[NonSerialized]
	public static float SpeedFall;

	private string IdleAnimation;

	private bool FailLock;

	private int oldDirection;

	private bool ForcePlayJump;

	private bool PressAction;

	private int StrikeTime;

	private int AfterStrikeTime;

	private int ShiftTime;

	private bool StrikeForce;

	private bool Jump1;

	private bool Jump2;

	private int NoDoubleJumpNow;

	private bool ToCP;

	private float AfraidEdge;

	public EdgeTest EdgeLeft;

	public EdgeTest EdgeRight;

	public int OxygenTimer;

	private GameObject GetZahvatObject;

	private int BackTimer;

	private float SetHorizontal;

	private int SetVertical;

	private int ShockTimer;

	private int ForceAction;

	private bool UP;

	private bool DOWN;

	public int oldLayer;

	private int PseudoCollTimer;

	private float SandSpeedFactor;

	public PonyControl()
	{
		speed = 3f;
		maxVelocityChange = 36f;
		jumpSpeed = 6f;
		jumpHeight = 2f;
		gravity = 16f;
		action = "idle";
		NoActionTimer = 30;
		IsNoFalling = true;
		grounded = true;
		layerMask = 16;
		moveDirection = Vector3.zero;
		ON_TRUE = true;
		Direction = 1;
		BeforeSaveTimer = 5;
		IdleAnimation = "idle";
		oldDirection = -100;
	}

	public virtual void OnEnable()
	{
		NoWaterFlashTimer = 30;
		block_animation = 0;
		lastVELOCITY = 0f;
		int num = 0;
		Vector3 velocity = rigid.velocity;
		float num2 = (velocity.y = num);
		Vector3 vector = (rigid.velocity = velocity);
		SpeedY = 0f;
		BeenInWater = false;
		InTheWater = false;
		OldInTheWater = InTheWater;
	}

	public virtual void Awake()
	{
		rigid = GetComponent<Rigidbody>();
		NoWaterFlashTimer = 30;
		Global.CanExitFromLevel = true;
		if ((bool)Global.ZahvatTransform)
		{
			UnityEngine.Object.Destroy(Global.ZahvatTransform);
		}
		Global.CurrentPlayerObject = transform;
		myTransform = transform;
		layerMask = ~layerMask;
		StartScale = myTransform.localScale;
	}

	public virtual void StartGetInTheHand(GameObject g_o)
	{
		Global.InTheHandObject = g_o;
		Global.InTheHandObject.GetComponent<Rigidbody>().useGravity = false;
		Global.InTheHandObject.GetComponent<Collider>().enabled = false;
		Global.InTheHandObject.GetComponent<Collider>().isTrigger = true;
		DontDropBoxTimer = 10;
	}

	public virtual void Start()
	{
		MobileInputBridge.EnsureDefaultControls();
		moveTouchPad = MobileInputBridge.FindJoystick("MoveJoystick");
		jumpTouchPad = MobileInputBridge.FindJoystick("JumpJoystick");
		actionTouchPad = MobileInputBridge.FindJoystick("ActionJoystick");
		CheckRollTouchPad();
		oldRollTouchState = false;
		oldActionTouchState = false;
		oldMoveDownTouchState = false;
		local_scale = transform.localScale.x;
		ChangeCloths(Global.InTheCloth);
		ToCP = true;
		Global.InTheHandObject = null;
		StartWithVehicle();
		if (Global.Guardian > 0)
		{
			for (int i = 0; i < Global.Guardian; i++)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(GuardianObject, myTransform.position, Quaternion.identity);
				Global.LastCreatedObject.SendMessage("ActiveON", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		animatedSprite = (tk2dSpriteAnimator)gameObject.GetComponent(typeof(tk2dSpriteAnimator));
		if (!animatedSprite)
		{
			animatedObject = gameObject.GetComponent<Animation>();
		}
		if (ToCP)
		{
			ToCP = false;
			ReturnToCheckPoint();
		}
	}

	public virtual void CheckRollTouchPad()
	{
		GameObject gameObject = GameObject.Find("RollJoystick");
		if ((bool)gameObject)
		{
			rollTouchPad = (Joystick)gameObject.GetComponent(typeof(Joystick));
			return;
		}
		if (!Global.MobilePlatform || !(bool)actionTouchPad)
		{
			return;
		}
		GUITexture gUITexture = (GUITexture)actionTouchPad.GetComponent(typeof(GUITexture));
		GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(actionTouchPad.gameObject);
		gameObject2.name = "RollJoystick";
		gameObject2.transform.parent = actionTouchPad.transform.parent;
		gameObject2.transform.localRotation = actionTouchPad.transform.localRotation;
		gameObject2.transform.localScale = actionTouchPad.transform.localScale;
		gameObject2.transform.localPosition = actionTouchPad.transform.localPosition;
		rollTouchPad = (Joystick)gameObject2.GetComponent(typeof(Joystick));
		GUITexture gUITexture2 = (GUITexture)gameObject2.GetComponent(typeof(GUITexture));
		if ((bool)gUITexture && (bool)gUITexture2)
		{
			Rect pixelInset = gUITexture.pixelInset;
			pixelInset.x -= pixelInset.width + Mathf.Max(12f, pixelInset.width * 0.18f);
			pixelInset.y += pixelInset.height + Mathf.Max(12f, pixelInset.height * 0.18f);
			gUITexture2.pixelInset = pixelInset;
			Color color = gUITexture.color;
			color.r = Mathf.Min(1f, color.r * 1.1f);
			color.g = Mathf.Min(1f, color.g * 1.05f);
			gUITexture2.color = color;
		}
		if ((bool)rollTouchPad)
		{
			rollTouchPad.ResetJoystick();
		}
	}

	public virtual bool RollTouchDown()
	{
		return MobileInputBridge.IsTouchDown(rollTouchPad, ref oldRollTouchState);
	}

	public virtual bool ActionTouchDown()
	{
		return MobileInputBridge.IsTouchDown(actionTouchPad, ref oldActionTouchState);
	}

	public virtual bool MoveDownTouchDown()
	{
		bool currentState = MobileInputBridge.IsMoveDownHeld(moveTouchPad, 0.55f, 0.85f);
		return MobileInputBridge.GetEdge(currentState, ref oldMoveDownTouchState);
	}

	public virtual float GetMoveTouchHorizontal()
	{
		return MobileInputBridge.GetDigitalAxis(moveTouchPad, horizontal: true, 0.18f);
	}

	public virtual void FixedUpdate()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			animatedObject.CrossFade(GetAnim("idle"), 0.15f);
			Animate(null);
			int num = 0;
			Vector3 velocity = rigid.velocity;
			float num2 = (velocity.x = num);
			Vector3 vector = (rigid.velocity = velocity);
			if (!(rigid.velocity.y <= 0f))
			{
				int num3 = -1;
				Vector3 velocity2 = rigid.velocity;
				float num4 = (velocity2.y = num3);
				Vector3 vector3 = (rigid.velocity = velocity2);
			}
			if (gameObject.layer != 8)
			{
				gameObject.layer = 8;
			}
		}
		else
		{
			if (!ON_TRUE)
			{
				return;
			}
			float num5 = default(float);
			if (Global.Pause)
			{
				return;
			}
			if (Global.PlatformDown > 0)
			{
				Global.PlatformDown--;
			}
			if (!(Global.MP > 0f))
			{
				tiredTimer--;
				if (tiredTimer <= 0)
				{
					tiredTimer = UnityEngine.Random.Range(15, 30);
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(SFXFail, transform.position + new Vector3((float)Direction * 0.55f, 0.5f, -0.1f), Quaternion.identity) as GameObject;
				}
			}
			if (NoStompCorrectionTimer > 0)
			{
				NoStompCorrectionTimer--;
			}
			Global.LastDirection = Direction;
			if (OxygenTimer > 0)
			{
				OxygenTimer--;
			}
			if (Global.Oxygen <= 0 && OxygenTimer <= 0)
			{
				Global.HP -= Global.MaxHP * 0.01f;
			}
			if (Global.OnPlatformDown > 0)
			{
				Global.OnPlatformDown--;
			}
			if (StrikeTime > 0)
			{
				StrikeTime--;
				if (StrikeTime != 0)
				{
					AfterStrikeTime = 5;
				}
			}
			if (ShiftTime > 0)
			{
				ShiftTime--;
				action = "shift";
				if (ShiftTime == 0)
				{
					gameObject.layer = 8;
					CheckPlatform();
					AfterStrikeTime = 5;
				}
			}
			if (AfterStrikeTime > 0)
			{
				AfterStrikeTime--;
			}
			if (RopeObject == null && EscapeFromRopeTimer > 0)
			{
				EscapeFromRopeTimer--;
				if (grounded)
				{
					oldRopeParent = null;
					EscapeFromRopeTimer = 0;
				}
				if (EscapeFromRopeTimer <= 0)
				{
					oldRopeParent = null;
				}
			}
			if (!(Global.HP > 0f) && !OnceDead)
			{
				action = "out";
				if (!animatedSprite)
				{
					animatedObject.CrossFade("Pony Out", 0.25f);
				}
				else
				{
					animatedSprite.Play(action);
				}
				int num6 = -5;
				Vector3 velocity3 = rigid.velocity;
				float num7 = (velocity3.y = num6);
				Vector3 vector5 = (rigid.velocity = velocity3);
				OnceDead = true;
				SlotItem.EscapeItem = true;
				MindMinus();
				DeadPony();
			}
			if (Global.CheatGodMode == 1)
			{
				Global.InvincibleTimer = 100;
			}
			if (NoWaterFlashTimer > 0)
			{
				NoWaterFlashTimer--;
			}
			if (NoActionTimer > 0)
			{
				NoActionTimer--;
			}
			ShockControl();
			if (BackTimer > 0)
			{
				BackTimer--;
			}
			CheckMouseControl();
			if (SendActionToFalse)
			{
				SendActionToFalse = false;
				SendAction = false;
			}
			Global.Stomp = false;
			Global.DashStomp = 1;
			if (!grounded && !Zahvat && RopeObject == null && NoStompCorrectionTimer <= 0)
			{
				Global.Stomp = true;
				SpeedFall = rigid.velocity.y;
				if (Dash)
				{
					Global.DashStomp = 2;
				}
			}
			DropControl();
			if (noJumpTime > 0)
			{
				noJumpTime--;
			}
			if (ZahvatDelay > 0)
			{
				ZahvatDelay--;
			}
			if (Zahvat && Global.BlockControl == 0)
			{
				Global.BlockControl = 1;
				if (Global.ZahvatTransform != null)
				{
					transform.position = Global.ZahvatTransform.transform.position;
				}
				rigid.velocity = Vector3.zero;
				NeedSpeed = Vector3.zero;
				IsNoFalling = true;
				if (noJumpTime <= 0 && (Jump1 || ForcePlayJump || SetVertical > 0 || jumpTouchPad.IsFingerDown() || !(moveTouchPad.position.y <= 0.5f)))
				{
					Zahvat = false;
					ZahvatDelay = 10;
					Global.BlockControl = 0;
					IsNoFalling = false;
					float y = CalculateJumpVerticalSpeed() * 0.85f;
					Vector3 velocity4 = rigid.velocity;
					float num8 = (velocity4.y = y);
					Vector3 vector7 = (rigid.velocity = velocity4);
				}
				if (Input.GetAxis("Vertical") < -0.25f || SetVertical < 0 || !(moveTouchPad.position.y >= -0.5f))
				{
					Zahvat = false;
					ZahvatDelay = 10;
					Global.BlockControl = 0;
					IsNoFalling = false;
					int num9 = -3;
					Vector3 velocity5 = rigid.velocity;
					float num10 = (velocity5.y = num9);
					Vector3 vector9 = (rigid.velocity = velocity5);
				}
			}
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			float horizontalMove = Input.GetAxis("Horizontal") + GetMoveTouchHorizontal() + SetHorizontal;
			if (ShiftTime <= 0 && StrikeTime > 0 && Mathf.Abs(horizontalMove) < 0.05f)
			{
				if (!grounded)
				{
					float x = rigid.velocity.x * 0.985f;
					Vector3 velocity6 = rigid.velocity;
					float num11 = (velocity6.x = x);
					Vector3 vector11 = (rigid.velocity = velocity6);
				}
				else
				{
					float num12 = rigid.velocity.x * 0.7f;
					Vector3 velocity7 = rigid.velocity;
					float num13 = (velocity7.x = num12);
					Vector3 vector13 = (rigid.velocity = velocity7);
				}
			}
			if (Global.BlockControl <= 0 && !NOWALK && Global.DemoMove == 0)
			{
				if (ShiftTime <= 0)
				{
					float moveScale = ((StrikeTime > 0 && grounded) ? 0.85f : 1f);
					if (PushTimer == 0)
					{
						float x2 = horizontalMove * speed * moveScale * SandSpeedFactor + NeedSpeed.x;
						Vector3 velocity8 = rigid.velocity;
						float num14 = (velocity8.x = x2);
						Vector3 vector15 = (rigid.velocity = velocity8);
					}
					else
					{
						float x3 = horizontalMove * speed * 0.5f * moveScale * SandSpeedFactor + NeedSpeed.x;
						Vector3 velocity9 = rigid.velocity;
						float num15 = (velocity9.x = x3);
						Vector3 vector17 = (rigid.velocity = velocity9);
					}
				}
			}
			if (Global.DemoMove != 0)
			{
				SetHorizontal = Global.DemoMove;
				if (UP)
				{
					int num16 = 5;
					Vector3 velocity10 = rigid.velocity;
					float num17 = (velocity10.y = num16);
					Vector3 vector19 = (rigid.velocity = velocity10);
				}
				if (DOWN)
				{
					int num18 = -5;
					Vector3 velocity11 = rigid.velocity;
					float num19 = (velocity11.y = num18);
					Vector3 vector21 = (rigid.velocity = velocity11);
				}
				if (!UP && !DOWN)
				{
					float x4 = (float)Global.DemoMove * speed * 0.75f;
					Vector3 velocity12 = rigid.velocity;
					float num20 = (velocity12.x = x4);
					Vector3 vector23 = (rigid.velocity = velocity12);
				}
			}
			NOWALK = false;
			SpeedY = 0f;
			if (!IsNoFalling)
			{
				SpeedY -= 0.32f;
			}
			SpeedY -= 0.12f;
			float y2 = rigid.velocity.y + SpeedY - OldNeedSpeed.y + NeedSpeed.y;
			Vector3 velocity13 = rigid.velocity;
			float num21 = (velocity13.y = y2);
			Vector3 vector25 = (rigid.velocity = velocity13);
			if (!(rigid.velocity.y >= -20f))
			{
				lastVELOCITY = rigid.velocity.y + 20f;
			}
			if (!(rigid.velocity.y <= 0f))
			{
				lastVELOCITY = 0f;
			}
			if (!(rigid.velocity.y >= -20f))
			{
				int num22 = -20;
				Vector3 velocity14 = rigid.velocity;
				float num23 = (velocity14.y = num22);
				Vector3 vector27 = (rigid.velocity = velocity14);
			}
			if (_CorrectTimer > 0)
			{
				_CorrectTimer--;
				float x5 = rigid.velocity.x * 0.25f;
				Vector3 velocity15 = rigid.velocity;
				float num24 = (velocity15.x = x5);
				Vector3 vector29 = (rigid.velocity = velocity15);
			}
			if (Global.DoubleJump && (IsNoFalling || grounded || Zahvat || InTheWater || (bool)RopeObject))
			{
				DoubleJump = true;
			}
			if (!grounded && !InTheWater && DoubleJump && NoDoubleJumpNow <= 0 && (Jump2 || ForcePlayJump || jumpTouchPad.IsFingerDown() || !(moveTouchPad.position.y <= 0.75f)) && block_animation <= 0 && Global.BlockControl <= 0)
			{
				DoubleJump = false;
				action = "salto";
				old_action = "salto";
				if (!animatedSprite)
				{
					animatedObject.CrossFade("Pony Salto", 0.025f);
				}
				else
				{
					animatedSprite.Play("salto");
				}
				float y3 = CalculateJumpVerticalSpeed() * 1.1f;
				Vector3 velocity16 = rigid.velocity;
				float num25 = (velocity16.y = y3);
				Vector3 vector31 = (rigid.velocity = velocity16);
			}
			if (NoDoubleJumpNow > 0)
			{
				NoDoubleJumpNow--;
			}
			Jump2 = false;
			if (edgeTimer > 0)
			{
				edgeTimer--;
			}
			if (IsNoFalling && !Zahvat)
			{
				if (grounded)
				{
					if (StrikeTime <= 0)
					{
						action = IdleAnimation;
						AfraidEdge = 0f;
						if (EdgeRight.test > 0)
						{
							if (edgeTimer < 40)
							{
								edgeTimer += 2;
							}
							AfraidEdge = -1f;
						}
						if (EdgeLeft.test > 0)
						{
							AfraidEdge += 1f;
							if (edgeTimer < 40)
							{
								edgeTimer += 2;
							}
						}
						if (edgeTimer >= 40 && !(Mathf.Abs(AfraidEdge) < 0.2f))
						{
							action = "edge";
							Direction = (int)Mathf.Sign(AfraidEdge);
						}
					}
					if ((Jump1 || ForcePlayJump || jumpTouchPad.IsFingerDown() || !(moveTouchPad.position.y <= 0.75f)) && block_animation <= 0 && Global.BlockControl <= 0)
					{
						ForcePlayJump = true;
						grounded = false;
						IsNoFalling = false;
						float y4 = CalculateJumpVerticalSpeed();
						Vector3 velocity17 = rigid.velocity;
						float num26 = (velocity17.y = y4);
						Vector3 vector33 = (rigid.velocity = velocity17);
					}
				}
				if (InTheWater && ShiftTime <= 0)
				{
					action = "swim";
					if (!(rigid.velocity.y >= -5f))
					{
						int num27 = -5;
						Vector3 velocity18 = rigid.velocity;
						float num28 = (velocity18.y = num27);
						Vector3 vector35 = (rigid.velocity = velocity18);
					}
					if (!(rigid.velocity.y <= 5f))
					{
						int num29 = 5;
						Vector3 velocity19 = rigid.velocity;
						float num30 = (velocity19.y = num29);
						Vector3 vector37 = (rigid.velocity = velocity19);
					}
					if (Global.BlockControl <= 0)
					{
						num5 = ((Mathf.Abs(moveTouchPad.position.y) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.y) : 0f);
						float y5 = (Input.GetAxis("Vertical") + num5 + (float)SetVertical) * speed + NeedSpeed.y;
						Vector3 velocity20 = rigid.velocity;
						float num31 = (velocity20.y = y5);
						Vector3 vector39 = (rigid.velocity = velocity20);
					}
					NeedSpeed.x *= 0.9f;
					NeedSpeed.y *= 0.9f;
					if (!Global.SwimFast)
					{
						rigid.velocity *= 0.75f;
					}
					else
					{
						rigid.velocity *= 0.9f;
					}
				}
			}
			else if (ShiftTime <= 0)
			{
				if (!(rigid.velocity.y <= 0.2f))
				{
					if (action != "salto")
					{
						action = "jump";
					}
				}
				else if (!(rigid.velocity.y <= -15f))
				{
					action = "jump3";
				}
				else
				{
					action = "jump4";
				}
			}
			if ((bool)RopeObject)
			{
				Vector3 force = new Vector3(Input.GetAxis("Horizontal") * 2f, 0f, 0f);
				action = "rope";
				if (!IsHorizontal)
				{
					RopeObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
				}
				else if ((Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f || Mathf.Abs(moveTouchPad.position.x) > 0.1f || SetHorizontal != 0f) && Global.BlockControl <= 0)
				{
					if (!(Input.GetAxis("Horizontal") + num5 + SetHorizontal <= 0.1f))
					{
						int rightJumpIH = RightJumpIH;
						Vector3 velocity21 = rigid.velocity;
						float num32 = (velocity21.y = rightJumpIH);
						Vector3 vector41 = (rigid.velocity = velocity21);
					}
					else
					{
						int leftJumpIH = LeftJumpIH;
						Vector3 velocity22 = rigid.velocity;
						float num33 = (velocity22.y = leftJumpIH);
						Vector3 vector43 = (rigid.velocity = velocity22);
					}
					RopeDelay = 15;
					IsNoFalling = false;
					RopeObject.layer = 9;
					if (Global.DoubleJump)
					{
						DoubleJump = true;
					}
					transform.position = RopeObject.transform.position;
					int num34 = -1;
					Vector3 position = transform.position;
					float num35 = (position.z = num34);
					Vector3 vector45 = (transform.position = position);
					RopeObject = null;
				}
				if ((bool)RopeObject && (Input.GetAxis("Vertical") > 0.2f || ForcePlayJump || jumpTouchPad.IsFingerDown() || SetVertical > 0 || !(moveTouchPad.position.y < 0.5f)) && Global.BlockControl <= 0)
				{
					RopeDelay = 15;
					IsNoFalling = false;
					RopeObject.layer = 9;
					gameObject.layer = 28;
					NeedSpeed = RopeObject.GetComponent<Rigidbody>().velocity * 0.25f;
					float y6 = CalculateJumpVerticalSpeed();
					Vector3 velocity23 = rigid.velocity;
					float num36 = (velocity23.y = y6);
					Vector3 vector47 = (rigid.velocity = velocity23);
					if (Global.DoubleJump)
					{
						DoubleJump = true;
					}
					transform.position = RopeObject.transform.position;
					int num37 = -1;
					Vector3 position2 = transform.position;
					float num38 = (position2.z = num37);
					Vector3 vector49 = (transform.position = position2);
					RopeObject = null;
				}
				if ((bool)RopeObject && (Input.GetAxis("Vertical") < -0.2f || SetVertical < 0 || !(moveTouchPad.position.y > -0.5f)) && Global.BlockControl <= 0)
				{
					RopeDelay = 15;
					IsNoFalling = false;
					RopeObject.layer = 9;
					NeedSpeed = RopeObject.GetComponent<Rigidbody>().velocity * 0.25f;
					int num39 = -3;
					Vector3 velocity24 = rigid.velocity;
					float num40 = (velocity24.y = num39);
					Vector3 vector51 = (rigid.velocity = velocity24);
					if (Global.DoubleJump)
					{
						DoubleJump = true;
					}
					transform.position = RopeObject.transform.position;
					int num41 = -1;
					Vector3 position3 = transform.position;
					float num42 = (position3.z = num41);
					Vector3 vector53 = (transform.position = position3);
					RopeObject = null;
				}
				if ((bool)RopeObject && Global.Break)
				{
					RopeDelay = 30;
					IsNoFalling = false;
					RopeObject.layer = 9;
					gameObject.layer = 28;
					if (Global.DoubleJump)
					{
						DoubleJump = true;
					}
					transform.position = RopeObject.transform.position;
					int num43 = -1;
					Vector3 position4 = transform.position;
					float num44 = (position4.z = num43);
					Vector3 vector55 = (transform.position = position4);
					RopeObject = null;
				}
			}
			if (action != "land")
			{
				if (Input.GetAxis("Horizontal") > 0f || moveTouchPad.position.x > 0.4f || !(SetHorizontal <= 0f))
				{
					if (!Zahvat)
					{
						if (Global.BlockControl <= 0)
						{
							Direction = 1;
							IdleAnimation = "idle";
							if (!RopeObject && grounded)
							{
								action = "walk";
							}
						}
					}
					else if (Direction < 0)
					{
						ZahvatDelay = 10;
						Zahvat = false;
					}
				}
				if (Input.GetAxis("Horizontal") < 0f || moveTouchPad.position.x < -0.4f || !(SetHorizontal >= 0f))
				{
					if (!Zahvat)
					{
						if (Global.BlockControl <= 0)
						{
							Direction = -1;
							IdleAnimation = "idle";
							if (!RopeObject && grounded)
							{
								action = "walk";
							}
						}
					}
					else if (Direction > 0)
					{
						ZahvatDelay = 10;
						Zahvat = false;
					}
				}
			}
			if (Global.VehicleName != string.Empty)
			{
				Global.VehicleName = string.Empty;
			}
			NeedSpeed.x *= 0.95f;
			NeedSpeed.y *= 0.95f;
			if (RopeDelay > 0)
			{
				RopeDelay--;
			}
			if (block_animation > 0)
			{
				block_animation--;
			}
			if (!InTheWater)
			{
				if (PushTimer > 0)
				{
					PushTimer--;
					if (Direction == 1)
					{
						action = "push";
					}
				}
				if (PushTimer < 0)
				{
					PushTimer++;
					if (Direction == -1)
					{
						action = "push";
					}
				}
				if (Zahvat)
				{
					action = "visit";
				}
			}
			SendAction = false;
			if (ToDropBox)
			{
				if (IsAction)
				{
					action = "action";
					ForceAction = 12;
					Global.BlockControl = 30;
				}
				else
				{
					action = "drop";
					Global.BlockControl = 20;
				}
			}
			if (ForceAction > 0)
			{
				ForceAction--;
				if (ForceAction == 0)
				{
					SendAction = true;
				}
			}
			ToDropBox = false;
			IsAction = false;
			if (!string.IsNullOrEmpty(Global.WithTheTool) && ToUseTheTools)
			{
				bool flag = true;
				if (!tool.neverending && Global.ToolsCount[Global.CurrentToolNumber] <= 0)
				{
					flag = false;
				}
				if (flag)
				{
					UseTheCurrentToolFunction();
				}
			}
			ToUseTheTools = false;
			if (DontDropBoxTimer <= 0 && old_action != "action" && !Zahvat && Global.BlockControl <= 0 && (PressAction || actionTouchPad.IsFingerDown()))
			{
				PressAction = false;
				rigid.velocity *= 0f;
				if (RopeObject == null)
				{
					if (Global.InTheHandObject == null)
					{
						action = "action";
						ForceAction = 12;
						Global.BlockControl = 30;
						ToUseTheTools = true;
					}
					else
					{
						ToDropBox = true;
					}
				}
			}
			if (DontDropBoxTimer > 0)
			{
				DontDropBoxTimer--;
			}
			if (ForcePlayJump)
			{
				action = "jump";
			}
			if (StrikeForce)
			{
				action = "whip";
			}
			if ((bool)RopeObject)
			{
				action = "visit";
			}
			if ((bool)RopeObject && StrikeTime > 0)
			{
				action = "whip";
			}
			if (Global.CatchTimer > 98)
			{
				action = "catch";
			}
			if (Dash)
			{
				action = "jump2";
			}
			Animate(null);
			ForcePlayJump = false;
			StrikeForce = false;
			Global.Break = false;
			OldInTheWater = InTheWater;
			InTheWater = false;
			if (BeenInWater && !InTheWater && !OldInTheWater)
			{
				if (NoWaterFlashTimer <= 0)
				{
					if ((bool)SFXWater)
					{
						AudioSource.PlayClipAtPoint(SFXWater, transform.position);
					}
					UnityEngine.Object.Instantiate(WaterSplash, myTransform.position + new Vector3(0f, 0f, -0.01f), Quaternion.identity);
				}
				BeenInWater = false;
				if ((Input.GetAxis("Vertical") >= 0.2f || ForcePlayJump || jumpTouchPad.IsFingerDown() || moveTouchPad.position.y > 0.1f || SetVertical > 0) && block_animation <= 0 && Global.BlockControl <= 0)
				{
					float y7 = CalculateJumpVerticalSpeed();
					Vector3 velocity25 = rigid.velocity;
					float num45 = (velocity25.y = y7);
					Vector3 vector57 = (rigid.velocity = velocity25);
				}
			}
			if (!(rigid.velocity.y >= -20f))
			{
				int num46 = -20;
				Vector3 velocity26 = rigid.velocity;
				float num47 = (velocity26.y = num46);
				Vector3 vector59 = (rigid.velocity = velocity26);
			}
			if (Dash)
			{
				int num48 = -20;
				Vector3 velocity27 = rigid.velocity;
				float num49 = (velocity27.y = num48);
				Vector3 vector61 = (rigid.velocity = velocity27);
			}
			CheckZahvat();
			OldNeedSpeed = NeedSpeed;
			SandSpeedFactor = 1f;
			if (NoGroundedTimer > 0)
			{
				NoGroundedTimer--;
			}
			else
			{
				if (!RopeObject)
				{
					IsNoFalling = false;
				}
				grounded = false;
			}
			Jump1 = false;
		}
	}

	public virtual void Cutscene(bool @bool)
	{
		if (@bool)
		{
			old_action = "cut";
			Global.BlockControl = 300;
			block_animation = 300;
			Global.InvincibleTimer = 300;
			rigid.velocity = Vector3.zero;
			if (!animatedSprite)
			{
				animatedObject.CrossFade(GetAnim(IdleAnimation), 0.25f);
			}
			else
			{
				animatedSprite.Play(IdleAnimation);
			}
		}
		else
		{
			Global.BlockControl = 0;
			block_animation = 0;
			Global.InvincibleTimer = 0;
		}
	}

	public virtual void IdleAnim(string anim)
	{
		IdleAnimation = anim;
		Global.BlockControl = 25;
		Global.InvincibleTimer = 25;
	}

	public virtual void Update()
	{
		if (rollTouchPad == null && Global.MobilePlatform)
		{
			CheckRollTouchPad();
		}
		bool rollTouchPressed = RollTouchDown();
		bool actionTouchPressed = ActionTouchDown();
		bool downTouchPressed = MoveDownTouchDown();
		if (Global.Pause || TalkPause.IsGameplayBlocked() || Global.FadeMode == 1)
		{
			return;
		}
		if (ToCP)
		{
			ToCP = false;
			ReturnToCheckPoint();
		}
		if (!InTheWater)
		{
			CheckRope();
		}
		SetPositionTool();
		if (!(rigid.velocity.y <= 0.2f))
		{
			Global.PlatformDown = 2;
			CheckPlatform();
		}
		if (Global.OnPlatformDown > 0 && (Input.GetButtonDown("Down") || downTouchPressed))
		{
			Global.PlatformDown = 16;
			grounded = false;
			IsNoFalling = false;
		}
		if (Input.GetButtonDown("Jump2"))
		{
			Jump1 = true;
			Jump2 = true;
		}
		if (grounded && Input.GetButton("Jump"))
		{
			ForcePlayJump = true;
		}
		if (Global.CatchTimer <= 0 && (Input.GetButtonDown("Down") || downTouchPressed) && ZahvatDelay <= 0 && RopeDelay <= 0 && Global.OnPlatformDown <= 0 && !Dash && Convert.ToInt32(Global.Var["dash"]) == 1 && !IsNoFalling && !grounded && !Zahvat && !InTheWater && !RopeObject)
		{
			Dash = true;
			Global.MP -= 10f;
			NeedSpeed *= 0.2f;
			rigid.velocity *= 0.2f;
			Global.CreateSFX(SFXStrike, transform.position, 1f, 1f);
		}
		if ((Input.GetButtonDown("Shift") || rollTouchPressed) && StrikeTime <= 0 && !(Global.HP <= 0f) && Global.FadeMode != 1 && AfterStrikeTime <= 0)
		{
			if (!(Global.MP <= 0f))
			{
				if (Input.GetMouseButtonDown(2))
				{
					Direction = (int)Mathf.Sign(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f)).x - transform.position.x);
				}
				Global.MP -= 10f;
				StrikeTime = 20;
				ShiftTime = 20;
				CheckPlatform();
				Zahvat = false;
				ZahvatDelay = 10;
				if (Global.CatchTimer < 99)
				{
					animatedObject.CrossFade("Pony Shift", 0.02f);
				}
				NeedSpeed.x = Direction;
				int num = 6;
				Vector3 velocity = rigid.velocity;
				float num2 = (velocity.y = num);
				Vector3 vector = (rigid.velocity = velocity);
				if ((bool)RopeObject)
				{
					RopeObject.layer = 9;
					RopeObject = null;
					EscapeFromRopeTimer = 20;
				}
				int num3 = 12 * Direction;
				Vector3 velocity2 = rigid.velocity;
				float num4 = (velocity2.x = num3);
				Vector3 vector3 = (rigid.velocity = velocity2);
				IsNoFalling = false;
				grounded = false;
			}
			else
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(SFXFail, transform.position + new Vector3((float)Direction * 0.55f, 0.5f, -0.1f), Quaternion.identity) as GameObject;
			}
		}
		bool flag = false;
		if (!Global.Boomerang && !Zahvat && AfterStrikeTime <= 0)
		{
			bool flag2 = false;
			bool flag3 = Input.GetButtonDown("Strike") || actionTouchPressed;
			if (flag3 && Global.CatchTimer < 98 && Global.offBlockTimer <= 0)
			{
				flag2 = true;
			}
			else if (flag3 && !Input.GetMouseButton(0))
			{
				flag2 = true;
			}
			if (flag2)
			{
				if (!(Global.MP <= 0f))
				{
					if (Global.BlockControl <= 0 && StrikeTime <= 0)
					{
						if (Input.GetMouseButtonDown(0))
						{
							Direction = (int)Mathf.Sign(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f)).x - transform.position.x);
						}
						Global.MP -= Global.skillPower * (Perks.GetPOWER() * 0.5f) * Global.skillStamina;
						StrikeTime = Global.skillTime;
						flag = true;
						StrikeForce = true;
					}
				}
				else if (!FailLock)
				{
					FailLock = true;
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(SFXFail, transform.position + new Vector3((float)Direction * 0.55f, 0.5f, -0.1f), Quaternion.identity) as GameObject;
				}
			}
			else
			{
				FailLock = false;
			}
		}
		if (Global.DemoMove == 0)
		{
			DirectionControl();
		}
		if (flag)
		{
			Global.CreateSFX(SFXWhip, transform.position, UnityEngine.Random.Range(0.35f, 0.644f), UnityEngine.Random.Range(0.95f, 1.2f));
			gameObject.BroadcastMessage("StrikeWhip", StrikeTime, SendMessageOptions.DontRequireReceiver);
		}
		CheckPlatform();
	}

	public virtual void OnGUI()
	{
	}

	public virtual void ReDoubleJump()
	{
		NeedSpeed *= 0f;
		Dash = false;
		IsNoFalling = false;
		grounded = false;
		if (Global.DoubleJump)
		{
			DoubleJump = true;
		}
	}

	public virtual void ActionHero()
	{
		if (Global.HP > 0f)
		{
			old_action = "action";
			Global.BlockControl = 25;
			block_animation = 25;
			int num = 0;
			Vector3 velocity = rigid.velocity;
			float num2 = (velocity.x = num);
			Vector3 vector = (rigid.velocity = velocity);
			if (!animatedSprite)
			{
				animatedObject.CrossFade(GetAnim("use"), 0.25f);
			}
			else
			{
				animatedSprite.Play("use");
			}
		}
	}

	public virtual void Animate(string name)
	{
		if (name != null)
		{
			old_action = null;
			action = name;
		}
		if (!(Global.HP > 0f) || !(action != old_action) || block_animation > 0)
		{
			return;
		}
		old_action = action;
		switch (action)
		{
		case "shift":
			block_animation = StrikeTime;
			animatedObject.CrossFade("Pony Shift", 0.02f);
			break;
		case "whip":
			block_animation = StrikeTime;
			if (!animatedSprite)
			{
				animatedObject.CrossFade(Global.skillAnim, 0.02f);
			}
			else
			{
				animatedSprite.Play("whip");
			}
			break;
		case "action":
			block_animation = 30;
			if (!animatedSprite)
			{
				animatedObject.CrossFade(GetAnim(action), 0.2f);
			}
			else
			{
				animatedSprite.Play(action);
			}
			break;
		default:
			if (!animatedSprite)
			{
				animatedObject.CrossFade(GetAnim(action), 0.15f);
			}
			else
			{
				animatedSprite.Play(action);
			}
			break;
		}
	}

	public virtual void HPMinus(float hp)
	{
		Global.QuakeStart(75, 30f);
		Zahvat = false;
		ZahvatDelay = 10;
		UnityEngine.Object.Destroy(Global.ZahvatTransform);
		if ((bool)RopeObject)
		{
			RopeDelay = 20;
			IsNoFalling = false;
			RopeObject.layer = 9;
			transform.position = RopeObject.transform.position;
			int num = -1;
			Vector3 position = transform.position;
			float num2 = (position.z = num);
			Vector3 vector = (transform.position = position);
			RopeObject = null;
			EscapeFromRopeTimer = 20;
		}
		_DropTheObject = 0;
		if (!(GetComponent<AudioSource>().clip == SFXOuch) || !GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = SFXOuch;
			GetComponent<AudioSource>().Play();
		}
		if ((bool)particleStrike)
		{
			UnityEngine.Object.Instantiate(particleStrike, myTransform.position + new Vector3(0f, 0.25f, -0.5f), Quaternion.identity);
		}
		Global.HP -= hp;
		Global.InvincibleTimer = 50;
		HurtMessage(hp);
		ShockTimer = 50;
		gameObject.BroadcastMessage("SHOCK", 1, SendMessageOptions.DontRequireReceiver);
		int num3 = 0;
		if (!(Global.HP > 0f))
		{
			num3 = 1000;
		}
		if (!(hp <= 0f))
		{
			string text = null;
			text = ((UnityEngine.Random.Range(1, 100) <= 50) ? "fail2" : "fail");
			FailMove();
			action = text;
			old_action = text;
			block_animation = 30 + num3;
			Global.BlockControl = 30 + num3;
		}
		else
		{
			action = "salto";
			old_action = "salto";
			block_animation = 30;
			Global.BlockControl = 30;
		}
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
		if (other.gameObject.tag == "Zahvat")
		{
			GetZahvatObject = other.gameObject;
		}
		if (other.gameObject.tag == "Danger")
		{
			Dangerz();
		}
	}

	public virtual void Dangerz()
	{
		Global.HP = 0f;
		old_action = "fail";
		FailMove();
		Global.BlockControl = 50;
		block_animation = 50;
	}

	public virtual void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Ignore")
		{
			return;
		}
		bool flag = default(bool);
		if (InTheWater || (bool)RopeObject)
		{
			return;
		}
		bool flag2 = false;
		int i = 0;
		ContactPoint[] contacts = collision.contacts;
		for (int length = contacts.Length; i < length; i++)
		{
			if (!(rigid.velocity.y > 3f))
			{
				if (!flag)
				{
					if (!(contacts[i].normal.x > -0.9f))
					{
						if (oldContact == 1)
						{
							Zahvat = false;
							float y = myTransform.position.y + 0.1f;
							Vector3 position = myTransform.position;
							float num = (position.y = y);
							Vector3 vector = (myTransform.position = position);
							MonoBehaviour.print("JJJJAAAAAAAAM!");
						}
						oldContact = -1;
						flag = true;
					}
					if (!(contacts[i].normal.x < 0.9f))
					{
						if (oldContact == -1)
						{
							Zahvat = false;
							float y2 = myTransform.position.y + 0.1f;
							Vector3 position2 = myTransform.position;
							float num2 = (position2.y = y2);
							Vector3 vector3 = (myTransform.position = position2);
							MonoBehaviour.print("JJJJAAAAAAAAM!");
						}
						oldContact = 1;
						flag = true;
					}
				}
				if (!(contacts[i].normal.y < 0.4f))
				{
					IsNoFalling = true;
					grounded = true;
					if (Dash)
					{
						DashCrush();
						Global.BreakGlassTimer = 10;
						collision.gameObject.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
						Global.BlockControl = 10;
						int num3 = 0;
						Vector3 velocity = rigid.velocity;
						float num4 = (velocity.x = num3);
						Vector3 vector5 = (rigid.velocity = velocity);
						NeedSpeed.x = 0f;
					}
					if (!(lastVELOCITY * 0.5f > -1f))
					{
						Global.InvincibleTimer = 0;
						if (!(GetComponent<AudioSource>().clip == SFXCrush) || !GetComponent<AudioSource>().isPlaying)
						{
							GetComponent<AudioSource>().clip = SFXCrush;
							GetComponent<AudioSource>().Play();
						}
						HPMinus(1f);
					}
					lastVELOCITY = 0f;
					NoGroundedTimer = 18;
				}
				if (!(contacts[i].normal.x > -0.9f))
				{
					if (IsNoFalling && (Input.GetAxis("Horizontal") > 0.1f || moveTouchPad.position.x > 0f || !(SetHorizontal <= 0f)))
					{
						PushTimer = 3;
					}
					if (!grounded && Direction > 0)
					{
						NOWALK = true;
					}
				}
				if (!(contacts[i].normal.x < 0.9f))
				{
					if (IsNoFalling && (Input.GetAxis("Horizontal") < -0.1f || moveTouchPad.position.x < 0f || !(SetHorizontal >= 0f)))
					{
						PushTimer = -3;
					}
					if (!grounded && Direction < 0)
					{
						NOWALK = true;
					}
				}
			}
			if (!flag2 && ShiftTime > 5 && Global.FadeMode == 0)
			{
				if (!(contacts[i].normal.x > -0.9f))
				{
					animatedObject.CrossFade("Pony Salto", 0.02f);
					Direction = -1;
					flag2 = true;
					float x = (float)ShiftTime * 0.6f * (float)Direction;
					Vector3 velocity2 = rigid.velocity;
					float num5 = (velocity2.x = x);
					Vector3 vector7 = (rigid.velocity = velocity2);
					NeedSpeed.x = 0f - Mathf.Abs(NeedSpeed.x);
					float y3 = CalculateJumpVerticalSpeed();
					Vector3 velocity3 = rigid.velocity;
					float num6 = (velocity3.y = y3);
					Vector3 vector9 = (rigid.velocity = velocity3);
				}
				if (!(contacts[i].normal.x < 0.9f))
				{
					animatedObject.CrossFade("Pony Salto", 0.02f);
					Direction = 1;
					flag2 = true;
					float x2 = (float)ShiftTime * 0.6f * (float)Direction;
					Vector3 velocity4 = rigid.velocity;
					float num7 = (velocity4.x = x2);
					Vector3 vector11 = (rigid.velocity = velocity4);
					NeedSpeed.x = Mathf.Abs(NeedSpeed.x);
					float y4 = CalculateJumpVerticalSpeed();
					Vector3 velocity5 = rigid.velocity;
					float num8 = (velocity5.y = y4);
					Vector3 vector13 = (rigid.velocity = velocity5);
				}
			}
		}
		if (!flag)
		{
			oldContact = 0;
		}
	}

	public virtual void SensoreOnTriggerStay(GameObject other)
	{
		if (NoActionTimer > 0 || !ON_TRUE || DontDropBoxTimer > 0 || !other)
		{
			return;
		}
		IsAction = true;
		if (SendAction || ToDropBox)
		{
			if (ToDropBox && !grounded && !OldInTheWater)
			{
				IsAction = false;
				return;
			}
			SendActionToFalse = true;
			ToUseTheTools = false;
			other.SendMessage("ActON", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual float CalculateJumpVerticalSpeed()
	{
		float result;
		if (Global.BlockMouseTime <= 0)
		{
			Jump2 = false;
			Jump1 = false;
			NoDoubleJumpNow = 6;
			edgeTimer = 0;
			if (ShiftTime <= 0)
			{
				Global.MP -= 2f;
			}
			NoStompCorrectionTimer = 10;
			if (!BeenInWater && (!(GetComponent<AudioSource>().clip == SFXJump) || !GetComponent<AudioSource>().isPlaying))
			{
				GetComponent<AudioSource>().clip = SFXJump;
				GetComponent<AudioSource>().Play();
			}
			IsNoFalling = false;
			grounded = false;
			Global.PlatformDown = 2;
			gameObject.layer = 28;
			result = ((rigid.velocity.y <= 5f) ? Mathf.Sqrt(2f * jumpHeight * gravity) : rigid.velocity.y);
		}
		else
		{
			result = 0f;
		}
		return result;
	}

	public virtual void FromEnemyJump(float y)
	{
		if (Dash)
		{
			DashCrush();
		}
		IsNoFalling = false;
		grounded = false;
		if (!(GetComponent<AudioSource>().clip == SFXJumpEnemy) || !GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = SFXJumpEnemy;
			GetComponent<AudioSource>().Play();
		}
		if (!(myTransform.position.y <= y))
		{
			int num = 6;
			Vector3 velocity = rigid.velocity;
			float num2 = (velocity.y = num);
			Vector3 vector = (rigid.velocity = velocity);
		}
		else
		{
			int num3 = -6;
			Vector3 velocity2 = rigid.velocity;
			float num4 = (velocity2.y = num3);
			Vector3 vector3 = (rigid.velocity = velocity2);
		}
		if (Global.DoubleJump)
		{
			DoubleJump = true;
		}
	}

	public virtual void DashCrush()
	{
		Global.CreateSFX(SFXCrush, transform.position, 1f, 1f);
		Global.QuakeStart(70, 30f);
		Dash = false;
	}

	public virtual void ReturnToCheckPoint()
	{
		Global.RTCP = false;
		if (Global.ToFindCheckPointXYZ)
		{
			GameObject[] array = null;
			array = GameObject.FindGameObjectsWithTag("CheckPoint");
			int i = 0;
			GameObject[] array2 = array;
			for (int length = array2.Length; i < length; i++)
			{
				if ((bool)(CampFire)array2[i].GetComponent(typeof(CampFire)) && array2[i].name == Global.CheckPointNameTemp)
				{
					Global.CheckPoint = array2[i].transform.position;
					Global.CheckPointDirectionToRight = ((CampFire)array2[i].GetComponent(typeof(CampFire))).DirectionToRight;
					Global.ToFindCheckPointXYZ = false;
				}
				SpawnPoint spawnPoint = (SpawnPoint)array2[i].GetComponent(typeof(SpawnPoint));
				if ((bool)spawnPoint && spawnPoint.ID == Global.CheckPointNameTemp)
				{
					Global.CheckPoint = array2[i].transform.position;
					Global.CheckPointDirectionToRight = ((SpawnPoint)array2[i].GetComponent(typeof(SpawnPoint))).DirectionToRight;
					Global.ToFindCheckPointXYZ = false;
				}
			}
		}
		if (!Global.ToFindCheckPointXYZ)
		{
			if (!string.IsNullOrEmpty(Global.CheckPointNameTemp))
			{
				myTransform.position = new Vector3(Global.CheckPoint.x, Global.CheckPoint.y, myTransform.position.z);
				if (RuntimeServices.ToBool(Global.CheckPointDirectionToRight))
				{
					Direction = 1;
				}
				else
				{
					Direction = -1;
				}
			}
			Global.ToFindCheckPointXYZ = true;
		}
		Global.CheckPointNameTemp = string.Empty;
	}

	public virtual void StreamSpeed(Vector3 needspeed)
	{
		NeedSpeed = Vector3.Lerp(NeedSpeed, needspeed, 0.25f);
	}

	public virtual void SetRopeObject(GameObject go)
	{
		if ((bool)RopeObject || RopeDelay > 0 || ((bool)go.transform.parent && oldRopeParent != null && EscapeFromRopeTimer > 0 && go.transform.parent == oldRopeParent))
		{
			return;
		}
		RopeObject = go;
		if ((bool)RopeObject.transform.parent)
		{
			oldRopeParent = RopeObject.transform.parent;
		}
		Global.BlockControl = 8;
		IsNoFalling = true;
		lastVELOCITY = 0f;
		if ((bool)RopeObject.transform.parent)
		{
			if ((bool)RopeObject.transform.parent.gameObject.GetComponent<AudioSource>())
			{
				RopeObject.transform.parent.gameObject.GetComponent<AudioSource>().Play();
			}
			if ((bool)RopeObject.transform.parent && RopeObject.transform.parent.gameObject.layer == 9)
			{
				IsHorizontal = ((Rope_Line)RopeObject.transform.parent.gameObject.GetComponent(typeof(Rope_Line))).IsHorizontal;
				if (IsHorizontal)
				{
					LeftJumpIH = ((Rope_Line)RopeObject.transform.parent.gameObject.GetComponent(typeof(Rope_Line))).LeftJumpIH;
					RightJumpIH = ((Rope_Line)RopeObject.transform.parent.gameObject.GetComponent(typeof(Rope_Line))).RightJumpIH;
				}
			}
		}
		RopeObject.layer = 24;
		if (ShiftTime <= 0)
		{
			RopeObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Clamp(rigid.velocity.x, -3f, 3f) * 0.75f, rigid.velocity.y, 0f), ForceMode.VelocityChange);
		}
		else
		{
			RopeObject.GetComponent<Rigidbody>().AddForce(new Vector3((float)(ShiftTime * Direction) * 2.5f, 0f, 0f), ForceMode.VelocityChange);
		}
		myTransform.position = new Vector3(RopeObject.transform.position.x, RopeObject.transform.position.y, myTransform.position.z);
	}

	public virtual void CheckRope()
	{
		if ((bool)RopeObject)
		{
			IsNoFalling = true;
			float x = 0f;
			Vector3 velocity = rigid.velocity;
			float num = (velocity.x = x);
			Vector3 vector = (rigid.velocity = velocity);
			float y = 0f;
			Vector3 velocity2 = rigid.velocity;
			float num2 = (velocity2.y = y);
			Vector3 vector3 = (rigid.velocity = velocity2);
			transform.position = new Vector3(RopeObject.transform.position.x - (float)Direction * 0.4f, RopeObject.transform.position.y, -1f);
		}
	}

	public virtual void OxigenCheck(bool @bool)
	{
		if (!Global.Pause && !Global.MenuPause && !TalkPause.IsGameplayBlocked())
		{
			if (@bool)
			{
				Global.Oxygen = (int)Mathf.Lerp(Global.Oxygen, Global.MaxOxygen, 0.2f);
			}
			else if (Global.FadeMode == 0)
			{
				Global.Oxygen = (int)((float)Global.Oxygen - 1f * Time.deltaTime * 50f);
			}
		}
	}

	public virtual void InWater()
	{
		if (Zahvat)
		{
			Zahvat = false;
			ZahvatDelay = 20;
		}
		lastVELOCITY = 0f;
		IsNoFalling = true;
		Dash = false;
		if (!Global.Vodolaz)
		{
			if (UnityEngine.Random.Range(0, 100) > 95 && (!(GetComponent<AudioSource>().clip == SFXBulp) || !GetComponent<AudioSource>().isPlaying))
			{
				GetComponent<AudioSource>().clip = SFXBulp;
				GetComponent<AudioSource>().Play();
			}
		}
		else
		{
			Global.Oxygen = Global.MaxOxygen;
		}
		if (!BeenInWater)
		{
			if (NoWaterFlashTimer <= 0)
			{
				if ((bool)SFXWater)
				{
					AudioSource.PlayClipAtPoint(SFXWater, transform.position);
				}
				UnityEngine.Object.Instantiate(WaterSplash, myTransform.position + new Vector3(0f, 0f, -0.01f), Quaternion.identity);
			}
			NeedSpeed = rigid.velocity - new Vector3(NeedSpeed.x, NeedSpeed.y * 0.25f, 0f) * 0.05f;
		}
		BeenInWater = true;
		InTheWater = true;
	}

	public virtual void CheckZahvat()
	{
		if ((bool)GetZahvatObject && !(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(GetZahvatObject.transform.position.x, GetZahvatObject.transform.position.y)) <= 2f))
		{
			GetZahvatObject = null;
		}
		else if (ZahvatDelay > 0)
		{
			GetZahvatObject = null;
		}
		else
		{
			if (IsNoFalling || Zahvat)
			{
				return;
			}
			if (!Zahvat && (bool)Global.ZahvatTransform)
			{
				UnityEngine.Object.Destroy(Global.ZahvatTransform);
			}
			if (Global.ZahvatTransform == null)
			{
				Zahvat = false;
			}
			if (Global.BlockControl != 0)
			{
				return;
			}
			if ((bool)GetZahvatObject && !grounded && !Dash && ShiftTime <= 0)
			{
				float x = GetZahvatObject.transform.position.x;
				Vector3 position = myTransform.position;
				float num = (position.x = x);
				Vector3 vector = (myTransform.position = position);
				float y = GetZahvatObject.transform.position.y;
				Vector3 position2 = myTransform.position;
				float num2 = (position2.y = y);
				Vector3 vector3 = (myTransform.position = position2);
				Zahvat = true;
				Global.ZahvatTransform = new GameObject("GameObject");
				Global.ZahvatTransform.transform.position = myTransform.position;
				float x2 = myTransform.position.x;
				Vector3 position3 = Global.ZahvatTransform.transform.position;
				float num3 = (position3.x = x2);
				Vector3 vector5 = (Global.ZahvatTransform.transform.position = position3);
				Global.ZahvatTransform.transform.parent = GetZahvatObject.transform;
			}
			else
			{
				if (Global.PlatformDown > 0 || ShiftTime > 0 || rigid.velocity.y >= 0f || Dash)
				{
					return;
				}
				RaycastHit hitInfo = default(RaycastHit);
				Vector3 origin = myTransform.position + new Vector3(0f, 0.14f, 0f);
				if (Physics.Raycast(origin, new Vector3(Direction, 0f, 0f), out hitInfo, 0.4f) && !(hitInfo.collider.gameObject == null) && (hitInfo.collider.gameObject.layer == 0 || hitInfo.collider.gameObject.layer == 14 || hitInfo.collider.gameObject.layer == 26) && !hitInfo.collider.isTrigger)
				{
					origin = myTransform.position + new Vector3(0f, 0.44f, 0f);
					GameObject gameObject = hitInfo.collider.gameObject;
					if (!Physics.Raycast(origin, new Vector3(Direction, 0f, 0f), out hitInfo, 0.4f))
					{
						Zahvat = true;
						Dash = false;
						Global.ZahvatTransform = new GameObject("GameObject");
						Global.ZahvatTransform.transform.position = myTransform.position;
						Global.ZahvatTransform.transform.parent = gameObject.transform;
					}
				}
			}
		}
	}

	public virtual void ChangeOneSprite(Transform child, string mod)
	{
		tk2dSprite tk2dSprite2 = null;
	}

	public virtual void StartGetWithTheTool()
	{
		if (Global.CurrentToolNumber < 0)
		{
			Global.CurrentToolNumber = 0;
		}
		if (!string.IsNullOrEmpty(Global.Tools[Global.CurrentToolNumber]))
		{
			Global.WithTheTool = Global.Tools[Global.CurrentToolNumber];
		}
		if (Global.WithTheTool == null)
		{
			Global.WithTheTool = string.Empty;
			Global.CurrentToolNumber = 0;
			Global.WithTheTool = Global.Tools[Global.CurrentToolNumber];
		}
		UnityEngine.Object.Destroy(Global.WithTheToolObject);
		Global.WithTheToolObject = null;
		if (Global.WithTheTool == string.Empty)
		{
			Global.CurrentToolNumber = 0;
			Global.WithTheTool = Global.Tools[Global.CurrentToolNumber];
			return;
		}
		MonoBehaviour.print("Global.WithTheTool: " + Global.WithTheTool);
		ResetToolOptions();
		CheckCountOfCurrentTool();
		bool flag = true;
		if (!tool.neverending && Global.ToolsCount[Global.CurrentToolNumber] <= 0)
		{
			flag = false;
		}
		if (flag)
		{
			Vector3 localScale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			Global.WithTheToolObject.transform.parent = _arm.transform;
			gameObject.transform.localScale = localScale;
			Global.WithTheToolObject.transform.localPosition = new Vector3(0f, 0f, -0.001f);
			if ((bool)Global.WithTheToolObject.GetComponent<Rigidbody>())
			{
				Global.WithTheToolObject.GetComponent<Rigidbody>().useGravity = false;
			}
			Global.WithTheToolObject.layer = 13;
			if ((bool)Global.WithTheToolObject.GetComponent<Collider>())
			{
				Global.WithTheToolObject.GetComponent<Collider>().enabled = false;
				Global.WithTheToolObject.GetComponent<Collider>().isTrigger = true;
			}
		}
	}

	public virtual void ResetToolOptions()
	{
		tool.neverending = true;
		tool.animation = "drop";
		tool.playerSpeed = new Vector3(0f, 0f, 0f);
		tool.blockTime = 30;
		tool.dropObject = null;
		tool.hideToolInAction = false;
		tool.dropSpeed = new Vector3(0f, 0f, 0f);
		tool.message = string.Empty;
		tool.sound = null;
		tool.ImmortalTime = 0;
		tool.mana = 0f;
		_DropTheObject = 0;
		if ((bool)Global.WithTheToolObject)
		{
			GetToolItem getToolItem = (GetToolItem)Global.WithTheToolObject.GetComponent("GetToolItem");
			if ((bool)getToolItem)
			{
				tool.neverending = getToolItem.toolOptions.neverending;
				tool.animation = getToolItem.toolOptions.animation;
				tool.playerSpeed = getToolItem.toolOptions.playerSpeed;
				tool.blockTime = getToolItem.toolOptions.blockTime;
				tool.dropObject = getToolItem.toolOptions.dropObject;
				tool.hideToolInAction = getToolItem.toolOptions.hideToolInAction;
				tool.ImmortalTime = getToolItem.toolOptions.ImmortalTime;
				tool.dropSpeed = getToolItem.toolOptions.dropSpeed;
				tool.message = getToolItem.toolOptions.message;
				tool.sound = getToolItem.toolOptions.sound;
				tool.mana = getToolItem.toolOptions.mana;
			}
		}
	}

	public virtual void SetPositionTool()
	{
	}

	public virtual void UseTheCurrentToolFunction()
	{
		if (Global.TimerNoVehicle > 1 || !Global.WithTheToolObject)
		{
			return;
		}
		block_animation = 0;
		action = tool.animation;
		if (tool.ImmortalTime > 0)
		{
			Global.InvincibleTimer = tool.ImmortalTime;
		}
		Global.BlockControl = tool.blockTime + 10;
		if ((bool)tool.dropObject)
		{
			if (!(Global.MP <= 0f))
			{
				Global.MP -= tool.mana * ((float)Global.RANG * Global.Formula);
				_DropTheObject = 7;
			}
			return;
		}
		rigid.velocity += new Vector3((float)Direction * tool.playerSpeed.x, tool.playerSpeed.y, tool.playerSpeed.z);
		if ((bool)tool.sound)
		{
			AudioSource.PlayClipAtPoint(tool.sound, transform.position);
		}
		if (!string.IsNullOrEmpty(tool.message))
		{
			Global.WithTheToolObject.SendMessage(tool.message, Direction, SendMessageOptions.DontRequireReceiver);
		}
		if ((bool)Global.WithTheToolObject.GetComponent<Collider>())
		{
			Global.WithTheToolObject.SendMessage("DisableSpray", new Vector3(15f, tool.blockTime - 10, 0f), SendMessageOptions.DontRequireReceiver);
			Global.WithTheToolObject.SendMessage("DisableCollider", new Vector3(15f, tool.blockTime - 10, 0f), SendMessageOptions.DontRequireReceiver);
		}
		if (!tool.neverending)
		{
			Global.ToolsCount[Global.CurrentToolNumber] = Global.ToolsCount[Global.CurrentToolNumber] - 1;
			CheckCountOfCurrentTool();
		}
	}

	public virtual void BackSpeed(Vector3 pstn)
	{
		if (BackTimer <= 0)
		{
			BackTimer = 20;
			float x = Mathf.Sign(myTransform.position.x - pstn.x) * 3f;
			Vector3 velocity = rigid.velocity;
			float num = (velocity.x = x);
			Vector3 vector = (rigid.velocity = velocity);
			float y = Mathf.Sign(myTransform.position.y - pstn.y) * 3f;
			Vector3 velocity2 = rigid.velocity;
			float num2 = (velocity2.y = y);
			Vector3 vector3 = (rigid.velocity = velocity2);
			block_animation = 0;
			Animate("salto2");
		}
	}

	public virtual void DropControl()
	{
		if (_DropTheObject <= 0)
		{
			return;
		}
		_DropTheObject--;
		if (_DropTheObject == 0)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(tool.dropObject, _arm.transform.position, Quaternion.identity) as GameObject;
			float x = Global.LastCreatedObject.transform.position.x + (float)Direction * 1.25f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.x = x);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			float y = Global.LastCreatedObject.transform.position.y + 0f;
			Vector3 position2 = Global.LastCreatedObject.transform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
			Global.LastCreatedObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			float x2 = Global.LastCreatedObject.transform.localScale.x * (float)Direction;
			Vector3 localScale = Global.LastCreatedObject.transform.localScale;
			float num3 = (localScale.x = x2);
			Vector3 vector5 = (Global.LastCreatedObject.transform.localScale = localScale);
			if ((bool)tool.sound)
			{
				GetComponent<AudioSource>().clip = tool.sound;
				GetComponent<AudioSource>().Play();
			}
			if (!string.IsNullOrEmpty(tool.message))
			{
				Global.LastCreatedObject.SendMessage(tool.message, Direction, SendMessageOptions.DontRequireReceiver);
				Global.WithTheToolObject.SendMessage(tool.message, Direction, SendMessageOptions.DontRequireReceiver);
			}
			if ((bool)Global.LastCreatedObject.GetComponent<Collider>())
			{
				Global.LastCreatedObject.GetComponent<Collider>().isTrigger = false;
			}
			if ((bool)Global.LastCreatedObject.GetComponent<Rigidbody>())
			{
				Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = new Vector3((float)Direction * tool.dropSpeed.x, tool.dropSpeed.y, tool.dropSpeed.z);
				float z = (float)Direction * tool.dropSpeed.z;
				Vector3 angularVelocity = Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity;
				float num4 = (angularVelocity.z = z);
				Vector3 vector7 = (Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity);
			}
			if (tool.hideToolInAction)
			{
				Global.WithTheToolObject.SendMessage("HideWithTimer", tool.blockTime, SendMessageOptions.DontRequireReceiver);
			}
			Global.LastCreatedObject.SendMessage("ShotTheTool", null, SendMessageOptions.DontRequireReceiver);
			if (!tool.neverending)
			{
				Global.ToolsCount[Global.CurrentToolNumber] = Global.ToolsCount[Global.CurrentToolNumber] - 1;
				CheckCountOfCurrentTool();
			}
		}
	}

	public virtual void CheckCountOfCurrentTool()
	{
		if (!tool.neverending && Global.ToolsCount[Global.CurrentToolNumber] <= 0)
		{
			Global.CurrentToolNumber = 0;
			UnityEngine.Object.DestroyImmediate(Global.WithTheToolObject);
			Global.WithTheTool = string.Empty;
		}
	}

	public virtual void CrushHP()
	{
		if (Global.InvincibleTimer > 0)
		{
			return;
		}
		float num = (float)Convert.ToInt32(Global.Var["defense"]) * 0.75f;
		float num2 = Global.LastStrike.pow * Global.GetDifficulty() - num;
		num2 += UnityEngine.Random.Range(num2 * -0.1f, num2 * 0.1f);
		if (!(Global.LastStrike.poison <= 0f))
		{
			Global.Var["poison"] = (float)Convert.ToInt32(Global.Var["poison"]) + Global.LastStrike.poison;
			Global.CreateText("+" + Global.LastStrike.poison + " POISON!", transform.position + new Vector3(0f, 1.5f, 0f), new Color(0.75f, 1f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
			if (Convert.ToInt32(Global.Var["poison"]) > 100)
			{
				Global.Var["poison"] = 100;
			}
		}
		float num3 = Global.MaxHP * Global.GetMinMinus();
		if (!(num2 >= num3))
		{
			num2 = num3;
		}
		HPMinus(num2);
		if ((bool)Global.LastStrike.trans)
		{
			if (!(myTransform.position.x <= Global.LastStrike.trans.position.x))
			{
				float x = 2.5f;
				Vector3 velocity = rigid.velocity;
				float num4 = (velocity.x = x);
				Vector3 vector = (rigid.velocity = velocity);
			}
		}
		else
		{
			float x2 = -2.5f;
			Vector3 velocity2 = rigid.velocity;
			float num5 = (velocity2.x = x2);
			Vector3 vector3 = (rigid.velocity = velocity2);
		}
		if (grounded)
		{
			int num6 = 3;
			Vector3 velocity3 = rigid.velocity;
			float num7 = (velocity3.y = num6);
			Vector3 vector5 = (rigid.velocity = velocity3);
		}
	}

	public virtual void HurtMessage(float showPow)
	{
		Global.CreateText("- " + FloatText(showPow), transform.position + new Vector3(0f, 1f, 0f), new Color(1f, 0.5f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
	}

	public virtual void ONTRUE()
	{
		ON_TRUE = true;
		NoActionTimer = 30;
	}

	public virtual void OFFTRUE()
	{
		ON_TRUE = false;
		action = IdleAnimation;
		old_action = IdleAnimation;
	}

	public virtual void ChangeCloths(int num)
	{
	}

	public virtual void StartWithVehicle()
	{
		GameObject gameObject = null;
		bool flag = false;
		if ((bool)VEHICLE_AT_START)
		{
			Global.VehicleName = string.Empty;
			gameObject = UnityEngine.Object.Instantiate(VEHICLE_AT_START);
			Global.VehicleName = "anyvehicle";
			gameObject.transform.position = myTransform.position;
			gameObject.SendMessage("EnterVehicle", false, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("Directions", Direction, SendMessageOptions.DontRequireReceiver);
			flag = true;
		}
		else if (Global.VehicleName == "anyvehicle")
		{
			Global.VehicleName = string.Empty;
		}
		else if (Global.VehicleName != null && Global.VehicleName != string.Empty)
		{
			gameObject = UnityEngine.Object.Instantiate(LoadData.GO("Vehicles/" + Global.VehicleName)) as GameObject;
			gameObject.transform.position = myTransform.position;
			gameObject.SendMessage("EnterVehicle", false, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("Directions", Direction, SendMessageOptions.DontRequireReceiver);
			flag = true;
		}
	}

	public virtual void CheckMouseControl()
	{
	}

	public virtual void BLOCK(bool @bool)
	{
		if (@bool)
		{
			Global.BlockControl = 10000000;
			NeedSpeed = Vector3.zero;
			rigid.velocity = Vector3.zero;
		}
		else
		{
			Global.BlockControl = 0;
			NeedSpeed = Vector3.zero;
			rigid.velocity = Vector3.zero;
		}
	}

	public virtual void ShockControl()
	{
		if (ShockTimer > 0)
		{
			ShockTimer--;
			if (ShockTimer == 0)
			{
				gameObject.BroadcastMessage("SHOCK", 0, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void SpecialAnimation(string anim)
	{
		rigid.useGravity = false;
		rigid.velocity = Vector3.zero;
		GetComponent<Collider>().enabled = false;
		NeedSpeed = Vector3.zero;
		ON_TRUE = false;
		action = anim;
		old_action = anim;
	}

	public virtual void SpecialStop()
	{
		float x = 0f;
		Vector3 velocity = rigid.velocity;
		float num = (velocity.x = x);
		Vector3 vector = (rigid.velocity = velocity);
	}

	public virtual void FailMove()
	{
		Global.BlockControl = 30;
		block_animation = 30;
		restmove_x = moveDirection.x;
		if (!animatedSprite)
		{
			animatedObject.CrossFade("Pony Shock", 0.2f);
		}
		else
		{
			animatedSprite.Play("fall");
		}
	}

	public virtual void OnMouseDown()
	{
		if ((bool)SlotItem.selected)
		{
			SlotItem.selected.SendMessage("UseIt", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void GravityOff()
	{
		Global.DemoMove = Direction;
		rigid.useGravity = false;
		gravity = 0f;
		UP = true;
	}

	public virtual void GravityDOWN()
	{
		Global.DemoMove = Direction;
		DOWN = true;
	}

	public virtual void CheckPlatform()
	{
		bool flag = false;
		if (ShiftTime <= 0)
		{
			if (Global.PlatformDown > 0 && gameObject.layer != 28)
			{
				oldLayer = 28;
				flag = true;
			}
			if (Global.PlatformDown <= 0 && gameObject.layer == 28)
			{
				oldLayer = 8;
				flag = true;
			}
			if (Zahvat)
			{
				oldLayer = 28;
				flag = true;
			}
			if (InTheWater)
			{
				oldLayer = 28;
				flag = true;
			}
		}
		else
		{
			oldLayer = 30;
			flag = true;
		}
		if (flag && oldLayer != gameObject.layer)
		{
			gameObject.layer = oldLayer;
		}
	}

	public virtual void DeadPony()
	{
		Global.CheckPointNameTemp = Global.CheckPointName;
		if (!(GetComponent<AudioSource>().clip == SFXFall) || !GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = SFXFall;
			GetComponent<AudioSource>().Play();
		}
		Camera.main.SendMessage("FadeOn", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual string GetAnim(string name)
	{
		object result;
		switch (name)
		{
		case "whip":
			result = Global.skillAnim;
			break;
		case "jump":
			result = "Pony Jump";
			break;
		case "swim":
			result = "Pony Swim";
			break;
		case "out":
			result = "Pony Out";
			break;
		case "catch":
			result = "Pony Catch";
			break;
		case "edge":
			result = "Pony Edge";
			break;
		case "push":
			result = "Pony Push";
			break;
		case "visit":
			result = "Pony Visit";
			break;
		case "fall":
			result = "Pony Shock";
			break;
		case "jump2":
			result = "Pony Fall";
			break;
		case "jump3":
			result = "Pony Fall2";
			break;
		case "jump4":
			result = "Pony Fall3";
			break;
		case "walk":
			result = "Pony Run";
			break;
		case "use":
			result = "Pony Action";
			break;
		case "sit":
			result = "Pony Sit";
			break;
		default:
			result = "Pony Idle";
			break;
		}
		return (string)result;
	}

	public virtual void SandControl(float f)
	{
		SandSpeedFactor = 0.5f;
		rigid.velocity *= f;
		if (!(rigid.velocity.y >= 0f))
		{
			PseudoCollTimer = 4;
			grounded = true;
			NoGroundedTimer = 18;
			IsNoFalling = true;
		}
		if (!(rigid.velocity.y >= -0.01f))
		{
			float y = -0.01f;
			Vector3 velocity = rigid.velocity;
			float num = (velocity.y = y);
			Vector3 vector = (rigid.velocity = velocity);
		}
	}

	public virtual void MindMinus()
	{
		if (Convert.ToInt32(Global.Var["mind"]) < Global.GetMaxMind())
		{
			int num = Convert.ToInt32(Global.Var["mind"]);
			Global.Var["mind"] = (float)num + (float)Global.GetMaxMind() * UnityEngine.Random.Range(0.1f, 0.15f);
			if (Convert.ToInt32(Global.Var["mind"]) > Global.GetMaxMind())
			{
				Global.Var["mind"] = Global.GetMaxMind();
			}
		}
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

	public virtual void SetDirect(object n)
	{
		Direction = RuntimeServices.UnboxInt32(n);
		oldDirection = 0;
		Global.CheckPointDirectionToRight = 0;
		Global.LastDirection = Direction;
		DirectionControl();
	}

	public virtual void DirectionControl()
	{
		if (oldDirection != Direction)
		{
			oldDirection = Direction;
			if (Direction == 1)
			{
				float z = (float)Direction * StartScale.z;
				Vector3 localScale = myTransform.localScale;
				float num = (localScale.z = z);
				Vector3 vector = (myTransform.localScale = localScale);
				int num2 = 0;
				Vector3 eulerAngles = myTransform.eulerAngles;
				float num3 = (eulerAngles.y = num2);
				Vector3 vector3 = (myTransform.eulerAngles = eulerAngles);
			}
			else
			{
				float z2 = (float)Direction * StartScale.z;
				Vector3 localScale2 = myTransform.localScale;
				float num4 = (localScale2.z = z2);
				Vector3 vector5 = (myTransform.localScale = localScale2);
				int num5 = 180;
				Vector3 eulerAngles2 = myTransform.eulerAngles;
				float num6 = (eulerAngles2.y = num5);
				Vector3 vector7 = (myTransform.eulerAngles = eulerAngles2);
			}
		}
	}

	public virtual void Main()
	{
	}
}
