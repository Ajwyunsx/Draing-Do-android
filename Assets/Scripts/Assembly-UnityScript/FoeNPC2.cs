using System;
using UnityEngine;

[Serializable]
public class FoeNPC2 : MonoBehaviour
{
	public Vector2 ChargeTime;

	private AI core;

	public FoeNPC2()
	{
		ChargeTime = new Vector2(20f, 25f);
	}

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
				core.NewAI("charge", string.Empty, (int)ChargeTime.x, (int)ChargeTime.y);
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
