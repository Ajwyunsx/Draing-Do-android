using System;
using UnityEngine;

[Serializable]
public class ParticleOff : MonoBehaviour
{
	public bool on;

	public int POWER;

	public bool Invert;

	public ParticleOff()
	{
		on = true;
		POWER = 1;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.tag != "Player"))
		{
			other.gameObject.SendMessage("TouchDanger", new Vector3(transform.position.x, transform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ByButton(bool on)
	{
		if (Invert)
		{
			on = !on;
		}
		if ((bool)GetComponent<AudioSource>())
		{
			if (on)
			{
				GetComponent<AudioSource>().Play();
			}
			else
			{
				GetComponent<AudioSource>().Stop();
			}
		}
		((ParticleEmitter)gameObject.GetComponent(typeof(ParticleEmitter))).emit = on;
		GetComponent<Collider>().enabled = on;
	}

	public virtual void Main()
	{
	}
}
