using System;
using UnityEngine;

[Serializable]
public class AutoReSpeedParticle : MonoBehaviour
{
	[HideInInspector]
	public Transform _parent;

	[HideInInspector]
	public int _set;

	[HideInInspector]
	public ParticleSystem _partSpeed;

	[HideInInspector]
	public float _initSpeed;

	[HideInInspector]
	public PonyControl _scriptHero;

	[HideInInspector]
	public int timer;

	public virtual void Start()
	{
		_parent = transform.parent.transform;
		_partSpeed = (ParticleSystem)GetComponent("ParticleSystem");
		_initSpeed = _partSpeed.startSpeed;
		_scriptHero = (PonyControl)Global.CurrentPlayerObject.GetComponent("PonyControl");
		_partSpeed.enableEmission = false;
	}

	public virtual void FixedUpdate()
	{
		if (!_scriptHero)
		{
			return;
		}
		if (_set >= 0 && _scriptHero.Direction < 0)
		{
			_set = -1;
			_partSpeed.startSpeed = _initSpeed;
		}
		if (_set <= 0 && _scriptHero.Direction > 0)
		{
			_set = 1;
			_partSpeed.startSpeed = 0f - _initSpeed;
		}
		if (timer > 0)
		{
			timer--;
			if (timer == 0)
			{
				_partSpeed.enableEmission = true;
			}
		}
	}

	public virtual void SprayON()
	{
		timer = 5;
	}

	public virtual void SprayOFF()
	{
		_partSpeed.enableEmission = false;
	}

	public virtual void Main()
	{
	}
}
