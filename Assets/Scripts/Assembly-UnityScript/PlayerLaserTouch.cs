using System;
using UnityEngine;

[Serializable]
public class PlayerLaserTouch : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		TouchToOther();
	}

	public virtual void OnTriggerEnter(Collider collision)
	{
		if (!(collision.gameObject.tag == "CheckPoint") && !(collision.gameObject.tag == "Player") && !(collision.gameObject.tag == "Ignore") && !(collision.gameObject.tag == "Deep") && !(collision.gameObject.tag == "Sensore"))
		{
			TouchToOther();
		}
	}

	public virtual void TouchToOther()
	{
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("explosion")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void OnBecameInvisible()
	{
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Main()
	{
	}
}
