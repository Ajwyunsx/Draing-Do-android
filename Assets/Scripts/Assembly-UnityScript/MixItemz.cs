using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class MixItemz : MonoBehaviour
{
	public MixOptions[] MixTable;

	public virtual void Start()
	{
	}

	public virtual void CreateNewItem(string name)
	{
		GameObject gameObject = SlotLib.CreateObj(name);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.005f), transform.rotation) as GameObject;
		gameObject2.name = gameObject.name;
		gameObject2.SendMessage("GetThatItem", this.gameObject, SendMessageOptions.DontRequireReceiver);
		SlotItem.EscapeItem = true;
	}

	public virtual void MixItems(GameObject other)
	{
		string text = null;
		string text2 = null;
		string text3 = null;
		if (MixTable == null)
		{
			return;
		}
		for (int i = 0; i < Extensions.get_length((System.Array)MixTable); i++)
		{
			text = other.name + "*" + gameObject.name;
			text2 = gameObject.name + "*" + other.name;
			text3 = MixTable[i].ItemName + "*" + gameObject.name;
			MonoBehaviour.print("MIX: " + text + "  " + text2 + "  " + text3);
			if (!(text3 == text) && !(text3 == text2))
			{
				continue;
			}
			SlotItem.SayNoMix = false;
			string[] array = MixTable[i].MiniScript.Split("/"[0]);
			for (int j = 0; j < Extensions.get_length((System.Array)array); j++)
			{
				switch (array[j])
				{
				case "me":
					UnityEngine.Object.Destroy(gameObject);
					break;
				case "all":
					UnityEngine.Object.Destroy(gameObject);
					UnityEngine.Object.Destroy(other);
					break;
				case "it":
					UnityEngine.Object.Destroy(other);
					break;
				default:
					CreateNewItem(array[j]);
					break;
				}
			}
			break;
		}
	}

	public virtual void Main()
	{
	}
}
