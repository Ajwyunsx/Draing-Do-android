using System;
using UnityEngine;

[Serializable]
public class InGameMessage : MonoBehaviour
{
	public GameObject Text;

	private bool InProcess;

	private int Timer;

	private int Mode;

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (Global.GameMessage.length > 0 && !InProcess)
		{
			Text.SendMessage("Rename", Global.GameMessage[0], SendMessageOptions.DontRequireReceiver);
			InProcess = true;
			Timer = 150;
			Mode = -1;
			Global.GameMessage.RemoveAt(0);
		}
		if (Timer > 0)
		{
			Timer--;
			if (Timer == 0)
			{
				if (Mode == -1)
				{
					Timer = 15;
					Mode = 1;
				}
				else if (Mode == 1)
				{
					Mode = 0;
					InProcess = false;
				}
			}
		}
		if (InProcess)
		{
			float y = Mathf.Lerp(transform.localPosition.y, (float)Mode * 0.07f, 0.125f);
			Vector3 localPosition = transform.localPosition;
			float num = (localPosition.y = y);
			Vector3 vector = (transform.localPosition = localPosition);
		}
	}

	public virtual void Main()
	{
	}
}
