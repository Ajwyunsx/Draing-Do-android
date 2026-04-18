using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Button : MonoBehaviour
{
	public AudioClip SFX;

	public GameObject[] ObjectList;

	public bool Once;

	private bool locOnce;

	public bool MassButton;

	public int Timer;

	public bool ON;

	public int ImageNumON;

	public int ImageNumOFF;

	[HideInInspector]
	public tk2dSprite sprite;

	[HideInInspector]
	public bool TriggerStay;

	private int timerNoPress;

	public virtual void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		if ((bool)SFX)
		{
			GetComponent<AudioSource>().clip = SFX;
		}
		ChangeSprite();
	}

	public virtual void Start()
	{
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
		if (!MassButton)
		{
			if (timerNoPress > 0)
			{
				timerNoPress--;
			}
		}
		else
		{
			if (Once && locOnce)
			{
				return;
			}
			if (ON && !TriggerStay)
			{
				ON = false;
				if ((bool)SFX)
				{
					GetComponent<AudioSource>().Play();
				}
				ChangeSprite();
				gameObject.SendMessage("CommandTo", null, SendMessageOptions.DontRequireReceiver);
			}
			TriggerStay = false;
		}
	}

	public virtual void ByButton(bool @bool)
	{
		ActON();
		OnTriggerStay();
	}

	public virtual void ByButtonOffSFX(bool @bool)
	{
		SFX = null;
		ActON();
		OnTriggerStay();
	}

	public virtual void ActON()
	{
		if ((!Once || !locOnce) && !MassButton && Extensions.get_length((System.Array)ObjectList) != 0 && timerNoPress <= 0)
		{
			timerNoPress = 35;
			if (Once)
			{
				GetComponent<Collider>().enabled = false;
			}
			if ((bool)SFX)
			{
				GetComponent<AudioSource>().Play();
			}
			if (ON)
			{
				ON = false;
			}
			else
			{
				ON = true;
			}
			locOnce = true;
			ChangeSprite();
			gameObject.SendMessage("CommandTo", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CommandTo()
	{
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			if ((bool)ObjectList[i])
			{
				ObjectList[i].SendMessage("ByButton", ON, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void OnTriggerStay()
	{
		if ((Once && locOnce) || !MassButton)
		{
			return;
		}
		if (!ON)
		{
			ON = true;
			if ((bool)SFX)
			{
				GetComponent<AudioSource>().Play();
			}
			locOnce = true;
			ChangeSprite();
			gameObject.SendMessage("CommandTo", null, SendMessageOptions.DontRequireReceiver);
		}
		if (Once)
		{
			GetComponent<Collider>().enabled = false;
		}
		TriggerStay = true;
	}

	public virtual void Main()
	{
	}
}
