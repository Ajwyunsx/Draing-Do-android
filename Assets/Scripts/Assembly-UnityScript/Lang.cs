using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Lang : MonoBehaviour
{
	public LangText[] TMenu;

	public LangText[] TText;

	public LangText[] TAward;

	public LangText[] TMessage;

	public LangText[] TMission;

	[NonSerialized]
	public static LangText[] LMenu;

	[NonSerialized]
	public static LangText[] LText;

	[NonSerialized]
	public static LangText[] LAward;

	[NonSerialized]
	public static LangText[] LMessage;

	[NonSerialized]
	public static LangText[] LMission;

	public LangText[] TTalk;

	[NonSerialized]
	public static LangText[] LTalk;

	public virtual void Awake()
	{
		LMenu = TMenu;
		LText = TText;
		LAward = TAward;
		LMessage = TMessage;
		LMission = TMission;
		LTalk = TTalk;
	}

	public static string Menu(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LMenu))
			{
				if (LMenu[num].id == text)
				{
					result = LMenu[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public static string Text(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LText))
			{
				if (LText[num].id == text)
				{
					result = LText[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public static string Award(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LAward))
			{
				if (LAward[num].id == text)
				{
					result = LAward[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public static string Mission(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LMission))
			{
				if (LMission[num].id == text)
				{
					result = LMission[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public static string CurrentMessage(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LMessage))
			{
				if (LMessage[num].id == text)
				{
					result = LMessage[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public static string Talks(string text)
	{
		int num = 0;
		string result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)LTalk))
			{
				if (LTalk[num].id == text)
				{
					result = LTalk[num].text;
					break;
				}
				num++;
				continue;
			}
			result = text;
			break;
		}
		return result;
	}

	public virtual void Main()
	{
	}
}
