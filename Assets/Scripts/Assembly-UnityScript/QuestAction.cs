using System;
using UnityEngine;

[Serializable]
public class QuestAction : MonoBehaviour
{
	public string NAME;

	public bool OnStart;

	public QuestAction()
	{
		NAME = "quest";
	}

	public virtual void Start()
	{
		if (OnStart)
		{
			CommandTo();
		}
		if (Global.CheckQuest(NAME))
		{
			gameObject.SendMessage("ByButtonOffSFX", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CommandTo()
	{
		Global.AddQuest(NAME);
	}

	public virtual void Main()
	{
	}
}
