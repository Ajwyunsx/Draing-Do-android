using System;
using UnityEngine;

[Serializable]
public class Jumper : MonoBehaviour
{
	public AudioClip SFXJump;

	public AudioClip SFXWake;

	public float Speed;

	private int Acting;

	public AnimationClip _animation;

	public AnimationClip _animation_wake;

	public AnimationClip _animation_sleep;

	public bool SLEEPING;

	public bool MAGNET;

	public GameObject ParticleObject;

	public string SendMessageToHero;

	public Jumper()
	{
		Speed = 20f;
		SLEEPING = true;
		MAGNET = true;
		SendMessageToHero = "ReDoubleJump";
	}

	public virtual void Start()
	{
		if (GetComponent<Renderer>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}
		if (SLEEPING && (bool)_animation_sleep)
		{
			GetComponent<Animation>().Play(_animation_sleep.name);
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			LetsJump(other.gameObject);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			LetsJump(other.gameObject);
		}
	}

	public virtual void LetsJump(GameObject other)
	{
		if (!(Global.VehicleName != string.Empty) && !SLEEPING && Acting <= 0)
		{
			if ((bool)SFXJump)
			{
				AudioSource.PlayClipAtPoint(SFXJump, transform.position);
			}
			if ((bool)_animation && (bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play(_animation.name);
			}
			if (MAGNET)
			{
				float x = transform.position.x;
				Vector3 position = other.transform.position;
				float num = (position.x = x);
				Vector3 vector = (other.transform.position = position);
				float y = transform.position.y;
				Vector3 position2 = other.transform.position;
				float num2 = (position2.y = y);
				Vector3 vector3 = (other.transform.position = position2);
			}
			other.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(transform.eulerAngles.z * ((float)Math.PI / 180f)) * (0f - Speed), Mathf.Cos(transform.eulerAngles.z * ((float)Math.PI / 180f)) * Speed, 0f);
			other.SendMessage(SendMessageToHero, null, SendMessageOptions.DontRequireReceiver);
			if (MAGNET)
			{
				Global.BlockControl = 30;
			}
			Global.Break = true;
			if ((bool)ParticleObject)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(ParticleObject) as GameObject;
				Global.LastCreatedObject.transform.position = transform.position + new Vector3(0f, 0f, -1f);
			}
			Acting = 25;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Acting > 0)
		{
			Acting--;
		}
	}

	public virtual void WakeUp()
	{
		if (SLEEPING)
		{
			if (GetComponent<Renderer>().isVisible && (bool)SFXWake)
			{
				AudioSource.PlayClipAtPoint(SFXWake, transform.position);
			}
			SLEEPING = false;
			if ((bool)_animation_wake)
			{
				GetComponent<Animation>().Play(_animation_wake.name);
			}
		}
	}

	public virtual void Main()
	{
	}
}
