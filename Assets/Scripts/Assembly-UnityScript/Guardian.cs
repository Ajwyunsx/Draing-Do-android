using System;
using UnityEngine;

[Serializable]
public class Guardian : MonoBehaviour
{
	public AudioClip SFXGet;

	public AudioClip SFXFail;

	public bool Active;

	private int randomFactor;

	private float randomSpeed;

	private float distance;

	private float maxDistance;

	private Transform myTransform;

	public GameObject particleStrike;

	public virtual void Awake()
	{
		if (!Global.FonarSkill)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Start()
	{
		myTransform = transform;
		randomFactor = UnityEngine.Random.Range(0, 360);
		randomSpeed = UnityEngine.Random.Range(3f, 4f);
		maxDistance = UnityEngine.Random.Range(2f, 3f);
	}

	public virtual void FixedUpdate()
	{
		if (Active)
		{
			if (!(distance >= maxDistance))
			{
				distance += 0.05f;
			}
			myTransform.position = Global.CurrentPlayerObject.position + new Vector3(Mathf.Sin((Time.time + (float)randomFactor) * randomSpeed) * distance, Mathf.Cos((Time.time + (float)randomFactor) * randomSpeed) * distance, -1f);
		}
	}

	public virtual void ActiveON()
	{
		Active = true;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!Global.FonarSkill)
		{
			return;
		}
		if (Global.Guardian < 3 && !Active && other.gameObject.tag == "Player")
		{
			Active = true;
			Global.Guardian++;
			if ((bool)SFXGet)
			{
				AudioSource.PlayClipAtPoint(SFXGet, transform.position);
			}
			if ((bool)particleStrike)
			{
				UnityEngine.Object.Instantiate(particleStrike, myTransform.position, Quaternion.identity);
			}
		}
		if (!Active || other.gameObject.layer != 17)
		{
			return;
		}
		if (UnityEngine.Random.Range(0, 100) > 95)
		{
			UnityEngine.Object.Destroy(gameObject);
			if ((bool)particleStrike)
			{
				UnityEngine.Object.Instantiate(particleStrike, myTransform.position, Quaternion.identity);
			}
			if ((bool)SFXFail)
			{
				AudioSource.PlayClipAtPoint(SFXFail, transform.position);
			}
			other.gameObject.SendMessage("CrushHP", 2, SendMessageOptions.DontRequireReceiver);
			Global.Guardian--;
		}
		else
		{
			other.gameObject.SendMessage("CrushHP", 1, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
