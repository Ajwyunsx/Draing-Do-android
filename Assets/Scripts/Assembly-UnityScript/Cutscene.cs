using System;
using UnityEngine;

[Serializable]
public class Cutscene : MonoBehaviour
{
	public GameObject MenuWindow;

	public float timer;

	private float oldTime;

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

	public Cutscene()
	{
		timer = 1.5f;
	}

	public virtual void Start()
	{
		oldTime = Time.realtimeSinceStartup;
		CheckTouchPads();
	}

	public virtual void Update()
	{
		bool flag = TouchAdvanceDown();
		if ((Input.GetKeyDown("escape") || Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || flag) && !(timer > Time.realtimeSinceStartup - oldTime))
		{
			EndScene();
			Input.ResetInputAxes();
		}
	}

	public virtual void EndScene()
	{
		if ((bool)MenuWindow)
		{
			Global.CreateMenuWindowObj(MenuWindow);
			return;
		}
		Global.HUD_ON = true;
		Time.timeScale = 1f;
		Global.Pause = false;
	}

	public virtual void Main()
	{
	}
}
