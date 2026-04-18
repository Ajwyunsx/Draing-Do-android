using System;
using UnityEngine;

[Serializable]
public class ClothTab : MonoBehaviour
{
	private string NAME;

	public string Body;

	public string Hand1;

	public string Hand2;

	public string Head;

	public string Leg1;

	public string Leg2;

	public string Boot1;

	public string Boot2;

	public string Skill;

	public ClothTab()
	{
		Body = "body";
		Hand1 = "hand";
		Hand2 = "hand";
		Head = "head";
		Leg1 = "leg";
		Leg2 = "leg";
		Boot1 = "boot";
		Boot2 = "boot";
		Skill = string.Empty;
	}

	public virtual void Awake()
	{
		NAME = gameObject.name;
	}

	public virtual void GetData()
	{
		if (!string.IsNullOrEmpty(Body))
		{
			Global.clothesSpriteNames[0] = Body;
		}
		if (!string.IsNullOrEmpty(Hand1))
		{
			Global.clothesSpriteNames[1] = Hand1;
		}
		if (!string.IsNullOrEmpty(Hand2))
		{
			Global.clothesSpriteNames[2] = Hand2;
		}
		if (!string.IsNullOrEmpty(Head))
		{
			Global.clothesSpriteNames[3] = Head;
		}
		if (!string.IsNullOrEmpty(Leg1))
		{
			Global.clothesSpriteNames[6] = Leg1;
		}
		if (!string.IsNullOrEmpty(Leg2))
		{
			Global.clothesSpriteNames[7] = Leg2;
		}
		if (!string.IsNullOrEmpty(Boot1))
		{
			Global.clothesSpriteNames[8] = Boot1;
		}
		if (!string.IsNullOrEmpty(Boot2))
		{
			Global.clothesSpriteNames[9] = Boot2;
		}
		if (Skill != string.Empty)
		{
			SetSkills();
		}
		else
		{
			ResetSkills();
		}
	}

	public virtual void SetSkills()
	{
		string skill = Skill;
		if (skill == "vodolaz")
		{
			Global.Vodolaz = true;
		}
	}

	public virtual void ResetSkills()
	{
		Global.Vodolaz = false;
	}

	public virtual void Main()
	{
	}
}
