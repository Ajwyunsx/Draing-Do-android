using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("Alex Code/Save Get Item")]
public class SaveScript : MonoBehaviour
{
	public string NAME;

	public bool SaveOnStart;

	public bool LevelNameCorrect;

	public bool IsLabirintBlockWall;

	public Vector2 ToPosition;

	public bool Invert;

	public SaveScript()
	{
		NAME = string.Empty;
	}

	public virtual void NOSave()
	{
		enabled = false;
	}

	public virtual void Start()
	{
		if (!Global.WorldON)
		{
			if (NAME == string.Empty && NAME == null)
			{
				return;
			}
			if (LevelNameCorrect)
			{
				NAME += Application.loadedLevelName;
			}
		}
		else if (!IsLabirintBlockWall)
		{
			NAME = "*" + transform.position.x + "," + transform.position.y;
			MonoBehaviour.print("GENERATOR NAME: " + NAME);
		}
		else
		{
			if (CheckNeigbour())
			{
				return;
			}
			GenerateBlockWallName();
		}
		if (Global.CheckStuff(NAME))
		{
			if (!Invert)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		else if (Invert)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		if (SaveOnStart)
		{
			DISAPPEAR();
		}
	}

	public virtual void DISAPPEAR()
	{
		if (!(NAME == string.Empty))
		{
			Global.AddStuff(NAME);
		}
	}

	public virtual void TryToSave()
	{
		if (!(NAME == string.Empty))
		{
			Global.AddStuff(NAME);
		}
	}

	public virtual void OnAppear(string name)
	{
		if (!(name == string.Empty) && !(name == null))
		{
			NAME = name;
		}
	}

	public virtual bool CheckNeigbour()
	{
		Vector2 worldPosition = Global.WorldPosition;
		Vector2 vector = worldPosition + ToPosition;
		int result;
		if (worldPosition.x < 0f || worldPosition.x >= Global.WorldMax.x || worldPosition.y < 0f || !(worldPosition.y < Global.WorldMax.y))
		{
			result = 0;
		}
		else if (Global.WorldLevel[(int)vector.x, (int)vector.y] != null && Global.WorldMap[(int)vector.x, (int)vector.y] != "secret")
		{
			UnityEngine.Object.Destroy(gameObject);
			result = 1;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual void GenerateBlockWallName()
	{
		Vector2 worldPosition = Global.WorldPosition;
		Vector2 vector = worldPosition + ToPosition;
		string rhs = string.Empty + worldPosition.x;
		string rhs2 = string.Empty + worldPosition.y;
		if (ToPosition.x != 0f)
		{
			rhs = ((ToPosition.x <= 0f) ? (worldPosition.x + "," + vector.x) : (worldPosition.x + "," + vector.x));
		}
		if (ToPosition.y != 0f)
		{
			rhs2 = ((ToPosition.y <= 0f) ? (worldPosition.y + "," + vector.y) : (worldPosition.y + "," + vector.y));
		}
		NAME = "*" + rhs + "*" + rhs2;
		MonoBehaviour.print("test name generator: " + NAME);
	}

	public virtual void Main()
	{
	}
}
