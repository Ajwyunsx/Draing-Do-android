using System;
using UnityEngine;

[Serializable]
public class PlayerShot : MonoBehaviour
{
	private int timer;

	public int Power;

	public PlayerShot()
	{
		timer = 100;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		timer--;
		if (timer <= 0)
		{
			TouchToOther();
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (timer <= 95 && !(collision.gameObject.tag == "CheckPoint") && !(collision.gameObject.tag == "Player") && !(collision.gameObject.tag == "Ignore") && !(collision.gameObject.tag == "Deep") && !(collision.gameObject.tag == "Sensore"))
		{
			TouchToOther();
			if (Power > 0)
			{
				collision.gameObject.SendMessage("CrushHP", Power, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void OnTriggerEnter(Collider collision)
	{
		if (!(collision.gameObject.tag == "CheckPoint") && !(collision.gameObject.tag == "Player") && !(collision.gameObject.tag == "Ignore") && !(collision.gameObject.tag == "Deep") && !(collision.gameObject.tag == "Sensore"))
		{
			TouchToOther();
			if (Power > 0)
			{
				collision.gameObject.SendMessage("CrushHP", Power, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void TouchToOther()
	{
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Main()
	{
	}
}
