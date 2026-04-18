using System;
using UnityEngine;

[Serializable]
public class FlyAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	private Vector3 rnd;

	public bool isSleep;

	public float WokenDistant;

	public bool BackAI;

	public bool DashJump;

	public Vector2 MovingArea;

	public Vector2 dashSpeed;

	public int dashTime;

	public bool UnparentByWake;

	public float stateZ;

	public FlyAI()
	{
		WokenDistant = 2f;
		MovingArea = new Vector2(2f, 2f);
		dashSpeed = new Vector2(25f, 4f);
		dashTime = 10;
		stateZ = -0.9f;
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		if (isSleep)
		{
			core.NewAI("sleep", string.Empty, 0, 0);
			core.LookRnd();
			core.Layer("shift");
			return;
		}
		core.NewAI("idle", string.Empty, 0, 0);
		float z = stateZ;
		Vector3 position = transform.position;
		float num = (position.z = z);
		Vector3 vector = (transform.position = position);
		core.Layer("fly");
		core.Coords = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f), stateZ);
	}

	public virtual void FixedUpdate()
	{
		if (core.target == null && Global.CurrentPlayerObject != null)
		{
			core.target = Global.CurrentPlayerObject;
		}
		if (TalkPause.IsGameplayBlocked() || core.target == null)
		{
			return;
		}
		switch (core.ai)
		{
		case "sleep":
			if (core.Distance(core.target.position, transform.position) <= WokenDistant || core.HurtTimer > 0)
			{
				if (UnparentByWake)
				{
					transform.parent = null;
				}
				core.NewAI("woken", "idle", 45, 50);
				core.Coords = new Vector3(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f), stateZ);
				float z = stateZ;
				Vector3 position = transform.position;
				float num = (position.z = z);
				Vector3 vector = (transform.position = position);
			}
			break;
		case "woken":
			core.MoveTo(core.Coords, 1f);
			if (core.timer <= 0)
			{
				Idle();
			}
			break;
		case "idle":
			core.LookTo(core.target.position, 1);
			core.MoveTo(rnd, 0.01f);
			if (DashJump)
			{
				Dash();
			}
			else if (BackAI)
			{
				Back();
			}
			if (core.timer <= 0)
			{
				Idle();
			}
			break;
		case "dash":
			if (core.timer <= 0)
			{
				Idle();
			}
			break;
		case "dead":
			break;
		}
	}

	public virtual void DISAPPEAR()
	{
		core.StartDeathSequence(100);
	}

	public virtual void Idle()
	{
		core.Layer("fly");
		core.NewAI("idle", string.Empty, 75, 180);
		rnd.x = core.Coords.x + UnityEngine.Random.Range(0f - MovingArea.x, MovingArea.x);
		rnd.y = core.Coords.y + UnityEngine.Random.Range(0f - MovingArea.y, MovingArea.y);
	}

	public virtual void Dash()
	{
		if (core.HurtTimer > 0)
		{
			core.Layer("shift");
			core.NewAI("dash", "idle", dashTime, dashTime);
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				core.DashTargetX(core.target.position.x, 0f - dashSpeed.x);
				core.DashY(dashSpeed.y);
				core.LookBySpeed(1);
			}
			else
			{
				core.DashTargetX(core.target.position.x, dashSpeed.x);
				core.DashY(dashSpeed.y);
				core.LookBySpeed(1);
			}
		}
	}

	public virtual void Back()
	{
		if (core.HurtTimer > 0)
		{
			core.Layer("shift");
			core.NewAI("dash", "idle", dashTime, dashTime);
			core.DashTargetX(core.target.position.x, 0f - dashSpeed.x);
			core.LookBySpeed(-1);
		}
	}

	public virtual void Main()
	{
	}
}
