using System;
using UnityEngine;

[Serializable]
public class ShotObject : MonoBehaviour
{
	private Transform myTransform;

	public int POWER;

	public GameObject CrushObject;

	public int ShotSpeed;

	public int timer;

	public Vector3 shootDir;

	public string DeadLevel;

	private bool once;

	public int POISON;

	public ShotObject()
	{
		POWER = 10;
		ShotSpeed = 9;
		timer = 1;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		GetComponent<Collider>().enabled = false;
	}

	public virtual void Start()
	{
		shootDir = Quaternion.Euler(0f, 0f, myTransform.eulerAngles.z) * Vector3.down;
		GetComponent<Rigidbody>().velocity = shootDir * ShotSpeed;
		GetComponent<Collider>().enabled = true;
	}

	public virtual void ShotPower(int pow)
	{
		POWER = pow;
	}

	public virtual void Delete()
	{
		if (!once)
		{
			once = true;
			if ((bool)CrushObject)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(CrushObject, myTransform.position, transform.rotation) as GameObject;
				Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
				Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void CrushHP()
	{
		Delete();
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			StrikeIt(other.gameObject);
		}
		Delete();
		if ((bool)CrushObject && other.gameObject.tag == "Player")
		{
			Global.LastCreatedObject.transform.parent = other.gameObject.transform;
			float x = Mathf.Lerp(Global.LastCreatedObject.transform.position.x, other.gameObject.transform.position.x, 0.5f);
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.x = x);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			float y = Mathf.Lerp(Global.LastCreatedObject.transform.position.y, other.gameObject.transform.position.y, 0.5f);
			Vector3 position2 = Global.LastCreatedObject.transform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
			Rigidbody component = Global.LastCreatedObject.GetComponent<Rigidbody>();
			if ((bool)component)
			{
				component.useGravity = false;
				component.isKinematic = true;
				component.detectCollisions = false;
			}
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag != "Tool"))
		{
			Delete();
			if ((bool)CrushObject)
			{
				float x = (float)UnityEngine.Random.Range(9, 12) * (0f - Mathf.Sign(other.gameObject.transform.position.x - myTransform.position.x));
				Vector3 velocity = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity;
				float num = (velocity.x = x);
				Vector3 vector = (Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = velocity);
				int num2 = UnityEngine.Random.Range(-100, 100);
				Vector3 angularVelocity = Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity;
				float num3 = (angularVelocity.z = num2);
				Vector3 vector3 = (Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity);
			}
		}
	}

	public virtual void StrikeIt(GameObject go)
	{
		Global.LastStrike = new SendStrike();
		if (!string.IsNullOrEmpty(DeadLevel))
		{
			Global.DeadLevel = DeadLevel;
		}
		else
		{
			Global.DeadLevel = null;
		}
		Global.LastStrike.trans = myTransform;
		Global.LastStrike.pow = POWER;
		Global.LastStrike.clan = "foe";
		Global.LastStrike.poison = POISON;
		go.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void SetDeadLevel(string text)
	{
		DeadLevel = text;
	}

	public virtual void Main()
	{
	}
}
