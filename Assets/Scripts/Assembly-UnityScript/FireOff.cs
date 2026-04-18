using System;
using UnityEngine;

[Serializable]
public class FireOff : MonoBehaviour
{
	[HideInInspector]
	public bool once;

	public int POWER;

	public FireOff()
	{
		POWER = 1;
	}

	public virtual void FireOFF(int hp)
	{
		if (!once)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			once = true;
			((ParticleEmitter)gameObject.GetComponent(typeof(ParticleEmitter))).emit = false;
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (POWER != 0 && !(other.tag != "Player"))
		{
			other.gameObject.SendMessage("TouchDanger", new Vector3(transform.position.x, transform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			other.gameObject.SendMessage("TouchDanger", new Vector3(transform.position.x, transform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
