using System;
using UnityEngine;

[Serializable]
public class GasOff : MonoBehaviour
{
	[HideInInspector]
	public bool once;

	public int POWER;

	public GasOff()
	{
		POWER = 1;
	}

	public virtual void GasOFF(int hp)
	{
		if (!once)
		{
			once = true;
			((ParticleEmitter)gameObject.GetComponent(typeof(ParticleEmitter))).emit = false;
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.tag != "Player"))
		{
			other.gameObject.SendMessage("GasTouchDanger", new Vector3(transform.position.x, transform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
