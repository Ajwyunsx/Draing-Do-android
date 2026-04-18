using System;
using UnityEngine;

[Serializable]
public class DirectionQuest : MonoBehaviour
{
	private Transform myTransform;

	public virtual void Start()
	{
		myTransform = transform;
		myTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	public virtual void Update()
	{
		myTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f) + new Vector3(0.1f, 0.1f, 0.1f) * Mathf.Sin(Time.time * 3f);
		if ((bool)Global.QuestLevelObject)
		{
			MakeDirection();
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("forDirect");
		if ((bool)gameObject)
		{
			Global.QuestLevelObject = gameObject.transform;
			MakeDirection();
			return;
		}
		int num = -10000;
		Vector3 position = myTransform.position;
		float num2 = (position.z = num);
		Vector3 vector = (myTransform.position = position);
	}

	public virtual void MakeDirection()
	{
		if (!(Global.CurrentPlayerObject == null))
		{
			myTransform.position = Vector3.Lerp(Global.CurrentPlayerObject.position, Global.QuestLevelObject.position, 0.5f);
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
			float z2 = Mathf.Atan2(Global.QuestLevelObject.position.y - myTransform.position.y, Global.QuestLevelObject.position.x - myTransform.position.x) * 57.29578f - 90f;
			Vector3 eulerAngles = myTransform.eulerAngles;
			float num8 = (eulerAngles.z = z2);
			Vector3 vector11 = (myTransform.eulerAngles = eulerAngles);
		}
	}

	public virtual void Main()
	{
	}
}
