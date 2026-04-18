using System;
using UnityEngine;

[Serializable]
public class QuestDeleter : MonoBehaviour
{
	public string NAME;

	public bool invert;

	public QuestDeleter()
	{
		NAME = "happy";
	}

	public virtual void Awake()
	{
		if (Global.CheckQuest(NAME))
		{
			if (!invert)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		else if (invert)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
