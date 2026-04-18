using System;
using UnityEngine;

[Serializable]
public class MiniSpiderAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		core.NewAI("idle", string.Empty, 50, 55);
	}

	public virtual void FixedUpdate()
	{
		if (!TalkPause.IsGameplayBlocked())
		{
			string ai = core.ai;
			if (ai == "idle" && core.timer <= 0)
			{
				core.NewAI("idle", string.Empty, 150, 200);
			}
		}
	}

	public virtual void Main()
	{
	}
}
