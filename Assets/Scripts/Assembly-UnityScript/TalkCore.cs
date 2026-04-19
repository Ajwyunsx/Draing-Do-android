using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class TalkCore : MonoBehaviour
{
	public GameObject TextObject;

	public float timez;

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

	public TalkCore()
	{
		timez = 0.5f;
	}

	public virtual void Awake()
	{
		Input.ResetInputAxes();
		oldTime = Time.realtimeSinceStartup;
		CheckTouchPads();
		MonoBehaviour.print("Awake scene!");
	}

	public virtual void SetTalk(string text)
	{
		string[] array = text.Split("/"[0]);
		for (int i = 0; i < Extensions.get_length((System.Array)array); i++)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(TextObject);
			Global.LastCreatedObject.transform.position = transform.position + new Vector3(0f, -0.2f - (float)i * 0.032f, -0.1f);
			Global.LastCreatedObject.BroadcastMessage("Rename", array[i], SendMessageOptions.DontRequireReceiver);
			Global.LastCreatedObject.transform.parent = transform;
		}
	}

	public virtual void Update()
	{
		bool flag = TouchAdvanceDown();
		if ((Input.GetKeyDown("escape") || Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Strike") || flag) && !(timez > Time.realtimeSinceStartup - oldTime))
		{
			MonoBehaviour.print("escapere");
			Global.Pause = false;
			Global.HUD_ON = true;
			Time.timeScale = 1f;
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
