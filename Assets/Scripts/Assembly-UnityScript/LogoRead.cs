using System;
using System.IO;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class LogoRead : MonoBehaviour
{
	private int CurrentNum;

	private string MODE;

	private float Alpha;

	private int timer;

	private Texture2D texture;

	private float aspect;

	private string WayPath;

	public int NumLogo;

	public LogoRead()
	{
		MODE = "Off";
		Alpha = 1f;
		NumLogo = 1;
	}

	public virtual void Start()
	{
		WayPath = Application.dataPath + "/logo/";
	}

	public virtual void Update()
	{
		if (Input.GetKeyDown("escape"))
		{
			MODE = "Off";
		}
		if (Input.GetKeyDown("enter"))
		{
			MODE = "Off";
		}
		if (Input.GetMouseButtonDown(0))
		{
			MODE = "Off";
		}
	}

	public virtual void FixedUpdate()
	{
		switch (MODE)
		{
		case "On":
			Alpha += 0.05f;
			if (!(Alpha < 1f))
			{
				Alpha = 1f;
				MODE = "Delay";
			}
			break;
		case "Off":
		{
			Alpha -= 0.05f;
			if (Alpha > 0f)
			{
				break;
			}
			Alpha = 0f;
			MODE = "On";
			int num = 0;
			if (ReadFile(NumLogo))
			{
				if ((bool)texture)
				{
					float num2 = Convert.ToSingle(texture.width);
					float num3 = Convert.ToSingle(texture.height);
					aspect = 1f * num2 / num3;
					MonoBehaviour.print("ASPECT: " + aspect + "  " + num2 + "  " + num3);
				}
				guiConsole.prind("just return");
				NumLogo++;
				num++;
			}
			else
			{
				Application.LoadLevel("main menu");
			}
			break;
		}
		case "Delay":
			timer++;
			if (timer > 150)
			{
				timer = 0;
				MODE = "Off";
			}
			break;
		}
	}

	public virtual bool ReadFile(int num)
	{
		texture = null;
		string text = WayPath + "logo" + num + ".jpg";
		guiConsole.prind("file: " + text);
		int result;
		if (File.Exists(text))
		{
			byte[] data = File.ReadAllBytes(text);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(data);
			texture = texture2D;
			guiConsole.prind("true texture");
			result = 1;
		}
		else
		{
			guiConsole.prind("false texture");
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual void OnGUI()
	{
		if (!(texture == null))
		{
			GUI.color = new Color(Alpha, Alpha, Alpha, Alpha);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), texture, ScaleMode.ScaleToFit, true, aspect);
		}
	}

	public virtual void Main()
	{
	}
}
