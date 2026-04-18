using System;
using UnityEngine;

[Serializable]
public class Sounds : MonoBehaviour
{
	public AudioClip SFX_Shot;

	public AudioClip SFX_Crush;

	public AudioClip SFX_HP;

	public AudioClip SFX_Gold;

	public AudioClip SFX_Win;

	public AudioClip SFX_Win2;

	public AudioClip MFX_GameOver;

	public AudioClip MFX_Level;

	public Sounds()
	{
		SFX_Shot = Resources.Load("SOUNDS/Crush") as AudioClip;
		SFX_Crush = Resources.Load("SOUNDS/Crush") as AudioClip;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void Sound(string name)
	{
		switch (name)
		{
		case "crush":
			GetComponent<AudioSource>().PlayOneShot(SFX_Crush);
			break;
		case "shot":
			GetComponent<AudioSource>().PlayOneShot(SFX_Shot);
			break;
		case "hp":
			GetComponent<AudioSource>().PlayOneShot(SFX_HP);
			break;
		case "gold":
			GetComponent<AudioSource>().PlayOneShot(SFX_Gold);
			break;
		case "win":
			GetComponent<AudioSource>().PlayOneShot(SFX_Win);
			break;
		case "win2":
			GetComponent<AudioSource>().PlayOneShot(SFX_Win2);
			break;
		}
	}

	public virtual void Music(string name)
	{
		if (name == "GameOver")
		{
			GetComponent<AudioSource>().clip = MFX_GameOver;
			GetComponent<AudioSource>().Play();
		}
		else if (name == "Level")
		{
			GetComponent<AudioSource>().clip = MFX_Level;
			GetComponent<AudioSource>().Play();
		}
	}

	public virtual void Main()
	{
	}
}
