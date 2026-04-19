using UnityEngine;

public static class MobileInputBridge
{
	public static void EnsureDefaultControls()
	{
		if (!Global.MobilePlatform)
		{
			return;
		}
		EnsureJoystick("MoveJoystick", false);
		EnsureJoystick("JumpJoystick", false);
		EnsureJoystick("ActionJoystick", true);
		EnsureJoystick("PauseJoystick", false);
	}

	private static Joystick EnsureJoystick(string name, bool normalize)
	{
		Joystick joystick = FindJoystick(name);
		if ((bool)joystick)
		{
			return joystick;
		}
		GameObject gameObject = new GameObject(name);
		Joystick joystick2 = (Joystick)gameObject.AddComponent(typeof(Joystick));
		joystick2.touchPad = true;
		joystick2.normalize = normalize;
		joystick2.deadZone = Vector2.zero;
		return joystick2;
	}

	public static Joystick FindJoystick(string name)
	{
		GameObject gameObject = GameObject.Find(name);
		if (!(bool)gameObject)
		{
			return null;
		}
		return (Joystick)gameObject.GetComponent(typeof(Joystick));
	}

	public static float GetAxis(Joystick joystick, bool horizontal, float deadZone)
	{
		if (!(bool)joystick)
		{
			return 0f;
		}
		float num = (!horizontal) ? joystick.position.y : joystick.position.x;
		if (Mathf.Abs(num) < deadZone)
		{
			return 0f;
		}
		return Mathf.Clamp(num, -1f, 1f);
	}

	public static bool GetEdge(bool currentState, ref bool oldState)
	{
		bool result = currentState && !oldState;
		oldState = currentState;
		return result;
	}

	public static bool IsTouchDown(Joystick joystick, ref bool oldState)
	{
		bool currentState = (bool)joystick && joystick.IsFingerDown();
		return GetEdge(currentState, ref oldState);
	}

	public static bool IsMoveDownHeld(Joystick joystick, float minY, float dominance)
	{
		if (!(bool)joystick || !joystick.IsFingerDown())
		{
			return false;
		}
		float num = Mathf.Abs(joystick.position.x);
		float num2 = 0f - joystick.position.y;
		return num2 >= minY && num2 >= num * dominance;
	}
}
