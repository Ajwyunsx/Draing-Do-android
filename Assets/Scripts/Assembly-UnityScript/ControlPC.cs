using System;
using UnityEngine;

[Serializable]
public class ControlPC : MonoBehaviour
{
	public AudioClip SFX;

	public GameObject vehicle;

	public bool locked;

	public int ImageNumON;

	public int ImageNumOFF;

	[HideInInspector]
	public tk2dSprite sprite;

	public GameObject info;

	public virtual void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		if ((bool)SFX)
		{
			GetComponent<AudioSource>().clip = SFX;
		}
		sprite = (tk2dSprite)info.GetComponent(typeof(tk2dSprite));
		ChangeSprite();
	}

	public virtual void ActON()
	{
		if (!locked && !(vehicle == null) && Global.VehicleName == string.Empty)
		{
			if ((bool)SFX)
			{
				GetComponent<AudioSource>().Play();
			}
			Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
			vehicle.SendMessage("EnterVehicle", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void HackUnlocked()
	{
		if (locked)
		{
			locked = false;
			ChangeSprite();
		}
	}

	public virtual void ChangeSprite()
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

	public virtual void Main()
	{
	}
}
