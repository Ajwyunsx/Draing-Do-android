using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GENERATORMap : MonoBehaviour
{
	public int MinIsles;

	public int MaxIsles;

	public GameObject OldGlobyCastle;

	public GameObject NewGlobyCastle;

	public GameObject DarkIsle;

	public GameObject[] StaticObjects;

	private GameObject[] CreatedObjects;

	public virtual void Awake()
	{
		int num = (int)Mathf.Floor(Global.MainQuest / 5);
		int num2 = UnityEngine.Random.Range(MinIsles, MaxIsles);
		CreatedObjects = new GameObject[36];
		int num3 = default(int);
		float num4 = default(float);
		float num5 = default(float);
		CreatedObjects[0] = UnityEngine.Object.Instantiate(NewGlobyCastle) as GameObject;
		CreatedObjects[0].transform.position = new Vector3(0f, 0f, 0f);
		CreatedObjects[25] = UnityEngine.Object.Instantiate(OldGlobyCastle) as GameObject;
		CreatedObjects[25].transform.position = new Vector3(UnityEngine.Random.Range(-15, -10), UnityEngine.Random.Range(3, 7), 0f);
		for (int i = 1; i <= num2; i++)
		{
			num4 = UnityEngine.Random.Range(-19.9f, 19.9f);
			num5 = UnityEngine.Random.Range(-9.9f, 9.9f);
			bool flag = true;
			int j = 0;
			GameObject[] createdObjects = CreatedObjects;
			for (int length = createdObjects.Length; j < length; j++)
			{
				if ((bool)createdObjects[j] && !(Vector3.Distance(new Vector3(num4, num5, 0f), createdObjects[j].transform.position) >= 2f))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				num3++;
				if (num < i)
				{
					CreatedObjects[i] = UnityEngine.Object.Instantiate(DarkIsle) as GameObject;
					CreatedObjects[i].transform.position = new Vector3(num4, num5, 0f);
				}
				else
				{
					CreatedObjects[i] = UnityEngine.Object.Instantiate(StaticObjects[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)StaticObjects))]) as GameObject;
					CreatedObjects[i].transform.position = new Vector3(num4, num5, 0f);
				}
				if (num3 >= 24)
				{
					break;
				}
			}
		}
		MonoBehaviour.print("count: " + num3);
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
