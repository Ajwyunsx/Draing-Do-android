using System;
using UnityEngine;

[Serializable]
public class TextureScale : MonoBehaviour
{
	public Vector2 waveScale;

	public bool onlyX;

	public bool onlyY;

	public TextureScale()
	{
		waveScale = new Vector2(1f, 1f);
	}

	public virtual void Awake()
	{
		Vector3 size = GetComponent<Renderer>().bounds.size;
		if (!onlyY)
		{
			float x = size.x * waveScale.x * 0.25f;
			Vector2 mainTextureScale = GetComponent<Renderer>().material.mainTextureScale;
			float num = (mainTextureScale.x = x);
			Vector2 vector = (GetComponent<Renderer>().material.mainTextureScale = mainTextureScale);
		}
		if (!onlyX)
		{
			float y = size.y * waveScale.y * 0.25f;
			Vector2 mainTextureScale2 = GetComponent<Renderer>().material.mainTextureScale;
			float num2 = (mainTextureScale2.y = y);
			Vector2 vector3 = (GetComponent<Renderer>().material.mainTextureScale = mainTextureScale2);
		}
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void Main()
	{
	}
}
