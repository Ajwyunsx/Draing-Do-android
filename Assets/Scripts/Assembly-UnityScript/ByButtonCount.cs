using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class ByButtonCount : MonoBehaviour
{
	public float factor;

	public ByButtonCount()
	{
		factor = -1.5f;
	}

	public virtual void Start()
	{
		if (!RuntimeServices.EqualityOperator(Global.ASKS, null) && Extensions.get_length((System.Array)Global.ASKS) > 0)
		{
			float y = (float)(9 - Extensions.get_length((System.Array)Global.ASKS)) * factor;
			Vector3 localPosition = transform.localPosition;
			float num = (localPosition.y = y);
			Vector3 vector = (transform.localPosition = localPosition);
		}
	}

	public virtual void Main()
	{
	}
}
