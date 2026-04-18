using System;
using UnityEngine;

[Serializable]
public class Matter : MonoBehaviour
{
	public string MatterName;

	public string MatterName2;

	public string SpecialReName;

	[Tooltip("UseItMode: exit/ use / zero / item / equip / food / drink / lock")]
	public string UseItMode;

	[Tooltip("UseItMode2: hide / empty")]
	public string UseItMode2;

	public bool CheckDistanceForUse;

	public bool MassDistancePoint;

	public bool DontWorkWithSelected;

	public bool NoInteractive;

	public string ModeMouse;

	public AudioClip SFX;

	public AudioClip SFX2;

	public bool UseButton;

	public bool UseMouse;

	public bool Seller;

	public Matter()
	{
		MatterName = "Water";
		UseItMode = "use";
		CheckDistanceForUse = true;
		MassDistancePoint = true;
	}

	public virtual void Start()
	{
		if (SpecialReName != string.Empty && SpecialReName != null)
		{
			gameObject.name = SpecialReName;
		}
	}

	public virtual void onMouseDown()
	{
		if (Monitor.dist && (bool)SlotItem.selected)
		{
			SlotItem.SayNoMix = true;
			SlotItem.selected.SendMessage("MixItems", gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnMouseOver()
	{
		if (!MassDistancePoint)
		{
			Monitor.LastOverTrans = transform;
		}
		else
		{
			Monitor.LastOverTrans = null;
		}
		Monitor.TextA = MatterName;
		if (!string.IsNullOrEmpty(MatterName2))
		{
			Monitor.TextB = MatterName2;
		}
		else
		{
			string lhs = string.Empty;
			string rhs = string.Empty;
			switch (UseItMode)
			{
			case "zero":
				Monitor.TextA = string.Empty;
				lhs = string.Empty;
				Global.MouseMode = null;
				break;
			case "unwear":
				lhs = "Click L.Mouse to unequip it.";
				break;
			case "wear":
				lhs = "Click L.Mouse to equip it.";
				break;
			case "levelUp":
				lhs = "Click R.Mouse to Level Up.";
				break;
			case "talk":
				lhs = "Click R.Mouse to Talk.";
				break;
			case "play":
				lhs = "Click it to Play.";
				break;
			case "diff":
				lhs = "Click it to select difficulty.";
				break;
			case "survive":
				lhs = "Click it to change Survival Mode.";
				break;
			case "char":
				lhs = "Click it to Play with that Puppet.";
				break;
			case "map":
				lhs = "Click it to enter.";
				break;
			case "enter":
				lhs = "Click R.Mouse to enter.";
				break;
			case "exit":
				lhs = "Click R.Mouse to exit.";
				break;
			case "menu":
				lhs = "Click L.Mouse or Press Q Button to open menu.";
				break;
			case "use":
				lhs = "Click R.Mouse to use.";
				break;
			case "item":
				lhs = "Click L.Mouse to get. Press E to use.";
				break;
			case "wait1":
				lhs = "Click R.Mouse to wait 1 hour.";
				break;
			case "sleep":
				lhs = "Click R.Mouse to sleep. (-6 hours)";
				break;
			case "equip":
				lhs = "Click L.Mouse to get. Press E to equip.";
				break;
			case "food":
				lhs = "Click L.Mouse to get. Press E to eat.";
				break;
			case "drink":
				lhs = "Click L.Mouse to get. Press E to drink.";
				break;
			case "lock":
				lhs = "LOCKED!";
				break;
			}
			if (Seller)
			{
				lhs += " Drag item here to sell it.";
			}
			string useItMode = UseItMode2;
			if (useItMode == "hide")
			{
				rhs = "  Press Q to hide.";
			}
			else if (useItMode == "empty")
			{
				rhs = "  Press Q to empty it.";
			}
			if (UseItMode != "no")
			{
				Monitor.TextB = lhs + rhs;
			}
		}
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			onMouseDown();
		}
		if (NoInteractive)
		{
			return;
		}
		Monitor.DontDrop = true;
		Monitor.MouseNo = CheckDistanceForUse;
		Global.MouseMode = ModeMouse;
		if ((bool)SlotItem.selected && Seller)
		{
			Global.MouseMode = "gold";
		}
		if (DontWorkWithSelected && (bool)SlotItem.selected)
		{
			Monitor.ForceNo = true;
		}
		if (UseMouse && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
		{
			if (!Monitor.dist && CheckDistanceForUse)
			{
				return;
			}
			gameObject.SendMessage("UseThatItem", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
		}
		if (!UseButton)
		{
			return;
		}
		if (Input.GetButtonDown("Use"))
		{
			if (!Monitor.dist && CheckDistanceForUse)
			{
				return;
			}
			gameObject.SendMessage("UseThatItem", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
		}
		if (Input.GetButtonDown("Use2") && (Monitor.dist || !CheckDistanceForUse))
		{
			gameObject.SendMessage("UseThatItem2", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)SFX2)
			{
				AudioSource.PlayClipAtPoint(SFX2, transform.position);
			}
		}
	}

	public virtual void OnMouseUpAsButton()
	{
		if (Seller && (bool)SlotItem.selected)
		{
			SlotItem component = SlotItem.selected.GetComponent<SlotItem>();
			if ((bool)component)
			{
				int num = (int)((float)component.Price * 0.5f);
				Global.Gold += num;
				Global.CreateText("+ " + num + " Gold!", transform.position + new Vector3(0f, 0.75f, -2f), new Color(1f, 0.9f, 0f, 1f), UnityEngine.Random.Range(-25, 25));
				AudioSource.PlayClipAtPoint(LoadData.SFX("buy"), transform.position);
				UnityEngine.Object.Destroy(SlotItem.selected);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (Seller)
		{
			HUDBar.goldTimer = 100;
		}
	}

	public virtual void Main()
	{
	}
}
