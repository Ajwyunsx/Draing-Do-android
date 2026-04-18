using System;
using UnityEngine;

[Serializable]
public class FoeNPC : MonoBehaviour
{
	private AI core;

	public virtual void Start()
	{
		core = GetComponent<AI>();
	}

	public virtual void FixedUpdate()
	{
		if (core.target == null && Global.CurrentPlayerObject != null)
		{
			core.target = Global.CurrentPlayerObject;
		}
		if (core.target == null || TalkPause.IsGameplayBlocked())
		{
			return;
		}
		switch (core.ai)
		{
		case "idle":
			core.LookTo(core.target.position, core.lookCorr * 1);
			core.MoveTo(core.target.position, core.distance);
			core.Walk();
			if (core.timer <= 0)
			{
				switch (UnityEngine.Random.Range(0, 2))
				{
				case 0:
					core.NewAI("toBlock", "idle", 5, 5);
					break;
				case 1:
					break;
				default:
					return;
				}
				core.NewAI("charge", string.Empty, 20, 25);
			}
			break;
		case "toBlock":
			if (core.timer <= 0)
			{
				core.NewAI("block", string.Empty, 55, 65);
			}
			break;
		case "charge":
			core.LookTo(core.target.position, core.lookCorr * 1);
			if (core.timer <= 0)
			{
				core.NewAI("strike", string.Empty, 30, 32);
			}
			break;
		case "strike":
			if (core.timer <= 0)
			{
				core.NewAI("idle", string.Empty, 55, 65);
			}
			break;
		case "block":
			core.LookTo(core.target.position, core.lookCorr * 1);
			if (core.timer <= 0)
			{
				core.NewAI("idle", string.Empty, 55, 65);
			}
			break;
		case "dead":
			break;
		}
	}

	public virtual void Main()
	{
	}
}
