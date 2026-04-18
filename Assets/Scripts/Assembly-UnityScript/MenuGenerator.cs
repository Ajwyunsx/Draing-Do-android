using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class MenuGenerator : MonoBehaviour
{
	public GameObject button;

	public GameObject button2;

	public Selector[] variants;

	private Transform[] buttons;

	public int MaxSlotOnPage;

	private int oldCurrentSlotNumber;

	public int CurrentSlotNumber;

	public GameObject ScrollObject;

	private float FactorOfScroll;

	public Transform IconPlace;

	public GameObject TextPlace;

	public GameObject ProgressPlace;

	public GameObject defaultAwardIcon;

	private GameObject Icon;

	public string MODE;

	public MenuGenerator()
	{
		MaxSlotOnPage = 8;
		oldCurrentSlotNumber = -10000000;
	}

	public virtual void Awake()
	{
		int num = 0;
		if (MODE == "Awards")
		{
			variants = new Selector[Extensions.get_length((System.Array)Achiev.Award)];
			for (num = 0; num < Extensions.get_length((System.Array)Achiev.Award); num++)
			{
				variants[num] = new Selector();
				variants[num].on = 1;
				variants[num].name = Lang.Award(Achiev.Award[num].name);
				variants[num].Option = "Awards";
				variants[num].price = 1;
				variants[num].ActionMode = "award";
				variants[num].icon = Achiev.Award[num].icon;
			}
		}
		if (MODE == "Rooms")
		{
			variants = new Selector[Extensions.get_length((System.Array)RoomData.rooms)];
			for (num = 0; num < Extensions.get_length((System.Array)RoomData.rooms); num++)
			{
				variants[num] = new Selector();
				variants[num].on = 1;
				variants[num].name = Lang.Menu(RoomData.rooms[num].name) + " = " + RoomData.rooms[num].price + " $";
				variants[num].Option = "Rooms";
				if (RoomData.rooms[num].on == 1)
				{
					variants[num].price = RoomData.rooms[num].price;
				}
				else
				{
					variants[num].price = 0;
				}
				variants[num].ActionMode = "room";
			}
		}
		buttons = new Transform[Extensions.get_length((System.Array)variants)];
		int num2 = 0;
		int num3 = 0;
		bool flag = default(bool);
		for (int i = 0; i < Extensions.get_length((System.Array)variants); i++)
		{
			if (variants[i].ActionMode != "award")
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(button, new Vector3(transform.position.x, transform.position.y + 0.15f - (float)num3 * 0.06f, transform.position.z - 0.001f), Quaternion.identity);
			}
			switch (variants[i].ActionMode)
			{
			case "room":
				Global.LastCreatedObject.SendMessage("SetGameObject", RoomData.rooms[i].GO, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.SendMessage("SetRealRoomNumber", i, SendMessageOptions.DontRequireReceiver);
				break;
			case "award":
				if (Achiev.Award[i].have != 1)
				{
					variants[i].icon = defaultAwardIcon;
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(button2, new Vector3(transform.position.x, transform.position.y + 0.15f - (float)num3 * 0.06f, transform.position.z - 0.001f), Quaternion.identity);
				}
				else
				{
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(button, new Vector3(transform.position.x, transform.position.y + 0.15f - (float)num3 * 0.06f, transform.position.z - 0.001f), Quaternion.identity);
				}
				Global.LastCreatedObject.SendMessage("SetSpecialName", Achiev.Award[i].name, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.SendMessage("SetProgress", Achiev.Award[i].progress, SendMessageOptions.DontRequireReceiver);
				break;
			case "buy_hair":
				if (Global.HairHero == variants[i].IndexNumber)
				{
					variants[i].price = 0;
				}
				variants[i].name = Lang.Text(variants[i].name) + " = " + variants[i].price + " $";
				break;
			case "buy_tool":
			{
				flag = false;
				for (int k = 0; k <= 14; k++)
				{
					if (Global.Tools[k] == string.Empty + variants[i].icon.name)
					{
						flag = true;
					}
				}
				variants[i].name = Lang.Text(variants[i].name) + " = " + variants[i].price + " $";
				if (flag)
				{
					variants[i].price = 0;
				}
				break;
			}
			case "buy_cloth":
			{
				flag = false;
				for (int j = 0; j <= 14; j++)
				{
					if (Global.Cloths[j] == variants[i].icon.name)
					{
						flag = true;
					}
				}
				variants[i].name = Lang.Text(variants[i].name) + " = " + variants[i].price + " $";
				if (flag)
				{
					variants[i].price = 0;
				}
				break;
			}
			}
			Global.LastCreatedObject.SendMessage("SetPrice", variants[i].price, SendMessageOptions.DontRequireReceiver);
			if (variants[i].price > 0)
			{
				Global.LastCreatedObject.BroadcastMessage("Rename", variants[i].name, SendMessageOptions.DontRequireReceiver);
				if ((bool)variants[i].icon)
				{
					Global.LastCreatedObject.SendMessage("SetIcon", variants[i].icon, SendMessageOptions.DontRequireReceiver);
				}
				if (!string.IsNullOrEmpty(variants[i].ActionMode))
				{
					Global.LastCreatedObject.SendMessage("SetActionMode", variants[i].ActionMode, SendMessageOptions.DontRequireReceiver);
				}
				Global.LastCreatedObject.SendMessage("SetRealOptionNumber", variants[i].IndexNumber, SendMessageOptions.DontRequireReceiver);
				num2++;
				Global.LastCreatedObject.SendMessage("ChangeXNumber", num2 + 1, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.transform.parent = transform;
				buttons[num3] = Global.LastCreatedObject.transform;
				num3++;
			}
			else
			{
				UnityEngine.Object.Destroy(Global.LastCreatedObject);
			}
		}
		gameObject.SendMessage("ChangeMaxSlotX", num2 + 1, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Update()
	{
		if (Global.MenuX > CurrentSlotNumber + 9)
		{
			CurrentSlotNumber += 8;
		}
		if (Global.MenuX <= CurrentSlotNumber + 2)
		{
			CurrentSlotNumber -= 8;
			if (Global.MenuX <= 2)
			{
				CurrentSlotNumber = 0;
			}
		}
		float t = Global.RealTime * 9f;
		for (int i = 0; i < Extensions.get_length((System.Array)buttons) && !(buttons[i] == null); i++)
		{
			float y = Mathf.Lerp(buttons[i].position.y, transform.position.y + 0.15f + (float)(-i + CurrentSlotNumber) * 0.06f, t);
			Vector3 position = buttons[i].position;
			float num = (position.y = y);
			Vector3 vector = (buttons[i].position = position);
		}
	}

	public virtual void ScrollMove()
	{
	}

	public virtual void GetIconFromChild(GameObject obj)
	{
		if ((bool)Icon)
		{
			UnityEngine.Object.Destroy(Icon);
		}
		if ((bool)obj)
		{
			Icon = UnityEngine.Object.Instantiate(obj, IconPlace.position, Quaternion.identity);
			float z = Icon.transform.position.z - 0.01f;
			Vector3 position = Icon.transform.position;
			float num = (position.z = z);
			Vector3 vector = (Icon.transform.position = position);
			Icon.transform.parent = transform;
		}
	}

	public virtual void DeleteIconFromChild()
	{
		if ((bool)Icon)
		{
			UnityEngine.Object.Destroy(Icon);
		}
		if ((bool)ProgressPlace)
		{
			ProgressPlace.SendMessage("Rename", string.Empty, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void SetTextFromChild(string text)
	{
		if ((bool)TextPlace)
		{
			TextPlace.SendMessage("Rename", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ProgressScript(string name)
	{
		if (!(ProgressPlace == null))
		{
			if (name == "Gold")
			{
				ProgressPlace.SendMessage("Rename", Lang.Menu(name) + ": " + Global.Gold, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
