using System;
using UnityEngine;

[Serializable]
public class Calendar : MonoBehaviour
{
	public string[] DayNames;

	private int oldDayNum;

	public GameObject text;

	public bool Turn;

	public float Correcter;

	public Calendar()
	{
		oldDayNum = -1;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (oldDayNum != Global.DAYS)
		{
			oldDayNum = Global.DAYS;
			if (Turn)
			{
				float z = (float)(Global.DAYS % 7) * 51.42f;
				Vector3 localEulerAngles = transform.localEulerAngles;
				float num = (localEulerAngles.z = z);
				Vector3 vector = (transform.localEulerAngles = localEulerAngles);
			}
			else
			{
				text.SendMessage("Rename", Lang.Text(DayNames[Global.DAYS % 7]), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
