using System;
using UnityEngine;

[Serializable]
public class AmbientChanger : MonoBehaviour
{
	public AudioClip SFXDefault;

	public AudioClip SFXNight;

	public AudioClip SFXRain;

	private string Situation;

	private string oldSituation;

	public AmbientChanger()
	{
		Situation = "def";
	}

	public virtual void Awake()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.CheckRain())
		{
			Situation = "rain";
		}
		else if (Global.DAYTIME > 5 && Global.DAYTIME < 22)
		{
			Situation = "def";
		}
		else
		{
			Situation = "night";
		}
		if (oldSituation != Situation)
		{
			oldSituation = Situation;
			GetComponent<AudioSource>().Stop();
			switch (Situation)
			{
			case "def":
				GetComponent<AudioSource>().clip = SFXDefault;
				break;
			case "night":
				GetComponent<AudioSource>().clip = SFXNight;
				break;
			case "rain":
				GetComponent<AudioSource>().clip = SFXRain;
				break;
			}
			GetComponent<AudioSource>().Play();
		}
	}

	public virtual void Main()
	{
	}
}
