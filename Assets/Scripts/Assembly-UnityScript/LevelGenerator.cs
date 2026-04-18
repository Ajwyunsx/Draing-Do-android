using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[AddComponentMenu("Alex Code/Level Generator")]
public class LevelGenerator : MonoBehaviour
{
	public string WorldName;

	private string[,] WorldMap;

	private string[,] WorldLevel;

	private int[,] Checker;

	private Vector2[] Locals;

	[NonSerialized]
	public static bool[,] Paths;

	private GameObject LastCreatedObject;

	[NonSerialized]
	public static Vector3 Place;

	[NonSerialized]
	public static Vector2 StartPlace;

	public int MaxX;

	public int MaxY;

	public Vector2 BasePlace;

	public string[] LibStart;

	public string[] LibPath;

	public string[] LibSecret;

	public string[] LibBoss;

	public LevelGenerator()
	{
		WorldName = "jungle";
		MaxX = 6;
		MaxY = 6;
		BasePlace = new Vector2(3f, 4f);
	}

	public virtual void Start()
	{
		if (!(Global.WorldName != WorldName))
		{
			return;
		}
		Global.RemoveGeneratingStuff();
		Place = transform.position;
		WorldMap = (string[,])System.Array.CreateInstance(typeof(string), new int[2]
		{
			MaxX + 1,
			MaxY + 1
		});
		WorldLevel = (string[,])System.Array.CreateInstance(typeof(string), new int[2]
		{
			MaxX + 1,
			MaxY + 1
		});
		Checker = (int[,])System.Array.CreateInstance(typeof(int), new int[2]
		{
			MaxX + 1,
			MaxY + 1
		});
		Paths = (bool[,])System.Array.CreateInstance(typeof(bool), new int[2]
		{
			(MaxX + 1) * 2,
			(MaxY + 1) * 2
		});
		UnityScript.Lang.Array array = new UnityScript.Lang.Array();
		int num = default(int);
		int num2 = (int)UnityEngine.Random.Range(BasePlace.x, BasePlace.y);
		Locals = new Vector2[num2];
		for (num = 0; num < num2; num++)
		{
			Vector2 randomFreeLocation = GetRandomFreeLocation();
			if (num == 0)
			{
				Place.x = randomFreeLocation.x;
				Place.y = randomFreeLocation.y;
			}
			WorldMap[(int)randomFreeLocation.x, (int)randomFreeLocation.y] = "path";
			Locals[num] = randomFreeLocation;
			array.Add(new Vector2(randomFreeLocation.x, randomFreeLocation.y));
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					CreatePath(Locals[num], Locals[i]);
				}
			}
		}
		for (int j = 0; j < MaxY; j++)
		{
			for (int k = 0; k < MaxX; k++)
			{
				Checker[k, j] = GetNeigbors(k, j);
			}
		}
		int index = UnityEngine.Random.Range(0, array.length);
		StartPlace = (Vector2)array[index];
		WorldMap[(int)StartPlace.x, (int)StartPlace.y] = "start";
		array = new UnityScript.Lang.Array();
		for (int j = 0; j < MaxY; j++)
		{
			for (int k = 0; k < MaxX; k++)
			{
				if (Checker[k, j] > 0)
				{
					array.Add(new Vector2(k, j));
				}
			}
		}
		index = UnityEngine.Random.Range(0, array.length);
		Vector2 vector = (Vector2)array[index];
		WorldMap[(int)vector.x, (int)vector.y] = "boss";
		array.RemoveAt(index);
		for (int j = 0; j < UnityEngine.Random.Range(2, 3); j++)
		{
			if (array.length > 0)
			{
				index = UnityEngine.Random.Range(0, array.length);
				vector = (Vector2)array[index];
				WorldMap[(int)vector.x, (int)vector.y] = "secret";
				array.RemoveAt(index);
			}
		}
		for (int j = 0; j < MaxY; j++)
		{
			for (int k = 0; k < MaxX; k++)
			{
				int num3 = default(int);
				string text = WorldMap[k, j];
				if (!(text == null))
				{
					switch (text)
					{
					case "start":
						num3 = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)LibStart));
						WorldLevel[k, j] = LibStart[num3];
						break;
					case "path":
						num3 = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)LibPath));
						WorldLevel[k, j] = LibPath[num3];
						break;
					case "secret":
						num3 = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)LibSecret));
						WorldLevel[k, j] = LibSecret[num3];
						break;
					case "boss":
						num3 = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)LibBoss));
						WorldLevel[k, j] = LibBoss[num3];
						break;
					}
				}
			}
		}
		Global.WorldMap = WorldMap;
		Global.WorldLevel = WorldLevel;
		Global.MapIndex = (string[,])System.Array.CreateInstance(typeof(string), new int[2]
		{
			MaxX + 1,
			MaxY + 1
		});
		Global.WorldVisible = (bool[,])System.Array.CreateInstance(typeof(bool), new int[2]
		{
			MaxX + 1,
			MaxY + 1
		});
		Global.WorldName = WorldName;
		Global.WorldStart = StartPlace;
		Global.WorldPosition = StartPlace;
		Global.WorldLevelToReturn = Application.loadedLevelName;
		Global.WorldMax = new Vector2(MaxX, MaxY);
	}

	public virtual int GetNeigbors(int x, int y)
	{
		int num = 0;
		int result;
		if (!string.IsNullOrEmpty(WorldMap[x, y]))
		{
			result = 0;
		}
		else
		{
			if (x > 0 && !string.IsNullOrEmpty(WorldMap[x - 1, y]))
			{
				num++;
			}
			if (x <= MaxX && !string.IsNullOrEmpty(WorldMap[x + 1, y]))
			{
				num++;
			}
			if (y > 0 && !string.IsNullOrEmpty(WorldMap[x, y - 1]))
			{
				num++;
			}
			if (y <= MaxY && !string.IsNullOrEmpty(WorldMap[x, y + 1]))
			{
				num++;
			}
			result = num;
		}
		return result;
	}

	public virtual Vector2 GetRandomFreeLocation()
	{
		Vector2 vector = default(Vector2);
		int num = 0;
		Vector2 result;
		while (true)
		{
			vector = new Vector2(UnityEngine.Random.Range(1, MaxX - 1), UnityEngine.Random.Range(1, MaxY - 1));
			if (!(WorldMap[(int)vector.x, (int)vector.y] == null))
			{
				num++;
				MonoBehaviour.print("oops! + " + num);
				if (num > 1000)
				{
					MonoBehaviour.print("FUUUUCK off oops!");
					result = vector;
					break;
				}
				continue;
			}
			result = vector;
			break;
		}
		return result;
	}

	public virtual void CreatePath(Vector2 ObjectA, Vector2 ObjectB)
	{
		int num = default(int);
		Vector2 vector = ObjectA;
		Vector2 vector2 = ObjectB;
		num = (int)(Mathf.Abs(vector.x - vector2.x) + Mathf.Abs(vector.y - vector2.y));
		Vector2 vector3 = new Vector2(vector.x, vector.y);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector4 = vector3;
			Vector2 vector5 = new Vector2(0f, 0f);
			if (!(vector3.x <= vector2.x))
			{
				vector5.x -= 1f;
			}
			if (!(vector3.x >= vector2.x))
			{
				vector5.x += 1f;
			}
			if (!(vector3.y <= vector2.y))
			{
				vector5.y -= 1f;
			}
			if (!(vector3.y >= vector2.y))
			{
				vector5.y += 1f;
			}
			if (vector5.x != 0f && vector5.y != 0f)
			{
				if (UnityEngine.Random.Range(0, 100) > 50)
				{
					vector3.x += vector5.x;
				}
				else
				{
					vector3.y += vector5.y;
				}
			}
			else
			{
				vector3 += vector5;
			}
			if (WorldMap[(int)vector3.x, (int)vector3.y] == null)
			{
				WorldMap[(int)vector3.x, (int)vector3.y] = "path";
			}
		}
	}

	public virtual void CreateRoad(Vector2 pos, Vector2 lastPos)
	{
		float z = Mathf.Atan2(pos.y - lastPos.y, pos.x - lastPos.x) * 57.29578f + 90f;
		Vector3 eulerAngles = LastCreatedObject.transform.eulerAngles;
		float num = (eulerAngles.z = z);
		Vector3 vector = (LastCreatedObject.transform.eulerAngles = eulerAngles);
	}

	public virtual void Main()
	{
	}
}
