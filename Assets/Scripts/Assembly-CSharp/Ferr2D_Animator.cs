using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ferr2D/Animator")]
[RequireComponent(typeof(Ferr2D_Sprite))]
public class Ferr2D_Animator : MonoBehaviour
{
	public Vector2 cellSize;

	public List<Ferr2D_Animation> animations = new List<Ferr2D_Animation>();

	public Action<Ferr2D_Animator, string> onAnimFinish;

	public Vector2 offset;

	private Ferr2D_Animation anim;

	private Ferr2D_Sprite sprite;

	private float time;

	private int cellsX;

	private bool usingShader;

	private void Start()
	{
		cellsX = (int)(1f / cellSize.x);
		for (int i = 0; i < animations.Count; i++)
		{
			animations[i].CalcMax();
		}
		if (HasAnim("default"))
		{
			SetAnimation("default");
		}
		if (HasAnim("idle"))
		{
			SetAnimation("idle");
		}
		GetComponent<Renderer>().material.SetTextureScale("_MainTex", cellSize);
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (anim == null)
		{
			return;
		}
		SetFrame(anim.GetFrame(time));
		if (anim.UpdateTime(ref time))
		{
			if (onAnimFinish != null)
			{
				onAnimFinish(this, anim.name);
			}
			if (anim.loop == Ferr2D_LoopMode.Next)
			{
				SetAnimation(anim.next);
			}
		}
	}

	public void SetFrame(int aFrame)
	{
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", GetPos(aFrame));
	}

	public void SetAnimation(string aName)
	{
		aName = aName.ToLower();
		if ((anim == null || !(anim.name == aName)) && HasAnim(aName))
		{
			anim = GetAnim(aName);
			time = 0f;
		}
	}

	public bool HasAnim(string aName)
	{
		for (int i = 0; i < animations.Count; i++)
		{
			if (animations[i].name == aName)
			{
				return true;
			}
		}
		return false;
	}

	public Ferr2D_Animation GetAnim(string aName)
	{
		for (int i = 0; i < animations.Count; i++)
		{
			if (animations[i].name == aName)
			{
				return animations[i];
			}
		}
		return null;
	}

	private Vector2 GetPos(int i)
	{
		return new Vector2((float)(i / cellsX) * cellSize.x, (float)(i % cellsX) * cellSize.y) + offset;
	}
}
