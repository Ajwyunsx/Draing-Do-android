using System;
using UnityEngine;

[Serializable]
public class SpawnerAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public int SpawnTimez;

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
			if (ai == "idle" && SpawnTimez > 0)
			{
				SpawnTimez--;
				BroadcastMessage("SpawnCreate", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void CrushHP()
	{
		SpawnTimez = UnityEngine.Random.Range(2, 4);
	}

	public virtual void DISAPPEAR()
	{
		core.StartDeathSequence(100);
	}

	public virtual void Main()
	{
	}
}
