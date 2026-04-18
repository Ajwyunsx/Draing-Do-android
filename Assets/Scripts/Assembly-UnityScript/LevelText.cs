using System;
using UnityEngine;

[Serializable]
public class LevelText : MonoBehaviour
{
	private TextMesh text;

	public virtual void Start()
	{
		text = (TextMesh)GetComponent(typeof(TextMesh));
		if (Global.TextQuest != text.text)
		{
			text.text = Global.TextQuest;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.TextQuest != text.text)
		{
			text.text = Global.TextQuest;
		}
	}

	public virtual void Main()
	{
	}
}
