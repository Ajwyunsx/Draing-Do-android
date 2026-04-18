using System;
using UnityEngine;

[Serializable]
public class RendererEnable : MonoBehaviour
{
	public virtual void Awake()
	{
		GetComponent<Renderer>().enabled = true;
	}

	public virtual void Main()
	{
	}
}
