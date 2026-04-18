using System;
using UnityEngine;

[Serializable]
public class DarkMoon : MonoBehaviour
{
	private int OldDAYTIME;

	private Transform myTransform;

	private Vector3 startPosition;

	public DarkMoon()
	{
		OldDAYTIME = -100000;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		startPosition = myTransform.localPosition;
	}

	public virtual void FixedUpdate()
	{
		if (OldDAYTIME != Global.DAYTIME)
		{
			OldDAYTIME = Global.DAYTIME;
			GetComponent<Renderer>().material.color = Camera.main.backgroundColor;
			MonoBehaviour.print("color: " + Camera.main.backgroundColor);
			float x = startPosition.x + Mathf.Sin((float)(Global.DAYS * 24 + Global.DAYTIME) * 0.02f) * -0.25f;
			Vector3 localPosition = myTransform.localPosition;
			float num = (localPosition.x = x);
			Vector3 vector = (myTransform.localPosition = localPosition);
			float y = startPosition.y + Mathf.Sin((float)(Global.DAYS * 24 + Global.DAYTIME) * 0.02f) * -0.25f;
			Vector3 localPosition2 = myTransform.localPosition;
			float num2 = (localPosition2.y = y);
			Vector3 vector3 = (myTransform.localPosition = localPosition2);
		}
	}

	public virtual void Main()
	{
	}
}
