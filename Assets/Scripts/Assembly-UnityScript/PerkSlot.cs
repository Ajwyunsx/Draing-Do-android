using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class PerkSlot : MonoBehaviour
{
	public string PerkName;

	private int PerkLevel;

	private string Description;

	private string Description2;

	private float Formula;

	private Sprite SpriteImage;

	public TextMesh LevelTextObject;

	public SpriteRenderer Sprite;

	private float scale;

	private int scaleTime;

	private Vector3 StartScale;

	private Transform trans;

	public PerkSlot()
	{
		scale = 1f;
		scaleTime = 10;
	}

	public virtual void Start()
	{
		trans = transform;
		StartScale = trans.localScale;
		for (int i = 0; i < Extensions.get_length((System.Array)Perks.Perk); i++)
		{
			if (!(PerkName == Perks.Perk[i].name))
			{
				continue;
			}
			PerkLevel = Perks.Perk[i].level;
			if (PerkLevel > 0)
			{
				Description = Perks.Perk[i].descript;
				Formula = Perks.Perk[i].formula;
				Description2 = Perks.Perk[i].descript2;
				SpriteImage = Perks.Perk[i].icon;
				if ((bool)SpriteImage)
				{
					Sprite.sprite = SpriteImage;
				}
				LevelTextObject.text = string.Empty + PerkLevel;
				if (Description2 == "%")
				{
					Description = string.Empty + Description + Formula * 100f * (float)PerkLevel + " %";
				}
				else
				{
					Description = string.Empty + Description + Formula * (float)PerkLevel + Description2;
				}
			}
			break;
		}
	}

	public virtual void Update()
	{
		if (scaleTime > 0)
		{
			scaleTime--;
			trans.localScale = Vector3.Lerp(trans.localScale, StartScale * scale, 0.5f);
		}
	}

	public virtual void OnMouseEnter()
	{
		scale = 1.05f;
		scaleTime = 10;
		if (Description != null)
		{
			Global.MenuText = Description;
			Global.LastMenuTextObject = gameObject;
		}
	}

	public virtual void OnMouseExit()
	{
		scale = 1f;
		scaleTime = 10;
		if (Description != null && Global.LastMenuTextObject == gameObject)
		{
			Global.MenuText = string.Empty;
			Global.LastMenuTextObject = null;
		}
	}

	public virtual void Main()
	{
	}
}
