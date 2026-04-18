using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Achiev : MonoBehaviour
{
	public Awards[] award;

	[NonSerialized]
	public static Awards[] Award;

	public virtual void Awake()
	{
		Award = award;
	}

	public virtual void FixedUpdate()
	{
		for (int i = 0; i < Extensions.get_length((System.Array)Award); i++)
		{
			if (Award[i].have != 0)
			{
				continue;
			}
			switch (Award[i].name)
			{
			case "gold 1000":
				if (Global.Gold >= 1000)
				{
					AwardComplete(i);
					Global.Gold += 100;
				}
				break;
			case "gold 10000":
				if (Global.Gold >= 10000)
				{
					AwardComplete(i);
					Global.Gold += 1000;
				}
				break;
			case "gold 100000":
				if (Global.Gold >= 100000)
				{
					AwardComplete(i);
					Global.Gold += 10000;
				}
				break;
			case "gold 1000000":
				if (Global.Gold >= 1000000)
				{
					AwardComplete(i);
					Global.Gold += 100000;
				}
				break;
			}
		}
	}

	public virtual void AwardComplete(int a)
	{
		Award[a].have = 1;
		Global.GameMessageAwardCreate(Award[a].name);
		Global.GameMessageBonusCreate(Award[a].name + " B");
	}

	public virtual void Main()
	{
	}
}
