using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class guiConsole : MonoBehaviour
{
	public GUISkin consoleSkin;

	public GUIStyle consoleStyle;

	private bool enableConsole;

	private bool enableHelp;

	private Rect consoleWindow;

	private Rect helpWindow;

	[NonSerialized]
	public static GameObject sgo;

	[NonSerialized]
	public static UnityScript.Lang.Array PrintMessages = new UnityScript.Lang.Array();

	private UnityScript.Lang.Array messages;

	private UnityScript.Lang.Array commands;

	private UnityScript.Lang.Array time;

	private string[] split;

	private string messageToSend;

	private string currentTime;

	public int maxMessages;

	private Vector2 scrollPosition;

	private Vector2 helpScrollPosition;

	private int scrollingCounter;

	public int timerforFocus;

	public guiConsole()
	{
		messages = new UnityScript.Lang.Array();
		commands = new UnityScript.Lang.Array();
		time = new UnityScript.Lang.Array();
		messageToSend = string.Empty;
		currentTime = string.Empty;
		maxMessages = 10;
	}

	public virtual void Awake()
	{
		sgo = gameObject;
	}

	public virtual void OnGUI()
	{
		GUI.skin = consoleSkin;
		Input.eatKeyPressOnTextFieldFocus = false;
		sendMessages();
		onOffConsole();
		if (!enableConsole)
		{
			return;
		}
		consoleWindow = new Rect(0f, 0f, Screen.width, Screen.height / 2);
		consoleWindow = GUI.Window(0, consoleWindow, consoleControls, string.Empty);
		if (timerforFocus > 0)
		{
			timerforFocus--;
			if (timerforFocus == 0)
			{
				GUI.FocusWindow(0);
				GUI.FocusControl("MyTextField");
			}
		}
	}

	public virtual void consoleControls(int windowID)
	{
		GUI.color = new Color(1f, 1f, 1f, 1f);
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		for (int i = 0; i < messages.Count; i++)
		{
			GUILayout.Label(string.Empty + messages[i], consoleStyle);
		}
		GUILayout.EndScrollView();
		GUI.SetNextControlName("MyTextField");
		messageToSend = GUILayout.TextField(messageToSend);
	}

	public virtual void helpControls(int windowID)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			enableHelp = false;
		}
	}

	public virtual void onOffConsole()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote) && !enableConsole && !Global.Pause)
		{
			Input.ResetInputAxes();
			Global.Pause = true;
			Time.timeScale = 0f;
			Global.OldPause = true;
			enableConsole = true;
			GUI.UnfocusWindow();
			timerforFocus = 10;
			messageToSend = string.Empty;
		}
		else if (Input.GetKeyDown(KeyCode.BackQuote) && enableConsole)
		{
			Input.ResetInputAxes();
			Global.Pause = false;
			Time.timeScale = 1f;
			Global.OldPause = false;
			enableConsole = false;
			GUI.UnfocusWindow();
			scrollingCounter = 0;
			enableHelp = false;
		}
		if (!Global.Pause && enableConsole)
		{
			Input.ResetInputAxes();
			Global.Pause = false;
			Time.timeScale = 1f;
			Global.OldPause = false;
			enableConsole = false;
			GUI.UnfocusWindow();
			scrollingCounter = 0;
			enableHelp = false;
		}
	}

	public virtual void sendMessages()
	{
		if (messageToSend.Length != 0 && enableConsole && Input.GetKeyDown(KeyCode.Return))
		{
			currentTime = DateTime.Now.ToString();
			time.Add(currentTime + " : ");
			consoleCommands();
			messages.Add(messageToSend);
			messageToSend = string.Empty;
		}
		if (messages.Count > maxMessages)
		{
			messages.RemoveAt(0);
			time.RemoveAt(0);
		}
	}

	public virtual void Pink(string name)
	{
		messages.Add(name);
	}

	public static void prind(string name)
	{
		sgo.SendMessage("Pink", name, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void consoleCommands()
	{
		split = messageToSend.Split(" "[0]);
		string text = split[0].ToUpper();
		if (text == "EXIT" as string)
		{
			messageToSend += " : QUIT!";
			closeApplication();
		}
		if (text == "GODMICH" as string)
		{
			Global.CheatGodMode = 1 - Global.CheatGodMode;
			if (Global.CheatGodMode == 1)
			{
				messageToSend += " : GOD MODE: ON!";
			}
			else
			{
				messageToSend += " : GOD MODE: OFF!";
			}
		}
		if (text == "GETALLTOOLS" as string)
		{
			Global.GetAllTools();
			messageToSend += " : CHEAT LEVEL!";
		}
		if (text == "CHEATMAXHP" as string)
		{
			Global.MaxHP = 12f;
			Global.HP = 12f;
			messageToSend += " : MAX HIT POINTS = 12 !";
		}
		if (text == "OPENALLMAP" as string)
		{
			Global.CheatMapOpen = 1 - Global.CheatMapOpen;
			if (Global.CheatMapOpen == 1)
			{
				messageToSend += " : MAP OPEN: ON!";
			}
			else
			{
				messageToSend += " : MAP OPEN: OFF!";
			}
		}
		if (text == "CGOLD" as string)
		{
			Global.Gold += 1000;
			messageToSend += " : GOLD + 1000 !";
		}
		if (text == "deleteAll" as string)
		{
			PlayerPrefs.DeleteAll();
			messageToSend += " : DELETE ALL SAVES!";
		}
		if (text == "GETLEVEL" as string)
		{
			messageToSend = "Level Name: " + Global.LEVEL.Replace(" ", "_");
		}
		if (text == "SETLEVEL" as string)
		{
			if (Extensions.get_length((System.Array)split) > 1)
			{
				MonoBehaviour.print("LEVEL: " + split[1] + "   " + split[1].Replace("_", " "));
				messageToSend = "Teleport to Level! " + split[1];
				Global.LoadLEVEL(split[1].Replace("_", " "), string.Empty);
			}
			else
			{
				messageToSend += " : WRONG NAME!";
			}
		}
		if (text == "FIRSTLEVEL" as string || text == "LEVEL1" as string)
		{
			messageToSend = "Teleport to First Level!";
			Global.LoadLEVEL("C1 city-start", string.Empty);
		}
		if (text == "RESTART" as string)
		{
			messageToSend = "Restart!";
			Global.LoadLEVEL(Global.LEVEL, string.Empty);
		}
		if (text == "SETQUEST" as string)
		{
			if (Extensions.get_length((System.Array)split) > 1)
			{
				messageToSend += " : SET QUEST!";
				Global.AddQuest(split[1].Replace("_", " "));
			}
			else
			{
				messageToSend += " : WRONG NAME!";
			}
		}
		if (!(text == "GETQUEST" as string))
		{
			return;
		}
		if (Extensions.get_length((System.Array)split) > 1)
		{
			if (Global.CheckQuest(split[1]))
			{
				messageToSend = "QUEST: " + split[1] + " = TRUE";
			}
			else
			{
				messageToSend = "QUEST: " + split[1].Replace(" ", "_") + " = FALSE";
			}
		}
		else
		{
			messageToSend += " : WRONG NAME!";
		}
	}

	public virtual void scrollingMessages()
	{
	}

	public virtual void setResolution(int h, int w, bool f)
	{
		Screen.SetResolution(h, w, f);
	}

	public virtual void closeApplication()
	{
		Application.Quit();
	}

	public virtual void Update()
	{
		scrollingMessages();
	}

	public virtual void Main()
	{
	}
}
