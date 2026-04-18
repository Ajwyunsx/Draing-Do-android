using System;
using UnityEngine;

[Serializable]
public class ExitToLevel : MonoBehaviour
{
	public string ID;

	public string LevelToLOAD;

	public GameObject child;

	public bool UP;

	public bool DOWN;

	public bool ToStartWorld;

	public bool InWorld;

	private string Direction;

	public virtual void Awake()
	{
		GetComponent<Renderer>().enabled = false;
		if (!InWorld && (bool)child)
		{
			child.SendMessage("SetID", ID, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Start()
	{
		if (!InWorld)
		{
			return;
		}
		Global.WorldVisible[(int)Global.WorldPosition.x, (int)Global.WorldPosition.y] = true;
		if (!(transform.position.x > LevelCore.Border.x))
		{
			Direction = "l";
		}
		if (!(transform.position.x < LevelCore.Border.z))
		{
			Direction = "r";
		}
		if (!(transform.position.y < LevelCore.Border.w))
		{
			Direction = "u";
		}
		if (!(transform.position.y > LevelCore.Border.y))
		{
			Direction = "d";
		}
		string value = null;
		ID = Direction;
		if ((bool)child)
		{
			switch (Direction)
			{
			case "l":
				value = "r";
				break;
			case "r":
				value = "l";
				break;
			case "u":
				value = "d";
				break;
			case "d":
				value = "u";
				break;
			}
			child.SendMessage("SetID", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(Global.HP > 0f) || !(other.gameObject.tag == "Player") || !Global.CanExitFromLevel)
		{
			return;
		}
		Global.CheckPointNameTemp = ID;
		Global.DemoMove = (int)(0f - Mathf.Sign(other.transform.position.x - transform.position.x));
		if (UP || Direction == "u")
		{
			other.gameObject.SendMessage("GravityOff", null, SendMessageOptions.DontRequireReceiver);
		}
		if (DOWN || Direction == "d")
		{
			other.gameObject.SendMessage("GravityDOWN", null, SendMessageOptions.DontRequireReceiver);
		}
		if (InWorld)
		{
			ID = Direction;
			switch (Direction)
			{
			case "l":
				Global.WorldPosition.x -= 1f;
				break;
			case "r":
				Global.WorldPosition.x += 1f;
				break;
			case "u":
				Global.WorldPosition.y += 1f;
				break;
			case "d":
				Global.WorldPosition.y -= 1f;
				break;
			}
			LevelToLOAD = Global.WorldLevel[(int)Global.WorldPosition.x, (int)Global.WorldPosition.y];
			if (Global.WorldPosition.x < 0f || Global.WorldPosition.y < 0f || Global.WorldPosition.x >= Global.WorldMax.x || !(Global.WorldPosition.y < Global.WorldMax.y))
			{
				MonoBehaviour.print("OFF AREA MAP");
				LevelToLOAD = Global.WorldLevelToReturn;
			}
			if (LevelToLOAD == null || LevelToLOAD == string.Empty)
			{
				MonoBehaviour.print("NO LOCATION MAP: " + LevelToLOAD + " " + Global.WorldPosition);
				LevelToLOAD = Global.WorldLevelToReturn;
			}
			MonoBehaviour.print("LevelToLOAD World: " + LevelToLOAD);
		}
		Global.LoadLEVEL(LevelToLOAD, ID);
		SlotItem.EscapeItem = true;
		GameObject[] array = null;
		array = GameObject.FindGameObjectsWithTag("Barrier");
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			UnityEngine.Object.Destroy(array2[i]);
		}
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Main()
	{
	}
}
