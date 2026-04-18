using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class MenuRoomGenerator : MonoBehaviour
{
	public GameObject button;

	public virtual void Awake()
	{
		int num = default(int);
		for (int i = 0; i < Extensions.get_length((System.Array)RoomData.rooms); i++)
		{
			if (RoomData.rooms[i].on == 1)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(button, new Vector3(transform.position.x, transform.position.y + 0.17f - (float)i * 0.06f, transform.position.z - 0.01f), Quaternion.identity);
				Global.LastCreatedObject.BroadcastMessage("Rename", RoomData.rooms[i].name + " " + RoomData.rooms[i].price + " $", SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.SendMessage("SetGameObject", RoomData.rooms[i].GO, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.SendMessage("SetPrice", RoomData.rooms[i].price, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.SendMessage("SetRealRoomNumber", i, SendMessageOptions.DontRequireReceiver);
				num++;
				Global.LastCreatedObject.SendMessage("ChangeXNumber", num + 1, SendMessageOptions.DontRequireReceiver);
				Global.LastCreatedObject.transform.parent = transform;
			}
		}
		gameObject.SendMessage("ChangeMaxSlotX", num + 1, SendMessageOptions.DontRequireReceiver);
	}

	public virtual void Main()
	{
	}
}
