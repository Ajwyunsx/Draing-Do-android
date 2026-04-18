using System;
using UnityEngine;

[Serializable]
public class AutoAnim : MonoBehaviour
{
	public string Mode;

	public Vector3 Speed;

	public Vector3 Distance;

	public Vector3 Correction;

	public bool alphaVibration;

	public float alphaScale;

	private Color InitColor;

	private Renderer rend;

	private Transform trans;

	private Vector3 startPosition;

	private Vector3 startRotation;

	private Vector3 startScale;

	private int rndm;

	public bool RndCorrect;

	public AutoAnim()
	{
		Mode = "turn";
		alphaScale = 0.4f;
	}

	public virtual void Awake()
	{
		trans = transform;
		startPosition = trans.localPosition;
		if (Mode != "noAngle")
		{
			startRotation = trans.localEulerAngles;
		}
		else
		{
			startRotation = trans.eulerAngles;
		}
		startScale = trans.localScale;
		if (alphaVibration)
		{
			rndm = UnityEngine.Random.Range(0, 360);
			rend = GetComponent<Renderer>();
			InitColor = rend.material.color;
		}
		if (RndCorrect)
		{
			Correction.x *= UnityEngine.Random.Range(-1f, 1f);
			Correction.y *= UnityEngine.Random.Range(-1f, 1f);
			Correction.z *= UnityEngine.Random.Range(-1f, 1f);
		}
	}

	public virtual void FixedUpdate()
	{
		if (alphaVibration)
		{
			float a = InitColor.a + Mathf.Sin(Time.time * Speed.x + (float)rndm) * (InitColor.a * alphaScale);
			Color color = rend.material.color;
			float num = (color.a = a);
			Color color2 = (rend.material.color = color);
		}
		switch (Mode)
		{
		case "noAngle":
		{
			float z3 = startRotation.z;
			Vector3 eulerAngles = trans.eulerAngles;
			float num6 = (eulerAngles.z = z3);
			Vector3 vector9 = (trans.eulerAngles = eulerAngles);
			break;
		}
		case "turnSin":
		{
			float z2 = startRotation.z + Mathf.Sin(Time.time * Speed.z) * Distance.z + Correction.z;
			Vector3 localEulerAngles2 = trans.localEulerAngles;
			float num5 = (localEulerAngles2.z = z2);
			Vector3 vector7 = (trans.localEulerAngles = localEulerAngles2);
			break;
		}
		case "turn":
		{
			float z = trans.localEulerAngles.z + Speed.z;
			Vector3 localEulerAngles = trans.localEulerAngles;
			float num4 = (localEulerAngles.z = z);
			Vector3 vector5 = (trans.localEulerAngles = localEulerAngles);
			break;
		}
		case "scale":
			if (Speed.x != 0f)
			{
				float x2 = startScale.x + Mathf.Sin(Time.time * Speed.x) * Distance.x + Correction.x;
				Vector3 localScale = trans.localScale;
				float num7 = (localScale.x = x2);
				Vector3 vector11 = (trans.localScale = localScale);
			}
			if (Speed.y != 0f)
			{
				float y2 = startScale.y + Mathf.Sin(Time.time * Speed.y) * Distance.y + Correction.y;
				Vector3 localScale2 = trans.localScale;
				float num8 = (localScale2.y = y2);
				Vector3 vector13 = (trans.localScale = localScale2);
			}
			break;
		case "move":
			if (Speed.x != 0f)
			{
				float x = startPosition.x + Mathf.Sin(Time.time * Speed.x) * Distance.x + Correction.x;
				Vector3 localPosition = trans.localPosition;
				float num2 = (localPosition.x = x);
				Vector3 vector = (trans.localPosition = localPosition);
			}
			if (Speed.y != 0f)
			{
				float y = startPosition.y + Mathf.Sin(Time.time * Speed.y) * Distance.y + Correction.z;
				Vector3 localPosition2 = trans.localPosition;
				float num3 = (localPosition2.y = y);
				Vector3 vector3 = (trans.localPosition = localPosition2);
			}
			break;
		}
	}

	public virtual void Main()
	{
	}
}
