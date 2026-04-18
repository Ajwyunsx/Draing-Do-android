using System;
using UnityEngine;

[Serializable]
public class LookAtHero : MonoBehaviour
{
	private Transform myTransform;

	private float LocalAngle;

	public int CorrectionAngle;

	public float speed;

	private bool Disappear;

	private int disappear_timer;

	[HideInInspector]
	public float Alpha;

	[HideInInspector]
	public tk2dSprite sprite;

	public LookAtHero()
	{
		speed = 0.1f;
		Alpha = 1f;
	}

	public virtual void Awake()
	{
		if (GetComponent<Renderer>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}
	}

	public virtual void Start()
	{
		myTransform = transform;
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
	}

	public virtual void FixedUpdate()
	{
		if (!Disappear)
		{
			if (GetComponent<Renderer>().isVisible && (bool)Global.CurrentPlayerObject)
			{
				LocalAngle = Mathf.Atan2(Global.CurrentPlayerObject.position.y - myTransform.position.y, Global.CurrentPlayerObject.position.x - myTransform.position.x) * 57.29578f + (float)CorrectionAngle;
				float num = Mathf.LerpAngle(myTransform.eulerAngles.z, LocalAngle, speed);
				float z = num;
				Vector3 eulerAngles = myTransform.eulerAngles;
				float num2 = (eulerAngles.z = z);
				Vector3 vector = (myTransform.eulerAngles = eulerAngles);
			}
			return;
		}
		disappear_timer--;
		if (disappear_timer <= 0)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		if ((bool)sprite)
		{
			Alpha -= 0.015f;
			sprite.color = new Color(1f, 1f, 1f, Alpha);
			GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, Alpha);
		}
	}

	public virtual void DISAPPEAR()
	{
		Disappear = true;
		disappear_timer = 60;
		if ((bool)GetComponent<Collider>())
		{
			GetComponent<Collider>().isTrigger = true;
		}
		if (!GetComponent<Rigidbody>())
		{
			Rigidbody rigidbody = (Rigidbody)gameObject.AddComponent(typeof(Rigidbody));
		}
		int num = UnityEngine.Random.Range(-2, 2);
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num2 = (velocity.x = num);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		int num3 = UnityEngine.Random.Range(0, 2);
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num4 = (velocity2.y = num3);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
		int num5 = UnityEngine.Random.Range(-10, 10);
		Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
		float num6 = (angularVelocity.z = num5);
		Vector3 vector5 = (GetComponent<Rigidbody>().angularVelocity = angularVelocity);
	}

	public virtual void Main()
	{
	}
}
