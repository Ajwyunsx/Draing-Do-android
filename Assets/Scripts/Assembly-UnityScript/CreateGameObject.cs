using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class CreateGameObject : MonoBehaviour
{
	public GameObject Game_Object;

	public virtual IEnumerator Start()
	{
		yield return 1;
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(Game_Object, transform.position, Quaternion.identity);
	}

	public virtual void Main()
	{
	}
}
