using System;
using UnityEngine;

[Serializable]
public class TextHUDTablo : MonoBehaviour
{
	public GUISkin newSkin;

	public string text;

	private string Text;

	private string oldText;

	private Vector3 pos;

	private Vector3 max;

	public bool YesNoMode;

	public bool ZapretRename;

	public bool Translate;

	public bool BIGLETTER;

	public TextHUDTablo()
	{
		text = "MENU";
	}

	public virtual void Awake()
	{
		TryTranslate();
	}

	public virtual void Update()
	{
		TryTranslate();
	}

	public virtual void TryTranslate()
	{
		if (!(oldText == text))
		{
			if (Translate)
			{
				Text = Lang.Menu(text);
			}
			else
			{
				Text = text;
			}
			oldText = text;
			if (BIGLETTER)
			{
				Text = Text.ToUpper();
			}
		}
	}

	public virtual void OnGUI()
	{
		if (!Global.YesNoMode || YesNoMode)
		{
			GUI.skin = newSkin;
			GUI.color = new Color(0f, 0f, 0f, 1f);
			GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1024f, (float)Screen.height / 768f, 1f));
			float num = 1024f / (float)Screen.width;
			float num2 = 768f / (float)Screen.height;
			pos = Camera.main.WorldToScreenPoint(transform.position - new Vector3(gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f, (0f - gameObject.GetComponent<Renderer>().bounds.size.y) * 0.5f, 0f));
			max = Camera.main.WorldToScreenPoint(transform.position + new Vector3(gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f, (0f - gameObject.GetComponent<Renderer>().bounds.size.y) * 0.5f, 0f));
			max -= pos;
			max.y = 0f - max.y;
			GUI.Label(new Rect(pos.x * num, ((float)Screen.height - pos.y) * num2, max.x * num, max.y * num2), Text);
		}
	}

	public virtual void Rename(string name)
	{
		if (!ZapretRename)
		{
			text = name;
		}
	}

	public virtual void Main()
	{
	}
}
