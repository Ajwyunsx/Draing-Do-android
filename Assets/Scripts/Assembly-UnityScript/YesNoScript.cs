using System;
using UnityEngine;

[Serializable]
public class YesNoScript : MonoBehaviour
{
	public GameObject selectorSlot;

	public float correctSize;

	[HideInInspector]
	public string localName;

	private float delay;

	public GameObject target;

	private bool OldBlock;

	[HideInInspector]
	public float timer;

	private bool Once;

	public YesNoScript()
	{
		correctSize = 3f;
	}

	public virtual void Awake()
	{
		OldBlock = Global.BlockEscape;
		Global.BlockEscape = true;
		Global.YesNoMode = true;
		Global.YesX = false;
		target = GameObject.Find("button_No");
		Input.ResetInputAxes();
	}

	public virtual void Update()
	{
		bool flag = false;
		localName = string.Empty;
		if (flag)
		{
			GameObject gameObject = null;
			gameObject = ((!Global.YesX) ? GameObject.Find("button_No") : GameObject.Find("button_Yes"));
			target = gameObject;
		}
		if (Input.GetButtonDown("Escape"))
		{
			Global.YesNoMode = false;
			Global.DOYES(false);
			Once = true;
			UnityEngine.Object.Destroy(this.gameObject);
		}
	}

	public virtual void FinalYes(bool yes)
	{
		if (!Once)
		{
			Once = true;
			if (yes)
			{
				Global.YesX = true;
			}
			Global.YesNoMode = false;
			Global.DOYES(Global.YesX);
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void OnDestroy()
	{
		Global.BlockEscape = OldBlock;
	}

	public virtual void Main()
	{
	}
}
