using System;
using UnityEngine;

[Serializable]
public class FairyStrike : MonoBehaviour
{
	public int POWER;

	public bool fairyStrikeEnabled;

	public bool parenting;

	public FairyStrike()
	{
		fairyStrikeEnabled = true;
	}

	public virtual void Awake()
	{
		if (parenting)
		{
			transform.parent = Global.CurrentPlayerObject;
		}
	}

	public virtual void FixedUpdate()
	{
		if (fairyStrikeEnabled)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(12f * Mathf.Sign(transform.localScale.x), 0f, 0f);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (fairyStrikeEnabled)
		{
			other.gameObject.SendMessage("FairyStrike", null, SendMessageOptions.DontRequireReceiver);
		}
		other.gameObject.SendMessage("CrushHP", POWER, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (fairyStrikeEnabled)
		{
			other.gameObject.SendMessage("FairyStrike", null, SendMessageOptions.DontRequireReceiver);
			other.gameObject.SendMessage("CrushHP", POWER, SendMessageOptions.DontRequireReceiver);
		}
		else if (other.gameObject.layer == 17)
		{
			other.gameObject.SendMessage("CrushHP", POWER, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
