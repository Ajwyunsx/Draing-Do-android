using System;
using UnityEngine;

[Serializable]
public class HUDBar : MonoBehaviour
{
	private int oldGold;

	private int oldSoul;

	private int oldHP;

	private int oldMP;

	private string oldQuest;

	private int oldExp;

	private int ExpBorderNext;

	private int ExpBorder;

	private int oldRang;

	private int oldOxygen;

	public GameObject HPBar;

	public Text3d HPText;

	public GameObject MPBar;

	public Text3d MPText;

	public Text3d GoldText;

	public Text3d SoulText;

	public GameObject OxygenBar;

	private bool oldHitAct;

	private int oldHitHP;

	private int oldHitLevel;

	public GameObject EnemyGOBar;

	public GameObject EnemyBar;

	public Text3d EnemyText;

	public GameObject HUDBAR;

	private bool oldShow;

	public Transform GoldGO;

	public Transform SoulGO;

	public Transform OxygenGO;

	[NonSerialized]
	public static int goldTimer;

	private int soulTimer;

	private int oxygenTimer;

	private Vector3 startGold;

	private Vector3 startSoul;

	private Vector3 startOxygen;

	private int noTimer;

	public HUDBar()
	{
		oldGold = -1;
		oldSoul = -1;
		oldHP = -1;
		oldMP = -1;
		oldQuest = string.Empty;
		oldExp = -1;
		oldRang = -1;
		oldHitAct = true;
		oldHitHP = -1;
		oldHitLevel = -1;
		oldShow = true;
	}

