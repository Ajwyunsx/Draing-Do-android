using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class FallingAI : MonoBehaviour
{
	public AudioClip SFXFall;

	public AudioClip SFXCrush;

	public int POWER;

	public float Speed;

	public float Distance;

	private Transform myTransform;

	private Vector3 startPosition;

	private bool isFall;

	public bool Invincible;

	public GameObject shardObject;

	private bool once;

	public FallingAI()
	{
		POWER = 1;
		Speed = 7f;
		Distance = 1.5f;
	}

	public virtual void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		if (GetComponent<Renderer>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}
		myTransform = transform;
		startPosition = myTransform.position;
	}

	public virtual void FixedUpdate()
	{
		if (isFall)
		{
			return;
		}
		myTransform.position = startPosition;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		if (!(Global.CurrentPlayerObject.position.y >= myTransform.position.y) && !(Mathf.Abs(Global.CurrentPlayerObject.position.x - myTransform.position.x) >= Distance))
		{
			if ((bool)SFXFall)
			{
				GetComponent<AudioSource>().clip = SFXFall;
				GetComponent<AudioSource>().Play();
			}
			isFall = true;
			float y = 0f - Speed;
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			float num = (velocity.y = y);
			Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (!Invincible)
		{
			DISAPPEAR();
			other.gameObject.SendMessage("CrushHP", 1, SendMessageOptions.DontRequireReceiver);
			other.gameObject.SendMessage("TouchDanger", new Vector3(myTransform.position.x, myTransform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CrushHP(int hp)
	{
		if (!Invincible)
		{
			DISAPPEAR();
		}
	}

	public virtual void DISAPPEAR()
	{
		if ((bool)shardObject)
		{
			ShardCrush();
		}
		if (GetComponent<Renderer>().isVisible && (bool)SFXCrush)
		{
			AudioSource.PlayClipAtPoint(SFXCrush, transform.position);
		}
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void ShardCrush()
	{
		if (!once)
		{
			once = true;
			for (int i = 0; i < 5; i++)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(shardObject) as GameObject;
				float x = myTransform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.x = x);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
				float y = myTransform.position.y + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position2 = Global.LastCreatedObject.transform.position;
				float num2 = (position2.y = y);
				Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
				float z = myTransform.position.z - 0.25f;
				Vector3 position3 = Global.LastCreatedObject.transform.position;
				float num3 = (position3.z = z);
				Vector3 vector5 = (Global.LastCreatedObject.transform.position = position3);
				int num4 = UnityEngine.Random.Range(-180, 180);
				Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
				float num5 = (eulerAngles.z = num4);
				Vector3 vector7 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
				Global.LastCreatedObject.SendMessage("FakePartVelocityAndDestroy", 4, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
