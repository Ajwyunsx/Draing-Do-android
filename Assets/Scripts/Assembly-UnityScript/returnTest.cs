using System;
using UnityEngine;

[Serializable]
public class returnTest : MonoBehaviour
{
	public virtual void OnTriggerStay()
	{
		GetComponent<Collider>().enabled = false;
		GetComponent<AudioSource>().Play();
	}

	public virtual void Main()
	{
	}
}
