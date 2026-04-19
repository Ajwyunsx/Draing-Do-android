using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GUITexture = LegacyCompat.GUITexture;

[Serializable]
[RequireComponent(typeof(GUITexture))]
public class Joystick : MonoBehaviour
{
	private const float HiddenAlpha = 0.18f;

	private const float VisibleAlpha = 0.38f;

	[NonSerialized]
	private static Joystick[] joysticks;

	[NonSerialized]
	private static bool enumeratedJoysticks;

	[NonSerialized]
	private static float tapTimeDelta = 0.3f;

	[NonSerialized]
	private static Texture2D moveTexture;

	[NonSerialized]
	private static Texture2D jumpTexture;

	[NonSerialized]
	private static Texture2D actionTexture;

	[NonSerialized]
	private static Texture2D rollTexture;

	[NonSerialized]
	private static Texture2D pauseTexture;

	public bool touchPad;

	public Rect touchZone;

	public Vector2 deadZone;

	public bool normalize;

	public Vector2 position;

	public int tapCount;

	private int lastFingerId;

	private float tapTimeWindow;

	private Vector2 fingerDownPos;

	private float fingerDownTime;

	private float firstDeltaTime;

	private GUITexture gui;

	private Rect defaultRect;

	private Boundary guiBoundary;

	private Vector2 guiTouchOffset;

	private Vector2 guiCenter;

	private bool Paused;

	private bool useFallbackVisual;

	private Texture fallbackTexture;

	private Color fallbackColor;

	private Rect GetTouchRect()
	{
		if (touchPad)
		{
			return touchZone;
		}
		if (gui != null)
		{
			return gui.pixelInset;
		}
		return defaultRect;
	}

	private Vector2 GetTouchCenter()
	{
		Rect touchRect = GetTouchRect();
		return new Vector2(touchRect.x + touchRect.width * 0.5f, touchRect.y + touchRect.height * 0.5f);
	}

	private bool ContainsTouchPosition(Vector2 touchPosition)
	{
		if (touchPad)
		{
			return touchZone.Contains(touchPosition);
		}
		return ((bool)gui && gui.HitTest(touchPosition)) || (useFallbackVisual && defaultRect.Contains(touchPosition));
	}

	private bool CanClaimTouch(int fingerId, Vector2 touchPosition)
	{
		if (lastFingerId == fingerId)
		{
			return true;
		}
		Joystick joystick = null;
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		int i = 0;
		Joystick[] array = joysticks;
		for (int length = array.Length; i < length; i++)
		{
			Joystick joystick2 = array[i];
			if (!(bool)joystick2 || !joystick2.enabled || !joystick2.gameObject.activeInHierarchy || joystick2.Paused)
			{
				continue;
			}
			if (joystick2.lastFingerId == fingerId)
			{
				return joystick2 == this;
			}
			if (joystick2.lastFingerId != -1 || !joystick2.ContainsTouchPosition(touchPosition))
			{
				continue;
			}
			Rect touchRect = joystick2.GetTouchRect();
			Vector2 touchCenter = joystick2.GetTouchCenter();
			float num3 = Mathf.Abs(touchPosition.x - touchCenter.x) / Mathf.Max(1f, touchRect.width * 0.5f);
			float num4 = Mathf.Abs(touchPosition.y - touchCenter.y) / Mathf.Max(1f, touchRect.height * 0.5f);
			float num5 = num3 * num3 + num4 * num4;
			float num6 = touchRect.width * touchRect.height;
			if (joystick == null || num5 < num || (Mathf.Approximately(num5, num) && num6 < num2))
			{
				joystick = joystick2;
				num = num5;
				num2 = num6;
			}
		}
		return joystick == null || joystick == this;
	}

	public Joystick()
	{
		deadZone = Vector2.zero;
		lastFingerId = -1;
		firstDeltaTime = 0.5f;
		guiBoundary = new Boundary();
		Paused = true;
	}

