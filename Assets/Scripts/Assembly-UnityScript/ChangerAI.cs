using System;
using UnityEngine;

[Serializable]
public class ChangerAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public float WakeDistance;

	public float EvadeX;

	public float EvadeY;

	public Vector2 TimerForTurn;

	public int ChanceForTurn;

	private int TurnTimer;

	public ChangerAI()
	{
		WakeDistance = 5f;
		EvadeX = 2f;
		EvadeY = 2f;
		TimerForTurn = new Vector2(50f, 250f);
		ChanceForTurn = 50;
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		core.NewAI("sleep", "Pony Idle", 50, 55);
		EvadeX *= UnityEngine.Random.Range(0.5f, 3f);
		EvadeY *= UnityEngine.Random.Range(0.9f, 2f);
		core.Speed *= UnityEngine.Random.Range(0.75f, 1.5f);
		if (UnityEngine.Random.Range(1, 100) < ChanceForTurn)
		{
			TurnTimer = (int)UnityEngine.Random.Range(TimerForTurn.x, TimerForTurn.y);
		}
	}

	public virtual void FixedUpdate()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		if (TurnTimer > 0 && core.ai != "sleep")
		{
			TurnTimer--;
			if (TurnTimer <= 0)
			{
				core.CreatePartsByStrike();
				SendMessage("Change", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		switch (core.ai)
		{
		case "sleep":
			if (core.Distance(core.trans.position, core.target.position) < WakeDistance || core.HurtTimer > 0)
			{
				core.NewAI("fly", "Pony Fly", 50, 55);
			}
			break;
		case "fly":
			core.MoveToX(core.target.position.x, EvadeX);
			core.MoveToY(core.target.position.y, EvadeY);
			if (!(core.target.position.y <= transform.position.y))
			{
				core.Speed.y = 1f;
			}
			core.LookTo(core.target.position, 1);
			if (core.timer <= 0)
			{
				if (!(core.Distance2D(core.target.position.x, core.trans.position.x) >= 5f))
				{
					core.NewAI("charge", "Pony FCharge", 50, 50);
				}
				else
				{
					core.timer = UnityEngine.Random.Range(50, 150);
				}
			}
			break;
		case "charge":
			core.LookTo(core.target.position, 1);
			if (core.timer <= 0)
			{
				core.NewAI("strike", "Pony FStrike", 42, 45);
				core.DashTarget(core.target.position, 30f);
			}
			break;
		case "strike":
			if (core.timer <= 0)
			{
				core.NewAI("fly", "Pony Fly", 50, 55);
			}
			break;
		}
	}

	public virtual void Main()
	{
	}
}
