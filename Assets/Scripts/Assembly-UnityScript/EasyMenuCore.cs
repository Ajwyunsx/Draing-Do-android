using System;
using UnityEngine;

[Serializable]
public class EasyMenuCore : MonoBehaviour
{
	public bool MakePause;

	public GameObject selectorSlot;

	public int MaxSlotX;

	public float correctSize;

	[HideInInspector]
	public string localName;

	private float delay;

	public GameObject target;

	private GameObject old_target;

	[HideInInspector]
	public float timer;

	public bool GlobalBlockEscape;

	public EasyMenuCore()
	{
		correctSize = 3f;
	}

	public virtual void ChangeMaxSlotX(int maxxslot)
	{
		MaxSlotX = maxxslot;
	}

	public virtual void Start()
	{
		Global.BlockEscape = GlobalBlockEscape;
		Global.MenuY = 0;
		Global.MenuX = 1;
		Global.Pause = MakePause;
	}

	public virtual void Update()
	{
		if (Global.YesNoMode)
		{
			return;
		}
		bool flag = false;
		localName = string.Empty;
		if (!(delay + 0.185f >= Time.realtimeSinceStartup) && !(Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0.2f))
		{
			delay = Time.realtimeSinceStartup;
			flag = true;
			float num = (float)Global.MenuX - Mathf.Sign(Input.GetAxisRaw("Vertical"));
			if (!(num >= 1f))
			{
				num = MaxSlotX;
			}
			if (!(num <= (float)MaxSlotX))
			{
				num = 1f;
			}
			Global.MenuX = (int)num;
		}
		if (flag)
		{
			GameObject gameObject = null;
			if (localName == string.Empty)
			{
				gameObject = GameObject.Find("slot" + Global.MenuX + "0");
				target = gameObject;
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
			selectorSlot.transform.localScale = Vector3.Lerp(selectorSlot.transform.localScale, target.GetComponent<Renderer>().bounds.size * correctSize, Global.RealTime * 25f);
		}
		if (Input.GetButtonUp("Enter") || Input.GetKeyUp(KeyCode.Return))
		{
			Global.GlobalObject.GetComponent<AudioSource>().Play();
			target.SendMessage("MenuSlotAction", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