	public virtual void Start()
	{
		oldHP = -11111;
		InitGO();
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 1f));
		transform.position = position;
		HUDBarControl();
		CheckGO();
		HUDBarHide();
	}

	public virtual void FixedUpdate()
	{
		HUDBarControl();
		CheckGO();
	}

	public virtual void LateUpdate()
	{
		HUDBarHide();
	}

	public virtual void HUDBarControl()
	{
		if (!Global.HUD_ON || Global.Pause)
		{
			return;
		}
		string text = null;
		if (oldGold != Global.Gold)
		{
			oldGold = Global.Gold;
			text = string.Empty + oldGold;
			GoldText.Rename(text);
			goldTimer = 150;
		}
		if (oldSoul != Global.Experience)
		{
			oldSoul = Global.Experience;
			text = string.Empty + oldSoul;
			SoulText.Rename(text);
			soulTimer = 150;
		}
		if ((float)oldHP != Global.HP)
		{
			float num = Mathf.Ceil(Global.HP);
			if (!(num >= 0f))
			{
				num = 0f;
			}
			oldHP = (int)Global.HP;
			text = Lang.Menu("Health:") + " " + num + "/" + Global.MaxHP;
			HPText.Rename(text);
			float x = 1f * num / Global.MaxHP;
			Vector3 localScale = HPBar.transform.localScale;
			float num2 = (localScale.x = x);
			Vector3 vector = (HPBar.transform.localScale = localScale);
		}
		if ((float)oldMP != Global.MP)
		{
			float num3 = Global.MP;
			if (!(num3 >= 0f))
			{
				num3 = 0f;
			}
			if (!(num3 <= Global.MaxMP))
			{
				num3 = Global.MaxMP;
			}
			oldMP = (int)Global.MP;
			text = Lang.Menu("Stamina:") + " " + oldMP + "/" + Global.MaxMP;
			MPText.Rename(text);
			float x2 = 1f * num3 / Global.MaxMP;
			Vector3 localScale2 = MPBar.transform.localScale;
			float num4 = (localScale2.x = x2);
			Vector3 vector3 = (MPBar.transform.localScale = localScale2);
		}
		if (Global.Oxygen != Global.MaxOxygen && oldOxygen != Global.Oxygen)
		{
			if (Global.Oxygen < 0)
			{
				Global.Oxygen = 0;
			}
			oldOxygen = Global.Oxygen;
			oxygenTimer = 5;
			float x3 = 1f * (float)Global.Oxygen / (float)Global.MaxOxygen;
			Vector3 localScale3 = OxygenBar.transform.localScale;
			float num5 = (localScale3.x = x3);
			Vector3 vector5 = (OxygenBar.transform.localScale = localScale3);
		}
		if (Global.EnemyHitTimer > 0 && !oldHitAct)
		{
			oldHitAct = true;
			EnemyGOBar.SetActive(oldHitAct);
		}
		if (Global.EnemyHitTimer <= 0 && oldHitAct)
		{
			oldHitAct = false;
			EnemyGOBar.SetActive(oldHitAct);
		}
		if (Global.EnemyHitTimer > 0)
		{
			if ((bool)Global.EnemAI)
			{
				Global.EnemyHitHP = (int)Global.EnemAI.HP;
			}
			if (oldHitHP != Global.EnemyHitHP || oldHitLevel != Global.EnemyHitLevel)
			{
				oldHitHP = Global.EnemyHitHP;
				oldHitLevel = Global.EnemyHitLevel;
				text = Lang.Menu("Enemy") + ": " + oldHitHP + "/" + Global.EnemyHitMaxHP;
				EnemyText.Rename(text);
				float num6 = 1f * (float)oldHitHP / (float)Global.EnemyHitMaxHP;
				if (!(num6 <= 0f))
				{
					float x4 = num6;
					Vector3 localScale4 = EnemyBar.transform.localScale;
					float num7 = (localScale4.x = x4);
					Vector3 vector7 = (EnemyBar.transform.localScale = localScale4);
				}
				else
				{
					int num8 = 0;
					Vector3 localScale5 = EnemyBar.transform.localScale;
					float num9 = (localScale5.x = num8);
					Vector3 vector9 = (EnemyBar.transform.localScale = localScale5);
				}
			}
		}
		else if (EnemyGOBar.active)
		{
			oldHitAct = false;
			EnemyGOBar.SetActive(false);
		}
	}

	public virtual void HUDBarHide()
	{
		if (HUDBAR == null)
		{
			return;
		}
		if (!Global.HUD_ON || TalkPause.IsGameplayBlocked())
		{
			if (oldShow)
			{
				oldShow = false;
				HUDBAR.SetActive(oldShow);
			}
		}
		else if (!oldShow)
		{
			oldShow = true;
			HUDBAR.SetActive(oldShow);
		}
	}

	public virtual void InitGO()
	{
		noTimer = 25;
		startSoul = SoulGO.localPosition;
		startGold = GoldGO.localPosition;
		startOxygen = OxygenGO.localPosition;
		SoulGO.localPosition = startSoul - new Vector3(0.1f, 0f, 0f);
		GoldGO.localPosition = startGold - new Vector3(0.1f, 0f, 0f);
		OxygenGO.localPosition = startOxygen - new Vector3(0.1f, 0f, 0f);
	}

	public virtual void CheckGO()
	{
		if (noTimer > 0)
		{
			noTimer--;
			soulTimer = 0;
			goldTimer = 0;
			oxygenTimer = 0;
			return;
		}
		if (soulTimer > 0)
		{
			soulTimer--;
			SoulGO.localPosition = Vector3.Lerp(SoulGO.localPosition, startSoul, 0.25f);
		}
		else
		{
			SoulGO.localPosition = Vector3.Lerp(SoulGO.localPosition, startSoul - new Vector3(0.1f, 0f, 0f), 0.25f);
		}
		if (goldTimer > 0)
		{
			goldTimer--;
			GoldGO.localPosition = Vector3.Lerp(GoldGO.localPosition, startGold, 0.25f);
		}
		else
		{
			GoldGO.localPosition = Vector3.Lerp(GoldGO.localPosition, startGold - new Vector3(0.1f, 0f, 0f), 0.25f);
		}
		if (oxygenTimer > 0)
		{
			oxygenTimer--;
			OxygenGO.localPosition = Vector3.Lerp(OxygenGO.localPosition, startOxygen, 0.25f);
		}
		else
		{
			OxygenGO.localPosition = Vector3.Lerp(OxygenGO.localPosition, startOxygen - new Vector3(0.1f, 0f, 0f), 0.25f);
		}
	}

	public virtual void Main()
	{
	}
}
