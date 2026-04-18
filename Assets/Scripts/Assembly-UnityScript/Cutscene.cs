using System;
using UnityEngine;

[Serializable]
public class Cutscene : MonoBehaviour
{
	public GameObject MenuWindow;

	public float timer;

	private float oldTime;

	public Cutscene()
	{
		timer = 1.5f;
	}

	public virtual void Start()
	{
		oldTime = Time.realtimeSinceStartup;
	}

	public virtual void Update()
	{
		if ((Input.GetKeyDown("escape") || Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !(timer > Time.realtimeSinceStartup - oldTime))
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
