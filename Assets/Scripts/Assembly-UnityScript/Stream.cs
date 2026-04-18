using System;
using UnityEngine;

[Serializable]
public class Stream : MonoBehaviour
{
	public Vector3 StreamSpeeds;

	public virtual void Awake()
	{
	}

	public virtual void OnTriggerStay(Collider other)
	{
		other.transform.SendMessage("StreamSpeed", StreamSpeeds, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
