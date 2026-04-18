using System;
using UnityEngine;

[Serializable]
public class WorldDoor : MonoBehaviour
{
	public string Name;

	public bool IsEnter;

	public WorldDoor()
	{
		Name = "Enter";
		IsEnter = true;
	}

	public virtual void Start()
	{
	}

	public virtual void OnMouseOver()
	{
		Global.MouseMode = "just";
		Monitor.TextA = Name;
		Monitor.TextB = string.Empty;
		Monitor.DontDrop = true;
		Monitor.MouseNo = true;
	}

	public virtual void OnMouseUpAsButton()
	{
		if (!Monitor.dist || (bool)SlotItem.selected)
		{
			return;
		}
		if ((bool)Global.CurrentPlayerObject)
		{
			Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
		}
		if (IsEnter)
		{
			Global.WorldON = true;
			Global.WorldPosition = Global.WorldStart;
			if (!string.IsNullOrEmpty(Global.WorldLevel[(int)Global.WorldPosition.x, (int)Global.WorldPosition.y]))
			{
				Global.LoadLEVEL(Global.WorldLevel[(int)Global.WorldPosition.x, (int)Global.WorldPosition.y], "start");
			}
		}
		else
		{
			Global.WorldON = false;
			Global.LoadLEVEL(Global.WorldLevelToReturn, "start");
		}
	}

	public virtual void Main()
	{
	}
}
