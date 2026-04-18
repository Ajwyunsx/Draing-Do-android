using System;
using UnityEngine;

[Serializable]
public class FairyFonar : MonoBehaviour
{
	public virtual void Awake()
	{
		if (!Global.FonarSkill)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
