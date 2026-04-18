using System;
using UnityEngine;

[Serializable]
public class EnemyHP : MonoBehaviour
{
	public int HP;

	[HideInInspector]
	public int MaxHP;

	public EnemyHP()
	{
		HP = 1;
	}

	public virtual void Awake()
	{
		MaxHP = HP;
	}

	public virtual void EyeSprite()
	{
	}

	public virtual void Main()
	{
	}
}
