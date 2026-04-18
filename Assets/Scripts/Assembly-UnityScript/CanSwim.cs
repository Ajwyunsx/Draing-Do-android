using System;
using UnityEngine;

[Serializable]
public class CanSwim : MonoBehaviour
{
	private Transform Parent;

	public bool Breather;

	private bool CheckBreath;

	public virtual void Start()
	{
		Parent = transform.parent;
	}

	public virtual void FixedUpdate()
	{
		if (Breather)
		{
			Parent.SendMessage("OxigenCheck", CheckBreath, SendMessageOptions.DontRequireReceiver);
			CheckBreath = true;
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!Breather)
		{
			if (other.tag == "Deep" && !(Parent == null))
			{
				Parent.SendMessage("InWater", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			CheckBreath = false;
		}
	}

	public virtual void Main()
	{
	}
}
