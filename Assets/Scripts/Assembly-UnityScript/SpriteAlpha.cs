using System;
using UnityEngine;

[Serializable]
public class SpriteAlpha : MonoBehaviour
{
	public float Alpha;

	public float speed;

	private Component[] rends;

	public bool DestroyOnZero;

	public SpriteAlpha()
	{
		Alpha = 1.5f;
		speed = 0.02f;
		DestroyOnZero = true;
	}

	public virtual void Start()
	{
		rends = GetComponentsInChildren(typeof(Renderer));
	}

	public virtual void FixedUpdate()
	{
		CheckAlpha();
		if (DestroyOnZero && !(Alpha > 0f))
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void CheckAlpha()
	{
		Alpha -= speed;
		int i = 0;
		Component[] array = rends;
		for (int length = array.Length; i < length; i++)
		{
			float alpha = Alpha;
			Color color = ((Renderer)array[i]).material.color;
			float num = (color.a = alpha);
			Color color2 = (((Renderer)array[i]).material.color = color);
		}
	}

	public virtual void Main()
	{
	}
}
