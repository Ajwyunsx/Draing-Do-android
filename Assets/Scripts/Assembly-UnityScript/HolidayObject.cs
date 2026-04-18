using System;
using UnityEngine;

[Serializable]
public class HolidayObject : MonoBehaviour
{
	public virtual void Start()
	{
		if (Global.DAYS % 7 != 6)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
