using System;
using UnityEngine;

[Serializable]
public class PlaySoundFX : MonoBehaviour
{
	public virtual void PlaySound(AudioClip sfx)
	{
		if ((bool)sfx)
		{
			GetComponent<AudioSource>().clip = sfx;
			GetComponent<AudioSource>().Play();
		}
	}

	public virtual void Main()
	{
	}
}
