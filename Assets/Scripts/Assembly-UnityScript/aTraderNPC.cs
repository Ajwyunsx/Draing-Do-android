using System;
using UnityEngine;

[Serializable]
public class aTraderNPC : MonoBehaviour
{
	private int BuyTimer;

	public GameObject MenuObject;

	public virtual void Awake()
	{
	}

	public virtual void FixedUpdate()
	{
		if (BuyTimer > 0)
		{
			BuyTimer--;
		}
	}

	public virtual void ActON()
	{
		if (BuyTimer <= 0)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			if ((bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play();
			}
			BuyTimer = 40;
			Global.CreateMenuWindowObj(MenuObject);
		}
	}

	public virtual void Main()
	{
	}
}
