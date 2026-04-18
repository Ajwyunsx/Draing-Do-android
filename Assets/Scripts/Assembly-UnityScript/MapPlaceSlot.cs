using System;
using UnityEngine;

[Serializable]
public class MapPlaceSlot : MonoBehaviour
{
	private string Name;

	private GameObject GO;

	public virtual void Awake()
	{
	}

	public virtual void SlotText(string text)
	{
		Name = text;
	}

	public virtual void SlotPrefab(GameObject go)
	{
		GO = go;
	}

	public virtual void GetText()
	{
		Global.TextQuest = Name;
	}

	public virtual void PlayLevel()
	{
		Global.TextQuest = Name;
		MonoBehaviour.print("LEVEL: " + gameObject.name);
		Global.LoadLEVEL(gameObject.name, string.Empty);
		Global.Pause = false;
		Global.MenuPause = false;
	}

	public virtual void Main()
	{
	}
}
