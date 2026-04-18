using System;
using UnityEngine;

[Serializable]
public class LastBoss : MonoBehaviour
{
	public bool timerON;

	private int timer;

	public string Level;

	public string QuestAdd;

	public string CurrentMission;

	public LastBoss()
	{
		timer = 200;
		Level = string.Empty;
		QuestAdd = "happy";
		CurrentMission = string.Empty;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void DISAPPEAR()
	{
		Global.LoadLEVEL(Level, string.Empty);
		Global.QuakeStart(75, 30f);
		Global.AddQuest(QuestAdd);
		if (CurrentMission != string.Empty)
		{
			Global.CurrentMission = CurrentMission;
		}
	}

	public virtual void Main()
	{
	}
}
