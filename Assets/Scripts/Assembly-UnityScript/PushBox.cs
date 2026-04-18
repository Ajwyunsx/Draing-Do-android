using System;
using UnityEngine;

[Serializable]
public class PushBox : MonoBehaviour
{
	public float factorMass;

	public float powerCrush;

	private Rigidbody rigid;

	private float lastSpeed;

	public PushBox()
	{
		factorMass = 0.9f;
		powerCrush = 10f;
	}

	public virtual void Start()
	{
		rigid = GetComponent<Rigidbody>();
	}

	public virtual void FixedUpdate()
	{
		float x = rigid.velocity.x * factorMass;
		Vector3 velocity = rigid.velocity;
		float num = (velocity.x = x);
		Vector3 vector = (rigid.velocity = velocity);
		lastSpeed = rigid.velocity.y;
		if (!(LevelCore.DeadZone <= transform.position.y))
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (!(lastSpeed > -2f) && !(other.transform.position.y >= transform.position.y))
		{
			other.gameObject.SendMessage("CrushHP", powerCrush, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
