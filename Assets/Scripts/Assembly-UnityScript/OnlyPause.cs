using System;
using UnityEngine;

[Serializable]
public class OnlyPause : MonoBehaviour
{
	public virtual void Awake()
	{
		Global.Pause = true;
		Global.MenuPause = true;
	}

	public virtual void Main()
	{
	}
}
