using System;
using UnityEngine;

[Serializable]
public class ItemOptions
{
	public GameObject treasure;

	public float chance;

	public ItemOptions()
	{
		chance = 100f;
	}
}
