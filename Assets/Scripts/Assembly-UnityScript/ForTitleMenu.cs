using System;
using UnityEngine;

[Serializable]
public class ForTitleMenu : MonoBehaviour
{
	public virtual void Awake()
	{
		Time.timeScale = 1f;
		if (PlayerPrefs.HasKey("Last Player"))
		{
			Global.PLAYER = PlayerPrefs.GetInt("Last Player");
		}
		if (!PlayerPrefs.HasKey("Player" + Global.PLAYER))
		{
			Global.PLAYER = 1;
			Global.LOAD();
		}
		else
		{
			Global.LOAD();
		}
		Global.MenuPause = true;
	}

	public virtual void RemoveAllOjects()
	{
		GameObject gameObject = this.gameObject;
		GameObject[] array = UnityEngine.Object.FindSceneObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (array2[i] != Global.GlobalObject)
			{
				UnityEngine.Object.Destroy(array2[i]);
			}
		}
	}

	public virtual void Main()
	{
	}
}
