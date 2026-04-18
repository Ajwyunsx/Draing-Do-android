using System;
using UnityEngine;

[Serializable]
public class WaterMove : MonoBehaviour
{
	public float scrollSpeed;

	public float scrollSpeedY;

	public bool invert;

	public bool windControl;

	public bool isRain;

	private int OldDAYTIME;

	public bool Anim;

	private int AnimTimer;

	public int AnimPause;

	public bool Bump;

	public WaterMove()
	{
		scrollSpeed = 0.1f;
		OldDAYTIME = -100000;
	}

	public virtual void FixedUpdate()
	{
		if (Anim)
		{
			if (AnimTimer < AnimPause)
			{
				AnimTimer++;
				return;
			}
			AnimTimer = 0;
		}
		float num = Time.time * scrollSpeed;
		float y = Time.time * scrollSpeedY;
		if (windControl)
		{
			num *= 0.2f * Global.DAYWIND;
		}
		if (invert)
		{
			num = 0f - num;
		}
		if (!Bump)
		{
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(num, y);
		}
		else
		{
			GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(num, y));
		}
		if (isRain)
		{
			if (Global.CheckRain())
			{
				GetComponent<Renderer>().enabled = true;
			}
			else
			{
				GetComponent<Renderer>().enabled = false;
			}
			if (OldDAYTIME != Global.DAYTIME)
			{
				OldDAYTIME = Global.DAYTIME;
				float z = Global.DAYWIND * -3f;
				Vector3 eulerAngles = transform.eulerAngles;
				float num2 = (eulerAngles.z = z);
				Vector3 vector = (transform.eulerAngles = eulerAngles);
			}
		}
	}

	public virtual void Main()
	{
	}
}
