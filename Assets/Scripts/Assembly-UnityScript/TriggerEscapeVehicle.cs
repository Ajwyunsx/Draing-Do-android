using System;
using UnityEngine;

[Serializable]
public class TriggerEscapeVehicle : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		other.SendMessage("EscapeVehicle", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
