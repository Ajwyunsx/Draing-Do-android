using System;
using UnityEngine;

[Serializable]
public class ChildRenderShader : MonoBehaviour
{
	public Shader sshader;

	public virtual void Start()
	{
		Component[] componentsInChildren = GetComponentsInChildren(typeof(Renderer));
		int i = 0;
		Component[] array = componentsInChildren;
		for (int length = array.Length; i < length; i++)
		{
			if (((Renderer)array[i]).gameObject.tag != "Ignore")
			{
				((Renderer)array[i]).material.shader = sshader;
			}
		}
	}

	public virtual void Main()
	{
	}
}
