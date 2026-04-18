using System;
using UnityEngine;

[Serializable]
public class CampUpgrade : MonoBehaviour
{
	public string NameUpgrade;

	public int LevelUpgrade;

	public bool DestroyIfHigh;

	public CampUpgrade()
	{
		NameUpgrade = "CampFarm";
	}

	public virtual void Awake()
	{
		if (DestroyIfHigh && Convert.ToInt32(Global.Var[NameUpgrade]) > LevelUpgrade)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		if (Convert.ToInt32(Global.Var[NameUpgrade]) < LevelUpgrade)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
