using System;
using UnityEngine;

[Serializable]
public class CorrectScaleRoom : MonoBehaviour
{
	private float scale;

	private Transform myTransform;

	public virtual void Start()
	{
		myTransform = transform;
		scale = myTransform.localScale.x;
	}

	public virtual void Update()
	{
		if (!(scale >= 0f))
		{
			float x = scale;
			Vector3 localScale = myTransform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (myTransform.localScale = localScale);
		}
	}

	public virtual void Main()
	{
	}
}
