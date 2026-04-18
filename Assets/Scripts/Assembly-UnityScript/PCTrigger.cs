using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class PCTrigger : MonoBehaviour
{
	public AudioClip SFXDenied;

	public AudioClip SFXHack;

	public bool locked;

	public int ImageNumON;

	public int ImageNumOFF;

	public int ImageNumOK;

	[HideInInspector]
	public tk2dSprite sprite;

	public GameObject info;

	private bool ON;

	public GameObject[] ObjectList;

	public GameObject MiniHackerVehicle;

	public int timer;

	public PCTrigger()
	{
		ImageNumOK = 49;
	}

	public virtual void Awake()
	{
		sprite = (tk2dSprite)info.GetComponent(typeof(tk2dSprite));
		ChangeSprite(0);
	}

	public virtual void CommandTo()
	{
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			ObjectList[i].SendMessage("ByButton", ON, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ActON()
	{
		if (locked)
		{
			if (!(GetComponent<AudioSource>().clip == SFXDenied) || !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = SFXDenied;
				GetComponent<AudioSource>().Play();
			}
		}
		else if (!ON)
		{
			if (!(GetComponent<AudioSource>().clip == SFXHack) || !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = SFXHack;
				GetComponent<AudioSource>().Play();
			}
			ON = true;
			ChangeSprite(49);
			CommandTo();
		}
	}

	public virtual void FixedUpdate()
	{
		if (timer > 0)
		{
			timer--;
		}
	}

	public virtual void HackUnlocked()
	{
		if (locked && timer <= 0)
		{
			timer = 50;
			if (MiniHackerVehicle != null && Global.VehicleName == string.Empty)
			{
				Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
				MiniHackerVehicle.SendMessage("EnterVehicle", true, SendMessageOptions.DontRequireReceiver);
				return;
			}
			locked = false;
			ON = true;
			ChangeSprite(0);
			CommandTo();
		}
	}

	public virtual void Hack()
	{
		locked = false;
		ON = true;
		ChangeSprite(49);
		CommandTo();
	}

	public virtual void ChangeSprite(int num)
	{
		if (num == 0)
		{
			if (!locked)
			{
				sprite.spriteId = ImageNumON;
				gameObject.layer = 11;
			}
			else
			{
				sprite.spriteId = ImageNumOFF;
				gameObject.layer = 14;
			}
		}
		else
		{
			sprite.spriteId = ImageNumOK;
			gameObject.layer = 11;
		}
	}

	public virtual void Main()
	{
	}
}
