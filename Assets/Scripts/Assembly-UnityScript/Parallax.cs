using System;
using UnityEngine;

[Serializable]
public class Parallax : MonoBehaviour
{
	[HideInInspector]
	public Transform cam;

	[HideInInspector]
	public Vector3 myPosition;

	[HideInInspector]
	public Transform myTransform;

	public float ScrollX;

	public float ScrollY;

	public Parallax()
	{
		ScrollX = 0.5f;
		ScrollY = 0.5f;
	}

	public virtual void Start()
	{
		cam = Camera.main.transform;
		myPosition = transform.position;
		myTransform = transform;
	}

	public virtual void Update()
	{
		myTransform.position = new Vector3(myPosition.x + cam.position.x * ScrollX, myPosition.y + cam.position.y * ScrollY, myPosition.z);
	}

	public virtual void Main()
	{
	}
}
