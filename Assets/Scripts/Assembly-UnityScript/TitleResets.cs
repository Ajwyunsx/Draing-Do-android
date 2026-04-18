using System;
using UnityEngine;

[Serializable]
public class TitleResets : MonoBehaviour
{
	public virtual void Start()
	{
		Global.HP = 4f;
		Global.MaxHP = 4f;
	}

	public virtual void Main()
	{
	}
}
