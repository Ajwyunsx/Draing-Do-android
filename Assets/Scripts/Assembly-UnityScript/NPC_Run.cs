using System;
using UnityEngine;

[Serializable]
public class NPC_Run : MonoBehaviour
{
	public bool isWalking;

	public float MaxSpeed;

	private float Speed;

	public int Dir;

	private Vector3 StartScale;

	private Transform myTransform;

	public int AiTimer;

	private bool StayOnGround;

	private int block_animation;

	private string action;

	public AnimationClip AnimationStop;

	public AnimationClip AnimationRun;

	public AnimationClip AnimationFall;

	public AnimationClip AnimationSwim;

	public NPC_Run()
	{
		MaxSpeed = 1f;
		Dir = 1;
	}

	public virtual void Awake()
	{
		GetComponent<Animation>()[AnimationRun.name].speed = 1.75f;
		myTransform = transform;
		StartScale = myTransform.localScale;
		Speed = MaxSpeed;
		Animate(AnimationRun, 0.15f);
	}

	public virtual void FixedUpdate()
	{
		float x = Speed * (float)Dir;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num = (velocity.x = x);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		if (isWalking)
		{
			AiTimer--;
			if (AiTimer <= 0)
			{
				int dir = Dir;
				Dir = UnityEngine.Random.Range(-1, 1);
				Speed = MaxSpeed;
				AiTimer = UnityEngine.Random.Range(50, 350);
				Animate(AnimationRun, 0.15f);
				if (Dir == 0)
				{
					Animate(AnimationStop, 0.15f);
					Speed = 0f;
					Dir = dir;
				}
			}
			if (action == AnimationFall.name && StayOnGround)
			{
				Speed = 0f;
				Animate(AnimationStop, 0.15f);
			}
		}
		StayOnGround = false;
	}

	public virtual void Update()
	{
		float x = (float)Dir * StartScale.x;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void OnCollisionStay(Collision other)
	{
		int i = 0;
		ContactPoint[] contacts = other.contacts;
		for (int length = contacts.Length; i < length; i++)
		{
			if (action != AnimationFall.name)
			{
				if (!(contacts[i].normal.x > -0.9f))
				{
					Dir = -1;
					break;
				}
				if (!(contacts[i].normal.x < 0.9f))
				{
					Dir = 1;
					break;
				}
			}
			if (!(contacts[i].normal.y < 0.5f))
			{
				StayOnGround = true;
				break;
			}
		}
	}

	public virtual void Animate(AnimationClip anim, float time)
	{
		if (anim == null)
		{
			return;
		}
		string text = anim.name;
		if (!(action == text))
		{
			action = text;
			if (time == 0f)
			{
				time = 0.15f;
			}
			GetComponent<Animation>().CrossFade(action, 0.25f);
		}
	}

	public virtual void Main()
	{
	}
}
