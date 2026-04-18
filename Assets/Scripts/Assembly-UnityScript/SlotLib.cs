using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SlotLib : MonoBehaviour
{
	[NonSerialized]
	public static GameObject[] Obj;

	public GameObject[] OBJECT;

	[NonSerialized]
	public static GameObject[] Skill;

	public GameObject[] SKILL;

	[NonSerialized]
	public static GameObject[] Tool;

	public GameObject[] TOOL;

	public virtual void Awake()
	{
		Obj = OBJECT;
		Skill = SKILL;
		Tool = TOOL;
	}

	public static bool EnsureLoaded()
	{
		if (Obj != null && Skill != null && Tool != null)
		{
			return true;
		}
		SlotLib slotLib = UnityEngine.Object.FindObjectOfType<SlotLib>();
		if (!(slotLib == null))
		{
			Obj = slotLib.OBJECT;
			Skill = slotLib.SKILL;
			Tool = slotLib.TOOL;
		}
		return Obj != null && Skill != null && Tool != null;
	}

	public static GameObject GetSkill(int index)
	{
		if (!EnsureLoaded() || index < 0 || index >= Skill.Length)
		{
			return null;
		}
		return Skill[index];
	}

	public static GameObject GetTool(int index)
	{
		if (!EnsureLoaded() || index < 0 || index >= Tool.Length)
		{
			return null;
		}
		return Tool[index];
	}

	public static GameObject CreateObj(string name)
	{
		if (!EnsureLoaded() || Obj == null)
		{
			MonoBehaviour.print("Obj: " + name + " NULL");
			return null;
		}
		int num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)Obj))
			{
				if (Obj[num] != null && Obj[num].name == name)
				{
					result = Obj[num];
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("Obj: " + name + " NULL");
			result = null;
			break;
		}
		return (GameObject)result;
	}

	public virtual void Main()
	{
	}
}
