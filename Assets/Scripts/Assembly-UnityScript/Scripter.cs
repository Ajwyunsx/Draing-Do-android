using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[AddComponentMenu("Alex Code/Scripter v2")]
public class Scripter : MonoBehaviour
{
	[TextArea(5, 20)]
	public string Code;

	private int LastLine;

	private Codes[] Text;

	private int CurrentLine;

	private GameObject LastCreatedObject;

	private GameObject CollidedObject;

	[NonSerialized]
	public static int[] GlobalInt = new int[10];

	public int[] LocalInt;

	public string[] LocalName;

	public GameObject[] ObjectLib;

	public Sprite[] SpriteLib;

	private int isStart;

	private int isAwake;

	private int isFixedUpdate;

	private int isLateUpdate;

	private int isUpdate;

	private int isCrushHP;

	private int isOnCollisionEnter;

	private int isOnCollisionStay;

	private int isOnTriggerEnter;

	private int isOnTriggerExit;

	private int isOnTriggerStay;

	private int isCommandTo;

	private int isDISAPPEAR;

	private int isOnMouseDown;

	private int isOnMouseUpAsButton;

	private int isCustom;

	private int isOnMouseOver;

	private int isOnMouseOver2;

	private int isUse;

	private int isUse2;

	private int isCloseIt;

	private int isWearClothScript;

	private int CUSTOM;

	private GameObject WearObject;

	private bool BeenDelay;

	private float DelayTime;

	private bool StopFixed;

	private bool StopLate;

	public Scripter()
	{
		LocalInt = new int[10];
		LocalName = new string[1];
		isStart = -1;
		isAwake = -1;
		isFixedUpdate = -1;
		isLateUpdate = -1;
		isUpdate = -1;
		isCrushHP = -1;
		isOnCollisionEnter = -1;
		isOnCollisionStay = -1;
		isOnTriggerEnter = -1;
		isOnTriggerExit = -1;
		isOnTriggerStay = -1;
		isCommandTo = -1;
		isDISAPPEAR = -1;
		isOnMouseDown = -1;
		isOnMouseUpAsButton = -1;
		isCustom = -1;
		isOnMouseOver = -1;
		isOnMouseOver2 = -1;
		isUse = -1;
		isUse2 = -1;
		isCloseIt = -1;
		isWearClothScript = -1;
	}

