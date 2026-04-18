using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("Alex Code/Delete by Random")]
public class DeleteByRandom : MonoBehaviour
{
	public int chanceToDelete;

	public virtual void Awake()
	{
		if (UnityEngine.Random.Range(1, 100) <= chanceToDelete)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
