using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class CutsceneAnimCommand : MonoBehaviour
{
	public GameObject[] ObjectList;

	public virtual void AnimPlay(string name)
	{
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			ObjectList[i].GetComponent<Animation>().Play(name);
		}
	}

	public virtual void Main()
	{
	}
}
