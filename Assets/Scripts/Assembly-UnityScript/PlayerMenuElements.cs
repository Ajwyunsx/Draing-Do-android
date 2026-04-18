using System;
using UnityEngine;

[Serializable]
public class PlayerMenuElements : MonoBehaviour
{
	public GameObject gold;

	public GameObject exp;

	public GameObject attack;

	public GameObject hp;

	public GameObject stamina;

	public GameObject defense;

	public GameObject food;

	public GameObject mind;

	public GameObject surviveGroup;

	public virtual void Start()
	{
		gold.SendMessage("Rename", string.Empty + Global.Gold, SendMessageOptions.DontRequireReceiver);
		exp.SendMessage("Rename", string.Empty + Global.Experience, SendMessageOptions.DontRequireReceiver);
		int num = (int)(Perks.GetPOWER() * Global.skillPower);
		int num2 = (int)Perks.GetPOWER();
		attack.SendMessage("Rename", string.Empty + num2 + " (" + num + ")", SendMessageOptions.DontRequireReceiver);
		hp.SendMessage("Rename", Convert.ToInt32(Global.HP) + " / " + Global.MaxHP, SendMessageOptions.DontRequireReceiver);
		stamina.SendMessage("Rename", Convert.ToInt32(Global.MP) + " / " + Global.MaxMP, SendMessageOptions.DontRequireReceiver);
		defense.SendMessage("Rename", string.Empty + Convert.ToInt32(Global.Var["defense"]), SendMessageOptions.DontRequireReceiver);
		if (Convert.ToInt32(Global.Var["survive"]) > 0)
		{
			food.SendMessage("Rename", string.Empty + (100 - Convert.ToInt32(Global.Var["food"])), SendMessageOptions.DontRequireReceiver);
			mind.SendMessage("Rename", string.Empty + (100 - Convert.ToInt32(Global.Var["mind"])), SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			UnityEngine.Object.Destroy(surviveGroup);
		}
	}

	public virtual void Main()
	{
	}
}
