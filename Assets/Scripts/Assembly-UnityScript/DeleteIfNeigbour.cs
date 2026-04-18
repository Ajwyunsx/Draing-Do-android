using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("Alex Code/Delete If Neighbour Room")]
public class DeleteIfNeigbour : MonoBehaviour
{
	public bool DeleteThen;

	public Vector2 ToPosition;

	public DeleteIfNeigbour()
	{
		DeleteThen = true;
		ToPosition = new Vector2(0f, 1f);
	}

	public virtual void Start()
	{
		if (CheckNeigbour())
		{
			if (DeleteThen)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		else if (!DeleteThen)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual bool CheckNeigbour()
	{
		Vector2 worldPosition = Global.WorldPosition;
		Vector2 vector = worldPosition + ToPosition;
		return !(worldPosition.x < 0f) && !(worldPosition.x >= Global.WorldMax.x) && !(worldPosition.y < 0f) && worldPosition.y < Global.WorldMax.y && ((Global.WorldLevel[(int)vector.x, (int)vector.y] != null) ? true : false);
	}

	public virtual void Main()
	{
	}
}
