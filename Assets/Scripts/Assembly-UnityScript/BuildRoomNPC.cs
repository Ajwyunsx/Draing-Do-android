using System;
using UnityEngine;

[Serializable]
public class BuildRoomNPC : MonoBehaviour
{
	private int BuyTimer;

	private int NPCNumber;

	public GameObject Room;

	public virtual void Awake()
	{
		Global.RoomNPCNumber++;
		NPCNumber = Global.RoomNPCNumber;
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
			Global.RoomNPCNumber = NPCNumber;
			Global.LastRoomNPC = gameObject;
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
			if ((bool)GetComponent<Animation>())
			{
				GetComponent<Animation>().Play();
			}
			BuyTimer = 40;
			Global.CreateMenuWindow("MenuRoom");
		}
	}

	public virtual void CreateRoom()
	{
		UnityEngine.Object.Destroy(Room);
	}

	public virtual void Main()
	{
	}
}
