using System;
using UnityEngine;

[Serializable]
public class WindFX : MonoBehaviour
{
	public bool windControl;

	public float resist;

	private float startAngle;

	private Transform myTransform;

	public float Speed;

	public float Amplitude;

	public int RandomInt;

	public bool ONTRUE;

	private int OldDAYTIME;

	public float Naklon;

	public WindFX()
	{
		windControl = true;
		resist = 1f;
		Speed = 1f;
		Amplitude = 7f;
		ONTRUE = true;
		OldDAYTIME = -100000;
	}

	public virtual void Start()
	{
		myTransform = transform;
		startAngle = myTransform.localEulerAngles.z;
		RandomInt = UnityEngine.Random.Range(-1000, 1000);
		Speed *= UnityEngine.Random.Range(0.9f, 1.1f);
	}

	public virtual void FixedUpdate()
	{
		if (!ONTRUE)
		{
			return;
		}
		if (windControl)
		{
			if (OldDAYTIME != Global.DAYTIME)
			{
				OldDAYTIME = Global.DAYTIME;
				Speed = 0.4f * Global.DAYWIND + 0.5f * Mathf.Sign(Global.DAYWIND);
				Amplitude = (1.1f * Global.DAYWIND + 2f * Mathf.Sign(Global.DAYWIND)) * resist;
			}
			float z = startAngle + Global.DAYWIND * Naklon + Mathf.Sin((Time.time + (float)RandomInt) * Speed) * Amplitude;
			Vector3 localEulerAngles = myTransform.localEulerAngles;
			float num = (localEulerAngles.z = z);
			Vector3 vector = (myTransform.localEulerAngles = localEulerAngles);
		}
		else
		{
			float z2 = startAngle + Mathf.Sin((Time.time + (float)RandomInt) * Speed) * Amplitude;
			Vector3 localEulerAngles2 = myTransform.localEulerAngles;
			float num2 = (localEulerAngles2.z = z2);
			Vector3 vector3 = (myTransform.localEulerAngles = localEulerAngles2);
		}
	}

	public virtual void WindStop()
	{
		ONTRUE = false;
	}

	public virtual void WindStart()
	{
		ONTRUE = true;
	}

	public virtual void DISAPPEAR()
	{
		ONTRUE = false;
	}

	public virtual void Main()
	{
	}
}
