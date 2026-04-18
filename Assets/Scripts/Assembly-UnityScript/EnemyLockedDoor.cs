using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class EnemyLockedDoor : MonoBehaviour
{
	public bool TrigCommand;

	public GameObject[] ObjectList;

	public GameObject[] EnemyObjects;

	[HideInInspector]
	public int delay;

	[HideInInspector]
	public bool IsOKPassword;

	public EnemyLockedDoor()
	{
		TrigCommand = true;
	}

	public virtual void Start()
	{
		delay = UnityEngine.Random.Range(0, 25);
	}

	public virtual void FixedUpdate()
	{
		if (!IsOKPassword)
		{
			delay++;
			if (delay >= 25)
			{
				delay = 0;
				CheckPassword();
			}
		}
	}

	public virtual void CheckPassword()
	{
		for (int i = 0; i < Extensions.get_length((System.Array)EnemyObjects); i++)
		{
			if ((bool)EnemyObjects[i])
			{
				return;
			}
		}
		CommandTo();
	}

	public virtual void CommandTo()
	{
		if ((bool)GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().Play();
		}
		IsOKPassword = true;
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			if ((bool)(ObjectList[i] as GameObject))
			{
				ObjectList[i].SendMessage("ByButton", TrigCommand, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void ByButton(bool @bool)
	{
		if (!IsOKPassword)
		{
			CommandTo();
		}
	}

	public virtual void Main()
	{
	}
}
