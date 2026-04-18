using System;
using UnityEngine;

[Serializable]
public class GhostAI : MonoBehaviour
{
	private AI core;

	private Vector3 randomSide;

	private Collider coll;

	public virtual void Awake()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		coll.enabled = false;
		core.POWER = Global.MaxHP * 0.5f;
		randomSide.x = UnityEngine.Random.Range(-1f, 1f);
		if (randomSide.x == 0f)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		core.Look((int)(Mathf.Sign(randomSide.x) * (float)core.lookCorr));
		core.NewAI("show", string.Empty, 50, 55);
		transform.localScale *= UnityEngine.Random.Range(0.75f, 1.25f);
	}

	public virtual void FixedUpdate()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		switch (core.ai)
		{
		case "show":
			if (core.timer <= 0)
			{
				coll.enabled = true;
				core.NewAI("idle", string.Empty, 150, 200);
				randomSide.y = UnityEngine.Random.Range(-1f, 1f);
			}
			break;
		case "idle":
			core.Move(randomSide.x, randomSide.y);
			if (core.timer <= 0 || core.HurtTimer > 0)
			{
				coll.enabled = false;
				core.NewAI("hide", string.Empty, 150, 200);
			}
			break;
		case "hide":
			if (core.timer <= 0)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			break;
		}
	}

	public virtual void Main()
	{
	}
}
