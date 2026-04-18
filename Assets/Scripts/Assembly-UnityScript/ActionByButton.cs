using System;
using UnityEngine;

[Serializable]
public class ActionByButton : MonoBehaviour
{
	public string MODE;

	[HideInInspector]
	public bool START;

	public string ID_CARD;

	public float SetA;

	public float SetB;

	public float SetTime;

	[HideInInspector]
	public int Timing;

	public bool Once;

	private bool locOnce;

	public bool Invert;

	[HideInInspector]
	public float InitA;

	[HideInInspector]
	public float InitB;

	[HideInInspector]
	public float SpeedX;

	[HideInInspector]
	public float SpeedY;

	private bool onceAudio;

	public GameObject waveObject;

	private bool okID;

	public bool OnStart;

	private int beginTimer;

	public bool NoBeginTimer;

	public string KeyVar;

	public ActionByButton()
	{
		MODE = "move";
		ID_CARD = string.Empty;
	}

	public virtual void ID_Check(string name)
	{
		if (!okID && ID_CARD == name)
		{
			okID = true;
			ByButton(true);
			if ((bool)waveObject)
			{
				UnityEngine.Object.Instantiate(waveObject, transform.position + new Vector3(0f, 0f, -2f), Quaternion.identity);
			}
			if (!onceAudio && (bool)GetComponent<AudioSource>())
			{
				onceAudio = true;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public virtual void Awake()
	{
		if (!NoBeginTimer)
		{
			beginTimer = 20;
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
		}
		if (OnStart)
		{
			ByButton(true);
		}
	}

	public virtual void FixedUpdate()
	{
		if (beginTimer > 0)
		{
			beginTimer--;
		}
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

	public virtual void ByButton(bool @bool)
	{
		if (Once && locOnce)
		{
			return;
		}
		if (Invert)
		{
			@bool = !@bool;
		}
		locOnce = true;
		if (!OnStart && beginTimer > 0)
		{
			SetTime = 1f;
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
		case "delete":
			UnityEngine.Object.Destroy(gameObject);
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
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player" && KeyVar != null && KeyVar != string.Empty && Convert.ToInt32(Global.Var[KeyVar]) > 0)
		{
			ByButton(true);
		}
	}

	public virtual void Main()
	{
	}
}
