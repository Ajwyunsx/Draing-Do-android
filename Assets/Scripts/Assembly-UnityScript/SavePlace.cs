using System;
using UnityEngine;

[Serializable]
public class SavePlace : MonoBehaviour
{
	public virtual void Awake()
	{
		Global.AddPlace(Application.loadedLevelName);
	}

	public virtual void Main()
	{
	}
}
