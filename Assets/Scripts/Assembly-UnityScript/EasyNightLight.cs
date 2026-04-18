using System;
using UnityEngine;

[Serializable]
public class EasyNightLight : MonoBehaviour
{
	public int from;

	public int till;

	public bool OnInvert;

	public EasyNightLight()
	{
		from = 6;
		till = 21;
		OnInvert = true;
	}

	public virtual void Awake()
	{
		if (Global.DAYTIME > from && Global.DAYTIME < till)
		{
			if (OnInvert)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		else if (!OnInvert)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
