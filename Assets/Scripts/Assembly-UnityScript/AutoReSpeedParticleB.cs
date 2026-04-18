using System;
using UnityEngine;

[Serializable]
public class AutoReSpeedParticleB : MonoBehaviour
{
	[HideInInspector]
	public ParticleEmitter _partSpeed;

	[HideInInspector]
	public int timer;

	public virtual void Start()
	{
		_partSpeed = (ParticleEmitter)GetComponent("ParticleEmitter");
		SprayOFF();
	}

	public virtual void FixedUpdate()
	{
		if (timer > 0)
		{
			timer--;
			if (timer == 0)
			{
				_partSpeed.emit = true;
			}
		}
	}

	public virtual void SprayON()
	{
		timer = 5;
	}

	public virtual void SprayOFF()
	{
		_partSpeed.emit = false;
	}

	public virtual void Main()
	{
	}
}
