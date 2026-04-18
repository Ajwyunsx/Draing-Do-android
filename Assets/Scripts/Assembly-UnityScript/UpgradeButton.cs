using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class UpgradeButton : MonoBehaviour
{
	public int plusTime;

	public bool CheckDistanceForUse;

	public string Name;

	public string UpgradeName;

	public Resource[] Materials;

	private string NameBack;

	public bool DontWorkWithSelected;

	private bool Inactive;

	public bool LeftClick;

	public bool RightClick;

	public Collider coll;

	private int timerCheck;

	[NonSerialized]
	public static UnityScript.Lang.Array numbers = new UnityScript.Lang.Array();

	public UpgradeButton()
	{
		plusTime = 1;
		Name = "Camp Upgrade";
		UpgradeName = "CampTent";
		DontWorkWithSelected = true;
	}

	public virtual void OnMouseOver()
	{
		Monitor.LastOverTrans = transform;
		Monitor.TextA = Name;
		Monitor.TextB = NameBack;
		Monitor.DontDrop = true;
		Monitor.MouseNo = CheckDistanceForUse;
		if (DontWorkWithSelected && (bool)SlotItem.selected)
		{
			Monitor.ForceNo = true;
		}
		if (LeftClick)
		{
			Global.offBlockTimer = 5;
			if (Input.GetMouseButtonDown(0))
			{
				onMouseDown();
			}
		}
		if (RightClick && Input.GetMouseButtonDown(1))
		{
			onMouseDown();
		}
	}

	public virtual void Start()
	{
		coll = GetComponent<Collider>();
		CheckInit();
	}

	public virtual void FixedUpdate()
	{
		timerCheck++;
		if (timerCheck <= 20)
		{
			return;
		}
		timerCheck = 0;
		int num = Convert.ToInt32(Global.Var[UpgradeName]);
		if (Extensions.get_length((System.Array)Materials) <= num || Extensions.get_length((System.Array)Materials[num].name) == 0)
		{
			return;
		}
		if (!CheckAllTools(num))
		{
			if (!Inactive)
			{
				Inactive = true;
				float a = 0.4f;
				Color color = GetComponent<Renderer>().material.color;
				float num2 = (color.a = a);
				Color color2 = (GetComponent<Renderer>().material.color = color);
				gameObject.SendMessage("SetOnModify", false, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (Inactive)
		{
			Inactive = false;
			int num3 = 1;
			Color color4 = GetComponent<Renderer>().material.color;
			float num4 = (color4.a = num3);
			Color color5 = (GetComponent<Renderer>().material.color = color4);
			gameObject.SendMessage("SetOnModify", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CheckInit()
	{
		int num = Convert.ToInt32(Global.Var[UpgradeName]);
		if (Extensions.get_length((System.Array)Materials) <= num)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		if (Extensions.get_length((System.Array)Materials[num].name) == 0)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		if (!CheckAllTools(num))
		{
			Inactive = true;
			float a = 0.4f;
			Color color = GetComponent<Renderer>().material.color;
			float num2 = (color.a = a);
			Color color2 = (GetComponent<Renderer>().material.color = color);
			gameObject.SendMessage("SetOnModify", false, SendMessageOptions.DontRequireReceiver);
		}
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		UnityScript.Lang.Array array2 = new UnityScript.Lang.Array();
		for (int i = 0; i < Extensions.get_length((System.Array)Materials[num].name); i++)
		{
			bool flag = false;
			for (int j = 0; j < array2.length; j++)
			{
				if (RuntimeServices.EqualityOperator(array2[j], Materials[num].name[i]))
				{
					flag = true;
					array[j] = Convert.ToInt32(array[j]) + 1;
					break;
				}
			}
			if (!flag)
			{
				array2.Add(Materials[num].name[i]);
				array.Add(1);
			}
		}
	}

	public virtual void onMouseDown()
	{
		if ((CheckDistanceForUse && !Monitor.dist) || (bool)SlotItem.selected)
		{
			return;
		}
		int num = Convert.ToInt32(Global.Var[UpgradeName]);
		if (Extensions.get_length((System.Array)Materials) > num && CheckAllTools(num))
		{
			for (int i = 0; i < numbers.length; i++)
			{
				Global.Tools[RuntimeServices.UnboxInt32(numbers[i])] = null;
			}
			Global.Var[UpgradeName] = num + 1;
			if (plusTime > 0)
			{
				Global.ChangeTime(Global.DAYTIME + plusTime, true);
				Global.DAYMINUTES = 0;
			}
			if ((bool)Global.CurrentPlayerObject)
			{
				Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
			}
			Slot.DontSaveSlots = true;
			Global.LoadLEVEL(Application.loadedLevelName, string.Empty);
		}
	}

	public virtual bool CheckAllTools(int lvl)
	{
		numbers = new UnityScript.Lang.Array();
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		UnityScript.Lang.Array array2 = new UnityScript.Lang.Array();
		UnityScript.Lang.Array array3 = new UnityScript.Lang.Array();
		string text = null;
		int num = default(int);
		int num2 = default(int);
		for (num2 = 0; num2 < Extensions.get_length((System.Array)Materials[lvl].name); num2++)
		{
			text = Materials[lvl].name[num2];
			bool flag = false;
			for (num = 0; num < array.length; num++)
			{
				if (RuntimeServices.EqualityOperator(array[num], text))
				{
					flag = true;
					array2[num] = Convert.ToInt32(array2[num]) + 1;
					break;
				}
			}
			if (!flag)
			{
				array.Add(text);
				array2.Add(1);
			}
		}
		for (num2 = 0; num2 < array2.length; num2++)
		{
			array3.Add(0);
		}
		for (num2 = 0; num2 < Extensions.get_length((System.Array)Global.Tools); num2++)
		{
			text = Global.Tools[num2];
			for (num = 0; num < array.length; num++)
			{
				if (RuntimeServices.EqualityOperator(array[num], text))
				{
					array3[num] = Convert.ToInt32(array3[num]) + 1;
					if (Convert.ToInt32(array3[num]) <= Convert.ToInt32(array2[num]))
					{
						numbers.Add(num2);
					}
					break;
				}
			}
		}
		if (!Inactive)
		{
			NameBack = "R.Mouse to Upgrade. Need items: ";
		}
		else
		{
			NameBack = "Need items in Inventory: ";
		}
		string lhs = string.Empty;
		string empty = string.Empty;
		for (int i = 0; i < array.length; i++)
		{
			if (i > 0)
			{
				lhs = ", ";
			}
			int num3 = RuntimeServices.UnboxInt32(array2[i]);
			empty = ((num3 <= 1) ? string.Empty : (" " + num3));
			NameBack += lhs + (array[i] as string) + empty;
		}
		num2 = 0;
		int result;
		while (true)
		{
			if (num2 < array.length)
			{
				if (Convert.ToInt32(array3[num2]) < Convert.ToInt32(array2[num2]))
				{
					result = 0;
					break;
				}
				num2++;
				continue;
			}
			result = 1;
			break;
		}
		return (byte)result != 0;
	}

	public virtual void Main()
	{
	}
}
