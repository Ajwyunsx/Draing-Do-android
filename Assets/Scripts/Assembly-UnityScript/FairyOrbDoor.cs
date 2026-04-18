using System;
using UnityEngine;

[Serializable]
public class FairyOrbDoor : MonoBehaviour
{
	private bool open;

	private bool once;

	private int timer;

	public Vector3 move;

	private Transform myTransform;

	public FairyOrbDoor()
	{
		move = new Vector3(0f, -0.07f, 0f);
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (open && timer > 0)
		{
			timer--;
			myTransform.position += move;
		}
	}

	public virtual void FairyStrike()
	{
		if (!once)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			open = true;
			once = true;
			timer = 50;
		}
	}

	public virtual void Main()
	{
	}
}
