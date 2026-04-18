using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SlotItem : MonoBehaviour
{
	public string UNITYNAME;

	private int slotNumber;

	private GameObject SlotObject;

	public int count;

	public string NameItem;

	[NonSerialized]
	public static GameObject selected;

	[NonSerialized]
	public static bool MixOver;

	private int DontDropTimer;

	[NonSerialized]
	public static bool EscapeItem;

	[NonSerialized]
	public static int JumpingEffect;

	private int oldJumping;

	public float powerDrop;

	public Shader MatA;

	public Shader MatB;

	public bool ForBuy;

	public int Price;

	private int oldDAYTIME;

	[NonSerialized]
	public static bool SayNoMix;

	public string TextBUseText;

	private string TextBUseText2;

	private string TextBPlus;

	public bool UseThat;

	public int HP;

	public int Hungry;

	public int Crazy;

	public int Poison;

	public AudioClip SFXByUse;

	public bool DestroyByUse;

	public string CreateItemByUse;

	public SlotItem()
	{
		powerDrop = 15f;
		oldDAYTIME = -1;
		DestroyByUse = true;
	}

	public virtual void Awake()
	{
		if (!string.IsNullOrEmpty(UNITYNAME) && UNITYNAME != string.Empty)
		{
			gameObject.name = UNITYNAME;
		}
		else
		{
			MonoBehaviour.print("ERROR gameObject.name: " + gameObject.name);
		}
		switch (TextBUseText)
		{
		case "use":
			TextBUseText2 = "R.Click - use it ";
			break;
		case "eat":
		case "food":
			TextBUseText2 = "R.Mouse - eat it ";
			break;
		case "drink":
			TextBUseText2 = "R.Mouse - drink it ";
			break;
		}
	}

	public virtual void Start()
	{
		if (UseThat)
		{
			TextBPlus += "  ( ";
			if (HP != 0)
			{
				TextBPlus = "HP" + ((HP <= 0) ? string.Empty : "+") + HP + "% " + TextBPlus;
			}
			if (Hungry != 0)
			{
				TextBPlus = "Hungry" + ((Hungry <= 0) ? string.Empty : "+") + Hungry + "% " + TextBPlus;
			}
			if (Crazy != 0)
			{
				TextBPlus = "Crazy" + ((Crazy <= 0) ? string.Empty : "+") + Crazy + "% " + TextBPlus;
			}
			if (Poison != 0)
			{
				TextBPlus = "Poison" + ((Poison <= 0) ? string.Empty : "+") + Poison + "% " + TextBPlus;
			}
			TextBPlus += ")";
		}
		if (ForBuy && (bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().useGravity = false;
		}
	}

	public virtual void FixedUpdate()
	{
		if (JumpingEffect != oldJumping)
		{
			oldJumping = JumpingEffect;
			if (selected == gameObject || (bool)SlotObject)
			{
				return;
			}
			float y = transform.position.y + 0.05f;
			Vector3 position = transform.position;
			float num = (position.y = y);
			Vector3 vector = (transform.position = position);
		}
		if (TextBUseText == "clock" && Global.DAYTIME != oldDAYTIME)
		{
			oldDAYTIME = Global.DAYTIME;
			string rhs = null;
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
			TextBUseText2 = "Time: " + Global.DAYTIME + ":00" + " " + rhs;
		}
	}

	public virtual void LateUpdate()
	{
		if (Global.Pause)
		{
			return;
		}
		if (selected == gameObject)
		{
			Global.offBlockTimer = 5;
			if (EscapeItem)
			{
				selected = null;
				EscapeItem = false;
				if (!GetToSlot())
				{
					transform.position = Global.CurrentPlayerObject.position;
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					GetComponent<Rigidbody>().useGravity = true;
					GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)120;
					SetOrder(0);
					return;
				}
			}
			if (DontDropTimer <= 0 && Monitor.Click && Monitor.On && !Monitor.DontDrop)
			{
				selected = null;
				Monitor.Click = false;
				SlotObject = null;
				transform.parent = null;
				transform.position = Global.CurrentPlayerObject.position + new Vector3(0f, 0f, -0.05f);
				SetOrder(0);
				if ((bool)GetComponent<Rigidbody>())
				{
					GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
					GetComponent<Rigidbody>().useGravity = true;
					GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)120;
					Vector3 vector = Monitor.XYZ - transform.position;
					vector.Normalize();
					MakeAction();
					Vector3 vector2 = vector * Vector3.Distance(transform.position, Monitor.XYZ) * 150f;
					vector2 = Vector3.ClampMagnitude(vector2, powerDrop);
					GetComponent<Rigidbody>().AddForce(vector2);
					MonoBehaviour.print("speed: " + vector2);
				}
			}
			else
			{
				transform.position = Monitor.XYZ + new Vector3(1f, 0f, -1f);
				if (DontDropTimer > 0)
				{
					DontDropTimer--;
				}
			}
		}
		else if ((bool)SlotObject)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, 0f, -0.1f), 0.1f);
		}
	}

	public virtual void onMouseDown()
	{
		if (!Monitor.dist && SlotObject == null)
		{
			return;
		}
		EscapeItem = false;
		if ((bool)selected)
		{
			SayNoMix = true;
			selected.SendMessage("MixItems", gameObject, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("MixItems", selected, SendMessageOptions.DontRequireReceiver);
			return;
		}
		if (ForBuy)
		{
			if (Global.Gold < Price)
			{
				AudioSource.PlayClipAtPoint(LoadData.SFX("no"), transform.position);
				return;
			}
			Global.Gold -= Price;
			AudioSource.PlayClipAtPoint(LoadData.SFX("buy"), transform.position);
			if ((bool)transform.parent)
			{
				transform.parent.SendMessage("RemoveIt", null, SendMessageOptions.DontRequireReceiver);
			}
			ForBuy = false;
		}
		gameObject.SendMessage("TryToSave", gameObject, SendMessageOptions.DontRequireReceiver);
		Slot.Timer = Time.realtimeSinceStartup;
		int num = 0;
		Vector3 eulerAngles = transform.eulerAngles;
		float num2 = (eulerAngles.z = num);
		Vector3 vector = (transform.eulerAngles = eulerAngles);
		if (!GetToSlot())
		{
			GetThatItem();
		}
	}

	public virtual void GetThatItem()
	{
		if (Global.FadeMode == 0)
		{
			SlotObject = null;
			transform.parent = null;
			selected = gameObject;
			SetOrder(2302);
			Global.offBlockTimer = 5;
			if ((bool)GetComponent<Rigidbody>())
			{
				GetComponent<Rigidbody>().useGravity = false;
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
			DontDropTimer = 10;
		}
	}

	public virtual bool GetToSlot()
	{
		int result;
		if ((bool)SlotObject)
		{
			result = 0;
		}
		else
		{
			GameObject[] array = OrderByName("Slot");
			int num = 0;
			GameObject[] array2 = array;
			int length = array2.Length;
			while (true)
			{
				if (num < length)
				{
					if (array2[num].transform.childCount == 0)
					{
						MakeAction();
						Global.offBlockTimer = 5;
						transform.parent = array2[num].transform;
						SlotObject = array2[num];
						SetOrder(2301);
						if ((bool)GetComponent<Rigidbody>())
						{
							GetComponent<Rigidbody>().velocity = Vector3.zero;
							GetComponent<Rigidbody>().useGravity = false;
							GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
						}
						result = 1;
						break;
					}
					num++;
					continue;
				}
				result = 0;
				break;
			}
		}
		return (byte)result != 0;
	}

	public virtual void GetThatSlot(GameObject go)
	{
		if (go.transform.childCount <= 0)
		{
			selected = null;
			transform.parent = go.transform;
			Global.offBlockTimer = 5;
			SlotObject = go;
			SetOrder(2301);
			if ((bool)GetComponent<Rigidbody>())
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().useGravity = false;
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
		}
	}

	public virtual void SetOrder(int num)
	{
		Component[] componentsInChildren = GetComponentsInChildren(typeof(Renderer));
		if (num > 0)
		{
			int i = 0;
			Component[] array = componentsInChildren;
			for (int length = array.Length; i < length; i++)
			{
				((Renderer)array[i]).material.shader = MatB;
			}
		}
		else
		{
			int j = 0;
			Component[] array2 = componentsInChildren;
			for (int length2 = array2.Length; j < length2; j++)
			{
				((Renderer)array2[j]).material.shader = MatB;
			}
		}
		int k = 0;
		Component[] array3 = componentsInChildren;
		for (int length3 = array3.Length; k < length3; k++)
		{
			((Renderer)array3[k]).sortingOrder = num;
		}
	}

	public virtual GameObject[] OrderByName(string tag)
	{
		GameObject[] array = null;
		UnityScript.Lang.Array array2 = new UnityScript.Lang.Array();
		UnityScript.Lang.Array array3 = new UnityScript.Lang.Array();
		array = GameObject.FindGameObjectsWithTag(tag);
		int i = 0;
		GameObject[] array4 = array;
		for (int length = array4.Length; i < length; i++)
		{
			array2.Push(array4[i].name);
		}
		array2.Sort();
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(array2);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is string))
			{
				obj = RuntimeServices.Coerce(obj, typeof(string));
			}
			string text = (string)obj;
			int j = 0;
			GameObject[] array5 = array;
			for (int length2 = array5.Length; j < length2; j++)
			{
				if (array5[j].name == text)
				{
					array3.Push(array5[j]);
				}
			}
		}
		return (GameObject[])array3.ToBuiltin(typeof(GameObject));
	}

	public virtual void Save()
	{
	}

	public virtual void Load()
	{
	}

	public virtual void CreateNewItem(string name)
	{
		GameObject gameObject = SlotLib.CreateObj(name);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.005f), transform.rotation) as GameObject;
		gameObject2.name = gameObject.name;
		gameObject2.SendMessage("GetThatItem", this.gameObject, SendMessageOptions.DontRequireReceiver);
		EscapeItem = true;
	}

	public virtual void OnMouseOver()
	{
		if (ForBuy)
		{
			Global.MouseMode = "gold";
		}
		else
		{
			Global.MouseMode = "just";
		}
		Monitor.LastOverTrans = transform;
		Monitor.TextA = NameItem;
		string empty = string.Empty;
		if (ForBuy)
		{
			empty = ((Global.Gold < Price) ? ("Price: " + Price + " gold. You don't have enough gold") : ("Price: " + Price + " gold. L.Mouse - buy it"));
		}
		else
		{
			empty += "L.Mouse - grab it ";
			empty = empty + " " + TextBUseText2;
		}
		Monitor.TextB = empty + TextBPlus;
		Monitor.DontDrop = true;
		if ((bool)SlotObject)
		{
			Monitor.MouseNo = false;
		}
		else
		{
			Monitor.MouseNo = true;
		}
		if (!Monitor.dist && SlotObject == null && !selected)
		{
			Global.offBlockTimer = 0;
		}
		else
		{
			Global.offBlockTimer = 5;
		}
		if (Input.GetMouseButtonDown(0))
		{
			onMouseDown();
		}
		if (Input.GetButtonDown("Use") && !ForBuy && (Monitor.dist || !(SlotObject == null)))
		{
			MakeAction();
			gameObject.SendMessage("TryToSave", gameObject, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("UseThatItem", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void MakeAction()
	{
		if ((bool)Global.CurrentPlayerObject)
		{
			Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void MakeBuy()
	{
		ForBuy = true;
		GetComponent<Rigidbody>().useGravity = false;
	}

	public virtual void UseThatItem()
	{
		if (UseThat)
		{
			AudioSource.PlayClipAtPoint(LoadData.SFX("use"), transform.position);
			Global.HP += Global.MaxHP * ((float)HP / 100f);
			if (!(Global.HP <= Global.MaxHP))
			{
				Global.HP = Global.MaxHP;
			}
			Global.Var["food"] = Convert.ToInt32(Global.Var["food"]) + Hungry;
			if (Hungry < 0)
			{
				Global.CreateText("+" + Mathf.Abs(Hungry) + " Food!", transform.position + new Vector3(0f, 0f, -1f), new Color(1f, 0.8f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
			}
			if (Convert.ToInt32(Global.Var["food"]) > 100)
			{
				Global.Var["food"] = 100;
			}
			if (Convert.ToInt32(Global.Var["food"]) < 0)
			{
				Global.Var["food"] = 0;
			}
			Global.Var["mind"] = Convert.ToInt32(Global.Var["mind"]) + Crazy;
			if (Convert.ToInt32(Global.Var["mind"]) > 100)
			{
				Global.Var["mind"] = 100;
			}
			if (Convert.ToInt32(Global.Var["mind"]) < 0)
			{
				Global.Var["mind"] = 0;
			}
			Global.Var["poison"] = Convert.ToInt32(Global.Var["poison"]) + Poison;
			if (Convert.ToInt32(Global.Var["poison"]) > 100)
			{
				Global.Var["poison"] = 100;
			}
			if (Convert.ToInt32(Global.Var["poison"]) < 0)
			{
				Global.Var["poison"] = 0;
			}
			if ((bool)SFXByUse)
			{
				Global.CreateSFX(SFXByUse, transform.position, 1f, 1f);
			}
			if (!string.IsNullOrEmpty(CreateItemByUse) && CreateItemByUse != string.Empty)
			{
				CreateNewItem(CreateItemByUse);
			}
			if (DestroyByUse)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void Main()
	{
	}
}
