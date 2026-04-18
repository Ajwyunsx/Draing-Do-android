using System;
using UnityEngine;

[Serializable]
public class MinusLight : MonoBehaviour
{
	public float Intensity;

	public MinusLight()
	{
		Intensity = -1f;
	}

	public virtual void Start()
	{
		GetComponent<Light>().intensity = Intensity;
	}

	public virtual void Main()
	{
	}
}
