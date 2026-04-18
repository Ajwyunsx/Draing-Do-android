using System;
using UnityEngine;

[Serializable]
public class AnimAI : MonoBehaviour
{
	public AudioClip SFXStrike;

	public string StrikeAnim;

	public string WaitAnim;

	private Transform trans;

	public int POWER;

	public float Distance;

	private string AI;

	private int AITimer;

	public int StrikeTimer;

	public int WaitTimer;

	public bool LikeTrapButton;

	public int ButtonTimer;

	public bool Always;

	public AnimAI()
	{
		POWER = 1;
		Distance = 9f;
		StrikeTimer = 40;
		WaitTimer = 100;
		ButtonTimer = 50;
	}

	public virtual void Start()
	{
		AI = "wait";
		trans = transform;
	}

	public virtual void FixedUpdate()
	{
		if (AI == "wait")
		{
			if (!Always)
			{
				if (!LikeTrapButton && (bool)Global.CurrentPlayerObject && !(Vector3.Distance(Global.CurrentPlayerObject.position, trans.position) > Distance))
				{
					AI = "strike";
					AITimer = StrikeTimer;
					Animate(StrikeAnim, 0.1f);
				}
			}
			else
			{
				AI = "charge";
				AITimer = ButtonTimer;
			}
		}
		if (AI == "charge")
		{
			AITimer--;
			if (AITimer <= 0)
			{
				AI = "strike";
				AITimer = StrikeTimer;
				Animate(StrikeAnim, 0.1f);
			}
		}
		if (AITimer > 0 && AI == "strike")
		{
			AITimer--;
			if (AITimer <= 0)
			{
				AI = "sleep";
				Animate(WaitAnim, 0.2f);
				AITimer = WaitTimer;
			}
		}
		if (AITimer > 0 && AI == "sleep")
		{
			AITimer--;
			if (AITimer <= 0)
			{
				AI = "wait";
			}
		}
	}

	public virtual void Animate(string anim, float time)
	{
		GetComponent<Animator>().CrossFade(anim, time);
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.gameObject.tag != "Player") && AI == "wait" && LikeTrapButton)
		{
			AI = "charge";
			AITimer = ButtonTimer;
		}
	}

	public virtual void Main()
	{
	}
}
