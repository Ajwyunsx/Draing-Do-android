using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class DirectionHUD : MonoBehaviour
{
	private Transform DirectionObject;

	private Transform myTransform;

	public bool CheckAreaZone;

	private string CurrentMission;

	public DirectionHUD()
	{
		CheckAreaZone = true;
	}

	public virtual void Start()
	{
		myTransform = transform;
		myTransform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
		FindTheDir();
	}

	public virtual void FixedUpdate()
	{
		if (CurrentMission != Global.CurrentMission)
		{
			FindTheDir();
		}
		if (string.IsNullOrEmpty(Global.CurrentMission) || !DirectionObject)
		{
			return;
		}
		myTransform.position = Vector3.Lerp(Global.CurrentPlayerObject.position, DirectionObject.position, 0.75f);
		if (CheckAreaZone)
		{
			float z = Global.CurrentPlayerObject.position.z - 4f;
			Vector3 position = myTransform.position;
			float num = (position.z = z);
			Vector3 vector = (myTransform.position = position);
			float num2 = 3.7f;
			float num3 = 1.4f;
			Vector3 position2 = Camera.main.transform.position;
			if (!(myTransform.position.x <= position2.x + num2))
			{
				float x = position2.x + num2;
				Vector3 position3 = myTransform.position;
				float num4 = (position3.x = x);
				Vector3 vector3 = (myTransform.position = position3);
			}
			if (!(myTransform.position.x >= position2.x - num2))
			{
				float x2 = position2.x - num2;
				Vector3 position4 = myTransform.position;
				float num5 = (position4.x = x2);
				Vector3 vector5 = (myTransform.position = position4);
			}
			if (!(myTransform.position.y <= position2.y + num3))
			{
				float y = position2.y + num3;
				Vector3 position5 = myTransform.position;
				float num6 = (position5.y = y);
				Vector3 vector7 = (myTransform.position = position5);
			}
			if (!(myTransform.position.y >= position2.y - num3))
			{
				float y2 = position2.y - num3;
				Vector3 position6 = myTransform.position;
				float num7 = (position6.y = y2);
				Vector3 vector9 = (myTransform.position = position6);
			}
		}
		else
		{
			float z2 = Global.CurrentPlayerObject.position.z - 1f;
			Vector3 position7 = myTransform.position;
			float num8 = (position7.z = z2);
			Vector3 vector11 = (myTransform.position = position7);
		}
		float z3 = Mathf.Atan2(DirectionObject.position.y - myTransform.position.y, DirectionObject.position.x - myTransform.position.x) * 57.29578f - 90f;
		Vector3 eulerAngles = myTransform.eulerAngles;
		float num9 = (eulerAngles.z = z3);
		Vector3 vector13 = (myTransform.eulerAngles = eulerAngles);
	}

	public virtual void FindTheDir()
	{
	}

	public virtual void FindTheDoor()
	{
		if ((bool)Global.PrioritetMapSlot)
		{
			DirectionObject = Global.PrioritetMapSlot.transform;
			return;
		}
		MonoBehaviour.print("Global.CurrentMission: " + Global.CurrentMission);
		if (string.IsNullOrEmpty(Global.CurrentMission))
		{
			ZeroPosition();
			return;
		}
		DirectionObject = null;
		bool flag = false;
		GameObject gameObject = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("forDirect");
		IEnumerator enumerator = array.GetEnumerator();
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is GameObject))
			{
				obj = RuntimeServices.Coerce(obj, typeof(GameObject));
			}
			gameObject = (GameObject)obj;
			if (Global.CurrentMission == gameObject.name)
			{
				DirectionObject = gameObject.transform;
				flag = true;
			}
		}
		if (!flag)
		{
			IEnumerator enumerator2 = array.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				if (!(obj2 is GameObject))
				{
					obj2 = RuntimeServices.Coerce(obj2, typeof(GameObject));
				}
				gameObject = (GameObject)obj2;
				if (gameObject.name == "back")
				{
					DirectionObject = gameObject.transform;
					flag = true;
				}
			}
		}
		if (!flag)
		{
			ZeroPosition();
		}
		CurrentMission = Global.CurrentMission;
	}

	public virtual void ZeroPosition()
	{
		DirectionObject = null;
		myTransform.position = new Vector3(-10000f, -10000f, -10000f);
	}

	public virtual void Main()
	{
	}
}
