using System;
using UnityEngine;

[Serializable]
public class MenuSlot : MonoBehaviour
{
	public int SlotX;

	public int SlotY;

	public string Action;

	public string ActionName;

	public int Price;

	public GameObject GO;

	public int RoomNumber;

	public int OptionNumber;

	public GameObject MenuIcon;

	public string SpecialName;

	public string progress;

	private Vector3 StartScale;

	[HideInInspector]
	public int maxNumber;

	[HideInInspector]
	public string CurrentCount;

	[HideInInspector]
	public GUISkin newSkin;

	[HideInInspector]
	public bool textON;

	private bool ToggleUpdate;

	public AudioClip SFXNo;

	private int inTarget;

	private bool OnceSelected;

	public MenuSlot()
	{
		Action = string.Empty;
	}

	public virtual void ChangeXNumber(int num)
	{
		SlotX = num;
	}

	public virtual void SetPrice(int price)
	{
		Price = price;
	}

	public virtual void SetGameObject(GameObject go)
	{
		GO = go;
	}

	public virtual void SetRealRoomNumber(int num)
	{
		RoomNumber = num;
	}

	public virtual void SetActionMode(string mode)
	{
		Action = mode;
	}

	public virtual void SetRealOptionNumber(int num)
	{
		OptionNumber = num;
	}

	public virtual void SetIcon(GameObject ob)
	{
		MenuIcon = ob;
	}

	public virtual void SetSpecialName(string text)
	{
		SpecialName = text;
	}

	public virtual void SetProgress(string text)
	{
		progress = text;
	}

	public virtual void Awake()
	{
		StartScale = transform.localScale;
	}

	public virtual void Start()
	{
		CheckToCreateIcon();
		switch (Action)
		{
		case "buy_cloth":
		case "buy_tool":
		case "buy_hair":
			if (GetComponent<AudioSource>() == null)
			{
				gameObject.AddComponent<AudioSource>();
			}
			SFXNo = Global.SFXNo;
			break;
		}
	}

	public virtual void OnMouseOver()
	{
		inTarget = 2;
	}

