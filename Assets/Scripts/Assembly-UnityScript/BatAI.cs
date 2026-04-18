using System;
using UnityEngine;

[Serializable]
public class BatAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public float WaveSpeed;

	public float WaveAmplitude;

	private int invTimer;

	public int HideTimer;

	private float AmpTimer;

	public bool isSleeping;

	public float WakeDistance;

	public float RandomAppearY;

	public BatAI()
	{
		WaveSpeed = 2f;
		WaveAmplitude = 1f;
		HideTimer = 100;
		isSleeping = true;
		WakeDistance = 3f;
		RandomAppearY = 1f;
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		if (!isSleeping)
		{
			core.NewAI("hide", "idle", 100, 105);
			core.trans.position = new Vector3(0f, 5000f, -1.2f);
		}
		else
		{
			core.NewAI("sleep", string.Empty, 100, 105);
		}
	}

	public virtual void FixedUpdate()
	{
		if (!core.target)
		{
			if (!Global.CurrentPlayerObject)
			{
				return;
			}
			core.target = Global.CurrentPlayerObject;
		}
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		switch (core.ai)
		{
		case "sleep":
			if (core.Distance(core.trans.position, core.target.position) < WakeDistance || core.HurtTimer > 0)
			{
				core.NewAI("idle", string.Empty, 2000, 4000);
				core.LookRnd();
			}
			break;
		case "hide":
			if (core.timer <= 0)
			{
				core.NewAI("idle", string.Empty, 2000, 4000);
				core.LookRnd();
				float x = Camera.main.transform.position.x - 7.5f * (float)core.Direction;
				Vector3 position2 = core.trans.position;
				float num2 = (position2.x = x);
				Vector3 vector3 = (core.trans.position = position2);
				if ((bool)core.target)
				{
					float y = core.target.position.y + UnityEngine.Random.Range(0f - RandomAppearY, RandomAppearY);
					Vector3 position3 = core.trans.position;
					float num3 = (position3.y = y);
					Vector3 vector5 = (core.trans.position = position3);
				}
				if (!(core.trans.position.y >= Global.SurfaceWaterPoint))
				{
					float surfaceWaterPoint2 = Global.SurfaceWaterPoint;
					Vector3 position4 = core.trans.position;
					float num4 = (position4.y = surfaceWaterPoint2);
					Vector3 vector7 = (core.trans.position = position4);
				}
			}
			break;
		case "idle":
			if (!(core.trans.position.y >= Global.SurfaceWaterPoint))
			{
				float surfaceWaterPoint = Global.SurfaceWaterPoint;
				Vector3 position = core.trans.position;
				float num = (position.y = surfaceWaterPoint);
				Vector3 vector = (core.trans.position = position);
			}
			if (!(AmpTimer > 1f))
			{
				AmpTimer += 0.05f;
			}
			core.Move(core.Direction, Mathf.Sin((float)core.timer * WaveSpeed) * WaveAmplitude * AmpTimer);
			if (!(core.Distance2D(core.trans.position.x, Camera.main.transform.position.x) <= 9f))
			{
				core.NewAI("hide", "idle", HideTimer, HideTimer);
				core.trans.position = new Vector3(0f, 5000f, -1.2f);
			}
			break;
		}
	}

	public virtual void Main()
	{
	}
}
