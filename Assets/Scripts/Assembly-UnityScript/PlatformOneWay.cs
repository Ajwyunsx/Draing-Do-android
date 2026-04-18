using System;
using UnityEngine;

[Serializable]
public class PlatformOneWay : MonoBehaviour
{
	public virtual void OnCollisionStay(Collision collision)
	{
		if (!(collision.gameObject.tag != "Player"))
		{
			Global.OnPlatformDown = 5;
		}
	}

	public virtual void Main()
	{
	}
}