	public virtual void Update()
	{
		if (inTarget != 0)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, StartScale * 1.05f, 0.5f);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, StartScale, 0.5f);
		}
		inTarget = 0;
		if ((Global.MenuX == SlotX && Global.MenuY == SlotY) || ToggleUpdate)
		{
			ToggleUpdate = false;
			if (!OnceSelected)
			{
				OnceSelected = true;
				string text = null;
				switch (Action)
				{
				case "award":
					text = Lang.Award(SpecialName + " B");
					transform.parent.gameObject.SendMessage("SetTextFromChild", text, SendMessageOptions.DontRequireReceiver);
					transform.parent.gameObject.SendMessage("GetIconFromChild", MenuIcon, SendMessageOptions.DontRequireReceiver);
					transform.parent.gameObject.SendMessage("ProgressScript", progress, SendMessageOptions.DontRequireReceiver);
					break;
				case "buy_cloth":
				case "buy_tool":
				case "buy_hair":
				case "buy_hat":
					transform.parent.gameObject.SendMessage("GetIconFromChild", MenuIcon, SendMessageOptions.DontRequireReceiver);
					break;
				default:
					transform.parent.gameObject.SendMessage("DeleteIconFromChild", null, SendMessageOptions.DontRequireReceiver);
					transform.parent.gameObject.SendMessage("SetTextFromChild", string.Empty, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
		}
		else if (OnceSelected)
		{
			OnceSelected = false;
		}
	}

	public virtual void MenuSlotAction()
	{
		if (Global.YesNoMode)
		{
			return;
		}
		int num = default(int);
		switch (Action)
		{
		case "ask":
			Global.AskSelected = Price;
			if ((bool)Global.TalkWindow)
			{
				UnityEngine.Object.Destroy(Global.TalkWindow);
			}
			break;
		case "yes_action":
		{
			Time.timeScale = 1f;
			Global.YesNoObject.SendMessage("RealActON", null, SendMessageOptions.DontRequireReceiver);
			int num2 = 10;
			Global.Pause = false;
			break;
		}
		case "hair_color":
		{
			Time.timeScale = 1f;
			gameObject.BroadcastMessage("GetColorFromButton", null, SendMessageOptions.DontRequireReceiver);
			int num2 = 10;
			Global.Pause = false;
			break;
		}
		case "buy_hat":
			if (Global.Gold >= Price)
			{
				Time.timeScale = 1f;
				Global.Gold -= Price;
				Global.ToPlay = Global.SFXBuy;
				Global.HatHero = OptionNumber;
				int num2 = 10;
				Global.Pause = false;
			}
			else
			{
				GetComponent<AudioSource>().clip = SFXNo;
				GetComponent<AudioSource>().Play();
			}
			break;
		case "buy_hair":
			if (Global.Gold >= Price)
			{
				Time.timeScale = 1f;
				Global.Gold -= Price;
				Global.ToPlay = Global.SFXBuy;
				Global.HairHero = OptionNumber;
				int num2 = 10;
				Global.Pause = false;
			}
			else
			{
				GetComponent<AudioSource>().clip = SFXNo;
				GetComponent<AudioSource>().Play();
			}
			break;
		case "diff":
			Global.Difficulty = Price;
			goto case "unpause";
		case "unpause":
		{
			int num2 = 10;
			Time.timeScale = 1f;
			Global.Pause = false;
			break;
		}
		case "menu":
			Global.CreateMenuWindow(ActionName);
			break;
		case "menuGO":
			Global.CreateMenuWindowObj(GO);
			break;
		case "back":
			Global.CreateMenuWindow("MenuPause");
			break;
		case "playgame":
			Global.DefaultData();
			Global.RTCP = false;
			Global.ToFindCheckPointXYZ = true;
			Global.CheckPointName = string.Empty;
			Global.CheckPointNameTemp = string.Empty;
			Global.SAVE();
			Global.LoadLEVEL(Global.DefaultLevel, string.Empty);
			Global.Pause = false;
			Time.timeScale = 1f;
			Global.MenuPause = false;
			break;
		case "options":
			Global.RemoveAllOjects();
			Application.LoadLevel("options");
			break;
		case "options2":
			Global.RemoveAllOjects();
			Application.LoadLevel("options2");
			break;
		case "exitgame":
			Global.YesNo("Quit?", "quit", 0);
			break;
		case "title":
			Global.RemoveAllOjects();
			Application.LoadLevel("main menu");
			break;
		case "exit":
			Global.YesNo("Quit?", "exit", 0);
			break;
		case "players":
			Global.RemoveAllOjects();
			Application.LoadLevel("players");
			break;
		case "player":
			MonoBehaviour.print("num: " + num);
			Global.PLAYER = Convert.ToInt32(ActionName);
			MonoBehaviour.print("Global.PLAYER: " + Global.PLAYER + " ");
			Global.LOAD();
			Global.RemoveAllOjects();
			Application.LoadLevel("main menu");
			break;
		case "delete":
			num = Convert.ToInt32(ActionName);
			if (PlayerPrefs.HasKey("Player" + num))
			{
				Global.YesNo("Delete?", "delete", num);
			}
			break;
		case "music":
			GO.SendMessage("Rename", ReturnMusic(true), SendMessageOptions.DontRequireReceiver);
			break;
		case "sound":
			GO.SendMessage("Rename", ReturnSound(true), SendMessageOptions.DontRequireReceiver);
			break;
		case "quality":
			GO.SendMessage("Rename", ReturnQuality(true), SendMessageOptions.DontRequireReceiver);
			break;
		case "screen":
			GO.SendMessage("Rename", ReturnScreen(true), SendMessageOptions.DontRequireReceiver);
			break;
		case "fullscreen":
			GO.SendMessage("Rename", ReturnFullScreen(true), SendMessageOptions.DontRequireReceiver);
			break;
		case "apply":
			Apply();
			break;
		}
	}

	public virtual void OnMouseUpAsButton()
	{
		if (!Global.YesNoMode)
		{
			MenuSlotAction();
		}
	}

	public virtual void CheckToCreateIcon()
	{
		string text = null;
		GameObject gameObject = null;
		switch (Action)
		{
		case "player":
			if (PlayerPrefs.HasKey("Player" + Convert.ToInt32(ActionName)))
			{
				GO.SendMessage("Rename", Lang.Menu("Player") + Convert.ToInt32(ActionName), SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				GO.SendMessage("Rename", "*empty*", SendMessageOptions.DontRequireReceiver);
			}
			break;
		case "players":
			if (PlayerPrefs.HasKey("Player" + Global.PLAYER))
			{
				GO.SendMessage("Rename", Lang.Menu("Player") + Global.PLAYER, SendMessageOptions.DontRequireReceiver);
			}
			break;
		case "music":
			GO.SendMessage("Rename", ReturnMusic(false), SendMessageOptions.DontRequireReceiver);
			break;
		case "sound":
			GO.SendMessage("Rename", ReturnSound(false), SendMessageOptions.DontRequireReceiver);
			break;
		case "quality":
			GO.SendMessage("Rename", ReturnQuality(false), SendMessageOptions.DontRequireReceiver);
			break;
		case "screen":
			GO.SendMessage("Rename", ReturnScreen(false), SendMessageOptions.DontRequireReceiver);
			break;
		case "fullscreen":
			GO.SendMessage("Rename", ReturnFullScreen(false), SendMessageOptions.DontRequireReceiver);
			break;
		case "apply":
			Global.oSOUND = Global.SOUND;
			Global.oMUSIC = Global.MUSIC;
			Global.oQUALITY = Global.QUALITY;
			break;
		}
	}

	public virtual string ReturnMusic(bool press)
	{
		if (!press)
		{
			Global.oMUSIC = Global.MUSIC;
		}
		else
		{
			Global.oMUSIC = 1 - Global.oMUSIC;
		}
		return (Global.oMUSIC != 1) ? (Lang.Menu("Music") + ": " + Lang.Menu("Off")) : (Lang.Menu("Music") + ": " + Lang.Menu("On"));
	}

	public virtual string ReturnSound(bool press)
	{
		if (!press)
		{
			Global.oSOUND = Global.SOUND;
		}
		else
		{
			Global.oSOUND = 1 - Global.oSOUND;
		}
		return (Global.oSOUND != 1) ? (Lang.Menu("Sound") + ": " + Lang.Menu("Off")) : (Lang.Menu("Sound") + ": " + Lang.Menu("On"));
	}

	public virtual void Apply()
	{
		Global.SOUND = Global.oSOUND;
		Global.MUSIC = Global.oMUSIC;
		Global.QUALITY = Global.oQUALITY;
		PlayerPrefs.SetInt("Player" + Global.PLAYER + "MUSIC", Global.MUSIC);
		PlayerPrefs.SetInt("Player" + Global.PLAYER + "SOUND", Global.SOUND);
		PlayerPrefs.SetInt("Player" + Global.PLAYER + "QUALITY", Global.QUALITY);
		QualitySettings.SetQualityLevel(Global.QUALITY, true);
	}

	public virtual string ReturnQuality(bool press)
	{
		if (!press)
		{
			Global.oQUALITY = Global.QUALITY;
		}
		else
		{
			Global.oQUALITY = 1 - Global.oQUALITY;
		}
		return (Global.oQUALITY == 0) ? (Lang.Menu("Quality") + ": " + Lang.Menu("Good")) : ((Global.oQUALITY != 1) ? null : (Lang.Menu("Quality") + ": " + Lang.Menu("Low")));
	}

	public virtual string ReturnScreen(bool press)
	{
		return null;
	}

	public virtual string ReturnFullScreen(bool press)
	{
		return null;
	}

	public virtual void OnGUI()
	{
		if (textON)
		{
			GUI.skin = Global.DefaultSKIN;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1024f, (float)Screen.height / 768f, 1f));
			float num = 1024f / (float)Screen.width;
			float num2 = 768f / (float)Screen.height;
			Vector3 vector = Camera.main.WorldToScreenPoint(transform.position - new Vector3(gameObject.GetComponent<Renderer>().bounds.size.x * 0.55f, (0f - gameObject.GetComponent<Renderer>().bounds.size.y) * 0.55f, 0f));
			GUI.Box(new Rect(vector.x * num, ((float)Screen.height - vector.y) * num2, 50f, 50f), CurrentCount);
		}
	}

	public virtual void Main()
	{
	}
}
