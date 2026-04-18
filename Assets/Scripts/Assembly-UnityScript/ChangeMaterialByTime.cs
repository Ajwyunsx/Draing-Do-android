using System;
using UnityEngine;

[Serializable]
public class ChangeMaterialByTime : MonoBehaviour
{
	private Material material1;

	public Material material2;

	private int OldDAYTIME;

	public int from;

	public int till;

	public ChangeMaterialByTime()
	{
		OldDAYTIME = -100000;
		from = 6;
		till = 21;
	}

	public virtual void Start()
	{
		material1 = GetComponent<Renderer>().material;
	}

	public virtual void FixedUpdate()
	{
		if (OldDAYTIME != Global.DAYTIME)
		{
			OldDAYTIME = Global.DAYTIME;
			if (Global.DAYTIME > from && Global.DAYTIME < till)
			{
				GetComponent<Renderer>().material = material1;
			}
			else
			{
				GetComponent<Renderer>().material = material2;
			}
		}
	}

	public virtual void Main()
	{
	}
}
