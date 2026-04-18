using System;
using UnityEngine;

[Serializable]
public class SinWaving : MonoBehaviour
{
	[HideInInspector]
	public Vector3 StartPosition;

	public Vector3 WavingPower;

	private float WavingCorrection;

	public float Speed;

	public bool YWaving;

	public SinWaving()
	{
		Speed = 1f;
		YWaving = true;
	}

	public virtual void Start()
	{
		StartPosition = transform.position;
	}

	public virtual void FixedUpdate()
	{
		if (YWaving)
		{
			if (!(WavingCorrection >= WavingPower.y))
			{
				WavingCorrection += 0.01f;
			}
			float y = StartPosition.y + Mathf.Sin(Time.time * Speed) * WavingCorrection;
			Vector3 position = transform.position;
			float num = (position.y = y);
			Vector3 vector = (transform.position = position);
		}
		else
		{
			if (!(WavingCorrection >= WavingPower.x))
			{
				WavingCorrection += 0.01f;
			}
			float x = StartPosition.x + Mathf.Sin(Time.time * Speed) * WavingCorrection;
			Vector3 position2 = transform.position;
			float num2 = (position2.x = x);
			Vector3 vector3 = (transform.position = position2);
		}
	}

	public virtual void Main()
	{
	}
}
