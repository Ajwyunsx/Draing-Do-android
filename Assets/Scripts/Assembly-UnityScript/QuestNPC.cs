using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class QuestNPC : MonoBehaviour
{
	public Quests[] Questz;

	public int ChanceToDelete;

	public string QuestType;

	public GameObject TalkObject;

	public string YesNoText;

	private int ChoosedNumberQuest;

	private int QuestNumber;

	public GameObject MenuObject;

	private int BuyTimer;

	public QuestNPC()
	{
		YesNoText = "Begin Mission?";
	}

	public virtual void Start()
	{
		if (UnityEngine.Random.Range(1, 100) <= ChanceToDelete)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		if (QuestType == "main")
		{
			if (Global.MainQuest >= Extensions.get_length((System.Array)Questz))
			{
				Global.MainQuest = Extensions.get_length((System.Array)Questz) - 1;
			}
			if ((bool)TalkObject)
			{
				TalkObject.SendMessage("Rename", Lang.Mission(Questz[Global.MainQuest].text), SendMessageOptions.DontRequireReceiver);
			}
			QuestNumber = Global.MainQuest;
		}
		else
		{
			int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)Questz));
			if ((bool)TalkObject)
			{
				TalkObject.SendMessage("Rename", Lang.Mission(Questz[num].text), SendMessageOptions.DontRequireReceiver);
			}
			QuestNumber = num;
		}
	}

	public virtual void FixedUpdate()
	{
		if (BuyTimer > 0)
		{
			BuyTimer--;
		}
	}

	public virtual void ActON()
	{
		if (BuyTimer <= 0)
		{
			BuyTimer = 50;
			Global.YesNoObject = gameObject;
			Global.YesMessage = YesNoText;
			Global.CreateMenuWindowObj(MenuObject);
		}
	}

	public virtual void RealActON()
	{
		if ((bool)GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().Play();
		}
		if ((bool)GetComponent<Animation>())
		{
			GetComponent<Animation>().Play();
		}
		PlayLevel();
	}

	public virtual void PlayLevel()
	{
		Global.CanSeed = true;
		MonoBehaviour.print("LEVEL: " + Questz[QuestNumber].level);
		Global.LoadLEVEL(Questz[QuestNumber].level, string.Empty);
	}

	public virtual void Main()
	{
	}
}
