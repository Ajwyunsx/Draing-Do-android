using System;
using UnityEngine;

[Serializable]
public class RndAnimatorSpeed : MonoBehaviour
{
	public float MinAnimSpeed;

	public float MaxAnimSpeed;

	public RndAnimatorSpeed()
	{
		MinAnimSpeed = 0.75f;
		MaxAnimSpeed = 1.25f;
	}

	public virtual void Awake()
	{
		Animator component = GetComponent<Animator>();
		if ((bool)component)
		{
			component.speed = UnityEngine.Random.Range(MinAnimSpeed, MaxAnimSpeed);
		}
	}

	public virtual void Main()
	{
	}
}
