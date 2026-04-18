using System;
using UnityEngine;

[Serializable]
public class CutsceneRun : MonoBehaviour
{
	public GameObject MenuWindow;

	private bool Once;

	public string Quest;

	public virtual void Awake()
	{
		if (!string.IsNullOrEmpty(Quest))
		{
			MonoBehaviour.print("Q1: " + Quest);
			if (Global.CheckQuest(Quest))
			{
				MonoBehaviour.print("Q2: " + Quest);
				Once = true;
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag != "Player"))
		{
			Run();
		}
	}

	public virtual void Run()
	{
		if (!Once)
		{
			Once = true;
			if (!string.IsNullOrEmpty(Quest))
			{
				MonoBehaviour.print("Q Add: " + Quest);
				Global.AddQuest(Quest);
			}
			if ((bool)MenuWindow)
			{
				Global.CreateMenuWindowObj(MenuWindow);
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
