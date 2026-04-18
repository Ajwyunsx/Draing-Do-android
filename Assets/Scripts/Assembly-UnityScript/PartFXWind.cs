using System;
using UnityEngine;

[Serializable]
public class PartFXWind : MonoBehaviour
{
	private ParticleEmitter emitter;

	public float speedFactor;

	public bool onlyDay;

	public bool onlyNight;

	public bool ByWindControl;

	private int OldDAYTIME;

	public Vector2 RandomEmission;

	public PartFXWind()
	{
		speedFactor = 1f;
		ByWindControl = true;
		OldDAYTIME = -100000;
		RandomEmission = new Vector2(0f, 10f);
	}

	public virtual void Start()
	{
		emitter = (ParticleEmitter)GetComponentInChildren(typeof(ParticleEmitter));
	}

	public virtual void FixedUpdate()
	{
		if (!emitter || OldDAYTIME == Global.DAYTIME)
		{
			return;
		}
		OldDAYTIME = Global.DAYTIME;
		if (onlyDay)
		{
			if (Global.DAYTIME > 5 && Global.DAYTIME < 21)
			{
				emitter.emit = true;
				if (Global.DAYRAIN > 40 && !(Global.DAYCLOUD < -6f) && !(Global.DAYCLOUD >= 27f))
				{
					emitter.emit = false;
				}
			}
			else
			{
				emitter.emit = false;
			}
		}
		if (onlyNight)
		{
			if (Global.DAYTIME > 5 && Global.DAYTIME < 21)
			{
				emitter.emit = false;
			}
			else
			{
				emitter.emit = true;
			}
		}
		if (ByWindControl)
		{
			float x = (0f - Global.DAYWIND) * speedFactor;
			Vector3 worldVelocity = emitter.worldVelocity;
			float num = (worldVelocity.x = x);
			Vector3 vector = (emitter.worldVelocity = worldVelocity);
		}
		emitter.maxEmission = UnityEngine.Random.Range(RandomEmission.x, RandomEmission.y);
	}

	public virtual void Main()
	{
	}
}
