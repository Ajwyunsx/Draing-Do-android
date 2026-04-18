using System;
using UnityEngine;

[Serializable]
public class ScreenHUDPos : MonoBehaviour
{
	public Vector2 Pos;

	public virtual void Start()
	{
		float z = Vector3.Distance(transform.position, Camera.main.transform.position);
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Pos.x, Pos.y, z));
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void Main()
	{
	}
}
