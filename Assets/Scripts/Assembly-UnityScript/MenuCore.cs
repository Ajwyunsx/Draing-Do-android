using System;
using UnityEngine;

[Serializable]
public class MenuCore : MonoBehaviour
{
	public GameObject selectorSlot;

	public GameObject exitSlot;

	public GameObject defaultSlot;

	public int defaultSlotX;

	public int defaultSlotY;

	public int MaxSlotX;

	public int MaxSlotY;

	public float correctSize;

	public bool DontSize;

	[HideInInspector]
	public string localName;

	private float delay;

	[HideInInspector]
	public GameObject target;

	[HideInInspector]
	public float timer;

	private GameObject old_target;

	public string MainText;

	public GameObject TextPlace;

	public string TextPlaceBegin;

	public MenuCore()
	{
		correctSize = 3f;
	}

	public virtual void Start()
	{
		Global.MenuX = 1;
		Global.MenuY = 0;
		if ((bool)defaultSlot)
		{
			target = defaultSlot;
			Global.MenuX = defaultSlotX;
			Global.MenuY = defaultSlotY;
		}
		else
		{
			target = exitSlot;
		}
	}

	public virtual void Update()
	{
		if (Global.YesNoMode)
		{
			return;
		}
		bool flag = false;
		localName = string.Empty;
		if (!(delay + 0.2f >= Time.realtimeSinceStartup))
		{
			if (!(Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 0.2f))
			{
				delay = Time.realtimeSinceStartup;
				flag = true;
				float num = (float)Global.MenuX + Mathf.Sign(Input.GetAxisRaw("Horizontal"));
				if (Global.MenuY != 0)
				{
					if (!(num <= (float)MaxSlotX))
					{
						num = 1f;
					}
					if (!(num >= 1f))
					{
						num = MaxSlotX;
					}
					Global.MenuX = (int)num;
				}
				else if (target == exitSlot)
				{
					localName = "toBack";
				}
			}
			if (!(Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0.2f))
			{
				delay = Time.realtimeSinceStartup;
				flag = true;
				float num2 = (float)Global.MenuY - Mathf.Sign(Input.GetAxisRaw("Vertical"));
				if (!(num2 >= 0f))
				{
					num2 = MaxSlotY;
				}
				if (!(num2 <= (float)MaxSlotY))
				{
					num2 = 0f;
				}
				Global.MenuY = (int)num2;
			}
		}
		if (flag)
		{
			GameObject gameObject = null;
			if (localName == string.Empty)
			{
				gameObject = GameObject.Find("slot" + Global.MenuX + Global.MenuY);
			}
			else
			{
				Global.MenuY = 0;
				gameObject = GameObject.Find(localName);
				if (localName == "toBack" && gameObject == null)
				{
					gameObject = GameObject.Find("slot11");
					Global.MenuX = 1;
					Global.MenuY = 1;
				}
			}
			if ((bool)gameObject)
			{
				target = gameObject;
			}
			else
			{
				target = exitSlot;
				Global.MenuX = 1;
				Global.MenuY = 0;
			}
		}
		if (!target)
		{
			return;
		}
		if (old_target != target)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			old_target = target;
		}
		if ((bool)selectorSlot)
		{
			selectorSlot.transform.position = Vector3.Lerp(selectorSlot.transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 0.0075f), Global.RealTime * 25f);
			if (!DontSize)
			{
				selectorSlot.transform.localScale = Vector3.Lerp(selectorSlot.transform.localScale, target.GetComponent<Collider>().bounds.size * correctSize, Global.RealTime * 25f);
			}
		}
		if (Input.GetButtonDown("Enter") || Input.GetKeyDown(KeyCode.Return))
		{
			Global.GlobalObject.GetComponent<AudioSource>().Play();
			target.SendMessage("MenuSlotAction", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void SetTextFromChild(string text)
	{
		if ((bool)TextPlace)
		{
			if (TextPlaceBegin == string.Empty || TextPlaceBegin == null)
			{
				TextPlace.SendMessage("Rename", text, SendMessageOptions.DontRequireReceiver);
			}
			else if (text != string.Empty && text != null)
			{
				TextPlace.SendMessage("Rename", text, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				TextPlace.SendMessage("Rename", Lang.Text(TextPlaceBegin), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
