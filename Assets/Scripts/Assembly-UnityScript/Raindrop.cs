using System;
using UnityEngine;

[Serializable]
public class Raindrop : MonoBehaviour
{
	[HideInInspector]
	public Transform MyTransform;

	[HideInInspector]
	public Vector3 targ;

	public float speed;

	public Raindrop()
	{
		speed = 1f;
	}

	public virtual void Awake()
	{
		MyTransform = transform;
		targ = new Vector3(5.8f, -7.5f, -20f);
	}

	public virtual void FixedUpdate()
	{
		if (!(Vector3.Distance(targ, transform.position) <= 1f))
		{
			float x = Mathf.Lerp(MyTransform.position.x, targ.x, Time.deltaTime * speed);
			Vector3 position = MyTransform.position;
			float num = (position.x = x);
			Vector3 vector = (MyTransform.position = position);
			float y = Mathf.Lerp(MyTransform.position.y, targ.y, Time.deltaTime * speed);
			Vector3 position2 = MyTransform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (MyTransform.position = position2);
			float z = Mathf.Lerp(MyTransform.position.z, targ.z, Time.deltaTime * speed);
			Vector3 position3 = MyTransform.position;
			float num3 = (position3.z = z);
			Vector3 vector5 = (MyTransform.position = position3);
		}
		else
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
