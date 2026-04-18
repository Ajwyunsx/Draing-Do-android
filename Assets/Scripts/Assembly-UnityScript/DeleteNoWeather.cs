using System;
using UnityEngine;

[Serializable]
public class DeleteNoWeather : MonoBehaviour
{
	public virtual void Start()
	{
		if (!Global.ShowWEATHER)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
