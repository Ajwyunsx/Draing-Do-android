using System;
using UnityEngine;

[Serializable]
public class ColorChange : MonoBehaviour
{
	public Color colorz;

	public ColorChange()
	{
		colorz = new Color(1f, 1f, 1f, 1f);
	}

	public virtual void Awake()
	{
		GetComponent<Renderer>().material.color = colorz;
	}

	public virtual void GetColorFromButton()
	{
		Global.HairColor = colorz;
	}

	public virtual void Main()
	{
	}
}
