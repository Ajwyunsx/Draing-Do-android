using System;
using UnityEngine;

[Serializable]
public class Listener : MonoBehaviour
{
	private Transform myTransform;

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if ((bool)Global.CurrentPlayerObject)
		{
			myTransform.position = Global.CurrentPlayerObject.position;
		}
	}

	public virtual void Main()
	{
	}
}
