using System;
using UnityEngine;

[Serializable]
public class Boomerang : MonoBehaviour
{
	private float MagnetPower;

	private float Speed;

	public virtual void Awake()
	{
		Global.Boomerang = true;
		MagnetPower = 0f;
		if (!(Global.CurrentPlayerObject.position.x >= transform.position.x))
		{
			Speed = 15f;
		}
		else
		{
			Speed = -15f;
		}
	}

	public virtual void FixedUpdate()
	{
		if (!(MagnetPower >= 2f))
		{
			MagnetPower += 0.003f;
		}
		float speed = Speed;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num = (velocity.x = speed);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		Speed *= 0.975f;
		float z = Mathf.Atan2(Global.CurrentPlayerObject.position.y - transform.position.y, Global.CurrentPlayerObject.position.x - transform.position.x) * 57.29578f - 90f;
		Vector3 vector3 = Quaternion.Euler(0f, 0f, z) * Vector3.down;
		transform.position -= vector3 * MagnetPower;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Capture();
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag == "Player"))
		{
			CrushF(other.gameObject);
			if (other.gameObject.layer != 12 && !(other.gameObject.tag == "Rope") && !other.gameObject.GetComponent<Collider>().isTrigger)
			{
				Speed = 0f;
			}
		}
	}

	public virtual void Capture()
	{
		if (!(MagnetPower <= 0.1f))
		{
			Global.Boomerang = false;
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void CrushF(GameObject go)
	{
		Global.LastStrike = new SendStrike();
		Global.LastStrike.trans = transform;
		Global.LastStrike.pow = Perks.GetPOWER();
		Global.LastStrike.multy = Global.skillPower;
		Global.LastStrike.clan = "hero";
		go.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
