using System;
using UnityEngine;

[Serializable]
public class ToolOptions
{
	public bool neverending;

	public string animation;

	public Vector3 playerSpeed;

	public int blockTime;

	public GameObject dropObject;

	public bool hideToolInAction;

	public Vector3 dropSpeed;

	public string message;

	public AudioClip sound;

	public int ImmortalTime;

	public float mana;

	public ToolOptions()
	{
		neverending = true;
		blockTime = 40;
		message = string.Empty;
	}
}
