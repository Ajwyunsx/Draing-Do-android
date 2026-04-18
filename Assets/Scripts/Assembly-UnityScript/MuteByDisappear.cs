using System;
using UnityEngine;

[Serializable]
public class MuteByDisappear : MonoBehaviour
{
	private float makeMute;

	private bool once;

	public GameObject @object;

	public AudioClip music;

	private int once2;

	public float music_Volume;

	public MuteByDisappear()
	{
		music_Volume = 0.4f;
	}

	public virtual void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		GetComponent<AudioSource>().Stop();
	}

	public virtual void FixedUpdate()
	{
		if (@object == null && !once)
		{
			once = true;
			makeMute = music_Volume;
		}
		if (once)
		{
			if (!(makeMute <= 0f))
			{
				makeMute -= 0.01f;
				GetComponent<AudioSource>().volume = makeMute;
				if (!(makeMute > 0f))
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
		else if (once2 < 10)
		{
			once2++;
			if (once2 == 10 && (bool)music && Global.MUSIC != 0)
			{
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = music;
				GetComponent<AudioSource>().loop = true;
				GetComponent<AudioSource>().Play();
				GetComponent<AudioSource>().volume = music_Volume;
			}
		}
	}

	public virtual void Main()
	{
	}
}
