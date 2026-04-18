using System;
using UnityEngine;

[Serializable]
public class Clock : MonoBehaviour
{
	private int oldTime;

	public Transform direction;

	public float Correcter;

	public int Dir;

	public Clock()
	{
		oldTime = -1;
		Dir = 1;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (oldTime != Global.DAYTIME)
		{
			oldTime = Global.DAYTIME;
			float z = ((float)(Global.DAYTIME * -15) + Correcter) * (float)Dir;
			Vector3 localEulerAngles = direction.localEulerAngles;
			float num = (localEulerAngles.z = z);
			Vector3 vector = (direction.localEulerAngles = localEulerAngles);
		}
	}

	public virtual void Main()
	{
	}
}
