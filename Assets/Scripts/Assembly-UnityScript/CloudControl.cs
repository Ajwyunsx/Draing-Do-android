using System;
using UnityEngine;

[Serializable]
public class CloudControl : MonoBehaviour
{
	private int OldDAYTIME;

	private Transform myTransform;

	public CloudControl()
	{
		OldDAYTIME = -100000;
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (OldDAYTIME != Global.DAYTIME)
		{
			OldDAYTIME = Global.DAYTIME;
			float dAYCLOUD = Global.DAYCLOUD;
			Vector3 localPosition = myTransform.localPosition;
			float num = (localPosition.y = dAYCLOUD);
			Vector3 vector = (myTransform.localPosition = localPosition);
		}
	}

	public virtual void Main()
	{
	}
}
