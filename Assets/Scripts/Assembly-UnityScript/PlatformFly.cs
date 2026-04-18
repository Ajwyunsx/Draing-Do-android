using System;
using UnityEngine;

[Serializable]
public class PlatformFly : MonoBehaviour
{
	public float Speed;

	public int Dir;

	public bool isVertical;

	private Transform myTransform;

	public bool ON;

	public PlatformFly()
	{
		Speed = 4f;
		Dir = 1;
		ON = true;
	}

	public virtual void Start()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (ON)
		{
			if (!isVertical)
			{
				float x = Speed * (float)Dir;
				Vector3 velocity = GetComponent<Rigidbody>().velocity;
				float num = (velocity.x = x);
				Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
			}
			else
			{
				float y = Speed * (float)Dir;
				Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
				float num2 = (velocity2.y = y);
				Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
			}
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if ((other.gameObject.tag == "Player" && !(other.transform.position.y <= myTransform.position.y)) || (other.gameObject.layer == 12 && !(other.transform.position.y <= myTransform.position.y)))
		{
			return;
		}
		if (!isVertical)
		{
			int i = 0;
			ContactPoint[] contacts = other.contacts;
			for (int length = contacts.Length; i < length; i++)
			{
				if (!(contacts[i].normal.x > -0.5f))
				{
					Dir = -1;
				}
				if (!(contacts[i].normal.x < 0.5f))
				{
					Dir = 1;
				}
			}
			return;
		}
		int j = 0;
		ContactPoint[] contacts2 = other.contacts;
		for (int length2 = contacts2.Length; j < length2; j++)
		{
			if (!(contacts2[j].normal.y > -0.5f))
			{
				Dir = -1;
			}
			if (!(contacts2[j].normal.y < 0.5f))
			{
				Dir = 1;
			}
		}
	}

	public virtual void ByButton(bool @bool)
	{
		ON = @bool;
	}

	public virtual void Main()
	{
	}
}
