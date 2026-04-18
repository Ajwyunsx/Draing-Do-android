using System;
using UnityEngine;

[Serializable]
public class RandomDetails : MonoBehaviour
{
	public GameObject SpawnObject;

	public Vector2 count;

	public Vector2 RandomAngle;

	public Vector2 RandomScale;

	public RandomDetails()
	{
		count = new Vector2(3f, 5f);
		RandomAngle = new Vector2(-15f, 15f);
		RandomScale = new Vector2(1f, 1f);
	}

	public virtual void Start()
	{
	}

	public virtual void Main()
	{
	}
}