	private bool KeepVisibleWhilePaused()
	{
		Scene activeScene = SceneManager.GetActiveScene();
		string name = activeScene.name;
		string path = activeScene.path ?? string.Empty;
		if (name == "players")
		{
			return true;
		}
		return path.Contains("/Minigame Cutscene/") || path.Contains("/end/");
	}

	private Rect GetDefaultRect()
	{
		float num = Mathf.Min(Screen.width, Screen.height);
		float num2 = Mathf.Round(num * 0.045f);
		float num3 = Mathf.Round(num * 0.3f);
		float num4 = Mathf.Round(num * 0.19f);
		float num5 = Mathf.Round(num * 0.11f);
		float num6 = Mathf.Round(num * 0.035f);
		string text = gameObject.name;
		if (text == "MoveJoystick")
		{
			return new Rect(num2, num2, num3, num3);
		}
		if (text == "JumpJoystick")
		{
			return new Rect((float)Screen.width - num4 - num2, num2, num4, num4);
		}
		if (text == "ActionJoystick")
		{
			return new Rect((float)Screen.width - num4 * 2f - num6 - num2, num2, num4, num4);
		}
		if (text == "RollJoystick")
		{
			return new Rect((float)Screen.width - num4 * 2f - num6 - num2, num2 + num4 + num6, num4, num4);
		}
		if (text == "PauseJoystick")
		{
			return new Rect((float)Screen.width - num5 - num2, (float)Screen.height - num5 - num2, num5, num5);
		}
		return new Rect(num2, num2, num4, num4);
	}

	private Color GetFallbackBaseColor()
	{
		string text = gameObject.name;
		if (text == "MoveJoystick")
		{
			return new Color(1f, 1f, 1f, 1f);
		}
		if (text == "JumpJoystick")
		{
			return new Color(0.45f, 0.85f, 1f, 1f);
		}
		if (text == "ActionJoystick")
		{
			return new Color(1f, 0.72f, 0.32f, 1f);
		}
		if (text == "RollJoystick")
		{
			return new Color(0.55f, 1f, 0.55f, 1f);
		}
		if (text == "PauseJoystick")
		{
			return new Color(1f, 1f, 1f, 1f);
		}
		return Color.white;
	}

	private Texture GetFallbackTexture()
	{
		string text = gameObject.name;
		if (text == "MoveJoystick")
		{
			if (moveTexture == null)
			{
				moveTexture = CreateCircleTexture(new Color(1f, 1f, 1f, 0.12f), new Color(1f, 1f, 1f, 0.5f));
			}
			return moveTexture;
		}
		if (text == "JumpJoystick")
		{
			if (jumpTexture == null)
			{
				jumpTexture = CreateCircleTexture(new Color(0.45f, 0.85f, 1f, 0.18f), new Color(0.45f, 0.85f, 1f, 0.65f));
			}
			return jumpTexture;
		}
		if (text == "ActionJoystick")
		{
			if (actionTexture == null)
			{
				actionTexture = CreateCircleTexture(new Color(1f, 0.72f, 0.32f, 0.18f), new Color(1f, 0.72f, 0.32f, 0.65f));
			}
			return actionTexture;
		}
		if (text == "RollJoystick")
		{
			if (rollTexture == null)
			{
				rollTexture = CreateCircleTexture(new Color(0.55f, 1f, 0.55f, 0.18f), new Color(0.55f, 1f, 0.55f, 0.65f));
			}
			return rollTexture;
		}
		if (pauseTexture == null)
		{
			pauseTexture = CreateCircleTexture(new Color(1f, 1f, 1f, 0.14f), new Color(1f, 1f, 1f, 0.55f));
		}
		return pauseTexture;
	}

