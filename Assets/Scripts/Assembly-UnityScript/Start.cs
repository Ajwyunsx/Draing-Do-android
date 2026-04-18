using System;
using UnityEngine;

[Serializable]
public class Start : MonoBehaviour
{
	public int timer;

	public string LoadToLevel;

	public string EscToLevel;

	public bool HUD_Toggle;

	public int Delay;

	public Start()
	{
		EscToLevel = string.Empty;
	}

	public virtual void Awake()
	{
		Global.HUD_ON = HUD_Toggle;
		if (EscToLevel == string.Empty)
		{
			EscToLevel = LoadToLevel;
		}
	}

	public virtual void Update()
	{
		Delay++;
		if (Delay <= 100)
		{
			return;
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			if (EscToLevel != "exit")
			{
				Application.LoadLevel(EscToLevel);
			}
			else
			{
				Application.Quit();
			}
		}
		else if (Input.anyKey && timer == 0 && Global.FadeMode == 0)
		{
			timer++;
			Camera.main.SendMessage("FadeOn", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void FixedUpdate()
	{
		if (timer > 0)
		{
			timer++;
			if (timer > 50)
			{
				Application.LoadLevel(LoadToLevel);
			}
		}
	}

	public virtual void Main()
	{
	}
}
