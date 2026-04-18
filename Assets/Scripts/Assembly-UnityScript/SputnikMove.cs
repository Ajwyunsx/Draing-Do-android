using System;
using UnityEngine;

[Serializable]
public class SputnikMove : MonoBehaviour
{
	private int OldDAYTIME;

	public int beginTime;

	public float speed;

	private Transform myTransform;

	public bool isRainbow;

	public SputnikMove()
	{
		OldDAYTIME = -100000;
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (OldDAYTIME == Global.DAYTIME)
		{
			return;
		}
		OldDAYTIME = Global.DAYTIME;
		if (isRainbow)
		{
			if (Global.DAYTIME > 8 && Global.DAYTIME < 19 && Global.DAYRAIN > 40 && !(Mathf.Abs(Global.DAYCLOUD) <= 18f))
			{
				GetComponent<Renderer>().enabled = true;
			}
			else
			{
				GetComponent<Renderer>().enabled = false;
			}
		}
		float x = Mathf.Sin((float)(Global.DAYS * 24 + Global.DAYTIME) * 0.02f) * 17f;
		Vector3 localPosition = myTransform.localPosition;
		float num = (localPosition.x = x);
		Vector3 vector = (myTransform.localPosition = localPosition);
		float y = -10f + (float)(Global.DAYTIME - beginTime) * speed;
		Vector3 localPosition2 = myTransform.localPosition;
		float num2 = (localPosition2.y = y);
		Vector3 vector3 = (myTransform.localPosition = localPosition2);
	}

	public virtual void Main()
	{
	}
}
