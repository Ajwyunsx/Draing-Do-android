using System;
using UnityEngine;

[Serializable]
public class EscapeToTitle : MonoBehaviour
{
	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("title");
		}
	}

	public virtual void Main()
	{
	}
}
