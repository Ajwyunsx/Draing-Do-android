using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GENERATORLevel : MonoBehaviour
{
	public GameObject PrefabQuest;

	public Transform HeroObject;

	private int[,] miniSectors;

	public GameObject JumperGO;

	public GameObject EdgeLand;

	public Lib[] TileLib;

	public Lib[] GreenLib;

	public Lib[] TreasureLib;

	public GameObject HPGenerator;

	public GameObject ManaGenerator;

	public Lib[] Enemy;

	public Lib[] Objects;

	public Lib[] NPCLib;

	public Lib[] BigLandLib;

	public Lib[] BigSkyLib;

	private string[,] ArraySectors;

	private string[,] easySectors;

	public GENERATORLevel()
	{
		miniSectors = (int[,])System.Array.CreateInstance(typeof(int), new int[2] { 5, 5 });
	}

	public virtual void Start()
	{
		UnityEngine.Random.seed = (int)Time.time;
		if (Global.CanSeed)
		{
			Global.LevelSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		}
		Global.CanSeed = true;
		UnityEngine.Random.seed = Global.LevelSeed;
		int num = default(int);
		Global.GenerateBounds = true;
		Global.LEVELMASS = new Vector2(UnityEngine.Random.Range(4, 6), UnityEngine.Random.Range(2, 4));
		Global.SECTORS = (GameObject[,])System.Array.CreateInstance(typeof(GameObject), new int[2]
		{
			(int)Global.LEVELMASS.x,
			(int)Global.LEVELMASS.y
		});
		ArraySectors = (string[,])System.Array.CreateInstance(typeof(string), new int[2]
		{
			(int)Global.LEVELMASS.x,
			(int)Global.LEVELMASS.y
		});
		easySectors = (string[,])System.Array.CreateInstance(typeof(string), new int[2]
		{
			(int)Global.LEVELMASS.x,
			(int)Global.LEVELMASS.y
		});
		for (int i = 1; (float)i < Global.LEVELMASS.x - 1f; i++)
		{
			Global.SECTORS[i, 0] = UnityEngine.Object.Instantiate(TileLib[0].@object, new Vector3(i * 10 + 5, 5f, -1.2f + UnityEngine.Random.Range(-0.015f, 0.015f)), Quaternion.identity) as GameObject;
		}
		Global.SECTORS[0, 0] = UnityEngine.Object.Instantiate(EdgeLand, new Vector3(5f, 5f, -1.2f + UnityEngine.Random.Range(-0.015f, 0.015f)), Quaternion.identity);
		Global.SECTORS[(int)(Global.LEVELMASS.x - 1f), 0] = UnityEngine.Object.Instantiate(EdgeLand, new Vector3((Global.LEVELMASS.x - 1f) * 10f + 5f, 5f, -1.2f + UnityEngine.Random.Range(-0.015f, 0.015f)), Quaternion.identity);
		int num2 = -1;
		Vector3 localScale = Global.SECTORS[(int)(Global.LEVELMASS.x - 1f), 0].transform.localScale;
		float num3 = (localScale.x = num2);
		Vector3 vector = (Global.SECTORS[(int)(Global.LEVELMASS.x - 1f), 0].transform.localScale = localScale);
		for (int j = 1; (float)j < Global.LEVELMASS.y; j++)
		{
			for (int i = 0; (float)i < Global.LEVELMASS.x; i++)
			{
				num = UnityEngine.Random.Range(1, Extensions.get_length((System.Array)TileLib));
				if ((bool)TileLib[num].@object && UnityEngine.Random.Range(1, 100) <= TileLib[num].Chance)
				{
					ArraySectors[i, j] = TileLib[num].Option;
					Global.SECTORS[i, j] = UnityEngine.Object.Instantiate(TileLib[num].@object, new Vector3(i * 10 + 5, j * 10 + 5, -1.2f + UnityEngine.Random.Range(-0.015f, 0.015f)), Quaternion.identity) as GameObject;
					Global.SECTORS[i, j].name = "a X+Y: " + i + "   " + j;
					if (UnityEngine.Random.Range(1, 100) < 50)
					{
						float x = Global.SECTORS[i, j].transform.localScale.x * -1f;
						Vector3 localScale2 = Global.SECTORS[i, j].transform.localScale;
						float num4 = (localScale2.x = x);
						Vector3 vector3 = (Global.SECTORS[i, j].transform.localScale = localScale2);
					}
				}
			}
		}
		GameObject[] array = null;
		GameObject gameObject = null;
		array = GameObject.FindGameObjectsWithTag("Cell");
		UnityScript.Lang.Array array2 = new UnityScript.Lang.Array();
		int k = 0;
		GameObject[] array3 = array;
		for (int length = array3.Length; k < length; k++)
		{
			array2.Add(new Vector2(array3[k].transform.position.x, array3[k].transform.position.y));
		}
		int l = 0;
		GameObject[] array4 = array;
		for (int length2 = array4.Length; l < length2; l++)
		{
			bool flag = false;
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(array2);
			while (enumerator.MoveNext())
			{
				Vector2 vector5 = (Vector2)enumerator.Current;
				if (vector5 != new Vector2(array4[l].transform.position.x, array4[l].transform.position.y) && !(array4[l].transform.position.y >= vector5.y) && !(Vector2.Distance(vector5, new Vector2(array4[l].transform.position.x, array4[l].transform.position.y)) >= 2f))
				{
					MonoBehaviour.print("DESTROY");
					UnityEngine.Object.DestroyImmediate(array4[l]);
					flag = true;
					break;
				}
			}
		}
		int num5 = default(int);
		for (int num6 = (int)(Global.LEVELMASS.y - 1f); num6 >= 0; num6--)
		{
			string lhs = string.Empty;
			if (num6 != 0)
			{
				for (num5 = 0; (float)num5 < Global.LEVELMASS.x; num5++)
				{
					if (ArraySectors[num5, num6] != null)
					{
						lhs += ArraySectors[num5, num6];
						continue;
					}
					ArraySectors[num5, num6] = "*";
					lhs += "*";
				}
			}
			else
			{
				for (num5 = 1; (float)num5 < Global.LEVELMASS.x - 1f; num5++)
				{
					ArraySectors[num5, num6] = "-";
				}
			}
		}
		for (int num6 = 0; (float)num6 < Global.LEVELMASS.y; num6++)
		{
			for (num5 = 0; (float)num5 < Global.LEVELMASS.x; num5++)
			{
				if ((float)num6 >= Global.LEVELMASS.y - 1f || (!(ArraySectors[num5, num6 + 1] == "*") && !(ArraySectors[num5, num6] == "s")) || !(ArraySectors[num5, num6] != "-") || !Global.SECTORS[num5, num6])
				{
					continue;
				}
				UnityScript.Lang.Array array5 = new UnityScript.Lang.Array();
				IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(Global.SECTORS[num5, num6].transform);
				while (enumerator2.MoveNext())
				{
					object obj = enumerator2.Current;
					if (!(obj is Transform))
					{
						obj = RuntimeServices.Coerce(obj, typeof(Transform));
					}
					Transform transform = (Transform)obj;
					if (transform.tag == "Cell")
					{
						array5.Add(transform);
						UnityRuntimeServices.Update(enumerator2, transform);
					}
				}
				int num7 = 0;
				int num8 = 0;
				int index = 0;
				IEnumerator enumerator3 = UnityRuntimeServices.GetEnumerator(array5);
				while (enumerator3.MoveNext())
				{
					object obj2 = enumerator3.Current;
					if (!(obj2 is Transform))
					{
						obj2 = RuntimeServices.Coerce(obj2, typeof(Transform));
					}
					Transform transform2 = (Transform)obj2;
					if (!(transform2.position.y <= (float)num7))
					{
						num7 = (int)transform2.position.y;
						UnityRuntimeServices.Update(enumerator3, transform2);
						index = num8;
					}
					num8++;
				}
				Transform transform3 = array5[index] as Transform;
				if ((bool)JumperGO)
				{
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(JumperGO) as GameObject;
					Global.LastCreatedObject.transform.position = transform3.position + new Vector3(0f, 0f, -0.3f + UnityEngine.Random.Range(-0.01f, 0.01f));
					Global.LastCreatedObject.name = "XX+YY: " + num5 + "   " + num6;
					UnityEngine.Object.DestroyImmediate(transform3.gameObject);
				}
			}
		}
		for (int num6 = 1; (float)num6 < Global.LEVELMASS.y; num6++)
		{
			for (num5 = 0; (float)num5 < Global.LEVELMASS.x; num5++)
			{
				easySectors[num5, num6] = "h";
			}
		}
		for (num5 = 0; (float)num5 < Global.LEVELMASS.x; num5++)
		{
			easySectors[num5, 0] = "e";
		}
		easySectors[0, 1] = "e";
		easySectors[1, 1] = "e";
		easySectors[(int)(Global.LEVELMASS.x - 1f), 1] = "e";
		easySectors[(int)(Global.LEVELMASS.x - 2f), 1] = "e";
		for (int num6 = 1; (float)num6 < Global.LEVELMASS.y; num6++)
		{
			for (num5 = 0; (float)num5 < Global.LEVELMASS.x && ArraySectors[num5, num6] != "*"; num5++)
			{
				if (ArraySectors[num5, num6 - 1] != "*" && ArraySectors[num5, num6 - 1] != "s")
				{
					easySectors[num5, num6] = "e";
					if (!((float)num6 >= Global.LEVELMASS.y - 1f) && ArraySectors[num5, num6 + 1] != "*" && ArraySectors[num5, num6] != "s")
					{
						easySectors[num5, num6 + 1] = "e";
					}
				}
			}
			num5 = (int)(Global.LEVELMASS.x - 1f);
			while (num5 >= 0 && ArraySectors[num5, num6] != "*")
			{
				if (ArraySectors[num5, num6 - 1] != "*" && ArraySectors[num5, num6 - 1] != "s")
				{
					easySectors[num5, num6] = "e";
					if (!((float)num6 >= Global.LEVELMASS.y - 1f) && ArraySectors[num5, num6 + 1] != "*" && ArraySectors[num5, num6] != "s")
					{
						easySectors[num5, num6 + 1] = "e";
					}
				}
				num5--;
			}
		}
		for (int num6 = 0; (float)num6 < Global.LEVELMASS.y; num6++)
		{
			for (num5 = 0; (float)num5 < Global.LEVELMASS.x; num5++)
			{
				if ((bool)Global.SECTORS[num5, num6])
				{
					Global.SECTORS[num5, num6].name = easySectors[num5, num6];
					MonoBehaviour.print("Global.SECTORS[xx,yy].name: " + Global.SECTORS[num5, num6].name);
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Cell");
		Global.EasyCells.Clear();
		Global.HardCells.Clear();
		int m = 0;
		GameObject[] array6 = array;
		for (int length3 = array6.Length; m < length3; m++)
		{
			if (array6[m].transform.parent.name == "e")
			{
				Global.EasyCells.Add(array6[m] as GameObject);
			}
			else
			{
				Global.HardCells.Add(array6[m] as GameObject);
			}
		}
		MonoBehaviour.print("ALL CELLS: " + Extensions.get_length((System.Array)array));
		int n = 0;
		GameObject[] array7 = array;
		for (int length4 = array7.Length; n < length4; n++)
		{
			array7[n].GetComponent<Renderer>().enabled = false;
			num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)GreenLib));
			if ((bool)GreenLib[num].@object && UnityEngine.Random.Range(1, 100) <= GreenLib[num].Chance)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(GreenLib[num].@object) as GameObject;
				Global.LastCreatedObject.transform.position = array7[n].transform.position + new Vector3(UnityEngine.Random.Range(0f - array7[n].GetComponent<Renderer>().bounds.extents.x, array7[n].GetComponent<Renderer>().bounds.extents.x), 0f - array7[n].GetComponent<Renderer>().bounds.extents.y, UnityEngine.Random.Range(-0.01f, 0.01f));
				int num9 = UnityEngine.Random.Range(-10, 10);
				Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
				float num10 = (eulerAngles.z = num9);
				Vector3 vector6 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
				Global.LastCreatedObject.transform.localScale = Global.LastCreatedObject.transform.localScale * UnityEngine.Random.Range(0.75f, 1f);
			}
		}
		if ((bool)PrefabQuest)
		{
			UnityEngine.Object.Instantiate(PrefabQuest);
			array = GameObject.FindGameObjectsWithTag("Cell");
		}
		int num11 = 0;
		GameObject[] array8 = array;
		for (int length5 = array8.Length; num11 < length5; num11++)
		{
			num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)TreasureLib));
			if ((bool)TreasureLib[num].@object && UnityEngine.Random.Range(1, 100) <= TreasureLib[num].Chance)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(TreasureLib[num].@object) as GameObject;
				Global.LastCreatedObject.transform.position = array8[num11].transform.position + new Vector3(UnityEngine.Random.Range((0f - array8[num11].GetComponent<Renderer>().bounds.extents.x) * 0.5f, array8[num11].GetComponent<Renderer>().bounds.extents.x * 0.5f), 0f, -0.2f + UnityEngine.Random.Range(-0.01f, 0.01f));
				UnityEngine.Object.DestroyImmediate(array8[num11]);
			}
		}
		CheckGameObjectInArray();
		num = UnityEngine.Random.Range(0, Global.EasyCells.length);
		object obj3 = Global.EasyCells[num];
		if (!(obj3 is GameObject))
		{
			obj3 = RuntimeServices.Coerce(obj3, typeof(GameObject));
		}
		gameObject = (GameObject)obj3;
		MonoBehaviour.print("number: " + num);
		if (gameObject == null)
		{
			MonoBehaviour.print("FAIL!");
		}
		if ((bool)HPGenerator)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(HPGenerator) as GameObject;
			Global.LastCreatedObject.transform.position = gameObject.transform.position + new Vector3(0f, 0f, -0.3f + UnityEngine.Random.Range(-0.01f, 0.01f));
			object obj4 = Global.EasyCells[num];
			if (!(obj4 is UnityEngine.Object))
			{
				obj4 = RuntimeServices.Coerce(obj4, typeof(UnityEngine.Object));
			}
			UnityEngine.Object.DestroyImmediate((UnityEngine.Object)obj4);
			Global.EasyCells.RemoveAt(num);
		}
		num = UnityEngine.Random.Range(0, Global.EasyCells.length);
		object obj5 = Global.EasyCells[num];
		if (!(obj5 is GameObject))
		{
			obj5 = RuntimeServices.Coerce(obj5, typeof(GameObject));
		}
		gameObject = (GameObject)obj5;
		MonoBehaviour.print("number: " + num);
		if (gameObject == null)
		{
			MonoBehaviour.print("FAIL!");
		}
		if ((bool)ManaGenerator)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ManaGenerator) as GameObject;
			Global.LastCreatedObject.transform.position = gameObject.transform.position + new Vector3(0f, 0f, -0.3f + UnityEngine.Random.Range(-0.01f, 0.01f));
			object obj6 = Global.EasyCells[num];
			if (!(obj6 is UnityEngine.Object))
			{
				obj6 = RuntimeServices.Coerce(obj6, typeof(UnityEngine.Object));
			}
			UnityEngine.Object.DestroyImmediate((UnityEngine.Object)obj6);
			Global.EasyCells.RemoveAt(num);
		}
		num = UnityEngine.Random.Range(0, Global.EasyCells.length);
		object obj7 = Global.EasyCells[num];
		if (!(obj7 is GameObject))
		{
			obj7 = RuntimeServices.Coerce(obj7, typeof(GameObject));
		}
		gameObject = (GameObject)obj7;
		MonoBehaviour.print("number: " + num);
		if (gameObject == null)
		{
			MonoBehaviour.print("FAIL!");
		}
		if ((bool)HeroObject)
		{
			float x2 = gameObject.transform.position.x;
			Vector3 position = HeroObject.position;
			float num12 = (position.x = x2);
			Vector3 vector8 = (HeroObject.position = position);
			float y = gameObject.transform.position.y;
			Vector3 position2 = HeroObject.position;
			float num13 = (position2.y = y);
			Vector3 vector10 = (HeroObject.position = position2);
			int num14 = -1;
			Vector3 position3 = HeroObject.position;
			float num15 = (position3.z = num14);
			Vector3 vector12 = (HeroObject.position = position3);
			object obj8 = Global.EasyCells[num];
			if (!(obj8 is UnityEngine.Object))
			{
				obj8 = RuntimeServices.Coerce(obj8, typeof(UnityEngine.Object));
			}
			UnityEngine.Object.DestroyImmediate((UnityEngine.Object)obj8);
			Global.EasyCells.RemoveAt(num);
		}
		MonoBehaviour.print(" *** Global.EasyCells.length: " + Global.EasyCells.length);
		if (!Global.DontGenerateEnemies)
		{
			array = GameObject.FindGameObjectsWithTag("Cell");
			int num16 = 0;
			GameObject[] array9 = array;
			for (int length6 = array9.Length; num16 < length6; num16++)
			{
				num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)Enemy));
				if ((bool)Enemy[num].@object && UnityEngine.Random.Range(1, 100) <= Enemy[num].Chance)
				{
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(Enemy[num].@object) as GameObject;
					Global.LastCreatedObject.transform.position = array9[num16].transform.position + new Vector3(0f, 0f, 1f);
					int num17 = -1;
					Vector3 position4 = Global.LastCreatedObject.transform.position;
					float num18 = (position4.z = num17);
					Vector3 vector14 = (Global.LastCreatedObject.transform.position = position4);
					UnityEngine.Object.DestroyImmediate(array9[num16]);
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Cell");
		int num19 = 0;
		GameObject[] array10 = array;
		for (int length7 = array10.Length; num19 < length7; num19++)
		{
			num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)Objects));
			if ((bool)Objects[num].@object && UnityEngine.Random.Range(1, 100) <= Objects[num].Chance)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(Objects[num].@object) as GameObject;
				Global.LastCreatedObject.transform.position = array10[num19].transform.position + new Vector3(UnityEngine.Random.Range((0f - array10[num19].GetComponent<Renderer>().bounds.extents.x) * 0.1f, array10[num19].GetComponent<Renderer>().bounds.extents.x * 0.1f), 0f, -1f);
				UnityEngine.Object.DestroyImmediate(array10[num19]);
			}
		}
		array = GameObject.FindGameObjectsWithTag("Cell");
		MonoBehaviour.print("FREE CELLS: " + Extensions.get_length((System.Array)array));
		UnityEngine.Random.seed = (int)Time.realtimeSinceStartup;
	}

	public virtual void CheckGameObjectInArray()
	{
		for (int num = Global.EasyCells.length - 1; num >= 0; num--)
		{
			if (RuntimeServices.EqualityOperator(Global.EasyCells[num], null))
			{
				Global.EasyCells.RemoveAt(num);
			}
		}
		for (int num = Global.HardCells.length - 1; num >= 0; num--)
		{
			if (RuntimeServices.EqualityOperator(Global.HardCells[num], null))
			{
				Global.HardCells.RemoveAt(num);
			}
		}
	}

	public virtual void Main()
	{
	}
}
