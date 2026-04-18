using System;
using UnityEngine;

[Serializable]
public class TalkPause : MonoBehaviour
{
	public GameObject obj;

	private bool oldPause;

	[NonSerialized]
	public static int menu;

	public virtual void OnEnable()
	{
		menu = Mathf.Max(0, menu) + 1;
		oldPause = Global.Pause;
	}

	public virtual void OnDisable()
	{
		menu = Mathf.Max(0, menu - 1);
	}

	public static bool IsGameplayBlocked()
	{
		if (menu <= 0)
		{
			return false;
		}
		return Global.Pause || Global.MenuPause || Global.BlockEscape || (bool)Global.TalkWindow || (bool)Global.MenuWindow || (bool)Global.YesNoWindow || Time.timeScale <= 0.001f;
	}

	public virtual void Update()
	{
		if (Global.ASKS == null && (Input.GetButtonDown("Strike") || Input.GetButtonDown("Use") || Input.GetButtonDown("Shift") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && Global.StopTalkText && !Global.Pause && (bool)Global.TalkWindow)
		{
			UnityEngine.Object.Destroy(Global.TalkWindow);
		}
		if (Global.Pause == oldPause)
		{
			return;
		}
		oldPause = Global.Pause;
		if (oldPause)
		{
			if ((bool)obj)
			{
				obj.SetActive(false);
			}
		}
		else if ((bool)obj)
		{
			obj.SetActive(true);
		}
	}

	public virtual void Main()
	{
	}
}
