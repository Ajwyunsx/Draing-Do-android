using System;
using UnityEngine;

[Serializable]
public class RopeFind : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		transform.position = transform.parent.position;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			transform.parent.SendMessage("SetRopeObject", other.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
