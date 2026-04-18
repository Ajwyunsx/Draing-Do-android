using System;
using UnityEngine;

[Serializable]
public class TimerAction : MonoBehaviour
{
	public string MODE;

	public string Name;

	public string Name2;

	[HideInInspector]
	public bool START;

	public float SetA;

	public float SetB;

	public float SetTime;

	[HideInInspector]
	public int Timing;

	public bool Invert;

	private float InitA;

	private float InitB;

	private float SpeedX;

	private float SpeedY;

	private tk2dSprite sprite;

	private int Timer;

	public int TIMER_ON;

	public int TIMER_OFF;

	public int TIMER_END;

	public TimerAction()
	{
		MODE = string.Empty;
		Name = string.Empty;
		Name2 = string.Empty;
	}

	public virtual void Start()
	{
		if (TIMER_OFF < TIMER_ON)
		{
			TIMER_OFF = TIMER_ON + 1;
		}
		if (TIMER_END < TIMER_OFF)
		{
			TIMER_END = TIMER_OFF + 1;
		}
		switch (MODE)
		{
		case "Light":
		case "light":
			InitA = GetComponent<Light>().intensity;
			InitB = GetComponent<Light>().range;
			break;
		case "Move":
		case "move":
			InitA = transform.position.x;
			InitB = transform.position.y;
			SetA = transform.position.x + SetA;
			SetB = transform.position.y + SetB;
			break;
		case "Rotate":
		case "rotate":
			InitA = transform.eulerAngles.z;
			SetA = transform.eulerAngles.z + SetA;
			break;
		case "Scale":
		case "scale":
			InitA = transform.localScale.x;
			InitB = transform.localScale.y;
			break;
		case "Show":
		case "show":
			if (GetComponent<Renderer>().enabled)
			{
				InitA = 1f;
			}
			else
			{
				InitA = 0f;
			}
			break;
		case "Play":
			break;
		case "play":
			break;
		case "Sprite":
		case "sprite":
			sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
			sprite.spriteId = (int)SetA;
			break;
		case "load":
		case "Load":
			sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
			break;
		}
	}

	public virtual void FixedUpdate()
	{
		TimerWork();
		if (!START)
		{
			return;
		}
		switch (MODE)
		{
		case "Move":
		case "move":
		{
			Timing++;
			if (!((float)Timing < SetTime))
			{
				START = false;
			}
			float x2 = transform.position.x + SpeedX;
			Vector3 position = transform.position;
			float num4 = (position.x = x2);
			Vector3 vector7 = (transform.position = position);
			float y2 = transform.position.y + SpeedY;
			Vector3 position2 = transform.position;
			float num5 = (position2.y = y2);
			Vector3 vector9 = (transform.position = position2);
			break;
		}
		case "Rotate":
		case "rotate":
		{
			Timing++;
			if (!((float)Timing < SetTime))
			{
				START = false;
			}
			float z = transform.eulerAngles.z + SpeedX;
			Vector3 eulerAngles = transform.eulerAngles;
			float num3 = (eulerAngles.z = z);
			Vector3 vector5 = (transform.eulerAngles = eulerAngles);
			break;
		}
		case "Scale":
		case "scale":
		{
			if (!(Mathf.Abs(transform.localScale.x - SpeedX) > 0.05f) && !(Mathf.Abs(transform.localScale.y - SpeedY) > 0.05f))
			{
				START = false;
			}
			float x = Mathf.Lerp(transform.localScale.x, SpeedX, SetTime);
			Vector3 localScale = transform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (transform.localScale = localScale);
			float y = Mathf.Lerp(transform.localScale.y, SpeedY, SetTime);
			Vector3 localScale2 = transform.localScale;
			float num2 = (localScale2.y = y);
			Vector3 vector3 = (transform.localScale = localScale2);
			break;
		}
		}
	}

	public virtual void TimerWork()
	{
		Timer++;
		int timer = Timer;
		if (timer == TIMER_ON)
		{
			ByButton(true);
		}
		else if (timer == TIMER_OFF)
		{
			ByButton(false);
		}
		if (Timer >= TIMER_END)
		{
			Timer = 0;
		}
	}

	public virtual void ByButton(bool @bool)
	{
		if (Invert)
		{
			@bool = !@bool;
		}
		switch (MODE)
		{
		case "Light":
		case "light":
			if (@bool)
			{
				GetComponent<Light>().intensity = SetA;
				GetComponent<Light>().range = SetB;
			}
			else
			{
				GetComponent<Light>().intensity = InitA;
				GetComponent<Light>().range = InitB;
			}
			break;
		case "Move":
		case "move":
			if (@bool)
			{
				SpeedX = (SetA - transform.position.x) / SetTime;
				SpeedY = (SetB - transform.position.y) / SetTime;
			}
			else
			{
				SpeedX = (InitA - transform.position.x) / SetTime;
				SpeedY = (InitB - transform.position.y) / SetTime;
			}
			START = true;
			Timing = 0;
			break;
		case "Rotate":
		case "rotate":
			if (@bool)
			{
				SpeedX = (SetA - transform.eulerAngles.z) / SetTime;
			}
			else
			{
				SpeedX = (InitA - transform.eulerAngles.z) / SetTime;
			}
			START = true;
			Timing = 0;
			break;
		case "Scale":
		case "scale":
			if (@bool)
			{
				SpeedX = SetA;
				SpeedY = SetB;
			}
			else
			{
				SpeedX = InitA;
				SpeedY = InitB;
			}
			START = true;
			break;
		case "Show":
			break;
		case "show":
			break;
		case "Play":
			break;
		case "play":
			break;
		case "Script":
			break;
		case "script":
			break;
		case "Sprite":
		case "sprite":
			if (@bool)
			{
				sprite.spriteId = (int)SetA;
			}
			else
			{
				sprite.spriteId = (int)SetB;
			}
			break;
		case "load":
		case "Load":
			Global.LoadLEVEL(Name, Name2);
			break;
		}
	}

	public virtual void Main()
	{
	}
}
