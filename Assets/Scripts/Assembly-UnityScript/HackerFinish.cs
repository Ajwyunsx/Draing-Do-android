using System;
using UnityEngine;

[Serializable]
public class HackerFinish : MonoBehaviour
{
	public GameObject PC;

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PC.SendMessage("Hack", null, SendMessageOptions.DontRequireReceiver);
			other.gameObject.SendMessage("EscapeVehicle", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
