using System;
using UnityEngine;

[Serializable]
public class TimedObjectDestructor : MonoBehaviour
{
	public int timer;

	public TimedObjectDestructor()
	{
		timer = 2;
	}

	public virtual void FixedUpdate()
	{
		if (timer > 0)
		{
			timer--;
			if (timer <= 0 && (bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = false;
			}
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		other.gameObject.SendMessage("CrushHP", 3, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
