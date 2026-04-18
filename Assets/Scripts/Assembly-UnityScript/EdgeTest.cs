using System;
using UnityEngine;

[Serializable]
public class EdgeTest : MonoBehaviour
{
	public int test;

	public Vector3 pose;

	private Transform trans;

	private Transform master;

	public virtual void Start()
	{
		trans = transform;
		master = transform.parent.transform;
		trans.parent = null;
	}

	public virtual void FixedUpdate()
	{
		if (test > 0)
		{
			test--;
		}
	}

	public virtual void Update()
	{
		if ((bool)master)
		{
			transform.position = master.position + pose;
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		test = 5;
	}

	public virtual void Main()
	{
	}
}
