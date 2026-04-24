using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Text3d : MonoBehaviour
{
	public string TextKey;

	public int SlotNumber;

	private string text;

	private string oldText;

	private TextMesh textMesh;

	public bool ZapretRename;

	public bool Translate;

	public bool BIGLETTER;

	public string PriceScript;

	private int OldGold;

	private int OldFloat;

	private string OldString;

	public string TextVariable;

	public string GlobalVarName;

	public bool DontSlash;

	private string DefaultText;

	private int stepWord;

	private int wordLength;

	private string LocalTalkText;

	private int delayText;

	public GameObject ButtonsObject;

	private bool ResetPrice;

	public Text3d()
	{
		OldGold = -1000001;
		OldFloat = -110111;
	}

	public virtual void Awake()
	{
		textMesh = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
		EnsureTextMaterial();
		text = TextKey;
		if (!string.IsNullOrEmpty(PriceScript))
		{
			GetPrice();
		}
		string textVariable = TextVariable;
		if (textVariable == "talk")
		{
			Global.StopTalkText = false;
			Global.TalkText = Global.TalkText.Replace("_", " ");
		}
		else
		{
			if (!(textVariable == "ask"))
			{
				return;
			}
			if (Global.ASKS != null && Extensions.get_length((System.Array)Global.ASKS) >= SlotNumber + 1)
			{
				if (Global.ASKS[SlotNumber] != null)
				{
					Global.ASKS[SlotNumber] = Global.ASKS[SlotNumber].Replace("_", " ");
					OldString = Global.ASKS[SlotNumber];
					text = Global.ASKS[SlotNumber];
				}
			}
			else
			{
				UnityEngine.Object.Destroy(transform.parent.gameObject);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.StopTalkText || !(TextVariable == "talk"))
		{
			return;
		}
		delayText++;
		if (Input.GetButton("Strike") || Input.GetButton("Use") || Input.GetButton("Use2"))
		{
			stepWord = 5;
		}
		else
		{
			stepWord = 2;
		}
		if (wordLength < Extensions.get_length(Global.TalkText))
		{
			wordLength += stepWord;
			if (wordLength > Extensions.get_length(Global.TalkText))
			{
				wordLength = Extensions.get_length(Global.TalkText);
			}
			LocalTalkText = Global.TalkText.Substring(0, wordLength);
			text = LocalTalkText;
		}
		if (delayText > 25 && wordLength >= Extensions.get_length(Global.TalkText))
		{
			ButtonsObject.SetActive(true);
			Global.StopTalkText = true;
		}
	}

	public virtual void Update()
	{
		TryTranslate();
		int num = default(int);
		switch (TextVariable)
		{
		case "diffNum":
			if (OldFloat != SlotNumber)
			{
				OldFloat = SlotNumber;
				text = Global.TextDifficulty(SlotNumber);
			}
			break;
		case "diff":
			if (OldFloat != Global.Difficulty)
			{
				OldFloat = Global.Difficulty;
				text = Global.TextDifficulty(Global.Difficulty);
			}
			break;
		case "MenuText":
			if (OldString != Global.MenuText)
			{
				OldString = Global.MenuText;
				text = Global.MenuText;
			}
			break;
		case "gold":
			if (OldGold != Global.Gold)
			{
				OldGold = Global.Gold;
				text = TextKey + ": " + Global.Gold;
			}
			break;
		case "exp":
			if (OldGold != Global.Experience)
			{
				OldGold = Global.Experience;
				text = TextKey + ": " + Global.Experience;
			}
			break;
		case "MaxHP":
			if ((float)OldFloat != Global.MaxHP)
			{
				OldFloat = (int)Global.MaxHP;
				text = TextKey + ": " + Global.MaxHP;
			}
			break;
		case "MaxMP":
			if ((float)OldFloat != Global.MaxMP)
			{
				OldFloat = (int)Global.MaxMP;
				text = TextKey + ": " + Global.MaxMP;
			}
			break;
		case "power":
			num = Convert.ToInt32(Global.Var["power"]);
			if (OldFloat != num)
			{
				OldFloat = num;
				text = TextKey + ": " + num;
			}
			break;
		case "defense":
			num = Convert.ToInt32(Global.Var["defense"]);
			if (OldFloat != num)
			{
				OldFloat = num;
				text = TextKey + ": " + num;
			}
			break;
		default:
			if (Extensions.get_length(GlobalVarName) > 0)
			{
				int num2 = Convert.ToInt32(Global.Var[GlobalVarName]);
				if (OldGold != num2)
				{
					OldGold = num2;
					text = TextKey + ": " + num2;
				}
			}
			break;
		}
		if (!string.IsNullOrEmpty(PriceScript) && ResetPrice)
		{
			ResetPrice = false;
			GetPrice();
		}
		if (OldGold != Global.Gold)
		{
			OldGold = Global.Gold;
			ResetPrice = true;
		}
	}

	public virtual void TryTranslate()
	{
		if (!(oldText == this.text))
		{
			if (DefaultText == null && (this.text != null || this.text != string.Empty))
			{
				DefaultText = this.text;
			}
			string text = null;
			text = ((!Translate) ? this.text : Lang.Menu(this.text));
			oldText = this.text;
			if (text == null)
			{
				text = string.Empty;
			}
			if (BIGLETTER)
			{
				text = text.ToUpper();
			}
			if (!DontSlash)
			{
				text = text.Replace("/", "\n");
			}
			textMesh.text = text;
		}
	}

	public virtual void Rename(string name)
	{
		if (!ZapretRename)
		{
			text = name;
		}
	}

	public virtual void DefaultName()
	{
		if (!ZapretRename)
		{
			text = DefaultText;
		}
	}

	public virtual void GetPrice()
	{
		string priceScript = PriceScript;
		if (priceScript == "castle")
		{
			text = string.Empty + Global.BuildPrice() + " $";
		}
	}

	public virtual void SetTextColor(Color c)
	{
		EnsureTextMaterial();
		if ((bool)textMesh)
		{
			textMesh.color = c;
		}
		float r = c.r;
		Color color = GetComponent<Renderer>().material.color;
		float num = (color.r = r);
		Color color2 = (GetComponent<Renderer>().material.color = color);
		float g = c.g;
		Color color4 = GetComponent<Renderer>().material.color;
		float num2 = (color4.g = g);
		Color color5 = (GetComponent<Renderer>().material.color = color4);
		float b = c.b;
		Color color7 = GetComponent<Renderer>().material.color;
		float num3 = (color7.b = b);
		Color color8 = (GetComponent<Renderer>().material.color = color7);
	}

	private void EnsureTextMaterial()
	{
		Renderer renderer = GetComponent<Renderer>();
		if (!(bool)renderer)
		{
			return;
		}

		Material material = renderer.material;
		if (!(bool)material)
		{
			return;
		}

		Shader textShader = Shader.Find("GUI/Text Shader");
		if (!(bool)textShader)
		{
			textShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
		}
		if ((bool)textShader)
		{
			material.shader = textShader;
		}

		if ((bool)textMesh && (bool)textMesh.font && (bool)textMesh.font.material && (bool)textMesh.font.material.mainTexture)
		{
			material.mainTexture = textMesh.font.material.mainTexture;
		}
	}

	public virtual void TextSetScale(float coef)
	{
		transform.localScale *= coef;
	}

	public virtual void DestroyIt()
	{
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Main()
	{
	}
}
