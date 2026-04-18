using System;
using UnityEngine;

[Serializable]
public class NightLight : MonoBehaviour
{
	private int OldDAYTIME;

	public bool scaleVibration;

	public bool alphaVibration;

	public bool OnInvert;

	private int rndm;

	private Transform MyTransform;

	private Vector3 InitScale;

	private Color InitColor;

	private Renderer rend;

	public float Speed;

	public float scale;

	public bool isTint;

	public NightLight()
	{
		OldDAYTIME = -100000;
		scaleVibration = true;
		alphaVibration = true;
		OnInvert = true;
		Speed = 5f;
		scale = 0.2f;
	}

	public virtual void Awake()
	{
		rndm = UnityEngine.Random.Range(0, 360);
		MyTransform = transform;
		InitScale = MyTransform.localScale;
		if (alphaVibration)
		{
			rend = GetComponent<Renderer>();
			if (!isTint)
			{
				InitColor = rend.material.color;
			}
			else
			{
				InitColor = rend.material.GetColor("_TintColor");
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (scaleVibration)
		{
			float x = InitScale.x + Mathf.Sin(Time.time * 8f + (float)rndm) * (InitScale.x * 0.02f);
			Vector3 localScale = MyTransform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (MyTransform.localScale = localScale);
			float y = InitScale.y + Mathf.Sin(Time.time * 8f + (float)rndm) * (InitScale.y * 0.02f);
			Vector3 localScale2 = MyTransform.localScale;
			float num2 = (localScale2.y = y);
			Vector3 vector3 = (MyTransform.localScale = localScale2);
		}
		if (alphaVibration)
		{
			if (!isTint)
			{
				float a = InitColor.a + Mathf.Sin(Time.time * Speed + (float)rndm) * (InitColor.a * scale);
				Color color = rend.material.color;
				float num3 = (color.a = a);
				Color color2 = (rend.material.color = color);
			}
			else
			{
				float a2 = InitColor.a + Mathf.Sin(Time.time * Speed + (float)rndm) * (InitColor.a * scale);
				rend.material.SetColor("_TintColor", new Color(InitColor.r, InitColor.g, InitColor.b, a2));
			}
		}
	}

	public virtual void Main()
	{
	}
}
