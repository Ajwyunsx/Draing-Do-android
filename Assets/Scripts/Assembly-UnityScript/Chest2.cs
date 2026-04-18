using System;
using UnityEngine;

[Serializable]
public class Chest2 : MonoBehaviour
{
	public string SAVENAME;

	[HideInInspector]
	public bool IsOPEN;

	[HideInInspector]
	public bool _AppearTreasure;

	public GameObject treasure;

	[HideInInspector]
	public int timer;

	private int timer2;

	private Animator animator;

	public bool LockChest;

	public GameObject locker;

	public Chest2()
	{
		SAVENAME = string.Empty;
		timer = 40;
	}

	public virtual void Start()
	{
		bool flag = Global.CheckStuff(SAVENAME);
		if (SAVENAME != string.Empty && flag)
		{
			FakeActON();
		}
		if (flag)
		{
			LockChest = false;
		}
		if (!LockChest)
		{
			DeleteLocker();
		}
		animator = (Animator)transform.GetComponent("Animator");
	}

	public virtual void FixedUpdate()
	{
		if (timer2 > 0)
		{
			timer2++;
			if (timer2 > 50)
			{
				timer2 = 0;
				PlayAct();
			}
		}
		if (_AppearTreasure || !IsOPEN)
		{
			return;
		}
		timer--;
		if (timer <= 0)
		{
			if ((bool)treasure)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(treasure, transform.position, Quaternion.identity);
				float z = Global.CurrentPlayerObject.position.z + 0.1f;
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.z = z);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
				Global.LastCreatedObject.SendMessage("OnAppear", SAVENAME, SendMessageOptions.DontRequireReceiver);
			}
			_AppearTreasure = true;
		}
	}

	public virtual void OnTriggerEnter(Collider collision)
	{
		if (LockChest)
		{
			if (Convert.ToInt32(Global.Var["key"]) <= 0)
			{
				return;
			}
			DeleteLocker();
			Global.Var["key"] = Convert.ToInt32(Global.Var["key"]) - 1;
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position;
			float z = Global.LastCreatedObject.transform.position.z - 0.2f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
		}
		if (!IsOPEN)
		{
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			animator.SetBool("isOpened", true);
			GetComponent<Collider>().enabled = false;
			IsOPEN = true;
			gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void FakeActON()
	{
		timer2 = 1;
	}

	public virtual void PlayAct()
	{
		if (!IsOPEN)
		{
			animator.SetBool("isOpened", true);
			GetComponent<Collider>().enabled = false;
			IsOPEN = true;
			gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void DeleteLocker()
	{
		locker.GetComponent<Renderer>().enabled = false;
	}

	public virtual void Main()
	{
	}
}
