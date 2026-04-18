using System;
using UnityEngine;

[Serializable]
public class LevelUpButton : MonoBehaviour
{
	private int price;

	public float FACTOR;

	public string Mode;

	private int oldExperience;

	private bool enable;

	private Color oldColor;

	public LevelUpButton()
	{
		FACTOR = 1f;
		Mode = "MaxHP";
		oldExperience = -1;
		enable = true;
	}

	public virtual void Start()
	{
		oldColor = GetComponent<Renderer>().material.color;
		StandartActions();
	}

	public virtual void FixedUpdate()
	{
		if (oldExperience != Global.Experience)
		{
			StandartActions();
		}
	}

	public virtual void StandartActions()
	{
		oldExperience = Global.Experience;
		price = (int)GetPrice(GetNumber());
		gameObject.BroadcastMessage("Rename", "Exp: " + price + string.Empty, SendMessageOptions.DontRequireReceiver);
		if (Global.Experience < price)
		{
			if (enable)
			{
				enable = false;
				GetComponent<Collider>().enabled = enable;
				GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
			}
		}
		else if (!enable)
		{
			enable = true;
			GetComponent<Collider>().enabled = enable;
			GetComponent<Renderer>().material.color = oldColor;
		}
	}

	public virtual void OnMouseUpAsButton()
	{
		if (Global.Experience >= price)
		{
			int num = default(int);
			Global.Experience -= price;
			AudioSource.PlayClipAtPoint(LoadData.SFX("buy"), transform.position);
			switch (Mode)
			{
			case "MaxHP":
				Global.MaxHP += 1f;
				Global.HP += 1f;
				break;
			case "MaxMP":
				Global.MaxMP += 1f;
				Global.MP += 1f;
				break;
			case "power":
				num = Convert.ToInt32(Global.Var["power"]);
				Global.Var["power"] = num + 1;
				break;
			case "defense":
				num = Convert.ToInt32(Global.Var["defense"]);
				Global.Var["defense"] = num + 1;
				break;
			}
			transform.parent.BroadcastMessage("StandartActions", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual int GetNumber()
	{
		int result;
		switch (Mode)
		{
		case "MaxHP":
			result = (int)Global.MaxHP;
			break;
		case "MaxMP":
			result = (int)Global.MaxMP;
			break;
		case "power":
			result = Convert.ToInt32(Global.Var["power"]);
			break;
		case "defense":
			result = Convert.ToInt32(Global.Var["defense"]);
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	public virtual float GetPrice(float baseNum)
	{
		return baseNum * FACTOR * 2f;
	}

	public virtual void Main()
	{
	}
}
