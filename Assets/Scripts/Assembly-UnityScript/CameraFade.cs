using System;
using UnityEngine;

[Serializable]
public class CameraFade : MonoBehaviour
{
	[HideInInspector]
	public float alphaFadeValue;

	[HideInInspector]
	public Texture blackTexture;

	[NonSerialized]
	public static float SpeedFactor;

	[NonSerialized]
	public static bool reset;

	public CameraFade()
	{
		alphaFadeValue = 1f;
	}

	public virtual void Awake()
	{
		SpeedFactor = 0.75f;
	}

	public virtual void Start()
	{
		blackTexture = LoadData.TEX("blackTexture") as Texture;
		Global.FadeMode = -1;
	}

	public virtual void OnGUI()
	{
		if (Global.Pause)
		{
			if (reset)
			{
				reset = false;
				if (Global.FadeMode == -1)
				{
					Global.FadeMode = 0;
					alphaFadeValue = 0f;
				}
			}
		}
		else if (Global.FadeMode != 0 || alphaFadeValue > 0f)
		{
			if (Global.FadeMode == 1)
			{
				alphaFadeValue = Mathf.Clamp01(alphaFadeValue + Time.deltaTime * SpeedFactor * (float)Global.FadeMode);
			}
			if (Global.FadeMode == -1)
			{
				alphaFadeValue = Mathf.Clamp01(alphaFadeValue + Time.deltaTime * SpeedFactor * (float)Global.FadeMode);
			}
			GUI.color = new Color(alphaFadeValue, alphaFadeValue, alphaFadeValue, alphaFadeValue);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), blackTexture);
			FadeControl();
		}
	}

	public virtual void FadeControl()
	{
		if (Global.FadeMode == -1 && !(alphaFadeValue > 0f))
		{
			Global.FadeMode = 0;
		}
		if (Global.FadeMode != 1 || alphaFadeValue < 1f)
		{
			return;
		}
		Global.FadeMode = 0;
		if (!(Global.HP > 0f))
		{
			if (!string.IsNullOrEmpty(Global.DeadLevel))
			{
				Global.NextLevel = Global.DeadLevel;
				MonoBehaviour.print("NextLevel: " + Global.NextLevel);
			}
			else
			{
				Global.NextLevel = Global.LEVEL;
				MonoBehaviour.print("Default: " + Global.NextLevel);
			}
			Global.HP = Global.MaxHP;
			Global.Reload = true;
		}
	}

	public virtual void FadeOff()
	{
		Global.FadeMode = -1;
	}

	public virtual void FadeOn()
	{
		Global.FadeMode = 1;
	}

	public virtual void Main()
	{
	}
}