	public virtual void Awake()
	{
		Code = Code.Replace("\t", string.Empty);
		string[] array = Code.Split("\n"[0]);
		Text = new Codes[Extensions.get_length((System.Array)array)];
		for (int i = 0; i < Extensions.get_length((System.Array)array); i++)
		{
			string[] collection = array[i].Split(" "[0]);
			UnityScript.Lang.Array array2 = new UnityScript.Lang.Array((IEnumerable)collection);
			for (int num = array2.length - 1; num > -1; num--)
			{
				if (array2[num] as string == string.Empty || array2[num] as string == null || array2[num] as string == " ")
				{
					array2.RemoveAt(num);
				}
			}
			Text[i] = new Codes();
			Text[i].Word = array2.ToBuiltin(typeof(string)) as string[];
			if (array2.length > 0 && RuntimeServices.EqualityOperator(array2[0], "void"))
			{
				object lhs = array2[1];
				if (RuntimeServices.EqualityOperator(lhs, "awake"))
				{
					isAwake = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "start"))
				{
					isStart = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "fixed"))
				{
					isFixedUpdate = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "late"))
				{
					isLateUpdate = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "update"))
				{
					isUpdate = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "crush"))
				{
					isCrushHP = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "collEnter"))
				{
					isOnCollisionEnter = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "collStay"))
				{
					isOnCollisionStay = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "trigEnter"))
				{
					isOnTriggerEnter = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "trigExit"))
				{
					isOnTriggerExit = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "trigStay"))
				{
					isOnTriggerStay = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "comm"))
				{
					isCommandTo = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "dis"))
				{
					isDISAPPEAR = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "mouse"))
				{
					isOnMouseDown = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "mouseUp"))
				{
					isOnMouseUpAsButton = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "custom"))
				{
					isCustom = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "wear"))
				{
					isWearClothScript = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "over"))
				{
					isOnMouseOver = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "mouse2"))
				{
					isOnMouseOver2 = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "use"))
				{
					isUse = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "use2"))
				{
					isUse2 = i;
				}
				else if (RuntimeServices.EqualityOperator(lhs, "close"))
				{
					isCloseIt = i;
				}
			}
		}
		ScriptReader(isAwake, null);
	}

	public virtual void Update()
	{
		ScriptReader(isUpdate, null);
	}

	public virtual void Start()
	{
		ScriptReader(isStart, null);
	}

	public virtual void FixedUpdate()
	{
		if (!StopFixed && !TalkPause.IsGameplayBlocked() && !BeenDelay)
		{
			ScriptReader(isFixedUpdate, null);
		}
	}

	public virtual void LateUpdate()
	{
		if (!Global.Pause && !TalkPause.IsGameplayBlocked() && !StopLate)
		{
			if (!BeenDelay)
			{
				ScriptReader(isLateUpdate, null);
				return;
			}
			BeenDelay = false;
			ScriptReader(LastLine - 1, null);
		}
	}

	public virtual void CrushHP()
	{
		ScriptReader(isCrushHP, null);
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		ScriptReader(isOnCollisionEnter, other.gameObject);
	}

	public virtual void OnCollisionStay(Collision other)
	{
		ScriptReader(isOnCollisionStay, other.gameObject);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		ScriptReader(isOnTriggerEnter, other.gameObject);
	}

	public virtual void OnTriggerExit(Collider other)
	{
		ScriptReader(isOnTriggerExit, other.gameObject);
	}

	public virtual void OnTriggerStay(Collider other)
	{
		ScriptReader(isOnTriggerStay, other.gameObject);
	}

	public virtual void CommandTo()
	{
		ScriptReader(isCommandTo, null);
	}

	public virtual void DISAPPEAR()
	{
		ScriptReader(isDISAPPEAR, null);
	}

	public virtual void OnMouseDown()
	{
		ScriptReader(isOnMouseDown, null);
	}

	public virtual void CloseIt()
	{
		ScriptReader(isCloseIt, null);
	}

	public virtual void OnMouseOver()
	{
		ScriptReader(isOnMouseOver, null);
		if (Input.GetMouseButtonDown(1))
		{
			ScriptReader(isOnMouseOver2, null);
		}
	}

	public virtual void UseThatItem()
	{
		ScriptReader(isUse, null);
	}

	public virtual void UseThatItem2()
	{
		ScriptReader(isUse2, null);
	}

	public virtual void Custom(string text)
	{
		if (text != string.Empty && text != null)
		{
			CUSTOM = Convert.ToInt32(text);
		}
		ScriptReader(isCustom, null);
	}

	public virtual void WearClothScript(GameObject go)
	{
		WearObject = go;
		ScriptReader(isWearClothScript, null);
	}

	public virtual void ScriptReader(int thatNumber, GameObject coll)
	{
		if (thatNumber == -1 || Extensions.get_length((System.Array)Text) == 0)
		{
			return;
		}
		thatNumber++;
		LastLine = thatNumber;
		if (LastLine >= Extensions.get_length((System.Array)Text))
		{
			LastLine = 0;
		}
		if (coll != null)
		{
			CollidedObject = coll;
		}
		for (CurrentLine = LastLine; CurrentLine < Extensions.get_length((System.Array)Text); CurrentLine++)
		{
			if (CurrentLine >= Extensions.get_length((System.Array)Text))
			{
				return;
			}
			LastLine = CurrentLine + 1;
			switch (ScriptCore(CurrentLine))
			{
			case "stop":
				LastLine = 0;
				return;
			case "delay":
				BeenDelay = true;
				LastLine = CurrentLine + 1;
				return;
			case "back":
				LastLine = CurrentLine;
				return;
			}
		}
		LastLine = 0;
	}

	public virtual float GrabInt(string name)
	{
		float result;
		if (name == null)
		{
			result = 0f;
		}
		else
		{
			string[] array = name.Split("!"[0]);
			switch (array[0])
			{
			case "f":
				result = float.Parse(array[1]);
				break;
			case "pow":
				result = Mathf.Pow(GrabInt(array[1]), GrabInt(array[2]));
				break;
			case "c":
				result = CUSTOM;
				break;
			case "enemy":
			{
				GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
				result = Extensions.get_length((System.Array)a);
				break;
			}
			case "pos":
				if (array[1] == "x")
				{
					result = transform.position.x;
					break;
				}
				if (array[1] == "y")
				{
					result = transform.position.y;
					break;
				}
				if (array[1] == "z")
				{
					result = transform.position.z;
					break;
				}
				goto case "wind";
			case "z":
				result = transform.localEulerAngles.z;
				break;
			case "wind":
				result = Global.DAYWIND;
				break;
			case "rain":
				result = Global.DAYRAIN;
				break;
			case "cloud":
				result = Global.DAYCLOUD;
				break;
			case "talk":
				result = Global.AskSelected;
				break;
			case "maxhp":
				result = Global.MaxHP;
				break;
			case "hp":
				result = Global.HP;
				break;
			case "gold":
				result = Global.Gold;
				break;
			case "r":
				result = UnityEngine.Random.Range(Convert.ToInt32(array[1]), Convert.ToInt32(array[2]));
				break;
			case "var":
				result = Convert.ToInt32(Global.Var[array[1]]);
				break;
			case "g":
				result = GlobalInt[Convert.ToInt32(array[1])];
				break;
			case "l":
				result = LocalInt[Convert.ToInt32(array[1])];
				break;
			case "level":
				result = Global.RANG;
				break;
			case "level+":
				result = Global.RANG + 1;
				break;
			default:
				result = Convert.ToInt32(name);
				break;
			}
		}
		return result;
	}

	public virtual float GrabFloat(string name)
	{
		float result;
		if (name == null || name == string.Empty)
		{
			result = 0f;
		}
		else
		{
			string[] array = name.Split("!"[0]);
			switch (array[0])
			{
			case "x":
				result = transform.position.x;
				break;
			case "y":
				result = transform.position.y;
				break;
			case "z":
				result = transform.position.z;
				break;
			case "r":
				result = UnityEngine.Random.Range(Convert.ToInt32(array[1]), Convert.ToInt32(array[2]));
				break;
			case "var":
				result = Convert.ToInt32(Global.Var[array[1]]);
				break;
			case "g":
				result = GlobalInt[Convert.ToInt32(array[1])];
				break;
			case "l":
				result = LocalInt[Convert.ToInt32(array[1])];
				break;
			default:
				result = float.Parse(name);
				break;
			}
		}
		return result;
	}

	public virtual bool GrabBool(string name)
	{
		int result;
		if (name == "true")
		{
			result = 1;
		}
		else if (name == "false")
		{
			result = 0;
		}
		else
		{
			int num = (int)GrabInt(name);
			result = ((num > 0) ? 1 : 0);
		}
		return (byte)result != 0;
	}

	public virtual GameObject GrabObject(string name)
	{
		object result;
		if (name == null)
		{
			result = null;
		}
		else
		{
			int num = default(int);
			string[] array = name.Split("!"[0]);
			switch (array[0])
			{
			case "*":
				result = null;
				break;
			case "find":
				result = GameObject.Find(array[1]);
				break;
			case "lib":
				result = ObjectLib[(int)GrabInt(array[1])];
				break;
			case "it":
				result = gameObject;
				break;
			case "null":
				result = null;
				break;
			case "child":
				result = ((!transform.GetChild(0)) ? null : transform.GetChild(0).gameObject);
				break;
			case "current":
			case "player":
				result = ((!Global.CurrentPlayerObject) ? null : Global.CurrentPlayerObject.gameObject);
				break;
			case "last":
				result = ((!LastCreatedObject) ? null : LastCreatedObject);
				break;
			case "coll":
				result = ((!CollidedObject) ? null : CollidedObject);
				break;
			case "obj":
				num = 0;
				while (true)
				{
					if (num < Extensions.get_length((System.Array)ObjectLib))
					{
						if (ObjectLib[num].name == array[1])
						{
							result = ObjectLib[num];
							break;
						}
						num++;
						continue;
					}
					result = null;
					break;
				}
				break;
			default:
				result = gameObject;
				break;
			}
		}
		return (GameObject)result;
	}

	public virtual Vector3 GrabVector(string name)
	{
		Vector3 result;
		if (name == null)
		{
			result = Vector3.zero;
		}
		else
		{
			string[] array = name.Split("!"[0]);
			switch (array[0])
			{
			case "v":
				result = new Vector3(GrabFloat(array[1]), GrabFloat(array[2]), GrabFloat(array[3]));
				break;
			case "t":
			case "trans":
				while (true)
				{
					if (Extensions.get_length((System.Array)array) > 1)
					{
						if (array[1] == "last" || array[1] == "l")
						{
							result = LastCreatedObject.transform.position;
							break;
						}
						if (array[1] == "player" || array[1] == "current")
						{
							result = Global.CurrentPlayerObject.position;
							break;
						}
						continue;
					}
					result = transform.position;
					break;
				}
				break;
			case "it":
				result = transform.position;
				break;
			default:
				result = Vector3.zero;
				break;
			}
		}
		return result;
	}

	public virtual Vector2 GrabVector2(string name)
	{
		Vector2 result;
		if (name == null)
		{
			result = Vector2.zero;
		}
		else
		{
			string[] array = name.Split("!"[0]);
			string text = array[0];
			result = ((!(text == "v2")) ? Vector2.zero : new Vector2(GrabFloat(array[1]), GrabFloat(array[2])));
		}
		return result;
	}

	public virtual Color GrabColor(string name)
	{
		Color result;
		if (name == null)
		{
			result = Color.white;
		}
		else
		{
			string[] array = name.Split("!"[0]);
			int num = default(int);
			switch (array[0])
			{
			case "red":
				result = Color.red;
				break;
			case "white":
				result = Color.white;
				break;
			case "black":
				result = Color.black;
				break;
			case "c":
				result = new Color(Convert.ToInt32(array[1]), Convert.ToInt32(array[2]), Convert.ToInt32(array[3]), Convert.ToInt32(array[4]));
				break;
			case "l":
				num = Convert.ToInt32(array[1]);
				num = LocalInt[num];
				result = new Color(num, num, num, 1f);
				break;
			case "1-l":
				num = Convert.ToInt32(array[1]);
				num = 1 - LocalInt[num];
				result = new Color(num, num, num, 1f);
				break;
			default:
				result = Color.white;
				break;
			}
		}
		return result;
	}

	public virtual bool IfCore(string[] WORD)
	{
		int result;
		if (WORD[1] == null)
		{
			result = 0;
		}
		else
		{
			int num = default(int);
			int num2 = default(int);
			bool flag = default(bool);
			string text = null;
			string text2 = WORD[1];
			string[] array;
			switch (text2)
			{
			case "axisH":
				result = (IfAction((int)Input.GetAxis("Horizontal"), WORD[2], WORD[3]) ? 1 : 0);
				break;
			case "axisV":
				result = (IfAction((int)Input.GetAxis("Vertical"), WORD[2], WORD[3]) ? 1 : 0);
				break;
			case "key":
				result = (Input.GetKeyUp(WORD[2]) ? 1 : 0);
				break;
			case "button":
				result = (Input.GetButtonDown(WORD[2]) ? 1 : 0);
				break;
			case "isNull":
				result = ((GrabObject(WORD[2]) == null) ? 1 : 0);
				break;
			case "poseX":
				result = (IfAction((int)GrabObject(WORD[2]).transform.position.x, WORD[2], WORD[3]) ? 1 : 0);
				break;
			case "poseY":
				result = (IfAction((int)GrabObject(WORD[2]).transform.position.y, WORD[2], WORD[3]) ? 1 : 0);
				break;
			case "poseZ":
				result = (IfAction((int)GrabObject(WORD[2]).transform.position.z, WORD[2], WORD[3]) ? 1 : 0);
				break;
			case "mouse":
				result = (Input.GetKeyDown(WORD[2]) ? 1 : 0);
				break;
			case "skip":
			{
				bool num3 = Input.GetButtonDown("Escape");
				if (!num3)
				{
					num3 = Input.GetKeyDown("return");
				}
				if (!num3)
				{
					num3 = Input.GetKeyDown("space");
				}
				if (!num3)
				{
					num3 = Input.GetMouseButtonDown(0);
				}
				bool flag2 = num3;
				if (!Global.Pause)
				{
					MonoBehaviour.print("ok1");
					flag2 = false;
				}
				if (flag2)
				{
					Global.DontEscTimer = 1;
					Input.ResetInputAxes();
					MonoBehaviour.print("ok2");
					Time.timeScale = 1f;
					Global.Pause = false;
					Global.MenuPause = false;
					Global.MenuWindow = null;
				}
				result = (flag2 ? 1 : 0);
				break;
			}
			case "tag":
				result = (CollidedObject ? ((CollidedObject.tag == WORD[2]) ? 1 : 0) : 0);
				break;
			case "quest":
				result = (Global.CheckQuest(WORD[2]) ? 1 : 0);
				break;
			case "stuff":
				result = (Global.CheckStuff(WORD[2]) ? 1 : 0);
				break;
			case "main":
				result = (RuntimeServices.EqualityOperator(Global.MainQuest, WORD[2]) ? 1 : 0);
				break;
			default:
				{
					if (text2 == string.Empty)
					{
						goto IL_070f;
					}
					switch (text2)
					{
					case "sel":
						break;
					case "selAny":
						goto IL_039e;
					case "world":
						goto IL_03d4;
					case "gamePause":
						goto IL_03fb;
					case "dist":
						goto IL_0422;
					case "day":
						goto IL_0449;
					case "time":
						goto IL_0470;
					case "wind":
						goto IL_0497;
					case "rainInt":
						goto IL_04bf;
					case "cloud":
						goto IL_04e6;
					case "rainbow":
						goto IL_050e;
					case "rain":
						goto IL_0536;
					case "night":
						goto IL_055d;
					case "talk":
						goto IL_0584;
					default:
						goto IL_059a;
					}
					string a = null;
					if ((bool)SlotItem.selected)
					{
						a = SlotItem.selected.name;
					}
					result = (IfName(a, WORD[2], WORD[3]) ? 1 : 0);
					break;
				}
				IL_059a:
				array = WORD[1].Split("!"[0]);
				switch (array[0])
				{
				case "maxhp":
					break;
				case "hp":
					goto IL_05f7;
				case "gold":
					goto IL_061f;
				case "r":
					goto IL_0646;
				case "var":
					goto IL_067f;
				case "g":
					goto IL_06b4;
				case "l":
					goto IL_06e7;
				default:
					goto IL_070f;
				}
				result = (IfAction((int)Global.MaxHP, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_070f:
				result = 0;
				break;
				IL_06e7:
				num = LocalInt[Convert.ToInt32(array[1])];
				result = (IfAction(num, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_06b4:
				num = GlobalInt[Convert.ToInt32(array[1])];
				result = (IfAction(num, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_067f:
				result = (IfAction(Convert.ToInt32(Global.Var[array[1]]), WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0646:
				result = (IfAction(UnityEngine.Random.Range(Convert.ToInt32(array[1]), Convert.ToInt32(array[2])), WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_061f:
				result = (IfAction(Global.Gold, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_05f7:
				result = (IfAction((int)Global.HP, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0584:
				result = (IfAction(Global.AskSelected, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_055d:
				result = (IfBoolean(Global.CheckNight(), WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0536:
				result = (IfBoolean(Global.CheckRain(), WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_050e:
				result = (IfAction((int)Global.DAYRAINBOW, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_04e6:
				result = (IfAction((int)Global.DAYCLOUD, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_04bf:
				result = (IfAction(Global.DAYRAIN, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0497:
				result = (IfAction((int)Global.DAYWIND, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0470:
				result = (IfAction(Global.DAYTIME, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0449:
				result = (IfAction(Global.DAYS, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_0422:
				result = (IfBoolean(Monitor.dist, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_03fb:
				result = (IfBoolean(Global.Pause, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_03d4:
				result = (IfBoolean(Global.WorldON, WORD[2], WORD[3]) ? 1 : 0);
				break;
				IL_039e:
				flag = false;
				if ((bool)SlotItem.selected)
				{
					flag = true;
				}
				result = (IfBoolean(flag, WORD[2], WORD[3]) ? 1 : 0);
				break;
			}
		}
		return (byte)result != 0;
	}

	public virtual bool IfAction(int A, string B, string B2)
	{
		int num = (int)GrabInt(B2);
		int result;
		switch (B)
		{
		case "=":
		case "==":
			if (A == num)
			{
				result = 1;
				break;
			}
			goto default;
		case "<":
			if (A < num)
			{
				result = 1;
				break;
			}
			goto default;
		case ">":
			if (A > num)
			{
				result = 1;
				break;
			}
			goto default;
		case "<=":
			if (A <= num)
			{
				result = 1;
				break;
			}
			goto default;
		case ">=":
			if (A >= num)
			{
				result = 1;
				break;
			}
			goto default;
		case "!=":
			if (A != num)
			{
				result = 1;
				break;
			}
			goto default;
		default:
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public virtual bool IfName(string A, string B, string C)
	{
		if (A == null)
		{
			A = "null";
		}
		int result;
		switch (B)
		{
		case "=":
		case "==":
			if (A == C)
			{
				result = 1;
				break;
			}
			goto default;
		case "!=":
			if (A != C)
			{
				result = 1;
				break;
			}
			goto default;
		default:
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public virtual bool IfBoolean(bool A, string B, string C)
	{
		bool flag = GrabBool(C);
		int result;
		switch (B)
		{
		case "=":
		case "==":
			if (A == flag)
			{
				result = 1;
				break;
			}
			goto IL_0070;
		case "!=":
			if (A != flag)
			{
				result = 1;
				break;
			}
			goto IL_0070;
		default:
			{
				if (A)
				{
					result = 1;
					break;
				}
				goto IL_0070;
			}
			IL_0070:
			result = 0;
			break;
		}
		return (byte)result != 0;
	}

	public virtual int MathAction(float A, string[] WORD)
	{
		float num = GrabInt(WORD[2]);
		float num2 = GrabInt(WORD[4]);
		int result;
		switch (WORD[1] + WORD[3])
		{
		case "=":
			result = (int)num;
			break;
		case "=+":
			result = (int)(num + num2);
			break;
		case "=-":
			result = (int)(num - num2);
			break;
		case "=*":
			result = (int)(num * num2);
			break;
		case "=/":
			result = (int)(num / num2);
			break;
		case "+":
			result = (int)(A + num);
			break;
		case "-":
			result = (int)(A - num);
			break;
		case "*":
			result = (int)(A * num);
			break;
		case "/":
			result = (int)(A / num);
			break;
		case "+*":
			result = (int)(A + num * num2);
			break;
		case "-*":
			result = (int)(A - num * num2);
			break;
		case "+/":
			result = (int)(A + num / num2);
			break;
		case "-/":
			result = (int)(A - num / num2);
			break;
		default:
			result = (int)num;
			break;
		}
		return result;
	}

	public virtual float MathActionF(float A, string[] WORD)
	{
		float num = GrabInt(WORD[2]);
		float num2 = GrabInt(WORD[4]);
		float result;
		switch (WORD[1] + WORD[3])
		{
		case "=":
			result = num;
			break;
		case "=+":
			result = num + num2;
			break;
		case "=-":
			result = num - num2;
			break;
		case "=*":
			result = num * num2;
			break;
		case "=/":
			result = num / num2;
			break;
		case "+":
			result = A + num;
			break;
		case "-":
			result = A - num;
			break;
		case "*":
			result = A * num;
			break;
		case "/":
			result = A / num;
			break;
		case "+*":
			result = A + num * num2;
			break;
		case "-*":
			result = A - num * num2;
			break;
		case "+/":
			result = A + num / num2;
			break;
		case "-/":
			result = A - num / num2;
			break;
		default:
			result = num;
			break;
		}
		return result;
	}

	public virtual string ScriptCore(int num)
	{
		object result;
		string[] array;
		string text;
		GameObject gameObject2;
		int num2;
		int num3;
		if (Extensions.get_length((System.Array)Text[num].Word) != 0)
		{
			if (Text[num].Word[0].Substring(0, 1) == "/")
			{
				result = string.Empty;
				goto IL_1cae;
			}
			num2 = 0;
			num3 = default(int);
			int num4 = default(int);
			int num5 = default(int);
			text = null;
			string text2 = null;
			Transform transform = null;
			GameObject gameObject = null;
			gameObject2 = null;
			array = new string[7];
			if (Extensions.get_length((System.Array)Text[num].Word) > 0)
			{
				array[0] = Text[num].Word[0];
			}
			if (Extensions.get_length((System.Array)Text[num].Word) > 1)
			{
				array[1] = Text[num].Word[1];
			}
			if (Extensions.get_length((System.Array)Text[num].Word) > 2)
			{
				array[2] = Text[num].Word[2];
			}
			if (Extensions.get_length((System.Array)Text[num].Word) > 3)
			{
				array[3] = Text[num].Word[3];
			}
			if (Extensions.get_length((System.Array)Text[num].Word) > 4)
			{
				array[4] = Text[num].Word[4];
			}
			switch (array[0])
			{
			case "print":
				break;
			case "void":
				goto IL_01e3;
			case "talk":
				goto IL_01fe;
			case "switch":
				goto IL_0247;
			case "if":
				goto IL_03f6;
			case "ifNot":
				goto IL_055d;
			case "else":
			case "case":
			case "def":
				goto IL_06e6;
			case "pause":
				goto IL_0817;
			case "stop":
				goto IL_083b;
			case "off":
				goto IL_0856;
			case "stopfixed":
				StopFixed = true;
				goto IL_1cad;
			case "stoplate":
				StopLate = true;
				goto IL_1cad;
			case "delay":
				goto IL_08b1;
			case "emit":
				((ParticleEmitter)GrabObject(array[1]).GetComponent(typeof(ParticleEmitter))).emit = GrabBool(array[2]);
				goto IL_1cad;
			case "partsPlay":
				GrabObject(array[1]).GetComponent<ParticleSystem>().Play();
				goto IL_1cad;
			case "light":
				GrabObject(array[1]).GetComponent<Light>().enabled = GrabBool(array[2]);
				goto IL_1cad;
			case "spriteEn":
				GrabObject(array[1]).GetComponent<SpriteRenderer>().enabled = GrabBool(array[2]);
				goto IL_1cad;
			case "script":
				(GetComponent(array[1]) as Behaviour).enabled = GrabBool(array[2]);
				goto IL_1cad;
			case "deadLevel":
				Global.DeadLevel = GrabName(array[1]);
				goto IL_1cad;
			case "load":
			{
				Time.timeScale = 1f;
				Global.Pause = false;
				Global.OldPause = false;
				string text3 = GrabName(array[1]).Replace("_", " ");
				string id = (Global.CheckPointNameTemp = GrabName(array[2]).Replace("_", " "));
				MonoBehaviour.print("CheckPointNameTemp: " + Global.CheckPointNameTemp);
				Global.LoadLEVEL(text3, id);
				goto IL_1cad;
			}
			case "wear":
				goto IL_0ac9;
			case "unwear":
				text = GrabName(array[1]).Replace("_", " ");
				transform = WearObject.transform.Find(text);
				if ((bool)transform)
				{
					transform.GetComponent<Renderer>().enabled = false;
				}
				else
				{
					MonoBehaviour.print("NO: " + text);
				}
				goto case "send";
			case "send":
				text = GrabName(array[3]).Replace("_", " ");
				GrabObject(array[1]).SendMessage(array[2], text, SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "dir":
				Global.CurrentPlayerObject.SendMessage("SetDirect", GrabInt(array[1]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "sendF":
				GrabObject(array[1]).SendMessage(array[2], GrabFloat(array[3]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "sendV2":
				GrabObject(array[1]).SendMessage(array[2], GrabVector2(array[3]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "sendV3":
				GrabObject(array[1]).SendMessage(array[2], GrabVector(array[3]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "sendbool":
				GrabObject(array[1]).SendMessage(array[2], GrabBool(array[3]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "sendGO":
				GrabObject(array[1]).SendMessage(array[2], GrabObject(array[3]), SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "broad":
				GrabObject(array[1]).BroadcastMessage(array[2], array[3], SendMessageOptions.DontRequireReceiver);
				goto IL_1cad;
			case "custom":
				if (array[1] == null)
				{
					array[1] = "it";
				}
				if (array[2] == null)
				{
					array[2] = "0";
				}
				gameObject2 = GrabObject(array[1]);
				if ((bool)gameObject2)
				{
					gameObject2.SendMessage("Custom", string.Empty + GrabInt(array[2]), SendMessageOptions.DontRequireReceiver);
				}
				goto IL_1cad;
			case "audio":
				goto IL_0e4e;
			case "sfx":
				AudioSource.PlayClipAtPoint(LoadData.SFX(array[1]), this.transform.position);
				goto IL_1cad;
			case "anim":
				GetComponent<Animation>().CrossFade(array[1], GrabFloat(array[2]));
				goto IL_1cad;
			case "animInt":
				GetComponent<Animator>().SetInteger(array[1], (int)GrabInt(array[2]));
				goto IL_1cad;
			case "animTrig":
				GetComponent<Animator>().SetTrigger(array[1]);
				goto IL_1cad;
			case "animBool":
				GetComponent<Animator>().SetBool(array[1], GrabBool(array[2]));
				goto IL_1cad;
			case "create":
				LastCreatedObject = UnityEngine.Object.Instantiate(GrabObject(array[1]), GrabVector(array[2]), Quaternion.identity);
				goto IL_1cad;
			case "color":
			{
				Renderer component = GrabObject(array[1]).GetComponent<Renderer>();
				if ((bool)component)
				{
					component.material.color = GrabColor(array[2]);
				}
				goto IL_1cad;
			}
			case "parts":
				ShardCrush(GrabObject(array[1]), (int)GrabInt(array[2]));
				goto IL_1cad;
			case "active":
				GrabObject(array[1]).SetActive(GrabBool(array[2]));
				goto IL_1cad;
			case "move":
				GrabObject(array[1]).transform.position = GrabObject(array[1]).transform.position + GrabVector(array[2]);
				goto IL_1cad;
			case "pos":
			case "position":
				goto IL_10d4;
			case "posX":
			{
				float x2 = GrabInt(array[2]);
				Vector3 position = GrabObject(array[1]).transform.position;
				float num10 = (position.x = x2);
				Vector3 vector9 = (GrabObject(array[1]).transform.position = position);
				goto IL_1cad;
			}
			case "locPos":
				GrabObject(array[1]).transform.localPosition = GrabVector(array[2]);
				goto IL_1cad;
			case "ang":
			case "angle":
				GrabObject(array[1]).transform.eulerAngles = GrabVector(array[2]);
				goto IL_1cad;
			case "locAng":
				GrabObject(array[1]).transform.localEulerAngles = GrabVector(array[2]);
				goto IL_1cad;
			case "z":
			{
				float z2 = MathActionF(this.transform.localEulerAngles.z, array);
				Vector3 localEulerAngles = this.transform.localEulerAngles;
				float num9 = (localEulerAngles.z = z2);
				Vector3 vector7 = (this.transform.localEulerAngles = localEulerAngles);
				goto IL_1cad;
			}
			case "velocityAng":
				gameObject2 = GrabObject(array[1]);
				if ((bool)gameObject2.GetComponent<Rigidbody>())
				{
					float z = GrabFloat(array[2]);
					Vector3 angularVelocity = gameObject2.GetComponent<Rigidbody>().angularVelocity;
					float num8 = (angularVelocity.z = z);
					Vector3 vector5 = (gameObject2.GetComponent<Rigidbody>().angularVelocity = angularVelocity);
				}
				goto IL_1cad;
			case "velocity":
				gameObject2 = GrabObject(array[1]);
				if ((bool)gameObject2.GetComponent<Rigidbody>())
				{
					gameObject2.GetComponent<Rigidbody>().velocity = GrabVector(array[2]);
				}
				goto IL_1cad;
			case "velocityY":
				gameObject2 = GrabObject(array[1]);
				if ((bool)gameObject2.GetComponent<Rigidbody>())
				{
					float y = GrabFloat(array[2]);
					Vector3 velocity2 = gameObject2.GetComponent<Rigidbody>().velocity;
					float num7 = (velocity2.y = y);
					Vector3 vector3 = (gameObject2.GetComponent<Rigidbody>().velocity = velocity2);
				}
				goto IL_1cad;
			case "velocityX":
				gameObject2 = GrabObject(array[1]);
				if ((bool)gameObject2.GetComponent<Rigidbody>())
				{
					float x = GrabFloat(array[2]);
					Vector3 velocity = gameObject2.GetComponent<Rigidbody>().velocity;
					float num6 = (velocity.x = x);
					Vector3 vector = (gameObject2.GetComponent<Rigidbody>().velocity = velocity);
				}
				goto IL_1cad;
			case "coll":
				gameObject = GrabObject(array[1]);
				if ((bool)gameObject && (bool)gameObject.GetComponent<Collider>())
				{
					gameObject.GetComponent<Collider>().enabled = GrabBool(array[2]);
				}
				goto IL_1cad;
			case "delete":
			case "destroy":
				goto IL_14ce;
			case "deleteItem":
				if ((bool)SlotItem.selected)
				{
					UnityEngine.Object.Destroy(SlotItem.selected);
				}
				goto IL_1cad;
			case "newItem":
				CreateNewItem(array[1]);
				if (!string.IsNullOrEmpty(array[2]))
				{
					PutItemToInventory();
				}
				goto IL_1cad;
			case "putItem":
				PutItemToInventory();
				goto IL_1cad;
			case "day":
				Global.DAYS = MathAction(Global.DAYS, array);
				goto IL_1cad;
			case "time":
				Global.DAYTIME = MathAction(Global.DAYTIME, array);
				goto IL_1cad;
			case "wind":
				Global.DAYWIND = MathAction(Global.DAYWIND, array);
				goto IL_1cad;
			case "rain":
				Global.DAYRAIN = MathAction(Global.DAYRAIN, array);
				goto IL_1cad;
			case "cloud":
				Global.DAYCLOUD = MathAction(Global.DAYCLOUD, array);
				goto IL_1cad;
			case "rainbow":
				Global.DAYRAINBOW = MathAction(Global.DAYRAINBOW, array);
				goto IL_1cad;
			case "quake":
				Global.QuakeStart((int)GrabInt(array[1]), GrabInt(array[2]));
				goto IL_1cad;
			case "ambient":
				RenderSettings.ambientLight = GrabColor(array[1]);
				goto IL_1cad;
			case "menu":
				Global.CreateMenuWindowObj(GrabObject(array[1]));
				goto IL_1cad;
			case "unpause":
				Time.timeScale = 1f;
				Global.Pause = false;
				goto IL_1cad;
			case "monitor":
				goto IL_179b;
			case "fakeHit":
				Global.GetEnemyHit(new Vector3(GrabInt(array[1]), GrabInt(array[2]), 0f), null);
				goto IL_1cad;
			case "fromTo":
				text = GrabHardName(array[1]);
				Global.FromDirection = text;
				text = GrabHardName(array[2]);
				Global.ToDirection = text;
				goto IL_1cad;
			case "mess":
				text = GrabHardName(array[1]);
				Global.GameMessageCreateOne(text);
				goto IL_1cad;
			case "menuText":
				text = GrabHardName(array[1]);
				Global.MenuText = text;
				goto IL_1cad;
			case "quest":
				Global.AddQuest(array[1]);
				goto IL_1cad;
			case "questRemove":
				Global.RemoveQuest(array[1]);
				goto IL_1cad;
			case "stuff":
				Global.AddStuff(array[1]);
				goto IL_1cad;
			case "hp%":
				Global.HP += Global.MaxHP * (GrabInt(array[1]) / 100f);
				if (!(Global.HP <= Global.MaxHP))
				{
					Global.HP = Global.MaxHP;
				}
				goto IL_1cad;
			case "food%":
				Global.Var["food"] = (float)Convert.ToInt32(Global.Var["food"]) + GrabInt(array[1]);
				if (Convert.ToInt32(Global.Var["food"]) > 100)
				{
					Global.Var["food"] = 100;
				}
				if (Convert.ToInt32(Global.Var["food"]) < 0)
				{
					Global.Var["food"] = 0;
				}
				goto IL_1cad;
			case "mind%":
				Global.Var["mind"] = (float)Convert.ToInt32(Global.Var["mind"]) + GrabInt(array[1]);
				if (Convert.ToInt32(Global.Var["mind"]) > 100)
				{
					Global.Var["mind"] = 100;
				}
				if (Convert.ToInt32(Global.Var["mind"]) < 0)
				{
					Global.Var["mind"] = 0;
				}
				goto IL_1cad;
			default:
				goto IL_1ae0;
			case "end":
			case "fog":
			case "main":
			case "sel":
				goto IL_1cad;
			}
			if (!string.IsNullOrEmpty(array[2]))
			{
				MonoBehaviour.print(string.Empty + GrabHardName(array[1]) + " " + GrabInt(array[2]));
			}
			else
			{
				MonoBehaviour.print(string.Empty + GrabHardName(array[1]));
			}
		}
		goto IL_1cad;
		IL_055d:
		if (!IfCore(array))
		{
			goto IL_1cad;
		}
		num2 = 0;
		num3 = num + 1;
		while (true)
		{
			if (num3 < Extensions.get_length((System.Array)Text))
			{
				if (Extensions.get_length((System.Array)Text[num3].Word) > 0)
				{
					if (Text[num3].Word[0] == "if")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "ifNot")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "void")
					{
						result = "stop";
						break;
					}
					if (Text[num3].Word[0] == "switch")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "end")
					{
						num2--;
					}
					if (Text[num3].Word[0] == "else" && num2 == 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
					if (num2 < 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
				}
				num3++;
				continue;
			}
			result = "stop";
			break;
		}
		goto IL_1cae;
		IL_1cad:
		result = null;
		goto IL_1cae;
		IL_0817:
		CurrentLine = num3 - 1;
		result = "stop";
		goto IL_1cae;
		IL_083b:
		result = "stop";
		goto IL_1cae;
		IL_06e6:
		num2 = 0;
		num3 = num + 1;
		while (true)
		{
			if (num3 < Extensions.get_length((System.Array)Text))
			{
				if (Extensions.get_length((System.Array)Text[num3].Word) > 0)
				{
					if (Text[num3].Word[0] == "if")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "ifNot")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "void")
					{
						result = "stop";
						break;
					}
					if (Text[num3].Word[0] == "switch")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "end")
					{
						num2--;
					}
					if (num2 < 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
				}
				num3++;
				continue;
			}
			result = "stop";
			break;
		}
		goto IL_1cae;
		IL_0e4e:
		gameObject2 = GrabObject(array[1]);
		if (gameObject2 == null)
		{
			gameObject2 = this.gameObject;
		}
		if (GrabBool(array[2]))
		{
			gameObject2.GetComponent<AudioSource>().Play();
		}
		else
		{
			gameObject2.GetComponent<AudioSource>().Stop();
		}
		goto IL_1cad;
		IL_03f6:
		if (IfCore(array))
		{
			goto IL_1cad;
		}
		num2 = 0;
		num3 = num + 1;
		while (true)
		{
			if (num3 < Extensions.get_length((System.Array)Text))
			{
				if (Extensions.get_length((System.Array)Text[num3].Word) > 0)
				{
					if (Text[num3].Word[0] == "if")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "ifNot")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "void")
					{
						result = "stop";
						break;
					}
					if (Text[num3].Word[0] == "switch")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "end")
					{
						num2--;
					}
					if (Text[num3].Word[0] == "else" && num2 == 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
					if (num2 < 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
				}
				num3++;
				continue;
			}
			result = "stop";
			break;
		}
		goto IL_1cae;
		IL_0856:
		UnityEngine.Object.Destroy(this);
		result = "stop";
		goto IL_1cae;
		IL_0ac9:
		if ((bool)Global.CurrentPlayerObject)
		{
			text = GrabName(array[1]).Replace("_", " ");
			string text2 = GrabName(array[2]).Replace("_", " ");
			Transform transform = WearObject.transform.Find(text);
			if ((bool)transform)
			{
				transform.GetComponent<Renderer>().enabled = true;
				tk2dSprite tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(text2);
			}
			else
			{
				MonoBehaviour.print("NO: " + text);
			}
		}
		goto IL_1cad;
		IL_08b1:
		if (DelayTime == 0f)
		{
			DelayTime = Time.timeSinceLevelLoad + GrabInt(array[1]);
		}
		if (!(Time.timeSinceLevelLoad >= DelayTime))
		{
			CurrentLine = num - 1;
			result = "delay";
			goto IL_1cae;
		}
		DelayTime = 0f;
		goto IL_1cad;
		IL_179b:
		text = GrabName(array[2]).Replace("_", " ");
		if (array[1] == "a")
		{
			Monitor.TextA = GrabName(text);
		}
		else
		{
			Monitor.TextB = GrabName(text);
		}
		goto IL_1cad;
		IL_14ce:
		if (array[1] == null)
		{
			UnityEngine.Object.Destroy(this.gameObject);
			result = "stop";
		}
		else
		{
			gameObject2 = GrabObject(array[1]);
			bool flag = false;
			if (gameObject2 == this.gameObject)
			{
				flag = true;
			}
			if (!string.IsNullOrEmpty(array[2]))
			{
				UnityEngine.Object.Destroy(gameObject2, GrabFloat(array[2]));
			}
			else
			{
				UnityEngine.Object.Destroy(GrabObject(array[1]));
			}
			if (!flag)
			{
				goto IL_1cad;
			}
			result = "stop";
		}
		goto IL_1cae;
		IL_1ae0:
		string[] array2 = array[0].Split("!"[0]);
		switch (array2[0])
		{
		case "maxhp":
			Global.MaxHP = MathAction(Global.MaxHP, array);
			break;
		case "hp":
			if (Global.HP > 0f)
			{
				Global.HP = MathAction(Global.HP, array);
				if (!(Global.HP >= 0f))
				{
					Global.HP = 0f;
				}
				if (!(Global.HP <= Global.MaxHP))
				{
					Global.HP = Global.MaxHP;
				}
			}
			break;
		case "gold":
			Global.Gold = MathAction(Global.Gold, array);
			break;
		case "var":
			Global.Var[array2[1]] = MathAction(Convert.ToInt32(Global.Var[array2[1]]), array);
			break;
		case "g":
		{
			int num4 = Convert.ToInt32(array2[1]);
			int num5 = MathAction(GlobalInt[num4], array);
			GlobalInt[num4] = num5;
			break;
		}
		case "l":
		{
			int num4 = Convert.ToInt32(array2[1]);
			int num5 = MathAction(LocalInt[num4], array);
			LocalInt[num4] = num5;
			break;
		}
		default:
			MonoBehaviour.print("WRONG Script Object: " + this.gameObject.name + " >>> WORD[0]: " + array[0]);
			break;
		}
		goto IL_1cad;
		IL_10d4:
		if (string.IsNullOrEmpty(array[3]))
		{
			GrabObject(array[1]).transform.position = GrabVector(array[2]);
		}
		else
		{
			GrabObject(array[1]).transform.position = GrabVector(array[2]) + GrabVector(array[3]);
		}
		goto IL_1cad;
		IL_1cae:
		return (string)result;
		IL_01e3:
		result = "stop";
		goto IL_1cae;
		IL_01fe:
		Talk(GrabHardName(array[1]), GrabHardName(array[2]), GrabObject(array[3]), GrabObject(array[4]));
		result = "delay";
		goto IL_1cae;
		IL_0247:
		num2 = 0;
		num3 = num + 1;
		while (true)
		{
			if (num3 < Extensions.get_length((System.Array)Text))
			{
				if (Extensions.get_length((System.Array)Text[num3].Word) > 0)
				{
					if (Text[num3].Word[0] == "if")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "ifNot")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "void")
					{
						result = "stop";
						break;
					}
					if (Text[num3].Word[0] == "switch")
					{
						num2++;
					}
					if (Text[num3].Word[0] == "end")
					{
						num2--;
					}
					int num11 = (int)GrabInt(array[1]);
					if (Text[num3].Word[0] == "case" && num2 == 0 && GrabInt(Text[num3].Word[1]) == (float)num11)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
					if (Text[num3].Word[0] == "def" && num2 == 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
					if (num2 < 0)
					{
						CurrentLine = num3;
						result = "ok";
						break;
					}
				}
				num3++;
				continue;
			}
			result = "stop";
			break;
		}
		goto IL_1cae;
	}

	public virtual void Talk(string text, string ask, GameObject face1, GameObject face2)
	{
		Global.TalkText = text;
		if (ask == "*")
		{
			ask = null;
		}
		Global.AskText = ask;
		FaceTalk.Face1 = face1;
		FaceTalk.Face2 = face2;
		Global.CreateTalkWindow();
	}

	public virtual string GrabHardName(string name)
	{
		string result;
		if (name == null)
		{
			result = string.Empty;
		}
		else
		{
			bool flag = false;
			if (name.IndexOf("$") > -1)
			{
				flag = true;
			}
			if (name.IndexOf("%") > -1)
			{
				flag = true;
			}
			if (!flag)
			{
				result = name.Replace("_", " ");
			}
			else
			{
				string[] array = name.Split("_"[0]);
				name = string.Empty;
				string text = null;
				for (int i = 0; i < Extensions.get_length((System.Array)array); i++)
				{
					text = array[i];
					char c = text[0];
					if (c == '$')
					{
						text = text.Replace("$", string.Empty);
						array[i] = GrabName(text);
					}
					else if (c == '%')
					{
						text = text.Replace("%", string.Empty);
						array[i] = string.Empty + GrabInt(text);
					}
					name = name + array[i] + " ";
				}
				result = name;
			}
		}
		return result;
	}

	public virtual string GrabName(string name)
	{
		object result;
		if (name == null)
		{
			result = string.Empty;
		}
		else
		{
			string rhs = null;
			string[] array = name.Split("*"[0]);
			string text = array[0];
			if (!(text == "clock"))
			{
				result = ((!(text == "l")) ? name : LocalName[Convert.ToInt32(array[1])]);
			}
			else
			{
				switch (Global.DAYS % 7)
				{
				case 0:
					rhs = "Monday";
					break;
				case 1:
					rhs = "Tuesday";
					break;
				case 2:
					rhs = "Wednesday";
					break;
				case 3:
					rhs = "Thursday";
					break;
				case 4:
					rhs = "Friday";
					break;
				case 5:
					rhs = "Saturday";
					break;
				case 6:
					rhs = "Sunday";
					break;
				}
				result = "Time: " + Global.DAYTIME + ":00" + " " + rhs;
			}
		}
		return (string)result;
	}

	public virtual void ShardCrush(GameObject obj, int num)
	{
		for (int i = 0; i < num; i++)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(obj) as GameObject;
			float x = transform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f);
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num2 = (position.x = x);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			float y = transform.position.y + UnityEngine.Random.Range(-0.25f, 0.25f);
			Vector3 position2 = Global.LastCreatedObject.transform.position;
			float num3 = (position2.y = y);
			Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
			float z = transform.position.z - 0.25f;
			Vector3 position3 = Global.LastCreatedObject.transform.position;
			float num4 = (position3.z = z);
			Vector3 vector5 = (Global.LastCreatedObject.transform.position = position3);
			int num5 = UnityEngine.Random.Range(-180, 180);
			Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
			float num6 = (eulerAngles.z = num5);
			Vector3 vector7 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
			Global.LastCreatedObject.SendMessage("FakePartVelocityAndDestroy", 4, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CreateNewItem(string name)
	{
		GameObject gameObject = SlotLib.CreateObj(name);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.005f), transform.rotation) as GameObject;
		gameObject2.name = gameObject.name;
		gameObject2.SendMessage("GetThatItem", this.gameObject, SendMessageOptions.DontRequireReceiver);
		SlotItem.EscapeItem = true;
	}

	public virtual void PutItemToInventory()
	{
		SlotItem.EscapeItem = true;
	}

	public virtual void Main()
	{
	}
}
