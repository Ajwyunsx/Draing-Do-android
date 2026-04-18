using System;
using UnityEngine;

[Serializable]
public class ExplodeWithTouch : MonoBehaviour
{
	public virtual void OnCollisionEnter(Collision other)
	{
		other.gameObject.SendMessage("OnEnemyCollision", null, SendMessageOptions.DontRequireReceiver);
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("Other/explosion")) as GameObject;
		Global.LastCreatedObject.transform.position = transform.position;
		GenerateItem();
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void GenerateItem()
	{
		switch (UnityEngine.Random.Range(0, 50))
		{
		case 4:
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("Items/HP")) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position;
			int num3 = 4;
			Vector3 velocity2 = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity;
			float num4 = (velocity2.y = num3);
			Vector3 vector3 = (Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = velocity2);
			break;
		}
		case 7:
		case 10:
		case 25:
		case 35:
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("Items/Money")) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position;
			int num = 4;
			Vector3 velocity = Global.LastCreatedObject.GetComponent<Rigidbody>().velocity;
			float num2 = (velocity.y = num);
			Vector3 vector = (Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = velocity);
			break;
		}
		}
	}

	public virtual void Main()
	{
	}
}
