using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class ButtonKluchDevice : MonoBehaviour
{
	public GameObject[] ObjectList;

	public bool Once;

	private bool locOnce;

	public int timer;

	public bool ON;

	public int ImageNumON;

	public int ImageNumOFF;

	[HideInInspector]
	public tk2dSprite sprite;

	public GameObject lamp;

	public GameObject wheel;

	private bool visible;

	public GameObject particleEffect;

	public virtual void Start()
	{
		sprite = (tk2dSprite)lamp.GetComponent(typeof(tk2dSprite));
		ChangeSprite();
	}

	public virtual void ChangeSprite()
	{
		if (ON)
		{
			sprite.spriteId = ImageNumON;
		}
		else
		{
			sprite.spriteId = ImageNumOFF;
		}
	}

	public virtual void FixedUpdate()
	{
		if (timer > 0)
		{
			timer--;
		}
		if (ON && (bool)wheel)
		{
			wheel.transform.Rotate(new Vector3(0f, 0f, 2.222f));
		}
	}

	public virtual void Kluch()
	{
		if ((Once && locOnce) || timer > 0)
		{
			return;
		}
		if ((bool)GetComponent<AudioSource>() && visible)
		{
			GetComponent<AudioSource>().Play();
			if ((bool)particleEffect)
			{
				UnityEngine.Object.Instantiate(particleEffect, transform.position + new Vector3(0f, 0f, -3f), Quaternion.identity);
			}
		}
		timer = 50;
		locOnce = true;
		if (ON)
		{
			ON = false;
		}
		else
		{
			ON = true;
		}
		ChangeSprite();
		if (Extensions.get_length((System.Array)ObjectList) != 0)
		{
			CommandTo();
		}
	}

	public virtual void CommandTo()
	{
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			ObjectList[i].SendMessage("ByButton", ON, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ByButton(bool @bool)
	{
		if (!Once || !locOnce)
		{
			Kluch();
		}
	}

	public virtual void OnBecameVisible()
	{
		visible = true;
	}

	public virtual void OnBecameInvisible()
	{
		visible = false;
	}

	public virtual void Main()
	{
	}
}
