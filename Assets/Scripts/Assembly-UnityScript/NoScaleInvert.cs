using System;
using UnityEngine;

[Serializable]
public class NoScaleInvert : MonoBehaviour
{
	private Transform parent;

	private float oldX;

	public string Mode;

	public bool CheckTrail;

	private TrailRenderer Trail;

	private Vector3 OldDist;

	public virtual void Awake()
	{
		parent = transform.parent.transform;
		if (CheckTrail)
		{
			Trail = (TrailRenderer)gameObject.GetComponent("TrailRenderer");
		}
	}

	public virtual void Start()
	{
		ReDistTrail();
	}

	public virtual void ReDistTrail()
	{
		OldDist = transform.position;
	}

	public virtual void FixedUpdate()
	{
		if (CheckTrail)
		{
			float num = Vector3.Distance(OldDist, transform.position);
			OldDist = transform.position;
			if (!(num > 0.1f))
			{
				num = 0f;
			}
			if (!(Trail.time >= num * 5f))
			{
				Trail.time = num * 5f;
			}
			else
			{
				Trail.time = Mathf.Lerp(Trail.time, num, 0.5f);
			}
			float num2 = Trail.time * 4f;
			if (!(num2 <= 1.5f))
			{
				num2 = 1.5f;
			}
			Trail.startWidth = num2;
		}
		if (Mode != "x")
		{
			if (oldX != parent.lossyScale.z)
			{
				oldX = parent.lossyScale.z;
				if (!(parent.lossyScale.z >= 0f))
				{
					int num3 = -1;
					Vector3 localScale = transform.localScale;
					float num4 = (localScale.z = num3);
					Vector3 vector = (transform.localScale = localScale);
				}
				if (!(parent.lossyScale.z <= 0f))
				{
					int num5 = 1;
					Vector3 localScale2 = transform.localScale;
					float num6 = (localScale2.z = num5);
					Vector3 vector3 = (transform.localScale = localScale2);
				}
			}
		}
		else if (oldX != parent.lossyScale.x)
		{
			oldX = parent.lossyScale.x;
			if (!(parent.lossyScale.x >= 0f))
			{
				int num7 = -1;
				Vector3 localScale3 = transform.localScale;
				float num8 = (localScale3.x = num7);
				Vector3 vector5 = (transform.localScale = localScale3);
			}
			if (!(parent.lossyScale.x <= 0f))
			{
				int num9 = 1;
				Vector3 localScale4 = transform.localScale;
				float num10 = (localScale4.x = num9);
				Vector3 vector7 = (transform.localScale = localScale4);
			}
		}
	}

	public virtual void Main()
	{
	}
}
