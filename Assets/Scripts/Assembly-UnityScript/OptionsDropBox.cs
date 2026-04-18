using System;
using UnityEngine;

[Serializable]
public class OptionsDropBox
{
	public string animation;

	public Vector3 position;

	public Vector3 dropspeed;

	public string message;

	public AudioClip sound;

	public OptionsDropBox()
	{
		animation = "drop";
		position = new Vector3(-0.3f, 0f, 0f);
		dropspeed = new Vector3(5f, 2f, 0f);
		message = string.Empty;
	}
}
