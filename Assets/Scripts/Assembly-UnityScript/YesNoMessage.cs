using System;
using UnityEngine;

[Serializable]
public class YesNoMessage : MonoBehaviour
{
	public virtual void Awake()
	{
		gameObject.SendMessage("Rename", Global.YesMessage, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
