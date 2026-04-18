using System;
using UnityEngine;

[Serializable]
public class UseScript : MonoBehaviour
{
	public int HP;

	public int Food;

	public int Mind;

	public int Poison;

	public AudioClip SFXByUse;

	public virtual void Start()
	{
	}

	public virtual void UseThatItem()
	{
		Global.HP += Global.MaxHP * ((float)HP / 100f);
		if (!(Global.HP <= Global.MaxHP))
		{
			Global.HP = Global.MaxHP;
		}
		Global.Var["food"] = Convert.ToInt32(Global.Var["food"]) + Food;
		if (Convert.ToInt32(Global.Var["food"]) > 100)
		{
			Global.Var["food"] = 100;
		}
		if (Convert.ToInt32(Global.Var["food"]) < 0)
		{
			Global.Var["food"] = 0;
		}
		Global.Var["mind"] = Convert.ToInt32(Global.Var["mind"]) + Mind;
		if (Convert.ToInt32(Global.Var["mind"]) > 100)
		{
			Global.Var["mind"] = 100;
		}
		if (Convert.ToInt32(Global.Var["mind"]) < 0)
		{
			Global.Var["mind"] = 0;
		}
		Global.Var["poison"] = Convert.ToInt32(Global.Var["poison"]) + Poison;
		if (Convert.ToInt32(Global.Var["poison"]) > 100)
		{
			Global.Var["poison"] = 100;
		}
		if (Convert.ToInt32(Global.Var["poison"]) < 0)
		{
			Global.Var["poison"] = 0;
		}
		Global.CreateSFX(SFXByUse, transform.position, 1f, 1f);
	}

	public virtual void Main()
	{
	}
}
