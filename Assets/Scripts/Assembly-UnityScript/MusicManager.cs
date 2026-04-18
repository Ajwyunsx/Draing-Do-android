using System;
using UnityEngine;

[Serializable]
public class MusicManager : MonoBehaviour
{
	private bool Once;

	public int oldMusic;

	public int oldSound;

	public float musicVolume;

	[NonSerialized]
	public static AudioClip oldClip;

	[NonSerialized]
	public static bool VolumeTemp;

	[NonSerialized]
	public static bool StopOk;

	public MusicManager()
	{
		musicVolume = 0.5f;
	}

	public virtual void Awake()
	{
		transform.parent = null;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		if (Global.SOUND == 0)
		{
			AudioListener.volume = 0f;
		}
		else
		{
			AudioListener.volume = 1f;
		}
		oldSound = Global.SOUND;
		oldClip = GetComponent<AudioSource>().clip;
	}

	public virtual void Start()
	{
		transform.parent = null;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		if (Global.ThatMusic != GetComponent<AudioSource>().clip)
		{
			MonoBehaviour.print("Play new: " + Global.ThatMusic);
			DestroyNotMe();
			PlayNewMusic();
		}
		else
		{
			DestroyAll();
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
			if (!array2[i].GetComponent<AudioSource>().isPlaying)
			{
				array2[i].GetComponent<AudioSource>().Stop();
				UnityEngine.Object.Destroy(array2[i]);
			}
		}
	}

	public virtual void DestroyNotMe()
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
			if (gameObject != array2[i])
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
			Global.ThatMusic = GetComponent<AudioSource>().clip;
			Once = true;
			GetComponent<AudioSource>().ignoreListenerVolume = true;
			Global.GlobalMusic = GetComponent<AudioSource>();
		}
	}

	public virtual void Update()
	{
		if (Global.MUSIC != oldMusic)
		{
			if (Global.MUSIC == 0)
			{
				GetComponent<AudioSource>().Stop();
			}
			else
			{
				GetComponent<AudioSource>().volume = musicVolume;
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

	public static void Default()
	{
		if (Global.MUSIC != 0)
		{
			if (Global.ThatMusic != oldClip)
			{
				Global.ThatMusic = null;
			}
			VolumeTemp = true;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.MUSIC == 0)
		{
			return;
		}
		if (Global.ThatMusic == null)
		{
			if (GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Stop();
			}
			GetComponent<AudioSource>().clip = oldClip;
			Global.ThatMusic = GetComponent<AudioSource>().clip;
			GetComponent<AudioSource>().volume = musicVolume;
			GetComponent<AudioSource>().Play();
		}
		if (StopOk && VolumeTemp)
		{
			GetComponent<AudioSource>().volume = musicVolume;
			GetComponent<AudioSource>().Play();
			MonoBehaviour.print("play3");
			StopOk = false;
		}
		VolumeTemp = false;
	}

	public virtual void Main()
	{
	}
}
