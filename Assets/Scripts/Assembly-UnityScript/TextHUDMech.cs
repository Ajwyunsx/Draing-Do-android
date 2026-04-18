using System;
using UnityEngine;

[Serializable]
public class TextHUDMech : MonoBehaviour
{
	public TextMesh mesh;

	public string text;

	private string Text;

	private string oldText;

	public bool ZapretRename;

	public bool Translate;

	public bool BIGLETTER;

	public TextHUDMech()
	{
		text = "MENU";
	}

	public virtual void Awake()
	{
		mesh = (TextMesh)GetComponent(typeof(TextMesh));
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
			Text = Text.Replace("NEWLINE", "\n");
			mesh.text = Text;
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
