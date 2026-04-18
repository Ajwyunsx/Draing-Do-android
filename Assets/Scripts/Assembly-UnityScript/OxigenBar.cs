using System;
using UnityEngine;

[Serializable]
public class OxigenBar : MonoBehaviour
{
	private Transform myTransform;

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		float x = 1f * (float)Global.Oxygen / (float)Global.MaxOxygen;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void Main()
	{
	}
}
