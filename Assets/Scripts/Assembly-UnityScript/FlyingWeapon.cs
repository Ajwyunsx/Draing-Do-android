using System;
using UnityEngine;

[Serializable]
public class FlyingWeapon : MonoBehaviour
{
	public Vector2 SpeedXY;

	public float gravitation;

	private int deTimer;

	private int Speed;

	private Rigidbody rigid;

	public float AngularVelocity;

	public int LifeTime;

	public bool NoWalls;

	public FlyingWeapon()
	{
		SpeedXY = new Vector2(15f, 5f);
		gravitation = 0.5f;
	}

	public virtual void Awake()
	{
		Global.Boomerang = true;
		if (!(Global.CurrentPlayerObject.position.x >= transform.position.x))
		{
			Speed = 1;
		}
		else
		{
			Speed = -1;
		}
		rigid = transform.GetComponent<Rigidbody>();
		float x = SpeedXY.x * (float)Speed;
		Vector3 velocity = rigid.velocity;
		float num = (velocity.x = x);
		Vector3 vector = (rigid.velocity = velocity);
		float y = SpeedXY.y;
		Vector3 velocity2 = rigid.velocity;
		float num2 = (velocity2.y = y);
		Vector3 vector3 = (rigid.velocity = velocity2);
	}

	public virtual void FixedUpdate()
	{
		if (LifeTime > 0)
		{
			LifeTime--;
			if (LifeTime <= 0)
			{
				Beat();
			}
		}
		if (deTimer > 0)
		{
			deTimer--;
			float x = Mathf.Lerp(transform.localScale.x, 0f, 0.01f);
			Vector3 localScale = transform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (transform.localScale = localScale);
			float y = Mathf.Lerp(transform.localScale.y, 0f, 0.01f);
			Vector3 localScale2 = transform.localScale;
			float num2 = (localScale2.y = y);
			Vector3 vector3 = (transform.localScale = localScale2);
			if (deTimer <= 0)
			{
				Global.Boomerang = false;
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		else
		{
			float y2 = rigid.velocity.y - gravitation;
			Vector3 velocity = rigid.velocity;
			float num3 = (velocity.y = y2);
			Vector3 vector5 = (rigid.velocity = velocity);
			float z = rigid.transform.localEulerAngles.z - AngularVelocity * (float)Speed;
			Vector3 localEulerAngles = rigid.transform.localEulerAngles;
			float num4 = (localEulerAngles.z = z);
			Vector3 vector7 = (rigid.transform.localEulerAngles = localEulerAngles);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (deTimer <= 0 && !(other.gameObject.tag == "Player"))
		{
			CrushF(other.gameObject);
			if (!NoWalls)
			{
				Beat();
			}
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (deTimer <= 0 && !(other.gameObject.tag == "Player"))
		{
			CrushF(other.gameObject);
			if (!NoWalls && other.gameObject.layer != 12 && !(other.gameObject.tag == "Rope") && !other.gameObject.GetComponent<Collider>().isTrigger)
			{
				Beat();
			}
		}
	}

	public virtual void CrushF(GameObject go)
	{
		Global.LastStrike = new SendStrike();
		Global.LastStrike.trans = Global.CurrentPlayerObject;
		Global.LastStrike.pow = Perks.GetPOWER();
		Global.LastStrike.multy = Global.skillPower;
		Global.LastStrike.clan = "hero";
		go.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Beat()
	{
		float x = rigid.velocity.x * (-0.1f + UnityEngine.Random.Range(-0.1f, 0.1f));
		Vector3 velocity = rigid.velocity;
		float num = (velocity.x = x);
		Vector3 vector = (rigid.velocity = velocity);
		float y = rigid.velocity.y * (-0.5f + UnityEngine.Random.Range(0.1f, 0.4f));
		Vector3 velocity2 = rigid.velocity;
		float num2 = (velocity2.y = y);
		Vector3 vector3 = (rigid.velocity = velocity2);
		float z = UnityEngine.Random.Range(-100f, 100f);
		Vector3 angularVelocity = rigid.angularVelocity;
		float num3 = (angularVelocity.z = z);
		Vector3 vector5 = (rigid.angularVelocity = angularVelocity);
		deTimer = 25;
	}

	public virtual void Main()
	{
	}
}
