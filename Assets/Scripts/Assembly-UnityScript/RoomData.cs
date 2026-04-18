using System;
using UnityEngine;

[Serializable]
public class RoomData : MonoBehaviour
{
	public Roomz[] roomz;

	[NonSerialized]
	public static Roomz[] rooms;

	public virtual void Awake()
	{
		rooms = roomz;
	}

	public virtual void Main()
	{
	}
}
