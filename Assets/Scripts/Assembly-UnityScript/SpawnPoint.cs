using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("MY SCRIPTS/Interactive/Spawn Point")]
public class SpawnPoint : MonoBehaviour
{
	public string ID;

	public bool DirectionToRight;

	public bool ToLastDirection;

	public bool noRENDER;

	public bool DeleteAfterLoad;

	public bool ForceNameOnAwake;

	public SpawnPoint()
	{
		ToLastDirection = true;
		noRENDER = true;
	}

	public virtual void Awake()
	{
		if (ToLastDirection)
		{
			if (Global.LastDirection == 1)
			{
				DirectionToRight = true;
			}
			if (Global.LastDirection == -1)
			{
				DirectionToRight = false;
			}
		}
		if (ID == null || ID == string.Empty)
		{
			ID = "point" + Mathf.RoundToInt(transform.position.x) + "+" + Mathf.RoundToInt(transform.position.y);
		}
		if (noRENDER)
		{
			GetComponent<Renderer>().enabled = false;
		}
		if (ForceNameOnAwake)
		{
			gameObject.name = ID;
		}
	}

	public virtual void SetID(string name)
	{
		ID = name;
		gameObject.name = ID;
	}

	public virtual void Main()
	{
	}
}
