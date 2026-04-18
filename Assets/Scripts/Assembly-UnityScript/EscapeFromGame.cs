using System;
using UnityEngine;

[Serializable]
public class EscapeFromGame : MonoBehaviour
{
	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public virtual void Main()
	{
	}
}
