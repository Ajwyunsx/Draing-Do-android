using System;
using UnityEngine;

[Serializable]
public class AutoScale : MonoBehaviour
{
	public Vector3 alwaysScale;

	private Transform myTransform;

	public AutoScale()
	{
		alwaysScale = Vector3.zero;
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (alwaysScale != Vector3.zero)
		{
			float x = myTransform.localScale.x + alwaysScale.x * Mathf.Sign(myTransform.localScale.x);
			Vector3 localScale = myTransform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (myTransform.localScale = localScale);
			float y = myTransform.localScale.y + alwaysScale.y;
			Vector3 localScale2 = myTransform.localScale;
			float num2 = (localScale2.y = y);
			Vector3 vector3 = (myTransform.localScale = localScale2);
			float z = myTransform.localScale.z + alwaysScale.z;
			Vector3 localScale3 = myTransform.localScale;
			float num3 = (localScale3.z = z);
			Vector3 vector5 = (myTransform.localScale = localScale3);
		}
	}

	public virtual void Main()
	{
	}
}
