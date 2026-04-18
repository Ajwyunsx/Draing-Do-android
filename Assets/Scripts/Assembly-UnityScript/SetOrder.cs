using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class SetOrder : MonoBehaviour
{
	public int Order;

	public bool AllChildrens;

	public virtual void Awake()
	{
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().sortingOrder = Order;
		}
		if (AllChildrens)
		{
			Component[] array = null;
			array = GetComponentsInChildren(typeof(Renderer));
			int i = 0;
			Component[] array2 = array;
			for (int length = array2.Length; i < length; i++)
			{
				((Renderer)array2[i]).sortingOrder = Order;
			}
		}
	}

	public virtual void Main()
	{
	}
}
