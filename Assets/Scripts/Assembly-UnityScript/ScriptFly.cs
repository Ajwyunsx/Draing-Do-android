using System;
using UnityEngine;

[Serializable]
public class ScriptFly : MonoBehaviour
{
	public Vector3 Speed;

	private Transform myTransform;

	public int LifeTime;

	private bool FlyOff;

	public ScriptFly()
	{
		Speed = new Vector3(0f, -0.1f, 0f);
		LifeTime = 250;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		if (GetComponent<Rigidbody>() == null)
		{
			gameObject.AddComponent<Rigidbody>();
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	public virtual void Update()
	{
		myTransform.position += Speed * Time.deltaTime * 50f;
	}

	public virtual void SetLifeTime(float num)
	{
		LifeTime = (int)num;
	}

	public virtual void DISAPPEAR()
	{
		FlyOff = true;
	}

	public virtual void FixedUpdate()
	{
		LifeTime--;
		if (LifeTime < 1)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "ZoneDelete")
		{
			MonoBehaviour.print("ZONE: " + gameObject.name);
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void ChangeSpeed(Vector3 speed)
	{
		Speed = speed;
	}

	public virtual void OnTime()
	{
		LifeTime = 2;
	}

	public virtual void Main()
	{
	}
}
