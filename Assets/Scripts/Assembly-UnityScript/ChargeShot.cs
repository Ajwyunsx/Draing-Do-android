using System;
using UnityEngine;

[Serializable]
public class ChargeShot : MonoBehaviour
{
	private Transform myTransform;

	public int timer;

	private int shotPower;

	public GameObject ShotGameObject;

	private float angle;

	public AudioClip SFX;

	public ChargeShot()
	{
		timer = 50;
	}

	public virtual void Start()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		myTransform = transform;
		angle = Mathf.Atan2(Global.CurrentPlayerObject.position.y - myTransform.position.y, Global.CurrentPlayerObject.position.x - myTransform.position.x) * 57.29578f + 90f + (float)UnityEngine.Random.Range(-5, 5);
	}

	public virtual void FixedUpdate()
	{
		myTransform.localScale *= 0.925f;
		timer--;
		if (timer <= 0)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ShotGameObject, myTransform.position, Quaternion.identity) as GameObject;
			float z = angle;
			Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
			float num = (eulerAngles.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			if (shotPower != 0)
			{
				Global.LastCreatedObject.SendMessage("ShotPower", shotPower, SendMessageOptions.DontRequireReceiver);
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void ShotPower(int pow)
	{
		shotPower = pow;
	}

	public virtual void Main()
	{
	}
}
