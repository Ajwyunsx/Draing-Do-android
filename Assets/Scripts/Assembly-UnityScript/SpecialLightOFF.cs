using System;
using UnityEngine;

[Serializable]
public class SpecialLightOFF : MonoBehaviour
{
	private bool pause;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (Global.Pause != pause)
		{
			pause = Global.Pause;
			if (Global.Pause)
			{
				GetComponent<Light>().enabled = false;
			}
			else
			{
				GetComponent<Light>().enabled = true;
			}
		}
	}

	public virtual void Main()
	{
	}
}
