using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class RandomColorBody : MonoBehaviour
{
	public Renderer[] rend;

	public Vector2 MaxR;

	public Vector2 MaxG;

	public Vector2 MaxB;

	public RandomColorBody()
	{
		MaxR = new Vector2(0f, 1f);
		MaxG = new Vector2(0f, 1f);
		MaxB = new Vector2(0f, 1f);
	}

	public virtual void Awake()
	{
		Color color = new Color(UnityEngine.Random.Range(MaxR.x, MaxR.y), UnityEngine.Random.Range(MaxG.x, MaxG.y), UnityEngine.Random.Range(MaxB.x, MaxB.y), 1f);
		for (int i = 0; i < Extensions.get_length((System.Array)rend); i++)
		{
			rend[i].material.color = color;
		}
	}

	public virtual void Main()
	{
	}
}
