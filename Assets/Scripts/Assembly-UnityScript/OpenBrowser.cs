using System;
using UnityEngine;

[Serializable]
public class OpenBrowser : MonoBehaviour
{
	public virtual void OnMouseDown()
	{
		Application.OpenURL("http://unity3d.com/");
	}

	public virtual void Main()
	{
	}
}
