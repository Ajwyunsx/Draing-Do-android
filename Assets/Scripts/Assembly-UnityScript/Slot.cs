using System;
using UnityEngine;

[Serializable]
public class Slot : MonoBehaviour
{
	public int SlotNumber;

	private float StartPosition;

	[NonSerialized]
	public static float Timer;

	public bool DontMove;

	[NonSerialized]
	public static bool DontSaveSlots;

	public virtual void Awake()
	{
		gameObject.name = "Slot " + SlotNumber;
	}

	public virtual void Start()
	{
		if (!DontMove)
		{
			StartPosition = transform.localPosition.y;
			float y = StartPosition + 2f;
			Vector3 localPosition = transform.localPosition;
			float num = (localPosition.y = y);
			Vector3 vector = (transform.localPosition = localPosition);
		}
		string text = Global.Tools[SlotNumber];
		if (text == null)
		{
			text = string.Empty;
		}
		if (text != string.Empty)
		{
			GameObject gameObject = SlotLib.CreateObj(text);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.005f), transform.rotation) as GameObject;
			gameObject2.name = gameObject.name;
			gameObject2.SendMessage("GetThatSlot", this.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Update()
	{
		if (DontMove)
		{
			return;
		}
		if (!Global.Pause)
		{
			if (!(Timer + 2f > Time.realtimeSinceStartup))
			{
				float y = Mathf.Lerp(transform.localPosition.y, StartPosition + 2f, Time.deltaTime * 10f);
				Vector3 localPosition = transform.localPosition;
				float num = (localPosition.y = y);
				Vector3 vector = (transform.localPosition = localPosition);
			}
			else
			{
				float y2 = Mathf.Lerp(transform.localPosition.y, StartPosition, Time.deltaTime * 10f);
				Vector3 localPosition2 = transform.localPosition;
				float num2 = (localPosition2.y = y2);
				Vector3 vector3 = (transform.localPosition = localPosition2);
			}
		}
		else
		{
			float y3 = StartPosition + 2f;
			Vector3 localPosition3 = transform.localPosition;
			float num3 = (localPosition3.y = y3);
			Vector3 vector5 = (transform.localPosition = localPosition3);
		}
	}

	public virtual void FixedUpdate()
	{
		if (!DontSaveSlots)
		{
			if (transform.childCount > 0)
			{
				Global.Tools[SlotNumber] = transform.GetChild(0).gameObject.name;
			}
			else
			{
				Global.Tools[SlotNumber] = null;
			}
		}
		if (SlotNumber == 0)
		{
			bool flag = false;
			if ((bool)SlotItem.selected)
			{
				flag = true;
			}
			if (!(Input.mousePosition.y <= (float)Screen.height * 0.7f))
			{
				flag = true;
			}
			if (flag)
			{
				Timer = Time.realtimeSinceStartup;
			}
		}
	}

	public virtual void onMouseDown()
	{
		Monitor.DontDrop = false;
		if ((bool)SlotItem.selected)
		{
			SlotItem.selected.SendMessage("GetThatSlot", gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnMouseOver()
	{
		if (string.IsNullOrEmpty(Global.Tools[SlotNumber]))
		{
			Monitor.TextA = "Inventory slot";
		}
		if ((bool)SlotItem.selected && string.IsNullOrEmpty(Global.Tools[SlotNumber]))
		{
			Monitor.TextB = "Put in your inventory!";
		}
		else
		{
			Monitor.TextB = string.Empty;
		}
		Global.offBlockTimer = 5;
		if (Input.GetMouseButtonDown(0))
		{
			onMouseDown();
		}
	}

	public virtual void Main()
	{
	}
}
