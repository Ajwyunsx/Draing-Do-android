using System;
using UnityEngine;

[Serializable]
public class WhipStrike : MonoBehaviour
{
	private int Crush;

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (Crush > 0)
		{
			Crush--;
		}
	}

	public virtual void Crusher()
	{
		Crush = 5;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag == "Player"))
		{
			Global.LastStrike = new SendStrike();
			Global.LastStrike.trans = Global.CurrentPlayerObject;
			Global.LastStrike.pow = Perks.GetPOWER();
			Global.LastStrike.multy = Global.skillPower;
			Global.LastStrike.clan = "hero";
			other.SendMessage("CrushHP", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
