using System;
using UnityEngine;

[Serializable]
public class XMap : MonoBehaviour
{
	public virtual void Start()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 50, Screen.height - 50, 0.75f));
		gameObject.transform.position = position;
	}

	public virtual void Main()
	{
	}
}
