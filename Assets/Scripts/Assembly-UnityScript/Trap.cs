using System;
using UnityEngine;

[Serializable]
public class Trap : MonoBehaviour
{
	public float POWER;

	public string DeadLevel;

	[HideInInspector]
	public Transform trans;

	public int POISON;

	public Trap()
	{
		POWER = 10f;
	}

	public virtual void Start()
	{
		trans = transform;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			StrikeIt(other.gameObject);
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			StrikeIt(other.gameObject);
		}
	}

	public virtual void StrikeIt(GameObject go)
	{
		Global.LastStrike = new SendStrike();
		if (!string.IsNullOrEmpty(DeadLevel))
		{
			Global.DeadLevel = DeadLevel;
		}
		else
		{
			Global.DeadLevel = null;
		}
		Global.LastStrike.trans = trans;
		Global.LastStrike.pow = POWER;
		Global.LastStrike.clan = "foe";
		Global.LastStrike.poison = POISON;
		go.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void SetDeadLevel(string text)
	{
		DeadLevel = text;
	}

	public virtual void Main()
	{
	}
}
