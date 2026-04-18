using System;
using UnityEngine;

[Serializable]
public class LookAtHeroMirror : MonoBehaviour
{
	private Transform myTransform;

	private bool Disappear;

	private int disappear_timer;

	[HideInInspector]
	public float Alpha;

	[HideInInspector]
	public tk2dSprite sprite;

	private float initScale;

	public int CorrectionDir;

	public int CorrAngleZRight;

	public int CorrAngleZLeft;

	public LookAtHeroMirror()
	{
		Alpha = 1f;
		CorrectionDir = 1;
	}

	public virtual void Start()
	{
		myTransform = transform;
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		initScale = myTransform.localScale.x;
	}

	public virtual void FixedUpdate()
	{
		if (!Disappear)
		{
			if ((bool)Global.CurrentPlayerObject)
			{
				int num = (int)(Mathf.Sign(Global.CurrentPlayerObject.position.x - myTransform.position.x) * (float)CorrectionDir);
				float x = initScale * (float)num;
				Vector3 localScale = myTransform.localScale;
				float num2 = (localScale.x = x);
				Vector3 vector = (myTransform.localScale = localScale);
				if (num < 0)
				{
					int corrAngleZRight = CorrAngleZRight;
					Vector3 eulerAngles = myTransform.eulerAngles;
					float num3 = (eulerAngles.z = corrAngleZRight);
					Vector3 vector3 = (myTransform.eulerAngles = eulerAngles);
				}
				else
				{
					int corrAngleZLeft = CorrAngleZLeft;
					Vector3 eulerAngles2 = myTransform.eulerAngles;
					float num4 = (eulerAngles2.z = corrAngleZLeft);
					Vector3 vector5 = (myTransform.eulerAngles = eulerAngles2);
				}
			}
		}
		else
		{
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
