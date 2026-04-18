using System;
using UnityEngine;

[Serializable]
public class SimpleNPC : MonoBehaviour
{
	private AI core;

	public virtual void Start()
	{
		core = GetComponent<AI>();
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void Main()
	{
	}
}
