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

	public static GameObject GetToolForSkill(int index, string skillName)
	{
		if (!EnsureLoaded())
		{
			return null;
		}

		int skillNumber = LeadingNumber(skillName);
		if (skillNumber >= 0)
		{
			GameObject numberedTool = FindToolByLeadingNumber(skillNumber);
			if ((bool)numberedTool)
			{
				return numberedTool;
			}
		}

		if (!string.IsNullOrEmpty(skillName) && Skill != null)
		{
			string normalizedSkillName = NormalizeName(skillName);
			for (int i = 0; i < Extensions.get_length((System.Array)Skill); i++)
			{
				if ((bool)Skill[i] && NormalizeName(Skill[i].name) == normalizedSkillName)
				{
					return GetTool(i);
				}
			}
		}

		return GetTool(index);
	}

	private static GameObject FindToolByLeadingNumber(int number)
	{
		if (Tool == null)
		{
			return null;
		}

		for (int i = 0; i < Extensions.get_length((System.Array)Tool); i++)
		{
			if ((bool)Tool[i] && LeadingNumber(Tool[i].name) == number)
			{
				return Tool[i];
			}
		}
		return null;
	}

	private static int LeadingNumber(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return -1;
		}

		string cleanName = NormalizeName(name);
		int value = 0;
		int count = 0;
		for (int i = 0; i < cleanName.Length; i++)
		{
			char c = cleanName[i];
			if (c < '0' || c > '9')
			{
				break;
			}
			value = value * 10 + (c - '0');
			count++;
		}
		return (count > 0) ? value : -1;
	}

	private static string NormalizeName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return string.Empty;
		}
		return name.Replace("(Clone)", string.Empty).Trim();
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
