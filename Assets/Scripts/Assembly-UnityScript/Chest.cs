using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Chest : MonoBehaviour
{
	public string SAVENAME;

	[HideInInspector]
	public bool IsOPEN;

	public GameObject[] ObjectChance;

	[HideInInspector]
	public int timer;

	public Chest()
	{
		SAVENAME = string.Empty;
		timer = 40;
	}

	public virtual void Start()
	{
		if (SAVENAME != string.Empty && Global.CheckStuff(SAVENAME))
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void ActON()
	{
		if (!IsOPEN)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			if ((bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play();
			}
			GetComponent<Collider>().enabled = false;
			IsOPEN = true;
		}
	}

	public virtual void OpenChest()
	{
		int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)ObjectChance));
		if ((bool)ObjectChance[num])
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ObjectChance[num]) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position;
			float z = Global.CurrentPlayerObject.position.z + 0.1f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num2 = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			Global.LastCreatedObject.SendMessage("NOSave", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
