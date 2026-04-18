using System;
using UnityEngine;

[Serializable]
public class ID_SENSOR : MonoBehaviour
{
	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "ID_Door")
		{
			other.gameObject.SendMessage("ID_Check", "id", SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
