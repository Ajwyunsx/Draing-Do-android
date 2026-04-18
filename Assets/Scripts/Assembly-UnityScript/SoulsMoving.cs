using System;
using UnityEngine;

[Serializable]
public class SoulsMoving : MonoBehaviour
{
	private Vector3 Pose;

	private float Speed;

	public float speed;

	public bool Own;

	public int LifeTimer;

	public GameObject PowerFX;

	public bool MASTER;

	public GameObject Target;

	public bool ONTRUE;

	public bool OnlyInWater;

	public float gravityPower;

	private bool InTheWater;

	public SoulsMoving()
	{
		speed = 12f;
		LifeTimer = 300;
		gravityPower = 10f;
	}

	public virtual void DISAPPEAR()
	{
		ONTRUE = false;
	}

	public virtual void Start()
	{
		if (!Own)
		{
			GetComponent<Collider>().isTrigger = false;
		}
		else
		{
			Effect();
		}
		Speed = speed * UnityEngine.Random.Range(0.9f, 1.1f);
		if (!MASTER)
		{
			Pose = new Vector3(UnityEngine.Random.Range(-3.1f, 3.1f), UnityEngine.Random.Range(-3.1f, 3.1f), 0f);
		}
	}

	public virtual void ModeON()
	{
		ONTRUE = true;
	}

	public virtual void FixedUpdate()
	{
		if (!ONTRUE)
		{
			return;
		}
		bool flag = true;
		if (OnlyInWater && !InTheWater)
		{
			GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f - gravityPower, 0f));
			flag = false;
		}
		if (flag)
		{
			if (!MASTER && UnityEngine.Random.Range(0, 100) == 98)
			{
				Pose = new Vector3(UnityEngine.Random.Range(-4.1f, 4.1f), UnityEngine.Random.Range(0.1f, 2.1f), 0f);
			}
			Vector3 vector = Global.CurrentPlayerObject.position + Pose - transform.position;
			GetComponent<Rigidbody>().AddForceAtPosition(vector.normalized * Speed, transform.position);
		}
		Vector3 vector2 = Global.CurrentPlayerObject.position - transform.position;
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		transform.rotation = Quaternion.AngleAxis(num + 180f, Vector3.forward);
		InTheWater = false;
	}

	public virtual void InWater()
	{
		InTheWater = true;
	}

	public virtual void SetOwn()
	{
		if (!Own)
		{
			Own = true;
			GetComponent<Collider>().isTrigger = true;
			Effect();
		}
	}

	public virtual void OnMouseUp()
	{
		SetOwn();
		MonoBehaviour.print("WOOOOOOOW!");
	}

	public virtual void Effect()
	{
		if ((bool)PowerFX)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(PowerFX);
			gameObject.transform.position = transform.position;
		}
	}

	public virtual void Insert()
	{
		if ((bool)PowerFX)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(PowerFX);
			gameObject.transform.position = transform.position;
		}
		UnityEngine.Object.Destroy(this.gameObject);
	}

	public virtual void Main()
	{
	}
}
