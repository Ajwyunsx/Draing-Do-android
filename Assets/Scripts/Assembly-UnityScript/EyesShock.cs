using System;
using UnityEngine;

[Serializable]
public class EyesShock : MonoBehaviour
{
	private tk2dSprite sprite;

	public int ImageShockNum;

	public int ImageShockNum2;

	private int ImageOld;

	public bool HideMode;

	public virtual void Start()
	{
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		ImageOld = sprite.spriteId;
	}

	public virtual void SHOCK(int trig)
	{
		if (!HideMode)
		{
			if (trig == 0)
			{
				sprite.spriteId = ImageOld;
			}
			if (trig == 1)
			{
				sprite.spriteId = ImageShockNum;
			}
			if (trig == 2)
			{
				sprite.spriteId = ImageShockNum2;
			}
		}
		else
		{
			if (trig == 0)
			{
				GetComponent<Renderer>().enabled = false;
			}
			if (trig == 1)
			{
				GetComponent<Renderer>().enabled = true;
			}
		}
	}

	public virtual void Main()
	{
	}
}
