using System;
using UnityEngine;

[Serializable]
public class SpriteShade : MonoBehaviour
{
	public float power;

	public SpriteShade()
	{
		power = 0.75f;
	}

	public virtual void Awake()
	{
		Recolor();
	}

	public virtual void Recolor()
	{
		tk2dSprite tk2dSprite2 = null;
		tk2dSprite2 = gameObject.GetComponent<tk2dSprite>();
		if ((bool)tk2dSprite2)
		{
			float r = tk2dSprite2.color.r * power;
			Color color = tk2dSprite2.color;
			float num = (color.r = r);
			Color color2 = (tk2dSprite2.color = color);
			float g = tk2dSprite2.color.g * power;
			Color color4 = tk2dSprite2.color;
			float num2 = (color4.g = g);
			Color color5 = (tk2dSprite2.color = color4);
			float b = tk2dSprite2.color.b * power;
			Color color7 = tk2dSprite2.color;
			float num3 = (color7.b = b);
			Color color8 = (tk2dSprite2.color = color7);
		}
	}

	public virtual void Main()
	{
	}
}
