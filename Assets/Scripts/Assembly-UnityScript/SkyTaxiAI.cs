using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SkyTaxiAI : MonoBehaviour
{
	public AudioClip SFX;

	private Vector3 target;

	public bool go;

	public int Dir;

	private Vector3 StartScale;

	private Transform myTransform;

	public float Speed;

	public string ToLevel;

	public bool ShowResult;

	private Vector3 speed;

	private int BuyTimer;

	public GameObject MenuObject;

	public SkyTaxiAI()
	{
		Dir = 1;
		Speed = 0.01f;
		ToLevel = Global.DefaultLevel;
		ShowResult = true;
	}

	public virtual void Awake()
	{
		Global.SkyTaxiCall = gameObject;
		myTransform = transform;
		StartScale = myTransform.localScale;
	}

	public virtual void Update()
	{
		float x = (float)Dir * StartScale.x;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.x = x);
		Vector3 vector = (myTransform.localScale = localScale);
	}

	public virtual void FixedUpdate()
	{
		if (BuyTimer > 0)
		{
			BuyTimer--;
		}
		if (go)
		{
			float x = Mathf.Lerp(myTransform.position.x, target.x, Speed);
			Vector3 position = myTransform.position;
			float num = (position.x = x);
			Vector3 vector = (myTransform.position = position);
			float y = Mathf.Lerp(myTransform.position.y, target.y, Speed);
			Vector3 position2 = myTransform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (myTransform.position = position2);
			float z = Mathf.Lerp(myTransform.position.z, target.z, Speed);
			Vector3 position3 = myTransform.position;
			float num3 = (position3.z = z);
			Vector3 vector5 = (myTransform.position = position3);
			if (!(Vector3.Distance(myTransform.position, target) >= 1f))
			{
				go = false;
			}
		}
		if (speed != Vector3.zero)
		{
			myTransform.position += speed;
		}
	}

	public virtual void FlyAway()
	{
		float x = myTransform.position.x;
		Vector3 position = Global.CurrentPlayerObject.position;
		float num = (position.x = x);
		Vector3 vector = (Global.CurrentPlayerObject.position = position);
		float y = myTransform.position.y;
		Vector3 position2 = Global.CurrentPlayerObject.position;
		float num2 = (position2.y = y);
		Vector3 vector3 = (Global.CurrentPlayerObject.position = position2);
		int num3 = -1;
		Vector3 position3 = myTransform.position;
		float num4 = (position3.z = num3);
		Vector3 vector5 = (myTransform.position = position3);
		float z = myTransform.position.z - 0.035f;
		Vector3 position4 = Global.CurrentPlayerObject.position;
		float num5 = (position4.z = z);
		Vector3 vector7 = (Global.CurrentPlayerObject.position = position4);
		Global.CurrentPlayerObject.parent = myTransform;
		Global.CurrentPlayerObject.SendMessage("SpecialAnimation", "sit", SendMessageOptions.DontRequireReceiver);
		speed.x = 0.15f;
		speed.y = 0.075f;
		int num6 = -5;
		Vector3 position5 = myTransform.position;
		float num7 = (position5.z = num6);
		Vector3 vector9 = (myTransform.position = position5);
		gameObject.BroadcastMessage("AnimateObject", "dark_bird", SendMessageOptions.DontRequireReceiver);
		if (!ShowResult)
		{
			Global.LoadLEVEL(ToLevel, Global.LEVEL);
		}
	}

	public virtual void ActON()
	{
		if (BuyTimer <= 0)
		{
			BuyTimer = 50;
			Global.YesNoObject = gameObject;
			Global.YesMessage = "Fly Away?";
			Global.CreateMenuWindowObj(MenuObject);
		}
	}

	public virtual IEnumerator RealActON()
	{
		if ((bool)Global.QuestLevelObject)
		{
			Global.QuestLevelObject.tag = "Ignore";
		}
		if ((bool)GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().Play();
		}
		FlyAway();
		go = false;
		yield return new WaitForSeconds(1f);
		if (ShowResult)
		{
			Camera.main.SendMessage("FadeOn", null, SendMessageOptions.DontRequireReceiver);
			Global.IsWin = true;
		}
	}

	public virtual void SendTaxi()
	{
		target.x = myTransform.parent.transform.position.x;
		target.y = myTransform.parent.transform.position.y;
		target.z = -0.75f;
		if ((bool)SFX)
		{
			AudioSource.PlayClipAtPoint(SFX, transform.position);
		}
		go = true;
	}

	public virtual void Main()
	{
	}
}
