using System;
using UnityEngine;

[Serializable]
public class WeedAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public float WakeDistance;

	public float Regen;

	public WeedAI()
	{
		WakeDistance = 1f;
		Regen = 1f;
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		core.NewAI("sleep", string.Empty, 50, 55);
	}

	public virtual void FixedUpdate()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		string ai = core.ai;
		if (ai == "sleep")
		{
			if (core.Distance(core.trans.position, core.target.position) < WakeDistance || core.HurtTimer > 0)
			{
				core.NewAI("idle", "show", 150, 200);
			}
		}
		else if (ai == "idle")
		{
			core.Regeneration(Regen);
		}
	}

	public virtual void DISAPPEAR()
	{
		core.StartDeathSequence(100);
	}

	public virtual void Main()
	{
	}
}
