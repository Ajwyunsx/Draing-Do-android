using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Perks : MonoBehaviour
{
	public Perkz[] perk;

	[NonSerialized]
	public static Perkz[] Perk;

	[NonSerialized]
	public static int n;

	[NonSerialized]
	public static int floor;

	[NonSerialized]
	public static float powerBonus;

	[NonSerialized]
	public static float bonusStaminaSpeed;

	public virtual void Awake()
	{
		Perk = perk;
	}

	public virtual void FixedUpdate()
	{
		if (!Global.Pause)
		{
		}
	}

	public static void ChangePerk(string perkName, int perkPlus)
	{
		for (n = 0; n < Extensions.get_length((System.Array)Perk); n++)
		{
			if (perkName == Perk[n].name)
			{
				if (perkPlus == 0)
				{
					Perk[n].level = 0;
					PerkUpdate();
				}
				else
				{
					Perk[n].level = Perk[n].level + perkPlus;
					PerkUpdate();
				}
				break;
			}
		}
	}

	public static void PerkUpdate()
	{
		for (n = 0; n < Extensions.get_length((System.Array)Perk); n++)
		{
			string text = Perk[n].name;
			if (text == "floor")
			{
				floor = (int)((float)Perk[n].level * Perk[n].formula);
			}
			else if (text == "staminaSpeed")
			{
				bonusStaminaSpeed = (float)Perk[n].level * Perk[n].formula;
			}
		}
	}

	public static void WearPerks()
	{
		PerkUpdate();
		for (n = 0; n < Extensions.get_length((System.Array)Perk); n++)
		{
			string text = Perk[n].name;
			if (text == "floor" && Global.RANG < floor)
			{
				Global.RANG = floor;
				SetStatsByRANG();
				Global.HP = Global.MaxHP;
			}
		}
	}

	public static float GetPOWER()
	{
		return (float)Convert.ToInt32(Global.Var["power"]) * Global.GetDDAttack();
	}

	public static int SetStatsByRANG()
	{
		Global.MaxHP = 80 * Global.RANG;
		return 0;
	}

	public static int NextLevelFormula(int ORang)
	{
		return (ORang != 0) ? (120 * ORang) : (120 * Global.RANG);
	}

	public virtual void Main()
	{
	}
}
