using System;
using UnityEngine;

[Serializable]
public class Cobweb : MonoBehaviour
{
	[NonSerialized]
	public static Transform Catch;

	private Vector3 CatchAxis;

	public float power;

	private int TrapPower;

	public int ChanceToEvadeWeb;

	public Cobweb()
	{
		power = 0.35f;
		ChanceToEvadeWeb = 97;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if ((bool)Catch)
		{
			Global.CatchTimer = 100;
			int num = 0;
			Vector3 velocity = Catch.GetComponent<Rigidbody>().velocity;
			float num2 = (velocity.y = num);
			Vector3 vector = (Catch.GetComponent<Rigidbody>().velocity = velocity);
			Catch.position = Vector3.Lerp(Catch.position, CatchAxis, power);
		}
	}

	public virtual void Update()
	{
		if ((bool)Catch)
		{
			if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
			{
				CatchDown();
			}
			if (Input.GetButtonDown("Shift") || Input.GetButtonDown("Strike"))
			{
				CatchDown();
			}
			if (TrapPower < 0)
			{
				Catch = null;
			}
		}
	}

	public virtual void CatchDown()
	{
		TrapPower--;
		Global.MP -= Global.MaxHP * 0.1f;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		SlotItem.EscapeItem = true;
		if (Global.CatchTimer <= 0 && !Catch && !(other.gameObject.tag != "Player") && UnityEngine.Random.Range(0, 100) >= ChanceToEvadeWeb)
		{
			Catch = other.transform;
			CatchAxis = other.transform.position;
			TrapPower = UnityEngine.Random.Range(10, 20);
		}
	}

	public virtual void Catched()
	{
		TrapPower = 10000000;
	}

	public virtual void Main()
	{
	}
}
