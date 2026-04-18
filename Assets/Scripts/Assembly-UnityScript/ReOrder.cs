using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("MY SCRIPTS/Re Order")]
[ExecuteInEditMode]
public class ReOrder : MonoBehaviour
{
	[HideInInspector]
	public float oldX;

	public int order;

	public bool off;

	private float highOld;

	public Transform OtherTransform;

	public float high;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (off)
		{
			return;
		}
		Component[] array = null;
		if (OtherTransform == null)
		{
			if (oldX != transform.position.x || highOld != high)
			{
				highOld = high;
				oldX = transform.position.x;
				order = (int)((oldX - high) * 20f);
				order = Mathf.Clamp(order, 0, 2000);
				array = GetComponentsInChildren(typeof(Renderer));
				int i = 0;
				Component[] array2 = array;
				for (int length = array2.Length; i < length; i++)
				{
					((Renderer)array2[i]).sortingOrder = order;
				}
			}
		}
		else if (oldX != transform.position.x || highOld != high)
		{
			highOld = high;
			oldX = OtherTransform.position.x;
			order = (int)((oldX - high) * 20f);
			order = Mathf.Clamp(order, 0, 2000);
			array = GetComponentsInChildren(typeof(Renderer));
			int j = 0;
			Component[] array3 = array;
			for (int length2 = array3.Length; j < length2; j++)
			{
				((Renderer)array3[j]).sortingOrder = order;
			}
		}
	}

	public virtual void OrderOff(int i)
	{
		off = true;
		Component[] componentsInChildren = GetComponentsInChildren(typeof(Renderer));
		int j = 0;
		Component[] array = componentsInChildren;
		for (int length = array.Length; j < length; j++)
		{
			((Renderer)array[j]).sortingOrder = i;
		}
	}

	public virtual void OrderOn()
	{
		off = false;
		oldX = 0f - transform.position.x;
	}

	public virtual void Main()
	{
	}
}
