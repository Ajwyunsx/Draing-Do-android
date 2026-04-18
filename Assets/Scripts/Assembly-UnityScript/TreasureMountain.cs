using System;
using UnityEngine;

[Serializable]
public class TreasureMountain : MonoBehaviour
{
	private Transform MyTransform;

	private float InitPosition;

	private int OldGold;

	public float max;

	public float factor;

	public TreasureMountain()
	{
		max = 37.5f;
		factor = 0.0001f;
	}

	public virtual void Awake()
	{
		MyTransform = transform;
		InitPosition = MyTransform.position.y;
	}

	public virtual void FixedUpdate()
	{
		if (OldGold != Global.Gold)
		{
			OldGold = Global.Gold;
			float num = (float)Global.Gold * factor;
			if (!(num <= max))
			{
				num = max;
			}
			float y = InitPosition + num;
			Vector3 position = MyTransform.position;
			float num2 = (position.y = y);
			Vector3 vector = (MyTransform.position = position);
		}
	}

	public virtual void Main()
	{
	}
}
