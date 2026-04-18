using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("MY SCRIPTS/GlueEffect")]
public class ChainBreakByTouch : MonoBehaviour
{
	private Vector3 startPosition;

	private Transform myTransform;

	public virtual void Start()
	{
		myTransform = transform;
		startPosition = myTransform.position;
	}

	public virtual void FixedUpdate()
	{
		myTransform.position = startPosition;
		if ((bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		enabled = false;
	}

	public virtual void Main()
	{
	}
}
