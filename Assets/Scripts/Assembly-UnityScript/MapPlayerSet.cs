using System;
using UnityEngine;

[Serializable]
public class MapPlayerSet : MonoBehaviour
{
	public virtual void Awake()
	{
		Global.CurrentPlayerObject = transform;
	}

	public virtual void Main()
	{
	}
}
