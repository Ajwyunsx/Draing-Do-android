using System;
using UnityEngine;

[Serializable]
public class ActByTouch : MonoBehaviour
{
	public GameObject TurnObject;

	public string MODE;

	private int coll;

	public string AnimStart;

	public string AnimEnd;

	public AudioClip SFXStart;

	public AudioClip SFXEnd;

	public bool IsAnimator;

	public GameObject inTheCage;

	public ActByTouch()
	{
		MODE = "anim";
	}

	public virtual void Awake()
	{
		if (((bool)SFXStart || (bool)SFXEnd) && GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			StartMODE();
			coll = 5;
		}
	}

	public virtual void StartMODE()
	{
		if ((bool)SFXStart)
		{
			GetComponent<AudioSource>().clip = SFXStart;
			GetComponent<AudioSource>().Play();
		}
		string mODE = MODE;
		if (mODE == "anim")
		{
			if (!string.IsNullOrEmpty(AnimStart))
			{
				if (!IsAnimator)
				{
					GetComponent<Animation>().CrossFade(AnimStart, 0.1f);
				}
				else
				{
					GetComponent<Animator>().CrossFade(AnimStart, 0.1f);
				}
			}
		}
		else if (mODE == "turn" && !(TurnObject == null))
		{
			UnityEngine.Object.Instantiate(TurnObject, transform.position + new Vector3(0f, 0f, -2.5f), Quaternion.identity);
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void EndMODE()
	{
		if ((bool)SFXEnd)
		{
			GetComponent<AudioSource>().clip = SFXEnd;
			GetComponent<AudioSource>().Play();
		}
		string mODE = MODE;
		if (mODE == "anim" && !string.IsNullOrEmpty(AnimEnd))
		{
			if (!IsAnimator)
			{
				GetComponent<Animation>().CrossFade(AnimEnd, 0.1f);
			}
			else
			{
				GetComponent<Animator>().CrossFade(AnimEnd, 0.1f);
			}
		}
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void SendFree()
	{
		if ((bool)inTheCage)
		{
			inTheCage.SendMessage("Freedom", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Quest(string name)
	{
		Global.AddQuest(name);
	}

	public virtual void Main()
	{
	}
}
