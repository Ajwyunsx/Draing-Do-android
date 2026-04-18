using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[AddComponentMenu("Alex Code/Item in Enemy")]
public class ItemInEnemy : MonoBehaviour
{
	public bool CreateByStart;

	public bool isFalling;

	public ItemOptions[] items;

	public float RandomSpeedX;

	public float RandomSpeedY;

	public float PlusSpeedY;

	public float RandomAngle;

	public bool NoDisappear;

	public float MultyChance;

	public float YPosOnStart;

	public ItemInEnemy()
	{
		MultyChance = 3.2f;
	}

	public virtual void Start()
	{
		if (CreateByStart)
		{
			Chance();
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void DISAPPEAR()
	{
		if (!NoDisappear)
		{
			Chance();
		}
	}

	public virtual void MakeFX()
	{
		if (NoDisappear)
		{
			Chance();
		}
	}

	public virtual void Chance()
	{
		int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)items));
		float num2 = items[num].chance * MultyChance;
		if (num2 == 0f)
		{
			num2 = 100f;
		}
		if ((bool)items[num].treasure && !(UnityEngine.Random.Range(0.5f, 100f) > num2))
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(items[num].treasure) as GameObject;
			Global.LastCreatedObject.name = items[num].treasure.name;
			Global.LastCreatedObject.transform.position = transform.position;
			float z = Global.CurrentPlayerObject.position.z + 0.015f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num3 = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			float y = Global.LastCreatedObject.transform.position.y + YPosOnStart;
			Vector3 position2 = Global.LastCreatedObject.transform.position;
			float num4 = (position2.y = y);
			Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
			if (isFalling)
			{
				Global.LastCreatedObject.SendMessage("Falling", transform.parent, SendMessageOptions.DontRequireReceiver);
			}
			Global.LastCreatedObject.SendMessage("TimerOn", null, SendMessageOptions.DontRequireReceiver);
			Global.LastCreatedObject.SendMessage("OnAppear", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)Global.LastCreatedObject.GetComponent<Rigidbody>())
			{
				Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(0f - RandomSpeedX, RandomSpeedX), UnityEngine.Random.Range(0f - RandomSpeedY, RandomSpeedY), 0f);
				float y2 = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity.y + PlusSpeedY;
				Vector3 velocity = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity;
				float num5 = (velocity.y = y2);
				Vector3 vector5 = (Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = velocity);
				int num6 = UnityEngine.Random.Range(-10, 10);
				Vector3 angularVelocity = Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity;
				float num7 = (angularVelocity.z = num6);
				Vector3 vector7 = (Global.LastCreatedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity);
			}
			float z2 = UnityEngine.Random.Range(0f - RandomAngle, RandomAngle);
			Vector3 localEulerAngles = Global.LastCreatedObject.transform.localEulerAngles;
			float num8 = (localEulerAngles.z = z2);
			Vector3 vector9 = (Global.LastCreatedObject.transform.localEulerAngles = localEulerAngles);
		}
	}

	public virtual void Main()
	{
	}
}
