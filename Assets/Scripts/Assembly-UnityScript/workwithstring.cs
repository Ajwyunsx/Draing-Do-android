using System;
using UnityEngine;

[Serializable]
public class workwithstring : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		string text = "asdXXXasd";
		int num = text.IndexOf("asd");
		string text2 = text.Substring(num + text.Length, 3);
	}

	public virtual void Main()
	{
	}
}
