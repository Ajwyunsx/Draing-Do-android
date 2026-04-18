using System;
using UnityEngine;

[Serializable]
public class EarthQuakeDisappear : MonoBehaviour
{
	public float QUAKE;

	public float FACTOR;

	public AudioClip SoundFX;

	public EarthQuakeDisappear()
	{
		QUAKE = 75f;
		FACTOR = 30f;
	}

	public virtual void DISAPPEAR()
	{
		Global.QuakeStart((int)QUAKE, FACTOR);
		if ((bool)SoundFX)
		{
			AudioSource.PlayClipAtPoint(SoundFX, transform.position);
		}
	}

	public virtual void Main()
	{
	}
}
