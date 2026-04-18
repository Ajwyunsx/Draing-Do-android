using System;
using UnityEngine;

[Serializable]
public class EasyMagnet : MonoBehaviour
{
	public Transform magnet;

	public float speed;

	private Transform myTransform;

	private Vector3 StartScale;

	public EasyMagnet()
	{
		speed = 0.045f;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		StartScale = myTransform.localScale;
	}

	public virtual void Update()
	{
		float x = Mathf.Sign(magnet.parent.localScale.x) * StartScale.x;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void FixedUpdate()
	{
		float x = Mathf.Lerp(myTransform.position.x, magnet.position.x, speed);
		Vector3 position = myTransform.position;
		float num = (position.x = x);
		Vector3 vector = (myTransform.position = position);
		float y = Mathf.Lerp(myTransform.position.y, magnet.position.y, speed);
		Vector3 position2 = myTransform.position;
		float num2 = (position2.y = y);
		Vector3 vector3 = (myTransform.position = position2);
	}

	public virtual void Main()
	{
	}
}
