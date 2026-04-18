using System;
using UnityEngine;

[Serializable]
public class ResultMenu : MonoBehaviour
{
	public GameObject Enemy;

	public GameObject Gold;

	public GameObject time;

	private int GlobalLevelEnemy;

	private int GlobalLevelGold;

	public virtual void Awake()
	{
		Global.LevelTime = (int)Time.timeSinceLevelLoad;
		Renames();
	}

	public virtual void Update()
	{
		GlobalLevelEnemy = (int)Mathf.Lerp(GlobalLevelEnemy, Global.LevelEnemy, Time.time * 0.1f);
		GlobalLevelGold = (int)Mathf.Lerp(GlobalLevelEnemy, Global.LevelEnemy, Time.time * 0.1f);
		Renames();
	}

	public virtual void Renames()
	{
		Enemy.BroadcastMessage("Rename", Lang.Menu("Enemy") + ": " + GlobalLevelEnemy, SendMessageOptions.DontRequireReceiver);
		Gold.BroadcastMessage("Rename", Lang.Menu("Gold") + ": " + GlobalLevelGold, SendMessageOptions.DontRequireReceiver);
		time.BroadcastMessage("Rename", Lang.Menu("Time") + ": " + Global.LevelTime, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
