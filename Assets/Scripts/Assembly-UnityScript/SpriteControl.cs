using System;
using UnityEngine;

[Serializable]
public class SpriteControl : MonoBehaviour
{
	private tk2dSprite sprite;

	public int ImageNum;

	public virtual void Awake()
	{
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
	}

	public virtual void SpriteSwap()
	{
		sprite.spriteId = ImageNum;
	}

	public virtual void Main()
	{
	}
}
