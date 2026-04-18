using System;
using UnityEngine;

[Serializable]
public class AmbientManager : MonoBehaviour
{
	private bool Once;

	public int oldMusic;

	public int oldSound;

	public virtual void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		if (Global.SOUND != 0)
		{
			AudioListener.volume = 0f;
		}
		else
		{
			AudioListener.volume = 1f;
		}
		oldSound = Global.SOUND;
	}

	public virtual void Start()
	{
		if (Global.ThatAmbient != GetComponent<AudioSource>().clip)
		{
			DestroyAll();
			PlayNewMusic();
		}
	}

	public virtual void DestroyAll()
	{
		GameObject[] array = null;
		array = GameObject.FindGameObjectsWithTag("MUSIC");
		if (array.Length <= 1)
		{
			return;
		}
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (array2[i].GetInstanceID() != gameObject.GetInstanceID())
			{
				array2[i].GetComponent<AudioSource>().Stop();
				UnityEngine.Object.Destroy(array2[i]);
			}
		}
	}

	public virtual void PlayNewMusic()
	{
		if (!Once)
		{
			Global.ThatAmbient = GetComponent<AudioSource>().clip;
			Once = true;
			GetComponent<AudioSource>().ignoreListenerVolume = true;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.MUSIC != oldMusic)
		{
			if (Global.MUSIC == 0)
			{
				GetComponent<AudioSource>().Stop();
			}
			else
			{
				GetComponent<AudioSource>().Play();
			}
		}
		oldMusic = Global.MUSIC;
		if (Global.SOUND != oldSound)
		{
			if (Global.SOUND == 0)
			{
				AudioListener.volume = 0f;
			}
			else
			{
				AudioListener.volume = 1f;
			}
		}
		oldSound = Global.SOUND;
	}

	public virtual void Main()
	{
	}
}
