using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SurvivalHUD : MonoBehaviour
{
	public BuffList[] Buffs;

	public GUISkin SkinC;

	private int sleepTimer;

	private int foodTimer;

	private int longTimer;

	private int poisonTimer;

	private int timerMind;

	public GameObject ScaryPrefab;

	[NonSerialized]
	public static GameObject ScaryObject;

	[NonSerialized]
	public static AudioSource ScaryMusic;

	private WindFX wind;

	private Texture DangerTexture;

	private Texture DarkTexture;

	public GameObject[] shades;

	public virtual void Start()
	{
		DangerTexture = LoadData.TEX("danger");
		DarkTexture = LoadData.TEX("darkness");
		if (ScaryObject == null)
		{
			ScaryObject = UnityEngine.Object.Instantiate(ScaryPrefab) as GameObject;
			UnityEngine.Object.DontDestroyOnLoad(ScaryObject);
			ScaryMusic = ScaryObject.GetComponent<AudioSource>();
		}
		ScaryMusic.volume = 0.5f - (1f - (float)Convert.ToInt32(Global.Var["mind"]) / 100f) * 0.5f;
	}

	public virtual void FixedUpdate()
	{
		if (!Global.HUD_ON)
		{
			return;
		}
		int num = default(int);
		float num2 = default(float);
		if (!(Global.HP / Global.MaxHP > 0.5f) && Global.FadeMode == 0)
		{
			Global.DangerAlpha = Mathf.Lerp(Global.DangerAlpha, 1f - Global.HP / Global.MaxHP, 0.05f);
		}
		else
		{
			Global.DangerAlpha = Mathf.Lerp(Global.DangerAlpha, 0f, 0.05f);
		}
		if (!RuntimeServices.EqualityOperator(Global.Var["survive"], 1))
		{
			return;
		}
		if (Global.PLUSTIME > 0)
		{
			MonoBehaviour.print("Plus: " + Global.PLUSTIME);
			num2 = Global.PLUSTIME;
			num = Convert.ToInt32(Global.Var["food"]);
			num = (int)((float)num + (num2 * 8f + (float)UnityEngine.Random.Range(-2, 3)));
			if (num > 100)
			{
				num = 100;
			}
			if (num < 0)
			{
				num = 0;
			}
			Global.Var["food"] = num;
			num = Convert.ToInt32(Global.Var["mind"]);
			num = (int)((float)num - (num2 * 2f + (float)UnityEngine.Random.Range(-1, 2)));
			if (num > 100)
			{
				num = 100;
			}
			if (num < 0)
			{
				num = 0;
			}
			Global.Var["mind"] = num;
			ScaryMusic.volume = 0.5f - (1f - (float)Convert.ToInt32(Global.Var["mind"]) / 100f) * 0.5f;
			num = Convert.ToInt32(Global.Var["sick"]);
			num = (int)((float)num - (num2 * 2f + (float)UnityEngine.Random.Range(-1, 2)));
			if (num > 100)
			{
				num = 100;
			}
			if (num < 0)
			{
				num = 0;
			}
			Global.Var["sick"] = num;
			num = Convert.ToInt32(Global.Var["poison"]);
			num = (int)((float)num - (num2 * 9f + (float)UnityEngine.Random.Range(-6, 6)));
			if (num > 100)
			{
				num = 100;
			}
			if (num < 0)
			{
				num = 0;
			}
			Global.Var["poison"] = num;
			Global.PLUSTIME = 0;
		}
		timerMind++;
		if (timerMind >= 500)
		{
			timerMind = 0;
			if (Convert.ToInt32(Global.Var["mind"]) < 100)
			{
				Global.Var["mind"] = Convert.ToInt32(Global.Var["mind"]) + 1;
			}
		}
		if (Convert.ToInt32(Global.Var["mind"]) < 0)
		{
			Global.Var["mind"] = 0;
		}
		if (Convert.ToInt32(Global.Var["mind"]) > 100)
		{
			Global.Var["mind"] = 100;
		}
		if ((bool)Global.GlobalMusic)
		{
			Global.GlobalMusic.volume = (1f - (float)Convert.ToInt32(Global.Var["mind"]) / 100f * 1.2f) * 0.5f;
		}
		ScaryMusic.volume = 0.5f - (1f - (float)Convert.ToInt32(Global.Var["mind"]) / 100f) * 0.5f;
		if (!(Global.HP <= 1f))
		{
			if (Convert.ToInt32(Global.Var["sleep"]) >= 75)
			{
				Global.HP -= 3.8E-05f * Global.MaxHP;
			}
			if (Convert.ToInt32(Global.Var["food"]) >= 75)
			{
				Global.HP -= 3.8E-05f * Global.MaxHP;
			}
			if (Convert.ToInt32(Global.Var["sick"]) >= 25)
			{
				Global.HP -= 8E-05f * Global.MaxHP;
			}
		}
		if (!(Global.HP <= 0f))
		{
			if (Convert.ToInt32(Global.Var["poison"]) >= 25)
			{
				Global.HP -= 0.00015f * Global.MaxHP;
			}
			if (!(Global.HP > 0f))
			{
				Global.LastStrike = new SendStrike();
				Global.DeadLevel = null;
				Global.CurrentPlayerObject.SendMessage("HPMinus", 1, SendMessageOptions.DontRequireReceiver);
			}
		}
		SpawnShades();
	}

	public virtual void OnGUI()
	{
		if (!Global.HUD_ON)
		{
			return;
		}
		GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1000f, (float)Screen.height / 563f, 1f));
		if (Convert.ToInt32(Global.Var["survive"]) == 1)
		{
			float num = (float)Convert.ToInt32(Global.Var["mind"]) * 0.0075f;
			if (!(num <= 0.01f))
			{
				GUI.color = new Color(0f, 0f, 0f, num - 0.2f);
				GUI.DrawTexture(new Rect(0f, 0f, 1000f, 563f), DarkTexture);
			}
		}
		if (!(Global.DangerAlpha <= 0.01f))
		{
			GUI.color = new Color(0.75f, 0f, 0f, Global.DangerAlpha - 0.2f);
			GUI.DrawTexture(new Rect(0f, 0f, 1000f, 563f), DangerTexture);
		}
		if (!RuntimeServices.EqualityOperator(Global.Var["survive"], 1))
		{
			return;
		}
		GUI.skin = SkinC;
		int num2 = 0;
		for (int i = 0; i < Extensions.get_length((System.Array)Buffs); i++)
		{
			int num3 = Convert.ToInt32(Global.Var[Buffs[i].VarName]);
			if (num3 > 0)
			{
				float a = 0.5f + (float)(num3 / 50);
				GUI.color = new Color(0f, 0f, 0f, a);
				GUI.Label(new Rect(841f, num2 * 22 + 1, 150f, 50f), Buffs[i].TextName + ": " + num3 + " %");
				if (num3 < 75)
				{
					GUI.color = new Color(1f, 1f, 1f, a);
				}
				else
				{
					GUI.color = new Color(1f, 0f, 0f, 1f);
				}
				GUI.Label(new Rect(840f, num2 * 22, 150f, 50f), Buffs[i].TextName + ": " + num3 + " %");
				num2++;
			}
		}
	}

	public virtual void SpawnShades()
	{
		if (!TalkPause.IsGameplayBlocked() && Convert.ToInt32(Global.Var["mind"]) >= 25)
		{
			int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)shades));
			float num2 = (float)Convert.ToInt32(Global.Var["mind"]) * 0.05f;
			if ((bool)shades[num] && !(UnityEngine.Random.Range(0.5f, 100f) > num2))
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(shades[num]) as GameObject;
				Global.LastCreatedObject.name = shades[num].name;
				Global.LastCreatedObject.transform.position = Global.CurrentPlayerObject.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-7, 7), 0f);
				float z = -1.3f;
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num3 = (position.z = z);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			}
		}
	}

	public virtual void Main()
	{
	}
}
