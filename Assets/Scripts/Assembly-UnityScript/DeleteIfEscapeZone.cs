using System;
using UnityEngine;

[Serializable]
public class DeleteIfEscapeZone : MonoBehaviour
{
	public virtual void Start()
	{
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().enabled = false;
		}
	}

	public virtual void OnTriggerExit(Collider other)
	{
		MonoBehaviour.print(other.gameObject.name);
		other.gameObject.SendMessage("OnTime", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
