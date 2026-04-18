using System;
using UnityEngine;

[Serializable]
public class AlwaysZero : MonoBehaviour
{
	public virtual void Start()
	{
		transform.position = Vector3.zero;
	}

	public virtual void Main()
	{
	}
}
