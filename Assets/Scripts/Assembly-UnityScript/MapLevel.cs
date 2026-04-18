using System;
using UnityEngine;

[Serializable]
public class MapLevel : MonoBehaviour
{
	public bool deleteCurrentLevel;

	public string CheckLevelName;

	public bool DontChangeColor;

	public virtual void Start()
	{
		if (CheckLevelName == null || CheckLevelName == string.Empty)
		{
			CheckLevelName = gameObject.name;
		}
		if (!Global.CheckPlace(CheckLevelName))
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		else if (CheckLevelName == Application.loadedLevelName)
		{
			if (deleteCurrentLevel)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			else if (!DontChangeColor)
			{
				GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
			}
		}
	}

	public virtual void Main()
	{
	}
}
