using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Global : MonoBehaviour
{
	[NonSerialized]
	public static string[] HashNames;

	[NonSerialized]
	public static Hashtable Var = new Hashtable();

	[NonSerialized]
	public static string skillAnim;

	[NonSerialized]
	public static float skillPower;

	[NonSerialized]
	public static int skillTime;

	[NonSerialized]
	public static float skillStamina;

	[NonSerialized]
	public static bool skillNoDelete;

	[NonSerialized]
	public static string skillGOName;

	[NonSerialized]
	public static bool Boomerang;

	[NonSerialized]
	public static int DashStomp = 1;

	[NonSerialized]
	public static SendStrike LastStrike;

	[NonSerialized]
	public static int offBlockTimer;

	[NonSerialized]
	public static string WorldName;

	[NonSerialized]
	public static bool[,] WorldVisible;

	[NonSerialized]
	public static string[,] WorldMap;

	[NonSerialized]
	public static string[,] WorldLevel;

	[NonSerialized]
	public static string[,] MapIndex;

	[NonSerialized]
	public static Vector2 WorldPosition;

	[NonSerialized]
	public static string WorldLevelToReturn;

	[NonSerialized]
	public static bool WorldON;

	[NonSerialized]
	public static Vector2 WorldStart;

	[NonSerialized]
	public static Vector2 WorldMax;

	[NonSerialized]
	public static float deltaTime;

	[NonSerialized]
	public static int LastDirection;

	[NonSerialized]
	public static string FromDirection;

	[NonSerialized]
	public static string ToDirection;

	public object ISFIRST;

	[NonSerialized]
	public static Material NoLiteMat;

	public Material GlobalNoLiteMat;

	[NonSerialized]
	public static int Prioritet;

	[NonSerialized]
	public static GameObject PrioritetMapSlot;

	[NonSerialized]
	public static int BlockMouseTime;

	[NonSerialized]
	public static string YesMessage;

	[NonSerialized]
	public static UnityScript.Lang.Array GameMessage = new UnityScript.Lang.Array();

	[NonSerialized]
	public static bool SaveWithExitToMainMenu;

	[NonSerialized]
	public static int BreakGlassTimer;

	[NonSerialized]
	public static string GoldLevel;

	[NonSerialized]
	public static Vector3 GoldPosition;

	[NonSerialized]
	public static int GoldCount;

	[NonSerialized]
	public static float SurfaceWaterPoint;

	public AudioClip sfxBuy;

	public AudioClip sfxNo;

	[NonSerialized]
	public static AudioClip SFXBuy;

	[NonSerialized]
	public static AudioClip SFXNo;

	[NonSerialized]
	public static AudioClip ToPlay;

	public AudioClip SFX_LEVELUP;

	[NonSerialized]
	public static bool CanExitFromLevel;

	[NonSerialized]
	public static string DefaultLevel = "gala";

	[NonSerialized]
	public static Transform QuestLevelObject;

	[NonSerialized]
	public static Transform CurrentPlayerObject;

	[NonSerialized]
	public static int PlatformDown;

	[NonSerialized]
	public static int OnPlatformDown;

	[NonSerialized]
	public static int CorrectMaxHP;

	[NonSerialized]
	public static int PLAYER;

	[NonSerialized]
	public static int BuildCastleLevel;

	[NonSerialized]
	public static int RoomNPCNumber;

	[NonSerialized]
	public static GameObject SkyTaxiCall;

	[NonSerialized]
	public static GameObject ZahvatTransform;

	[NonSerialized]
	public static GameObject InTheHandObject;

	[NonSerialized]
	public static string WithTheTool = string.Empty;

	[NonSerialized]
	public static GameObject WithTheToolObject;

	[NonSerialized]
	public static int InTheCloth;

	[NonSerialized]
	public static int HairHero = 1;

	[NonSerialized]
	public static int HatHero;

	[NonSerialized]
	public static Color HairColor = new Color(1f, 1f, 0f, 1f);

	[NonSerialized]
	public static string[] clothesSpriteNames = new string[12];

	[NonSerialized]
	public static AudioClip ThatMusic;

	[NonSerialized]
	public static AudioClip ThatAmbient;

	[NonSerialized]
	public static AudioSource GlobalMusic;

	[NonSerialized]
	public static bool NoMouse;

	[NonSerialized]
	public static bool GoldBarEffect;

	[NonSerialized]
	public static int CheatGodMode;

	[NonSerialized]
	public static int CheatMapOpen;

	[NonSerialized]
	public static bool DoubleJump;

	[NonSerialized]
	public static bool FonarSkill;

	[NonSerialized]
	public static bool SwimFast;

	[NonSerialized]
	public static bool Vodolaz;

	[NonSerialized]
	public static int Oxygen;

	[NonSerialized]
	public static int MaxOxygen = 500;

	[NonSerialized]
	public static GameObject GlobalObject;

	[NonSerialized]
	public static string VehicleName = string.Empty;

	[NonSerialized]
	public static bool Passager_OFF;

	[NonSerialized]
	public static string LEVEL = string.Empty;

	[NonSerialized]
	public static int CurrentToolNumber;

	[NonSerialized]
	public static float BonusHP;

	[NonSerialized]
	public static float BonusMP;

	[NonSerialized]
	public static float BonusManaSpeed = 1f;

	[NonSerialized]
	public static float BonusDiscont;

	[NonSerialized]
	public static bool WIN;

	[NonSerialized]
	public static string DeadLevel;

	[NonSerialized]
	public static string DeadSpecial;

	[NonSerialized]
	public static float HP = 10f;

	[NonSerialized]
	public static float MaxHP = 10f;

	[NonSerialized]
	public static float MP = 10f;

	[NonSerialized]
	public static float MaxMP = 10f;

	[NonSerialized]
	public static int InvincibleTimer;

	[NonSerialized]
	public static int Boolet = 10;

	[NonSerialized]
	public static float Rainbow;

	[NonSerialized]
	public static int Potion;

	[NonSerialized]
	public static int Gold;

	[NonSerialized]
	public static int Guardian;

	[NonSerialized]
	public static int GunPower = 1;

	[NonSerialized]
	public static int MaxRainbow = 10;

	[NonSerialized]
	public static bool Break;

	[NonSerialized]
	public static string CurrentMission = "gala";

	[NonSerialized]
	public static int Weather;

	[NonSerialized]
	public static int Clock;

	[NonSerialized]
	public static int[] Upgrades = new int[21];

	[NonSerialized]
	public static UnityScript.Lang.Array Place = new UnityScript.Lang.Array();

	[NonSerialized]
	public static UnityScript.Lang.Array Quest = new UnityScript.Lang.Array();

	[NonSerialized]
	public static UnityScript.Lang.Array Stuff = new UnityScript.Lang.Array();

	[NonSerialized]
	public static int MenuX;

	[NonSerialized]
	public static int MenuY;

	[NonSerialized]
	public static string LastMenuName;

	[NonSerialized]
	public static bool YesNoMode;

	[NonSerialized]
	public static bool YesX;

	[NonSerialized]
	[HideInInspector]
	public static GameObject YesNoWindow;

	[NonSerialized]
	public static string YesNoCOMMAND;

	[NonSerialized]
	public static int YesNoID;

	[NonSerialized]
	public static int Difficulty = -2;

	[NonSerialized]
	public static int BlockControl;

	[NonSerialized]
	public static int DemoMove;

	[HideInInspector]
	public bool ResetThatTime;

	[NonSerialized]
	public static bool RTCP;

	[NonSerialized]
	public static int FadeMode;

	[NonSerialized]
	public static string NextLevel = string.Empty;

	[NonSerialized]
	public static object Reload;

	[NonSerialized]
	public static int WakeUpTime;

	[NonSerialized]
	public static bool MobilePlatform;

	[NonSerialized]
	public static string MenuText = string.Empty;

	[NonSerialized]
	public static GameObject LastMenuTextObject;

	[NonSerialized]
	[HideInInspector]
	public static bool Pause;

	[NonSerialized]
	public static bool MenuPause;

	[NonSerialized]
	[HideInInspector]
	public static int QUALITY;

	[NonSerialized]
	[HideInInspector]
	public static int SOUND = 1;

	[NonSerialized]
	[HideInInspector]
	public static int MUSIC = 1;

	[NonSerialized]
	public static int oMUSIC;

	[NonSerialized]
	public static int oSOUND = 1;

	[NonSerialized]
	public static int oQUALITY;

	[NonSerialized]
	public static bool Stomp;

	[NonSerialized]
	public static int NEXTTIME;

	[NonSerialized]
	public static bool ShowWEATHER;

	[NonSerialized]
	public static int DAYS;

	[NonSerialized]
	public static int DAYTIME;

	[NonSerialized]
	public static int DAYMINUTES;

	[NonSerialized]
	public static float DAYWIND;

	[NonSerialized]
	public static int DAYRAIN;

	[NonSerialized]
	public static float DAYCLOUD;

	[NonSerialized]
	public static float DAYRAINBOW;

	[NonSerialized]
	public static GameObject[,] SECTORS = (GameObject[,])System.Array.CreateInstance(typeof(GameObject), new int[2] { 8, 4 });

	[NonSerialized]
	public static Vector2 LEVELMASS;

	[NonSerialized]
	public static bool GenerateBounds;

	[NonSerialized]
	public static GameObject LastRoomNPC;

	[NonSerialized]
	public static int LevelSeed;

	[NonSerialized]
	public static bool CanSeed;

	[NonSerialized]
	public static int Experience;

	[NonSerialized]
	public static int RANG = 1;

	[NonSerialized]
	public static int LevelRANG;

	[NonSerialized]
	public static float Formula = 3f;

	[NonSerialized]
	public static AI EnemAI;

	[NonSerialized]
	public static int EnemyHitTimer;

	[NonSerialized]
	public static int EnemyHitHP;

	[NonSerialized]
	public static int EnemyHitMaxHP;

	[NonSerialized]
	public static int EnemyHitLevel;

	[NonSerialized]
	public static int LevelEnemy;

	[NonSerialized]
	public static int LevelGold;

	[NonSerialized]
	public static int LevelTime;

	[NonSerialized]
	public static bool DontGenerateEnemies;

	[NonSerialized]
	public static bool IsWin;

	[NonSerialized]
	public static bool IsMainQuest;

	[NonSerialized]
	public static int MainQuest;

	[NonSerialized]
	public static string TextQuest;

	[NonSerialized]
	public static UnityScript.Lang.Array EasyCells = new UnityScript.Lang.Array();

	[NonSerialized]
	public static UnityScript.Lang.Array HardCells = new UnityScript.Lang.Array();

	[NonSerialized]
	[HideInInspector]
	public static GameObject LastClickObject;

	[NonSerialized]
	[HideInInspector]
	public static GameObject LastCreatedObject;

	[NonSerialized]
	[HideInInspector]
	public static GameObject MenuWindow;

	[NonSerialized]
	[HideInInspector]
	public static bool HUD_ON;

	public GUISkin newSkin;

	[NonSerialized]
	public static GUISkin DefaultSKIN;

	public int ToolsMAX;

	[NonSerialized]
	public static int ToolsMax;

	[NonSerialized]
	public static string[] Tools = new string[16];

	[NonSerialized]
	public static int[] ToolsCount = new int[16];

	[NonSerialized]
	public static string[] Cloths = new string[15];

	[NonSerialized]
	public static Vector3 CheckPoint;

	[NonSerialized]
	public static string CheckPointName = string.Empty;

	[NonSerialized]
	public static string CheckPointNameTemp = string.Empty;

	[NonSerialized]
	public static object CheckPointDirectionToRight;

	[NonSerialized]
	public static bool ToFindCheckPointXYZ;

	[NonSerialized]
	public static int TimerNoVehicle;

	public Joystick pauseTouchPad;

	[NonSerialized]
	public static Color OldAmbient = Color.gray;

	[NonSerialized]
	public static int pauseAfterTime;

	public int timerUnload;

	[NonSerialized]
	public static bool OldPause;

	[NonSerialized]
	public static bool BlockEscape;

	[NonSerialized]
	[HideInInspector]
	public static GameObject[] _Gobjects;

	[NonSerialized]
	[HideInInspector]
	public static GameObject _Ggo;

	[NonSerialized]
	public static bool isCreated;

	[NonSerialized]
	public static bool MouseTrig;

	[NonSerialized]
	public static int DontEscTimer;

	private int mt_x;

	private int mt_y;

	private int mt_time;

	[NonSerialized]
	public static int AskSelected;

	[NonSerialized]
	public static string TalkText;

	[NonSerialized]
	public static string AskText;

	[NonSerialized]
	public static GameObject FaceObject;

	[NonSerialized]
	public static int FaceDir;

	[NonSerialized]
	public static int FacePos;

	[NonSerialized]
	public static GameObject TalkWindow;

	[NonSerialized]
	public static string[] ASKS;

	[NonSerialized]
	public static bool StopTalkText;

	[NonSerialized]
	public static GameObject YesNoObject;

	[NonSerialized]
	public static int CatchTimer;

	[NonSerialized]
	public static float DangerAlpha;

	public Texture MouseImage;

	public Texture MouseUseImage;

	public Texture MouseFarImage;

	public Texture MouseGoldImage;

	public Texture MouseTalkImage;

	public Texture MouseLockImage;

	[NonSerialized]
	public static string MouseMode;

	private Texture oldCursor;

	[NonSerialized]
	public static int QUAKE;

	[NonSerialized]
	public static float FACTOR;

	[NonSerialized]
	public static float CAM_Y;

	[NonSerialized]
	public static Color StartColor = new Color(1f, 1f, 1f, 1f);

	[NonSerialized]
	public static int PLUSTIME;

	private float lastTime;

	[NonSerialized]
	public static float RealTime;

	[NonSerialized]
	public static GameObject sfxObj;

	public Global()
	{
		ToolsMAX = 30;
	}

	public static void InitNewHashTable()
	{
		Var["hard"] = 0;
		Var["statue"] = 0;
		Var["survive"] = 0;
		Var["sleep"] = 0;
		Var["food"] = 0;
		Var["water"] = 0;
		Var["mind"] = 0;
		Var["sick"] = 0;
		Var["poison"] = 0;
		Var["dash"] = 0;
		Var["power"] = 10;
		Var["defense"] = 10;
		Var["crit"] = 0;
		Var["CampFarm"] = 0;
		Var["CampCraft"] = 0;
		Var["CampTent"] = 0;
		Var["CampBag"] = 0;
		Var["CampWall1"] = 0;
		Var["CampWall2"] = 0;
		Var["CampWall3"] = 0;
		Var["Wardrobe"] = 0;
		Var["Clock"] = 0;
		Var["Body"] = 1;
		Var["Hat"] = 0;
		Var["Mask"] = 0;
		Var["Sock"] = 0;
		Var["Skill"] = 0;
		Var["Weapon1"] = 0;
		Var["Weapon2"] = 0;
		Var["Cloth1"] = 0;
		HashNames = null;
		HashNames = new string[Var.Count];
		int num = 0;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Var);
		while (enumerator.MoveNext())
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
			string[] hashNames = HashNames;
			int num2 = num;
			object obj = dictionaryEntry.Key;
			if (!(obj is string))
			{
				obj = RuntimeServices.Coerce(obj, typeof(string));
			}
			hashNames[num2] = (string)obj;
			UnityRuntimeServices.Update(enumerator, dictionaryEntry);
			num++;
		}
	}

	public virtual void Awake()
	{
		NoLiteMat = GlobalNoLiteMat;
		OldAmbient = RenderSettings.ambientLight;
		Cursor.visible = false;
		if (isCreated && !RuntimeServices.ToBool(ISFIRST))
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		else if (!RuntimeServices.ToBool(ISFIRST))
		{
			isCreated = true;
			Oxygen = MaxOxygen;
			ToolsMax = ToolsMAX;
			Tools = new string[ToolsMax + 1];
			ToolsCount = new int[ToolsMax + 1];
			MenuText = string.Empty;
			Difficulty = -2;
			SFXBuy = sfxBuy;
			SFXNo = sfxNo;
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			{
				NoMouse = true;
				MobilePlatform = true;
			}
			InitNewHashTable();
			ISFIRST = true;
			DefaultSKIN = newSkin;
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			GlobalObject = gameObject;
		}
	}

	public virtual void Start()
	{
	}

	public virtual void MouseTrigCheck()
	{
		MouseTrig = true;
	}

	public static void SetPauseMenu(string name)
	{
		Time.timeScale = 0f;
		pauseAfterTime = 50;
		CreateMenuWindow(name);
		Input.ResetInputAxes();
		HUD_ON = false;
		Pause = true;
		OldPause = Pause;
	}

	public virtual void Update()
	{
		deltaTime = Time.unscaledDeltaTime;
		if (!(deltaTime <= 0.025f))
		{
			deltaTime = 0.025f;
		}
		MouseTrigCheck();
		GlobalTimer();
		if (!MenuPause)
		{
			if (OldPause != Pause && (bool)pauseTouchPad)
			{
				if (!OldPause)
				{
					if (MenuWindow == null)
					{
						CreateMenuWindow("MenuPause");
					}
				}
				else
				{
					BlockEscape = false;
					Time.timeScale = 1f;
					Input.ResetInputAxes();
					if ((bool)MenuWindow)
					{
						UnityEngine.Object.Destroy(MenuWindow);
					}
					HUD_ON = LevelCore.HUDToggle;
				}
			}
			OldPause = Pause;
			if (DontEscTimer <= 0)
			{
				if (Input.GetButtonUp("Escape") && !BlockEscape)
				{
					if (!Pause)
					{
						Pause = true;
					}
					else
					{
						Pause = false;
					}
				}
				if (!Pause && Input.GetButtonUp("Enter"))
				{
					Pause = true;
				}
			}
		}
		if (BlockMouseTime > 0)
		{
			BlockMouseTime--;
		}
		if (Pause && BlockMouseTime <= 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				GetMyTarget();
			}
			if (Input.GetMouseButtonDown(1))
			{
				GetMyTarget();
			}
			if (Input.GetMouseButtonDown(2))
			{
				GetMyTarget();
			}
		}
	}

	public static void CreateMenuWindow(string name)
	{
		BlockEscape = false;
		pauseAfterTime = 50;
		Time.timeScale = 0f;
		if (MenuWindow == null)
		{
		}
		offBlockTimer = 5;
		HUD_ON = false;
		Pause = true;
		OldPause = Pause;
		Input.ResetInputAxes();
		if ((bool)MenuWindow)
		{
			MonoBehaviour.print("menuwindow: " + MenuWindow.name);
			UnityEngine.Object.Destroy(MenuWindow);
		}
		MenuWindow = UnityEngine.Object.Instantiate(LoadData.HUD(name), new Vector3(0f, 0f, 3.5f), Quaternion.identity) as GameObject;
		MenuWindow.transform.parent = Camera.main.transform;
		MenuWindow.transform.localPosition = new Vector3(0f, 0f, 3.5f + (13f - Camera.main.fieldOfView) * 0.225f);
	}

	public static void CreateMenuWindowObj(GameObject obj)
	{
		BlockEscape = false;
		pauseAfterTime = 50;
		Time.timeScale = 0f;
		if (MenuWindow == null)
		{
		}
		offBlockTimer = 5;
		HUD_ON = false;
		Pause = true;
		OldPause = Pause;
		Input.ResetInputAxes();
		if ((bool)MenuWindow)
		{
			UnityEngine.Object.Destroy(MenuWindow);
		}
		MenuWindow = UnityEngine.Object.Instantiate(obj, new Vector3(0f, 0f, 3.5f), Quaternion.identity) as GameObject;
		MenuWindow.transform.parent = Camera.main.transform;
		MenuWindow.transform.localPosition = new Vector3(0f, 0f, 3.5f);
	}

	public static void CreateTalkWindow()
	{
		Input.ResetInputAxes();
		if ((bool)TalkWindow)
		{
			UnityEngine.Object.Destroy(TalkWindow);
		}
		if (!string.IsNullOrEmpty(AskText))
		{
			ASKS = AskText.Split("/"[0]);
		}
		else
		{
			ASKS = null;
		}
		TalkWindow = UnityEngine.Object.Instantiate(LoadData.HUD("Talk"), new Vector3(0f, 0f, 4.5f), Quaternion.identity) as GameObject;
		TalkWindow.transform.parent = Camera.main.transform;
		TalkWindow.transform.localPosition = new Vector3(0f, 0f, 4.5f);
	}

	public static void YesNo(string name, string command, int id)
	{
		YesNoCOMMAND = command;
		YesNoID = id;
		if ((bool)YesNoWindow)
		{
			UnityEngine.Object.Destroy(YesNoWindow);
		}
		YesNoWindow = UnityEngine.Object.Instantiate(LoadData.HUD("MenuYesNo"), new Vector3(0f, 0f, 2.5f), Quaternion.identity) as GameObject;
		YesNoWindow.transform.parent = Camera.main.transform;
		YesNoWindow.transform.localPosition = new Vector3(0f, 0f, 2.5f);
		YesNoWindow.BroadcastMessage("Rename", name, SendMessageOptions.DontRequireReceiver);
	}

	public static void DOYES(bool yes)
	{
		if (yes)
		{
			switch (YesNoCOMMAND)
			{
			case "delete":
				DELETE_SAVE(YesNoID);
				RemoveAllOjects();
				Application.LoadLevel("players");
				break;
			case "quit":
				Application.Quit();
				break;
			case "exit":
				RemoveAllOjects();
				Application.LoadLevel("main menu");
				break;
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (Pause)
		{
			return;
		}
		if (offBlockTimer > 0)
		{
			offBlockTimer--;
		}
		if (!(MP >= MaxMP))
		{
			MP += 0.004f * MaxMP;
			if (!(MP <= MaxMP))
			{
				MP = MaxMP;
			}
		}
		float num = GetDifficulty() * -2f;
		if (!(MP >= num))
		{
			MP = num;
		}
		if (CatchTimer > 0)
		{
			CatchTimer--;
		}
		if ((bool)ToPlay)
		{
			AudioSource.PlayClipAtPoint(ToPlay, transform.position);
			ToPlay = null;
		}
		DAYMINUTES++;
		if (pauseAfterTime > 0)
		{
			pauseAfterTime--;
		}
		if (pauseTouchPad == null)
		{
			CheckpauseTouchPad();
		}
		if (TimerNoVehicle > 0)
		{
			TimerNoVehicle--;
		}
		if (BlockControl > 0)
		{
			BlockControl--;
			if (!ResetThatTime)
			{
				ResetThatTime = true;
			}
			if (BlockControl == 0)
			{
				ResetThatTime = false;
			}
		}
		if (BreakGlassTimer > 0)
		{
			BreakGlassTimer--;
		}
		QuakeControl();
		if (EnemyHitTimer > 0)
		{
			EnemyHitTimer--;
		}
		if (InvincibleTimer > 0)
		{
			InvincibleTimer--;
		}
		if (RuntimeServices.ToBool(Reload) && FadeMode == 0)
		{
			if (!(HP > 0f))
			{
				HP = MaxHP;
				VehicleName = string.Empty;
			}
			Reload = false;
			if (NextLevel != string.Empty)
			{
				RemoveAllOjects();
				Application.LoadLevel(NextLevel);
			}
			else
			{
				RemoveAllOjects();
				Application.LoadLevel(Application.loadedLevelName);
			}
			NextLevel = string.Empty;
		}
		CheckExperience();
		if (WakeUpTime > 0)
		{
			WakeUpTime--;
		}
		if (timerUnload > 0)
		{
			timerUnload--;
		}
	}

	public static void RemoveAllOjects()
	{
		Time.timeScale = 1f;
		Pause = false;
		OldPause = false;
		MenuPause = false;
		BlockEscape = false;
		TalkPause.menu = 0;
		if ((bool)MenuWindow)
		{
			UnityEngine.Object.Destroy(MenuWindow);
			MenuWindow = null;
		}
		if ((bool)TalkWindow)
		{
			UnityEngine.Object.Destroy(TalkWindow);
			TalkWindow = null;
		}
		if ((bool)YesNoWindow)
		{
			UnityEngine.Object.Destroy(YesNoWindow);
			YesNoWindow = null;
		}
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (!(array2[i] == null) && array2[i] != GlobalObject)
			{
				UnityEngine.Object.Destroy(array2[i]);
			}
		}
	}

	public virtual void GetMyTarget()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 1000f))
		{
			hitInfo.transform.SendMessage("ActivateByMouse", null, SendMessageOptions.DontRequireReceiver);
			LastClickObject = hitInfo.transform.gameObject;
			MonoBehaviour.print("LastClickObject: " + LastClickObject);
		}
	}

	public virtual void OnGUI()
	{
		if (!MouseTrig)
		{
			return;
		}
		Texture texture = MouseUseImage;
		switch (MouseMode)
		{
		case "zero":
			texture = MouseImage;
			break;
		case "gold":
			texture = MouseGoldImage;
			break;
		case "talk":
			texture = MouseTalkImage;
			break;
		case "lock":
			texture = MouseLockImage;
			Monitor.MouseNo = false;
			break;
		}
		if (!Monitor.DontDrop)
		{
			texture = MouseImage;
		}
		else if (!Monitor.ForceNo)
		{
			if (!Monitor.dist && Monitor.MouseNo)
			{
				texture = MouseFarImage;
			}
		}
		else
		{
			texture = MouseFarImage;
		}
		if (oldCursor != texture)
		{
			oldCursor = texture;
			Cursor.SetCursor((Texture2D)texture, new Vector2(0f, 0f), CursorMode.Auto);
		}
	}

	public static void LoadLEVEL(string name, string id)
	{
		Time.timeScale = 1f;
		Pause = false;
		OldPause = false;
		MenuPause = false;
		BlockEscape = false;
		TalkPause.menu = 0;
		if ((bool)MenuWindow)
		{
			UnityEngine.Object.Destroy(MenuWindow);
			MenuWindow = null;
		}
		if ((bool)TalkWindow)
		{
			UnityEngine.Object.Destroy(TalkWindow);
			TalkWindow = null;
		}
		if ((bool)YesNoWindow)
		{
			UnityEngine.Object.Destroy(YesNoWindow);
			YesNoWindow = null;
		}
		Input.ResetInputAxes();
		ToFindCheckPointXYZ = true;
		SlotItem.EscapeItem = true;
		NextLevel = name;
		Reload = true;
		FadeMode = 1;
	}

	public virtual void CheckpauseTouchPad()
	{
		GameObject gameObject = GameObject.Find("PauseJoystick");
		if ((bool)gameObject)
		{
			pauseTouchPad = (Joystick)gameObject.GetComponent(typeof(Joystick));
		}
	}

	public static bool GetThatTool(string _NameParameter, int _Parameter, bool _JustBulletsParameter)
	{
		MonoBehaviour.print("get that tool");
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < ToolsMax; i++)
		{
			if (Tools[i] == _NameParameter)
			{
				num = i;
			}
			if (num2 == -1 && (Tools[i] == string.Empty || Tools[i] == null))
			{
				num2 = i;
			}
		}
		int result;
		if (num > -1)
		{
			if (_Parameter != 0)
			{
				ToolsCount[num] += _Parameter;
				if (ToolsCount[num] > 100)
				{
					ToolsCount[num] = 100;
				}
				result = 1;
			}
			else
			{
				result = 1;
			}
		}
		else if (_JustBulletsParameter)
		{
			result = 0;
		}
		else if (num2 > -1)
		{
			Tools[num2] = _NameParameter;
			ToolsCount[num2] = _Parameter;
			result = 1;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static void GetThatCloth(string _NameParameter)
	{
		int num = -1;
		for (int i = 0; i <= 14; i++)
		{
			if (Cloths[i] == _NameParameter)
			{
				return;
			}
			if (num == -1 && (Cloths[i] == string.Empty || Cloths[i] == null))
			{
				num = i;
			}
		}
		if (num > -1)
		{
			Cloths[num] = _NameParameter;
		}
	}

	public static int CheckThatCloth(string _NameParameter)
	{
		int num = 0;
		int result;
		while (true)
		{
			if (num <= 14)
			{
				if (Cloths[num] == _NameParameter)
				{
					result = num;
					break;
				}
				num++;
				continue;
			}
			result = 0;
			break;
		}
		return result;
	}

	public static bool CheckPlace(string text)
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Place);
		int result;
		while (true)
		{
			if (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is string))
				{
					obj = RuntimeServices.Coerce(obj, typeof(string));
				}
				string text2 = (string)obj;
				if (text2 == text)
				{
					result = 1;
					break;
				}
				continue;
			}
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public static bool AddPlace(string text)
	{
		int result;
		if (!(text == string.Empty) && !(text == null))
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Place);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (!(obj is string))
					{
						obj = RuntimeServices.Coerce(obj, typeof(string));
					}
					string text2 = (string)obj;
					if (text2 == text)
					{
						result = 0;
						break;
					}
					continue;
				}
				Place.Add(text);
				result = 1;
				break;
			}
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static bool CheckQuest(string text)
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Quest);
		int result;
		while (true)
		{
			if (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is string))
				{
					obj = RuntimeServices.Coerce(obj, typeof(string));
				}
				string text2 = (string)obj;
				if (text2 == text)
				{
					result = 1;
					break;
				}
				continue;
			}
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public static bool AddQuest(string text)
	{
		int result;
		if (!(text == string.Empty) && !(text == null))
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Quest);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (!(obj is string))
					{
						obj = RuntimeServices.Coerce(obj, typeof(string));
					}
					string text2 = (string)obj;
					if (text2 == text)
					{
						result = 0;
						break;
					}
					continue;
				}
				Quest.Add(text);
				result = 1;
				break;
			}
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static bool RemoveQuest(string text)
	{
		int result;
		if (!(text == string.Empty) && !(text == null))
		{
			int num = 0;
			while (true)
			{
				if (num < Quest.length)
				{
					if (RuntimeServices.EqualityOperator(Quest[num], text))
					{
						Quest.RemoveAt(num);
						result = 1;
						break;
					}
					num++;
					continue;
				}
				MonoBehaviour.print("There's NO QUEST: " + text);
				result = 0;
				break;
			}
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static bool CheckStuff(string text)
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Stuff);
		int result;
		while (true)
		{
			if (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is string))
				{
					obj = RuntimeServices.Coerce(obj, typeof(string));
				}
				string text2 = (string)obj;
				if (text2 == text)
				{
					result = 1;
					break;
				}
				continue;
			}
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public static bool AddStuff(string text)
	{
		int result;
		if (!(text == string.Empty) && !(text == null))
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Stuff);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (!(obj is string))
					{
						obj = RuntimeServices.Coerce(obj, typeof(string));
					}
					string text2 = (string)obj;
					if (text2 == text)
					{
						result = 0;
						break;
					}
					continue;
				}
				Stuff.Add(text);
				result = 1;
				break;
			}
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static bool RemoveGeneratingStuff()
	{
		int num = 0;
		int num2 = 0;
		for (int i = default(int); i < Stuff.length; i++)
		{
			num++;
			if ((Stuff[i] as string).Substring(0, 1) == "*")
			{
				Stuff.RemoveAt(i);
				num2++;
			}
		}
		MonoBehaviour.print("All Stuff Objects: " + num + "  Generating Objects: " + num2);
		return true;
	}

	public static void SAVE()
	{
		MonoBehaviour.print("SAVE!!!");
		if (PLAYER == 0)
		{
			PLAYER = 1;
		}
		string text = "Player" + PLAYER;
		PlayerPrefs.SetInt(text, 1);
		PlayerPrefs.SetString(text + "Vehicle", VehicleName);
		PlayerPrefs.SetInt(text + "Experience", Experience);
		PlayerPrefs.SetInt(text + "RANG", RANG);
		PlayerPrefs.SetInt(text + "MP", (int)MP);
		PlayerPrefs.SetInt(text + "MaxMP", (int)MaxMP);
		PlayerPrefs.SetInt(text + "HP", (int)HP);
		PlayerPrefs.SetInt(text + "MaxHP", (int)MaxHP);
		PlayerPrefs.SetInt(text + "BonusHP", (int)BonusHP);
		PlayerPrefs.SetInt(text + "BonusMP", (int)BonusMP);
		PlayerPrefs.SetInt(text + "BonusManaSpeed", (int)BonusManaSpeed);
		PlayerPrefs.SetInt(text + "BonusDiscont", (int)BonusDiscont);
		PlayerPrefs.SetString(text + "LEVEL", LEVEL);
		PlayerPrefs.SetInt(text + "Tool", CurrentToolNumber);
		PlayerPrefs.SetInt(text + "Cloth", InTheCloth);
		PlayerPrefs.SetInt(text + "HatHero", HatHero);
		PlayerPrefs.SetInt(text + "HairHero", HairHero);
		PlayerPrefs.SetFloat(text + "HairColorR", HairColor.r);
		PlayerPrefs.SetFloat(text + "HairColorG", HairColor.g);
		PlayerPrefs.SetFloat(text + "HairColorB", HairColor.b);
		PlayerPrefs.SetString(text + "Mission", CurrentMission);
		PlayerPrefs.SetInt(text + "Gold", Gold);
		PlayerPrefs.SetInt(text + "Guardian", Guardian);
		PlayerPrefs.SetString(text + "Check", CheckPointName);
		PlayerPrefs.SetString(text + "GoldLevel", GoldLevel);
		PlayerPrefs.SetInt(text + "GoldPositionX", (int)GoldPosition.x);
		PlayerPrefs.SetInt(text + "GoldPositionY", (int)GoldPosition.y);
		PlayerPrefs.SetInt(text + "GoldCount", GoldCount);
		PlayerPrefs.SetInt(text + "DAYS", DAYS);
		PlayerPrefs.SetInt(text + "DAYTIME", DAYTIME);
		PlayerPrefs.SetFloat(text + "DAYWIND", DAYWIND);
		PlayerPrefs.SetInt(text + "DAYRAIN", DAYRAIN);
		PlayerPrefs.SetFloat(text + "DAYCLOUD", DAYCLOUD);
		PlayerPrefs.SetFloat(text + "DAYRAINBOW", DAYRAINBOW);
		PlayerPrefs.SetInt(text + "Difficulty", Difficulty);
		PlayerPrefs.SetInt(text + "LevelSeed", LevelSeed);
		PlayerPrefs.SetString(text + "TextQuest", TextQuest);
		SetBool(text + "DoubleJump", DoubleJump);
		SetBool(text + "FonarSkill", FonarSkill);
		SetBool(text + "SwimFast", SwimFast);
		PlayerPrefs.SetInt(text + "SOUND", SOUND);
		PlayerPrefs.SetInt(text + "MUSIC", MUSIC);
		PlayerPrefs.SetInt(text + "QUALITY", QUALITY);
		int num = default(int);
		for (num = 0; num < ToolsMax; num++)
		{
			PlayerPrefs.SetString(text + "T" + num, Tools[num]);
			PlayerPrefs.SetInt(text + "TC" + num, ToolsCount[num]);
		}
		for (num = 0; num <= 14; num++)
		{
			PlayerPrefs.SetString(text + "C" + num, Cloths[num]);
		}
		PlayerPrefs.SetInt(text + "BuildCastleLevel", BuildCastleLevel);
		for (num = 0; num <= 20; num++)
		{
			PlayerPrefs.SetInt(text + "R" + num, Upgrades[num]);
		}
		int length = Place.length;
		PlayerPrefs.SetInt(text + "countP", length);
		if (length > 0)
		{
			for (num = 0; num < Place.length; num++)
			{
				string key = text + "P" + num;
				object obj = Place[num];
				if (!(obj is string))
				{
					obj = RuntimeServices.Coerce(obj, typeof(string));
				}
				PlayerPrefs.SetString(key, (string)obj);
			}
		}
		for (num = length; num <= 10000 && PlayerPrefs.HasKey(text + "P" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "P" + num);
		}
		length = Quest.length;
		PlayerPrefs.SetInt(text + "countQ", length);
		if (length > 0)
		{
			for (num = 0; num < Quest.length; num++)
			{
				string key2 = text + "Q" + num;
				object obj2 = Quest[num];
				if (!(obj2 is string))
				{
					obj2 = RuntimeServices.Coerce(obj2, typeof(string));
				}
				PlayerPrefs.SetString(key2, (string)obj2);
			}
		}
		for (num = length; num <= 10000 && PlayerPrefs.HasKey(text + "Q" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "Q" + num);
		}
		length = Stuff.length;
		PlayerPrefs.SetInt(text + "countS", length);
		if (length > 0)
		{
			for (num = 0; num < Stuff.length; num++)
			{
				string key3 = text + "S" + num;
				object obj3 = Stuff[num];
				if (!(obj3 is string))
				{
					obj3 = RuntimeServices.Coerce(obj3, typeof(string));
				}
				PlayerPrefs.SetString(key3, (string)obj3);
			}
		}
		for (num = length; num <= 10000 && PlayerPrefs.HasKey(text + "S" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "S" + num);
		}
		for (num = 0; num < Extensions.get_length((System.Array)Achiev.Award); num++)
		{
			PlayerPrefs.SetInt(text + "A" + num, Achiev.Award[num].have);
		}
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(Var.Keys);
		while (enumerator.MoveNext())
		{
			object obj4 = enumerator.Current;
			if (!(obj4 is string))
			{
				obj4 = RuntimeServices.Coerce(obj4, typeof(string));
			}
			string text2 = (string)obj4;
			PlayerPrefs.SetInt(text + "V" + text2, RuntimeServices.UnboxInt32(Var[text2]));
			UnityRuntimeServices.Update(enumerator, text2);
		}
	}

	public static void LOAD()
	{
		MonoBehaviour.print("LOAD!!! " + PLAYER);
		DAYMINUTES = 0;
		if (PLAYER == 0)
		{
			PLAYER = 1;
		}
		string text = "Player" + PLAYER;
		PlayerPrefs.SetInt("Last Player", PLAYER);
		if (!PlayerPrefs.HasKey(text))
		{
			MonoBehaviour.print("WRONG SAVE!");
			DefaultData();
			SAVE();
			return;
		}
		VehicleName = PlayerPrefs.GetString(text + "Vehicle");
		Experience = PlayerPrefs.GetInt(text + "Experience");
		RANG = PlayerPrefs.GetInt(text + "RANG");
		MP = PlayerPrefs.GetInt(text + "MP");
		MaxMP = PlayerPrefs.GetInt(text + "MaxMP");
		HP = PlayerPrefs.GetInt(text + "HP");
		if (!(HP > 0f))
		{
			HP = 0.5f;
		}
		MaxHP = PlayerPrefs.GetInt(text + "MaxHP");
		LEVEL = PlayerPrefs.GetString(text + "LEVEL");
		BonusHP = PlayerPrefs.GetInt(text + "BonusHP");
		BonusMP = PlayerPrefs.GetInt(text + "BonusMP");
		BonusManaSpeed = PlayerPrefs.GetInt(text + "BonusManaSpeed");
		BonusDiscont = PlayerPrefs.GetInt(text + "BonusDiscont");
		CurrentToolNumber = PlayerPrefs.GetInt(text + "Tool");
		InTheCloth = PlayerPrefs.GetInt(text + "Cloth");
		HatHero = PlayerPrefs.GetInt(text + "HatHero");
		HairHero = PlayerPrefs.GetInt(text + "HairHero");
		HairColor.r = PlayerPrefs.GetFloat(text + "HairColorR");
		HairColor.g = PlayerPrefs.GetFloat(text + "HairColorG");
		HairColor.b = PlayerPrefs.GetFloat(text + "HairColorB");
		CurrentMission = PlayerPrefs.GetString(text + "Mission");
		Gold = PlayerPrefs.GetInt(text + "Gold");
		Guardian = PlayerPrefs.GetInt(text + "Guardian");
		CheckPointName = PlayerPrefs.GetString(text + "Check");
		GoldLevel = PlayerPrefs.GetString(text + "GoldLevel");
		GoldPosition.x = PlayerPrefs.GetInt(text + "GoldPositionX");
		GoldPosition.y = PlayerPrefs.GetInt(text + "GoldPositionY");
		GoldCount = PlayerPrefs.GetInt(text + "GoldCount");
		DAYS = PlayerPrefs.GetInt(text + "DAYS");
		DAYTIME = PlayerPrefs.GetInt(text + "DAYTIME");
		DAYWIND = PlayerPrefs.GetFloat(text + "DAYWIND");
		DAYRAIN = PlayerPrefs.GetInt(text + "DAYRAIN");
		DAYCLOUD = PlayerPrefs.GetFloat(text + "DAYCLOUD");
		DAYRAINBOW = PlayerPrefs.GetFloat(text + "DAYRAINBOW");
		Difficulty = PlayerPrefs.GetInt(text + "Difficulty");
		LevelSeed = PlayerPrefs.GetInt(text + "LevelSeed");
		CanSeed = false;
		TextQuest = PlayerPrefs.GetString(text + "TextQuest");
		DoubleJump = GetBool(text + "DoubleJump");
		FonarSkill = GetBool(text + "FonarSkill");
		SwimFast = GetBool(text + "SwimFast");
		SOUND = PlayerPrefs.GetInt(text + "SOUND");
		MUSIC = PlayerPrefs.GetInt(text + "MUSIC");
		QUALITY = PlayerPrefs.GetInt(text + "QUALITY");
		QualitySettings.SetQualityLevel(QUALITY, true);
		ResetAllMassives();
		int num = default(int);
		int num2 = 1;
		for (num = 0; num < ToolsMax; num++)
		{
			Tools[num] = PlayerPrefs.GetString(text + "T" + num);
			ToolsCount[num] = PlayerPrefs.GetInt(text + "TC" + num);
		}
		for (num = 0; num <= 14; num++)
		{
			Cloths[num] = PlayerPrefs.GetString(text + "C" + num);
		}
		BuildCastleLevel = PlayerPrefs.GetInt(text + "BuildCastleLevel");
		for (num = 0; num <= 20; num++)
		{
			Upgrades[num] = PlayerPrefs.GetInt(text + "R" + num);
		}
		int num3 = PlayerPrefs.GetInt(text + "countP");
		if (num3 > 0)
		{
			for (num = 0; num < num3; num++)
			{
				Place.Add(PlayerPrefs.GetString(text + "P" + num));
			}
		}
		for (num = num3; num <= 10000 && PlayerPrefs.HasKey(text + "P" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "P" + num);
		}
		num3 = PlayerPrefs.GetInt(text + "countQ");
		if (num3 > 0)
		{
			for (num = 0; num < num3; num++)
			{
				Quest.Add(PlayerPrefs.GetString(text + "Q" + num));
			}
		}
		for (num = num3; num <= 10000 && PlayerPrefs.HasKey(text + "Q" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "Q" + num);
		}
		num3 = PlayerPrefs.GetInt(text + "countS");
		if (num3 > 0)
		{
			for (num = 0; num < num3; num++)
			{
				Stuff.Add(PlayerPrefs.GetString(text + "S" + num));
			}
		}
		for (num = num3; num <= 10000 && PlayerPrefs.HasKey(text + "S" + num); num++)
		{
			PlayerPrefs.DeleteKey(text + "S" + num);
		}
		for (num = 0; num < Extensions.get_length((System.Array)Achiev.Award); num++)
		{
			Achiev.Award[num].have = PlayerPrefs.GetInt(text + "A" + num);
		}
		Var = null;
		Var = new Hashtable();
		int i = 0;
		string[] hashNames = HashNames;
		for (int length = hashNames.Length; i < length; i++)
		{
			Var[hashNames[i]] = PlayerPrefs.GetInt(text + "V" + hashNames[i]);
		}
	}

	public static void ResetAllMassives()
	{
		InitNewHashTable();
		for (int i = 0; i < Extensions.get_length((System.Array)Achiev.Award); i++)
		{
			Achiev.Award[i].have = 0;
		}
		GameMessage = new UnityScript.Lang.Array();
		Upgrades = new int[21];
		Tools = new string[ToolsMax + 1];
		ToolsCount = new int[ToolsMax + 1];
		Cloths = new string[15];
		Place.Clear();
		Quest.Clear();
		Stuff.Clear();
	}

	public static void SetBool(string name, bool value)
	{
		PlayerPrefs.SetInt(name, value ? 1 : 0);
	}

	public static bool GetBool(string name)
	{
		return PlayerPrefs.GetInt(name) == 1;
	}

	public static void DefaultData()
	{
		VehicleName = string.Empty;
		Experience = 0;
		RANG = 1;
		HP = 10f;
		MaxHP = 10f;
		MP = 10f;
		MaxMP = 10f;
		BonusHP = 0f;
		BonusMP = 0f;
		Difficulty = -2;
		BonusManaSpeed = 1f;
		BonusDiscont = 0f;
		LEVEL = "gala";
		CurrentToolNumber = 0;
		InTheCloth = 0;
		HairColor = new Color(1f, 1f, 0f, 1f);
		HairHero = 1;
		HatHero = 0;
		CanSeed = true;
		TextQuest = string.Empty;
		CurrentMission = "start";
		Gold = 0;
		Guardian = 0;
		CheckPointName = string.Empty;
		CheckPointNameTemp = string.Empty;
		BuildCastleLevel = 0;
		DAYS = 0;
		DAYTIME = 2;
		DoubleJump = false;
		FonarSkill = false;
		SwimFast = false;
		SOUND = 1;
		MUSIC = 1;
		QUALITY = 0;
		QualitySettings.SetQualityLevel(QUALITY, true);
		ResetAllMassives();
	}

	public static void DELETE_SAVE(int num)
	{
		string text = "Player" + num;
		PlayerPrefs.DeleteKey(text);
		PlayerPrefs.DeleteKey(text + "Vehicle");
		PlayerPrefs.DeleteKey(text + "Experience");
		PlayerPrefs.DeleteKey(text + "RANG");
		PlayerPrefs.DeleteKey(text + "HP");
		PlayerPrefs.DeleteKey(text + "MaxHP");
		PlayerPrefs.DeleteKey(text + "MP");
		PlayerPrefs.DeleteKey(text + "MaxMP");
		PlayerPrefs.DeleteKey(text + "BonusHP");
		PlayerPrefs.DeleteKey(text + "BonusMP");
		PlayerPrefs.DeleteKey(text + "BonusManaSpeed");
		PlayerPrefs.DeleteKey(text + "BonusDiscont");
		PlayerPrefs.DeleteKey(text + "LEVEL");
		PlayerPrefs.DeleteKey(text + "Tool");
		PlayerPrefs.DeleteKey(text + "Hand");
		PlayerPrefs.DeleteKey(text + "Cloth");
		PlayerPrefs.DeleteKey(text + "Mission");
		PlayerPrefs.DeleteKey(text + "Gold");
		PlayerPrefs.DeleteKey(text + "Guardian");
		PlayerPrefs.DeleteKey(text + "Check");
		PlayerPrefs.DeleteKey(text + "GoldLevel");
		PlayerPrefs.DeleteKey(text + "GoldPositionX");
		PlayerPrefs.DeleteKey(text + "GoldPositionY");
		PlayerPrefs.DeleteKey(text + "GoldCount");
		PlayerPrefs.DeleteKey(text + "DAYS");
		PlayerPrefs.DeleteKey(text + "DAYTIME");
		PlayerPrefs.DeleteKey(text + "DAYWIND");
		PlayerPrefs.DeleteKey(text + "DAYRAIN");
		PlayerPrefs.DeleteKey(text + "DAYCLOUD");
		PlayerPrefs.DeleteKey(text + "Difficulty");
		PlayerPrefs.DeleteKey(text + "HatHero");
		PlayerPrefs.DeleteKey(text + "HairHero");
		PlayerPrefs.DeleteKey(text + "HairColorR");
		PlayerPrefs.DeleteKey(text + "HairColorG");
		PlayerPrefs.DeleteKey(text + "HairColorB");
		PlayerPrefs.DeleteKey(text + "TextQuest");
		PlayerPrefs.DeleteKey(text + "DoubleJump");
		PlayerPrefs.DeleteKey(text + "FonarSkill");
		PlayerPrefs.DeleteKey(text + "SwimFast");
		PlayerPrefs.DeleteKey(text + "SOUND");
		PlayerPrefs.DeleteKey(text + "MUSIC");
		PlayerPrefs.DeleteKey(text + "QUALITY");
		int num2 = default(int);
		PlayerPrefs.DeleteKey(text + "BuildCastleLevel");
		for (num2 = 0; num2 <= 20; num2++)
		{
			PlayerPrefs.DeleteKey(text + "R" + num2);
		}
		for (num2 = 0; num2 < ToolsMax; num2++)
		{
			PlayerPrefs.DeleteKey(text + "T" + num2);
			PlayerPrefs.DeleteKey(text + "TC" + num2);
		}
		for (num2 = 0; num2 <= 14; num2++)
		{
			PlayerPrefs.DeleteKey(text + "C" + num2);
		}
		PlayerPrefs.DeleteKey(text + "countP");
		for (num2 = 0; num2 <= 10000 && PlayerPrefs.HasKey(text + "P" + num2); num2++)
		{
			PlayerPrefs.DeleteKey(text + "P" + num2);
		}
		PlayerPrefs.DeleteKey(text + "countQ");
		for (num2 = 0; num2 <= 10000 && PlayerPrefs.HasKey(text + "Q" + num2); num2++)
		{
			PlayerPrefs.DeleteKey(text + "Q" + num2);
		}
		PlayerPrefs.DeleteKey(text + "countS");
		for (num2 = 0; num2 <= 10000 && PlayerPrefs.HasKey(text + "S" + num2); num2++)
		{
			PlayerPrefs.DeleteKey(text + "S" + num2);
		}
		PlayerPrefs.DeleteKey(text + "countA");
		for (num2 = 0; num2 <= 10000 && PlayerPrefs.HasKey(text + "A" + num2); num2++)
		{
			PlayerPrefs.DeleteKey(text + "A" + num2);
		}
	}

	public virtual void QuakeControl()
	{
		if (QUAKE > 0)
		{
			QUAKE--;
			CAM_Y = Mathf.Sin(Time.time * FACTOR * 0.8f) * (float)QUAKE * 0.00075f;
		}
	}

	public static void QuakeStart(int q, float f)
	{
		QUAKE = q;
		FACTOR = f;
	}

	public static void GetAllTools()
	{
		LoadLEVEL("cheat", string.Empty);
	}

	public static void ChangeTime(int t, bool ControlWeather)
	{
		Vector4 vector = default(Vector4);
		int dAYTIME = DAYTIME;
		int dAYS = DAYS;
		DAYTIME = t;
		PLUSTIME = 0;
		if (DAYTIME > 23)
		{
			DAYTIME -= 24;
			DAYS++;
			PLUSTIME++;
		}
		else
		{
			PLUSTIME = DAYTIME - dAYTIME;
		}
		if (ShowWEATHER)
		{
			RenderSettings.fogEndDistance = 190f;
			RenderSettings.fogStartDistance = 30f;
			RenderSettings.fogColor = new Color(1f, 1f, 1f, 1f);
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogDensity = 0.005f;
			Color color = new Color(0.3f, 0.4f, 0.6f, 1f);
			switch (DAYTIME)
			{
			case 5:
				vector = new Color(0.4f, 0.5f, 0.6f, 1f);
				RenderSettings.fogColor = vector;
				Camera.main.backgroundColor = new Color(0.4f, 0.45f, 0.6f, 1f);
				break;
			case 6:
				vector = new Color(1f, 0.5f, 0.25f, 1f);
				RenderSettings.fogColor = vector;
				Camera.main.backgroundColor = new Color(1f, 0.55f, 0.5f, 1f);
				break;
			case 7:
				vector = new Color(0.65f, 0.55f, 0.5f, 1f);
				RenderSettings.fogColor = new Color(0.75f, 0.55f, 0.35f, 1f);
				Camera.main.backgroundColor = new Color(0.65f, 0.55f, 0.5f, 1f);
				break;
			case 8:
				vector = new Color(0.975f, 0.75f, 0.75f, 1f);
				RenderSettings.fogColor = new Color(0.75f, 0.75f, 1f, 1f);
				Camera.main.backgroundColor = new Color(0.6f, 0.6f, 0.75f, 1f);
				break;
			case 9:
				vector = new Color(1f, 0.9f, 0.9f, 1f);
				RenderSettings.fogColor = new Color(0.25f, 0.5f, 1f, 1f);
				RenderSettings.fogColor = vector * 0.5f;
				Camera.main.backgroundColor = new Color(0.7f, 0.7f, 0.9f, 1f);
				break;
			case 10:
				vector = new Color(1f, 0.95f, 0.95f, 1f);
				RenderSettings.fogColor = vector;
				RenderSettings.fogColor = vector * 0.5f;
				Camera.main.backgroundColor = new Color(0.5f, 0.64f, 0.95f, 1f);
				break;
			case 11:
				vector = new Color(1f, 0.975f, 1f, 1f);
				RenderSettings.fogColor = vector;
				RenderSettings.fogColor = vector * 0.5f;
				Camera.main.backgroundColor = new Color(0.5f, 0.64f, 0.95f, 1f);
				break;
			case 12:
			case 13:
			case 14:
				RenderSettings.fogColor = new Color(0.45f, 0.75f, 1f, 1f);
				vector = new Color(1f, 1f, 1f, 1f);
				Camera.main.backgroundColor = new Color(0.5f, 0.64f, 1f, 1f);
				break;
			case 15:
				vector = new Color(0.9f, 1f, 0.9f, 1f);
				Camera.main.backgroundColor = new Color(0.5f, 0.64f, 1f, 1f);
				break;
			case 16:
				vector = new Color(0.8f, 1f, 0.85f, 1f);
				Camera.main.backgroundColor = new Color(0.5f, 0.64f, 1f, 1f);
				break;
			case 17:
				vector = new Color(0.85f, 1f, 0.8f, 1f);
				Camera.main.backgroundColor = new Color(0.65f, 0.75f, 0.85f, 1f);
				break;
			case 18:
				vector = new Color(1f, 0.8f, 0.8f, 1f);
				Camera.main.backgroundColor = new Color(0.9f, 0.75f, 0.64f, 1f);
				break;
			case 19:
				vector = new Color(1f, 0.7f, 0.7f, 1f);
				Camera.main.backgroundColor = new Color(1f, 0.5f, 0.45f, 1f);
				break;
			case 20:
				vector = new Color(1f, 0.7f, 0.7f, 1f);
				Camera.main.backgroundColor = new Color(1f, 0.64f, 0.6f, 1f);
				break;
			case 21:
				vector = new Color(0.8f, 0.6f, 0.5f, 1f);
				RenderSettings.fogColor = vector;
				RenderSettings.fogColor = vector * 0.5f;
				Camera.main.backgroundColor = new Color(0.95f, 0.3f, 0.25f, 1f);
				break;
			default:
				vector = new Color(0.3f, 0.4f, 0.6f, 1f);
				RenderSettings.fogColor = vector;
				RenderSettings.fogColor = vector * 0.5f;
				Camera.main.backgroundColor = new Color(0.1f, 0.15f, 0.25f, 1f);
				break;
			}
			RenderSettings.fogEndDistance += UnityEngine.Random.Range(-5f, 5f);
			RenderSettings.fogColor += new Color(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), 1f);
			Camera.main.backgroundColor = Camera.main.backgroundColor + new Color(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), 1f);
			if (ControlWeather)
			{
				DAYWIND += UnityEngine.Random.Range(-2.75f, 2.75f);
			}
			if (!(DAYWIND <= 5f))
			{
				DAYWIND = 5f;
			}
			if (!(DAYWIND >= -5f))
			{
				DAYWIND = -5f;
			}
			if (ControlWeather)
			{
				DAYCLOUD += UnityEngine.Random.Range(-18f, 18f);
			}
			if (!(DAYCLOUD <= 35f))
			{
				DAYCLOUD = 35f;
			}
			if (!(DAYCLOUD >= -35f))
			{
				DAYCLOUD = -35f;
			}
			if (ControlWeather)
			{
				DAYRAIN += UnityEngine.Random.Range(-45, 45);
			}
			if (DAYRAIN > 100)
			{
				DAYRAIN = 100;
			}
			if (DAYRAIN < 0)
			{
				DAYRAIN = 0;
			}
			if (DAYRAIN > 0 && CheckRain())
			{
				float num = (float)DAYRAIN * 0.001f;
				RenderSettings.fogEndDistance -= (float)DAYRAIN * 0.15f;
				Camera.main.backgroundColor = Camera.main.backgroundColor - new Color(num, num, num, 0f);
				RenderSettings.fogColor -= new Color(num, num, num, 0f);
				vector -= (Vector4)new Color(num * 2f, num * 2f, num * 2f, 0f);
			}
			if (!(DAYCLOUD <= -2f) && !(DAYCLOUD >= 12f) && DAYRAIN > 64 && UnityEngine.Random.Range(0, 100) > 20)
			{
				RenderSettings.fogEndDistance *= 0.75f;
			}
			float num2 = (16f - Mathf.Abs(DAYCLOUD)) / 100f;
			if (!(num2 <= 0f))
			{
				vector -= new Vector4(num2, num2, num2, 0f);
			}
			vector += (Vector4)new Color(0.3f, 0.3f, 0.3f, 0f);
			if (!(vector.x <= 0.95f))
			{
				vector.x = 0.95f;
			}
			if (!(vector.y <= 0.95f))
			{
				vector.y = 0.95f;
			}
			if (!(vector.z <= 0.95f))
			{
				vector.z = 0.95f;
			}
			RenderSettings.ambientLight = vector;
		}
	}

	public virtual void CheckExperience()
	{
	}

	public static int NextLevelFormula(int ORang)
	{
		return 0;
	}

	public static void GetEnemyHit(Vector3 enhit, AI ai)
	{
		if (!(enhit.x > 0f))
		{
			EnemyHitTimer = 0;
			return;
		}
		EnemAI = ai;
		EnemyHitTimer = 150;
		EnemyHitHP = (int)enhit.x;
		EnemyHitMaxHP = (int)enhit.y;
		EnemyHitLevel = (int)enhit.z;
	}

	public static int BuildPrice()
	{
		return 7500 + BuildCastleLevel * 9400;
	}

	public static bool CheckRain()
	{
		return (DAYRAIN > 40 && !(DAYCLOUD < -6f) && !(DAYCLOUD >= 27f)) ? true : false;
	}

	public static bool CheckNight()
	{
		return (DAYTIME >= 22 || DAYTIME <= 5) ? true : false;
	}

	public virtual void GlobalTimer()
	{
		lastTime = Time.realtimeSinceStartup - lastTime;
		RealTime = lastTime;
		lastTime = Time.realtimeSinceStartup;
	}

	public static void GameMessageAwardCreate(object name)
	{
		UnityScript.Lang.Array gameMessage = GameMessage;
		string lhs = Lang.Award("Achievement") + ": /";
		object obj = name;
		if (!(obj is string))
		{
			obj = RuntimeServices.Coerce(obj, typeof(string));
		}
		gameMessage.Add(lhs + (Lang.Award((string)obj) as string));
	}

	public static void GameMessageBonusCreate(object name)
	{
		UnityScript.Lang.Array gameMessage = GameMessage;
		object obj = name;
		if (!(obj is string))
		{
			obj = RuntimeServices.Coerce(obj, typeof(string));
		}
		gameMessage.Add(Lang.Award((string)obj) as string);
	}

	public static void GameMessageCreate(object name)
	{
		UnityScript.Lang.Array gameMessage = GameMessage;
		object obj = name;
		if (!(obj is string))
		{
			obj = RuntimeServices.Coerce(obj, typeof(string));
		}
		gameMessage.Add(Lang.Menu((string)obj) as string);
	}

	public static void GameMessageCreateOne(object name)
	{
		if (GameMessage.length <= 0)
		{
			UnityScript.Lang.Array gameMessage = GameMessage;
			object obj = name;
			if (!(obj is string))
			{
				obj = RuntimeServices.Coerce(obj, typeof(string));
			}
			gameMessage.Add(Lang.Text((string)obj) as string);
		}
	}

	public static void SkyTaxiCallFunction()
	{
		if (!SkyTaxiCall)
		{
			SkyTaxiCall = UnityEngine.Object.Instantiate(LoadData.GO("skytaxi"), CurrentPlayerObject.position + new Vector3(-30f, 0f, -0.6f), Quaternion.identity) as GameObject;
		}
		SkyTaxiCall.SendMessage("SendTaxi", null, SendMessageOptions.DontRequireReceiver);
	}

	public static int UpgradePrice(int num)
	{
		int result;
		if (Tools[num] == null)
		{
			result = 0;
		}
		else
		{
			string text = Tools[num];
			result = ((text == "sword") ? (ToolsCount[num] * 350) : 0);
		}
		return result;
	}

	public static bool CheckByte(int BigNumber, int NeedNumber)
	{
		int result;
		if (BigNumber >= 256)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 256;
		}
		if (BigNumber >= 128)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 128;
		}
		if (BigNumber >= 64)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 64;
		}
		if (BigNumber >= 32)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 32;
		}
		if (BigNumber >= 16)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 16;
		}
		if (BigNumber >= 8)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 8;
		}
		if (BigNumber >= 4)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 4;
		}
		if (BigNumber >= 2)
		{
			if (NeedNumber == BigNumber)
			{
				result = 1;
				goto IL_0113;
			}
			BigNumber -= 2;
		}
		result = ((BigNumber >= 1 && NeedNumber == BigNumber) ? 1 : 0);
		goto IL_0113;
		IL_0113:
		return (byte)result != 0;
	}

	public static GameObject CreateSFX(AudioClip clip, Vector3 position, float volume, float pitch)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.position = position;
		gameObject.AddComponent<AudioSource>();
		gameObject.GetComponent<AudioSource>().pitch = pitch;
		gameObject.GetComponent<AudioSource>().PlayOneShot(clip, volume);
		UnityEngine.Object.Destroy(gameObject, clip.length / pitch);
		return gameObject;
	}

	public static float GetDDAttack()
	{
		float result;
		switch (Difficulty)
		{
		case -2:
			result = 2f;
			break;
		case -1:
			result = 1.5f;
			break;
		case 0:
			result = 1f;
			break;
		case 1:
			result = 1f;
			break;
		case 2:
			result = 1f;
			break;
		case 3:
			result = 1f;
			break;
		case 4:
			result = 1f;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	public static float GetMinMinus()
	{
		float result;
		switch (Difficulty)
		{
		case -2:
			result = 0.04f;
			break;
		case -1:
			result = 0.05f;
			break;
		case 0:
			result = 0.07f;
			break;
		case 1:
			result = 0.1f;
			break;
		case 2:
			result = 0.125f;
			break;
		case 3:
			result = 0.15f;
			break;
		case 4:
			result = 0.18f;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	public static float GetDifficulty()
	{
		float result;
		switch (Difficulty)
		{
		case -2:
			result = 0.1f;
			break;
		case -1:
			result = 0.25f;
			break;
		case 0:
			result = 0.5f;
			break;
		case 1:
			result = 1f;
			break;
		case 2:
			result = 1.5f;
			break;
		case 3:
			result = 1.75f;
			break;
		case 4:
			result = 2f;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	public static int GetMaxMind()
	{
		int result;
		switch (Difficulty)
		{
		case -2:
			result = 15;
			break;
		case -1:
			result = 25;
			break;
		case 0:
			result = 35;
			break;
		case 1:
			result = 50;
			break;
		case 2:
			result = 70;
			break;
		case 3:
			result = 80;
			break;
		case 4:
			result = 100;
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	public static string TextDifficulty(int num)
	{
		object result;
		switch (num)
		{
		case -2:
			result = "Very Easy";
			break;
		case -1:
			result = "Easy";
			break;
		case 0:
			result = "Normal";
			break;
		case 1:
			result = "Hard";
			break;
		case 2:
			result = "Very Hard";
			break;
		case 3:
			result = "HardCore";
			break;
		case 4:
			result = "NightMare";
			break;
		default:
			result = null;
			break;
		}
		return (string)result;
	}

	public static void CreateText(string txt, Vector3 pos, Color clr, float ang)
	{
		LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.HUD("CreateText")) as GameObject;
		LastCreatedObject.transform.position = pos;
		Vector3 localEulerAngles = LastCreatedObject.transform.localEulerAngles;
		float num = (localEulerAngles.z = ang);
		Vector3 vector = (LastCreatedObject.transform.localEulerAngles = localEulerAngles);
		LastCreatedObject.BroadcastMessage("SetTextColor", clr, SendMessageOptions.DontRequireReceiver);
		LastCreatedObject.BroadcastMessage("Rename", txt, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
