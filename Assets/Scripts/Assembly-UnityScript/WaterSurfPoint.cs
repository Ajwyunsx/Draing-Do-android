using System;
using UnityEngine;

[Serializable]
public class WaterSurfPoint : MonoBehaviour
{
	public virtual void Start()
	{
		Global.SurfaceWaterPoint = transform.position.y;
	}

	public virtual void Main()
	{
	}
}
