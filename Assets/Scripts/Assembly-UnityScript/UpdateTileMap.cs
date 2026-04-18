using System;
using UnityEngine;

[Serializable]
public class UpdateTileMap : MonoBehaviour
{
	public virtual void Awake()
	{
		GetComponent<tk2dTileMap>().ForceBuild();
	}

	public virtual void Main()
	{
	}
}
