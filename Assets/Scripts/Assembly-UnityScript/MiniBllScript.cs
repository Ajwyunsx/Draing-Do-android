using System;
using UnityEngine;

[Serializable]
public class MiniBllScript : MonoBehaviour
{
	public GameObject DotGO;

	public Transform target;

	public Vector3 Speed;

	public float MaxSpeed;

	public bool DontMove;

	public int DotTimer;

	public int BeforeTimer;

	public MiniBllScript()
	{
		MaxSpeed = 0.5f;
		BeforeTimer = 50;
	}

	public virtual void Start()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CheckPoint");
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (array2[i].name == Global.FromDirection)
			{
				transform.position = array2[i].transform.position;
			}
			if (array2[i].name == Global.ToDirection)
			{
				target = array2[i].transform;
			}
		}
		DontMove = true;
	}

	public virtual void FixedUpdate()
	{
		if (BeforeTimer > 0)
		{
			BeforeTimer--;
			if (BeforeTimer <= 0)
			{
				DontMove = false;
			}
		}
		if (!DontMove)
		{
			DotTimer++;
			if (DotTimer >= 12)
			{
				DotTimer = 0;
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(DotGO);
				Global.LastCreatedObject.transform.position = transform.position + new Vector3(0f, 0f, 0.1f);
			}
			MoveTo(target.position, 0.1f);
			float x = transform.position.x + Speed.x;
			Vector3 position = transform.position;
			float num = (position.x = x);
			Vector3 vector = (transform.position = position);
			float y = transform.position.y + Speed.y;
			Vector3 position2 = transform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (transform.position = position2);
		}
	}

	public virtual void MoveTo(Vector3 targ, float dist)
	{
		float num = Distance(transform.position, targ);
		Vector3 normalized = (targ - transform.position).normalized;
		if (!(num <= dist))
		{
			Speed.x = normalized.x * MaxSpeed;
			Speed.y = normalized.y * MaxSpeed;
		}
		else
		{
			DontMove = true;
			Global.LoadLEVEL(Global.ToDirection, "derp");
		}
	}

	public virtual float Distance(Vector3 trans1, Vector3 trans2)
	{
		return Vector2.Distance(new Vector2(trans1.x, trans1.y), new Vector2(trans2.x, trans2.y));
	}

	public virtual void Main()
	{
	}
}