	private static Texture2D CreateCircleTexture(Color fillColor, Color edgeColor)
	{
		int num = 64;
		Texture2D texture2D = new Texture2D(num, num, TextureFormat.ARGB32, false);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.filterMode = FilterMode.Bilinear;
		texture2D.hideFlags = HideFlags.DontSave;
		float num2 = ((float)num - 1f) * 0.5f;
		float num3 = num2 - 1f;
		float num4 = num3 * 0.78f;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				float num5 = (float)i - num2;
				float num6 = (float)j - num2;
				float num7 = Mathf.Sqrt(num5 * num5 + num6 * num6);
				Color color = Color.clear;
				if (num7 <= num3)
				{
					if (num7 >= num4)
					{
						float t = Mathf.InverseLerp(num3, num4, num7);
						color = Color.Lerp(edgeColor, fillColor, t);
					}
					else
					{
						color = fillColor;
					}
				}
				texture2D.SetPixel(i, j, color);
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	private void SetupFallbackVisual()
	{
		useFallbackVisual = true;
		fallbackTexture = GetFallbackTexture();
		fallbackColor = GetFallbackBaseColor();
		defaultRect = GetDefaultRect();
		touchZone = defaultRect;
		float x = 0f;
		Vector3 vector = transform.position;
		float num = (vector.x = x);
		Vector3 vector2 = (transform.position = vector);
		float y = 0f;
		Vector3 vector4 = transform.position;
		float num2 = (vector4.y = y);
		Vector3 vector5 = (transform.position = vector4);
	}

	private void ApplyVisibility(bool visible)
	{
		float a = (!visible) ? HiddenAlpha : VisibleAlpha;
		if (gui != null)
		{
			Color color = gui.color;
			float num = (color.a = a);
			Color color2 = (gui.color = color);
			gui.enabled = visible;
		}
		else if (useFallbackVisual)
		{
			float num2 = (fallbackColor.a = a);
		}
	}

	public virtual void Start()
	{
		gui = (GUITexture)GetComponent(typeof(GUITexture));
		if (!Global.MobilePlatform)
		{
			if (gui != null)
			{
				gui.enabled = false;
			}
			enabled = false;
			return;
		}
		if (gui == null)
		{
			SetupFallbackVisual();
		}
		else
		{
			defaultRect = gui.pixelInset;
			defaultRect.x += transform.position.x * (float)Screen.width;
			defaultRect.y += transform.position.y * (float)Screen.height;
			float x = 0f;
			Vector3 vector = transform.position;
			float num = (vector.x = x);
			Vector3 vector2 = (transform.position = vector);
			float y = 0f;
			Vector3 vector4 = transform.position;
			float num2 = (vector4.y = y);
			Vector3 vector5 = (transform.position = vector4);
		}
		if (touchPad)
		{
			touchZone = defaultRect;
		}
		else
		{
			guiTouchOffset.x = defaultRect.width * 0.5f;
			guiTouchOffset.y = defaultRect.height * 0.5f;
			guiCenter.x = defaultRect.x + guiTouchOffset.x;
			guiCenter.y = defaultRect.y + guiTouchOffset.y;
			guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
			guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
			guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
			guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
		}
		Paused = Global.Pause || Global.MenuPause;
		if (KeepVisibleWhilePaused())
		{
			Paused = false;
		}
		ApplyVisibility(!Paused);
	}

	public virtual void Disable()
	{
		gameObject.SetActive(false);
		enumeratedJoysticks = false;
	}

	public virtual void ResetJoystick()
	{
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;
		if (gui != null)
		{
			gui.pixelInset = defaultRect;
		}
		if (touchPad)
		{
			ApplyVisibility(false);
		}
	}

	public virtual bool IsFingerDown()
	{
		return lastFingerId != -1;
	}

	public virtual void LatchedFinger(int fingerId)
	{
		if (lastFingerId == fingerId)
		{
			ResetJoystick();
		}
	}

	public virtual void Update()
	{
		if (gui == null && !useFallbackVisual)
		{
			SetupFallbackVisual();
		}
		if (gui == null && !useFallbackVisual)
		{
			enabled = false;
			return;
		}
		bool pausedState = Global.Pause || Global.MenuPause;
		if (KeepVisibleWhilePaused())
		{
			pausedState = false;
		}
		if (pausedState != Paused)
		{
			Paused = pausedState;
			ApplyVisibility(!Paused);
		}
		if (pausedState)
		{
			return;
		}
		if (!enumeratedJoysticks)
		{
			joysticks = ((Joystick[])UnityEngine.Object.FindObjectsOfType(typeof(Joystick))) as Joystick[];
			enumeratedJoysticks = true;
		}
		int touchCount = Input.touchCount;
		if (!(tapTimeWindow <= 0f))
		{
			tapTimeWindow -= Time.deltaTime;
		}
		else
		{
			tapCount = 0;
		}
		if (touchCount == 0)
		{
			ResetJoystick();
		}
		else
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 vector = touch.position - guiTouchOffset;
				bool flag = false;
				flag = ContainsTouchPosition(touch.position);
				if (flag && lastFingerId == -1 && CanClaimTouch(touch.fingerId, touch.position))
				{
					if (touchPad)
					{
						ApplyVisibility(true);
						lastFingerId = touch.fingerId;
						fingerDownPos = touch.position;
						fingerDownTime = Time.time;
					}
					lastFingerId = touch.fingerId;
					if (!(tapTimeWindow <= 0f))
					{
						tapCount++;
					}
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}
					int j = 0;
					Joystick[] array = joysticks;
					for (int length = array.Length; j < length; j++)
					{
						if (array[j] != this)
						{
							array[j].LatchedFinger(touch.fingerId);
						}
					}
				}
				if (lastFingerId == touch.fingerId)
				{
					if (touch.tapCount > tapCount)
					{
						tapCount = touch.tapCount;
					}
					if (touchPad)
					{
						position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2f), -1f, 1f);
						position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2f), -1f, 1f);
					}
					else
					{
						float num3 = Mathf.Clamp(vector.x, guiBoundary.min.x, guiBoundary.max.x);
						Rect pixelInset = gui.pixelInset;
						float num4 = (pixelInset.x = num3);
						Rect rect = (gui.pixelInset = pixelInset);
						float num6 = Mathf.Clamp(vector.y, guiBoundary.min.y, guiBoundary.max.y);
						Rect pixelInset2 = gui.pixelInset;
						float num7 = (pixelInset2.y = num6);
						Rect rect3 = (gui.pixelInset = pixelInset2);
					}
					if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						ResetJoystick();
					}
				}
			}
		}
		if (!touchPad)
		{
			if (gui != null)
			{
				position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;
				position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;
			}
			else
			{
				position = Vector2.zero;
			}
		}
		float num9 = Mathf.Abs(position.x);
		float num10 = Mathf.Abs(position.y);
		if (!(num9 >= deadZone.x))
		{
			position.x = 0f;
		}
		else if (normalize)
		{
			position.x = Mathf.Sign(position.x) * (num9 - deadZone.x) / (1f - deadZone.x);
		}
		if (!(num10 >= deadZone.y))
		{
			position.y = 0f;
		}
		else if (normalize)
		{
			position.y = Mathf.Sign(position.y) * (num10 - deadZone.y) / (1f - deadZone.y);
		}
	}

	public virtual void Main()
	{
	}

	public virtual void OnGUI()
	{
		if (!useFallbackVisual || !isActiveAndEnabled || fallbackTexture == null || fallbackColor.a <= 0f)
		{
			return;
		}
		Rect rect = defaultRect;
		rect.y = (float)Screen.height - rect.y - rect.height;
		Color color = GUI.color;
		GUI.color = fallbackColor;
		GUI.DrawTexture(rect, fallbackTexture, ScaleMode.StretchToFill, true);
		GUI.color = color;
	}
}
