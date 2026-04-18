using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class RatsRun : MonoBehaviour
{
	public float Speed;

	public int Dir;

	private Vector3 StartScale;

	private Transform myTransform;

	private int timer;

	public RatsRun()
	{
		Dir = 1;
	}

	public virtual void Awake()
	{
		GetComponent<Animation>()["rat_run"].speed = 2f;
		myTransform = transform;
		StartScale = myTransform.localScale;
	}

	public virtual void Command(int dir)
	{
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
		float z = Global.LastCreatedObject.transform.position.z - 0.2f;
		Vector3 position = Global.LastCreatedObject.transform.position;
		float num = (position.z = z);
		Vector3 vector = (Global.LastCreatedObject.transform.position = position);
		GameObject gameObject = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("aMichsRatsRun");
		IEnumerator enumerator = array.GetEnumerator();
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is GameObject))
			{
				obj = RuntimeServices.Coerce(obj, typeof(GameObject));
			}
			gameObject = (GameObject)obj;
			if (this.gameObject != gameObject && gameObject.name == "aMichsRat")
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
				Global.LastCreatedObject.transform.position = gameObject.transform.position;
				float z2 = Global.LastCreatedObject.transform.position.z - 0.2f;
				Vector3 position2 = Global.LastCreatedObject.transform.position;
				float num2 = (position2.z = z2);
				Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.gameObject.name = "aMichsRat";
		Dir = dir;
	}

	public virtual void FixedUpdate()
	{
		float x = Speed * (float)Dir;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num = (velocity.x = x);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
	}

	public virtual void Update()
	{
		float x = (float)Dir * StartScale.x;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (timer < 10)
		{
			timer++;
			return;
		}
		int i = 0;
		ContactPoint[] contacts = other.contacts;
		for (int length = contacts.Length; i < length; i++)
		{
			if (!(contacts[i].normal.x > -0.9f))
			{
				Dir = -1;
				break;
			}
			if (!(contacts[i].normal.x < 0.9f))
			{
				Dir = 1;
				break;
			}
		}
	}

	public virtual void Main()
	{
	}
}
