using System;
using UnityEngine;

[Serializable]
public class ScaleDirCorrection : MonoBehaviour
{
	private Vector3 StartScale;

	private Transform myTransform;

	public virtual void Start()
	{
		myTransform = transform;
		StartScale = myTransform.localScale;
	}

	public virtual void Update()
	{
		float x = StartScale.x;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void Main()
	{
	}
}
