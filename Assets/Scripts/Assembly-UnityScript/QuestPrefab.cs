using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class QuestPrefab : MonoBehaviour
{
	public string QuestMessage;

	private GameObject[] list;

	public GameObject[] QuestObject;

	private bool IsWin;

	public bool IsMainQuest;

	public bool DontGenerateEnemies;

	public bool CheckCount;

	public bool DontCheckWin;

	private int Count;

	private int oldCount;

	public bool DontDirectionSkyTaxi;

	public virtual void Awake()
	{
		Global.IsWin = false;
		Global.DontGenerateEnemies = DontGenerateEnemies;
		Global.IsMainQuest = IsMainQuest;
		Global.TextQuest = Lang.CurrentMessage(QuestMessage);
		if (Extensions.get_length((System.Array)QuestObject) == 0)
		{
			return;
		}
		int num = default(int);
		int num2 = (int)(5f + (float)Global.RANG * 0.1f + (float)UnityEngine.Random.Range(-3, 3));
		int num3 = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)QuestObject));
		list = new GameObject[num2];
		for (int i = 0; i < num2; i++)
		{
			num = UnityEngine.Random.Range(0, Global.EasyCells.length);
			if (!QuestObject[num3])
			{
				continue;
			}
			GameObject gameObject = Global.EasyCells[num] as GameObject;
			if ((bool)gameObject)
			{
				list[i] = UnityEngine.Object.Instantiate(QuestObject[num3]) as GameObject;
				float x = gameObject.transform.position.x;
				Vector3 position = list[i].transform.position;
				float num4 = (position.x = x);
				Vector3 vector = (list[i].transform.position = position);
				float y = gameObject.GetComponent<Renderer>().bounds.center.y - gameObject.GetComponent<Renderer>().bounds.extents.y;
				Vector3 position2 = list[i].transform.position;
				float num5 = (position2.y = y);
				Vector3 vector3 = (list[i].transform.position = position2);
				float z = -0.8f;
				Vector3 position3 = list[i].transform.position;
				float num6 = (position3.z = z);
				Vector3 vector5 = (list[i].transform.position = position3);
				object obj = Global.EasyCells[num];
				if (!(obj is UnityEngine.Object))
				{
					obj = RuntimeServices.Coerce(obj, typeof(UnityEngine.Object));
				}
				UnityEngine.Object.DestroyImmediate((UnityEngine.Object)obj);
				Global.EasyCells.RemoveAt(num);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (CheckCount && oldCount != Count)
		{
			oldCount = Count;
			Global.TextQuest = Lang.CurrentMessage(QuestMessage) + "/" + Lang.CurrentMessage("count") + " " + Count;
		}
		if (DontCheckWin || IsWin)
		{
			return;
		}
		int num = 0;
		if (list != null)
		{
			int i = 0;
			GameObject[] array = list;
			for (int length = array.Length; i < length; i++)
			{
				if (array[i] != null)
				{
					num++;
				}
			}
		}
		if (num == 0)
		{
			if (!DontDirectionSkyTaxi)
			{
				Global.SkyTaxiCall.tag = "forDirect";
				Global.SkyTaxiCallFunction();
				Global.TextQuest = Lang.CurrentMessage("Return to castle!");
			}
			IsWin = true;
		}
	}

	public virtual void CheckDistance()
	{
		if (IsWin)
		{
			return;
		}
		if (list != null)
		{
			int num = default(int);
			float num2 = 10000000f;
			int num3 = default(int);
			Count = 0;
			int i = 0;
			GameObject[] array = list;
			for (int length = array.Length; i < length; i++)
			{
				if (array[i] != null)
				{
					float num4 = Vector3.Distance(array[i].transform.position, Global.CurrentPlayerObject.transform.position);
					if (!(num4 >= num2))
					{
						num2 = num4;
						num3 = num;
					}
					Count++;
				}
				num++;
			}
			if ((bool)list[num3])
			{
				Global.QuestLevelObject = list[num3].transform;
			}
			else
			{
				Global.QuestLevelObject = null;
			}
		}
		else
		{
			Global.QuestLevelObject = null;
		}
	}

	public virtual void Main()
	{
		InvokeRepeating("CheckDistance", 1f, 1f);
	}
}
