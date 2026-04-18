using System;
using UnityEngine;

[Serializable]
public class SandTrap : MonoBehaviour
{
	public float SpeedFactor;

	public SandTrap()
	{
		SpeedFactor = 0.75f;
	}

	public virtual void Start()
	{
	}

	public virtual void OnTriggerStay(Collider other)
	{
		bool flag = false;
		if (other.tag == "Enemy")
		{
			flag = true;
		}
		if (other.tag == "Player")
		{
			flag = true;
		}
		if (flag)
		{
			other.SendMessage("SandControl", SpeedFactor, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
