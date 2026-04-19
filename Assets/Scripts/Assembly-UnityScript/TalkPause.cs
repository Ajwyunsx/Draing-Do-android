using System;
using UnityEngine;

[Serializable]
public class TalkPause : MonoBehaviour
{
	public GameObject obj;

	private bool oldPause;

	[NonSerialized]
	public static int menu;

	private Joystick jumpTouchPad;

	private Joystick actionTouchPad;

	private Joystick rollTouchPad;

	private bool oldJumpTouchState;

	private bool oldActionTouchState;

	private bool oldRollTouchState;

	private void CheckTouchPads()
	{
		if (!Global.MobilePlatform)
		{
			return;
		}
		MobileInputBridge.EnsureDefaultControls();
		if (!(bool)jumpTouchPad)
		{
			jumpTouchPad = MobileInputBridge.FindJoystick("JumpJoystick");
		}
		if (!(bool)actionTouchPad)
		{
			actionTouchPad = MobileInputBridge.FindJoystick("ActionJoystick");
		}
		if (!(bool)rollTouchPad)
		{
			rollTouchPad = MobileInputBridge.FindJoystick("RollJoystick");
		}
	}

	private bool TouchAdvanceDown()
	{
		CheckTouchPads();
		bool flag = MobileInputBridge.IsTouchDown(actionTouchPad, ref oldActionTouchState);
		bool flag2 = MobileInputBridge.IsTouchDown(jumpTouchPad, ref oldJumpTouchState);
		bool flag3 = MobileInputBridge.IsTouchDown(rollTouchPad, ref oldRollTouchState);
		return flag || flag2 || flag3;
	}

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
		bool flag = TouchAdvanceDown();
		if (Global.ASKS == null && (Input.GetButtonDown("Strike") || Input.GetButtonDown("Use") || Input.GetButtonDown("Shift") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || flag) && Global.StopTalkText && !Global.Pause && (bool)Global.TalkWindow)
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
