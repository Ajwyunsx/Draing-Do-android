using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class GeneratedMap : MonoBehaviour
{
	public GameObject sprite;

	public GameObject start;

	public GameObject boss;

	public GameObject secret;

	public GameObject exit;

	public virtual void Awake()
	{
		if (!Global.WorldON)
		{
			return;
		}
		GameObject gameObject = null;
		float num = (Global.WorldMax.x - 1f) * 0.05f * 0.5f;
		float num2 = (Global.WorldMax.y - 1f) * 0.05f * 0.5f;
		for (int i = 0; (float)i < Global.WorldMax.y; i++)
		{
			for (int j = 0; (float)j < Global.WorldMax.x; j++)
			{
				gameObject = null;
				if (!Global.WorldVisible[j, i])
				{
					continue;
				}
				string text = Global.WorldMap[j, i];
				if (!(text == null))
				{
					switch (text)
					{
					case "start":
						gameObject = UnityEngine.Object.Instantiate(start, transform.position + new Vector3((float)j * 0.05f - num, (float)i * 0.05f - num2, -0.1f), Quaternion.identity);
						break;
					case "boss":
						gameObject = UnityEngine.Object.Instantiate(boss, transform.position + new Vector3((float)j * 0.05f - num, (float)i * 0.05f - num2, -0.1f), Quaternion.identity);
						break;
					case "secret":
						gameObject = UnityEngine.Object.Instantiate(secret, transform.position + new Vector3((float)j * 0.05f - num, (float)i * 0.05f - num2, -0.1f), Quaternion.identity);
						break;
					default:
						gameObject = UnityEngine.Object.Instantiate(sprite, transform.position + new Vector3((float)j * 0.05f - num, (float)i * 0.05f - num2, -0.1f), Quaternion.identity);
						break;
					}
				}
				if ((bool)gameObject)
				{
					CreateNeigbours(j, i, gameObject.transform.position);
					gameObject.transform.parent = transform;
					if (Global.WorldPosition.x == (float)j && Global.WorldPosition.y == (float)i)
					{
						gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
					}
				}
			}
		}
	}

	public virtual void CreateNeigbours(object x, object y, Vector3 pos)
	{
		Vector2 vector = default(Vector2);
		GameObject gameObject = null;
		float num = 0.02f;
		vector = new Vector2(0f, 1f);
		if (CheckNeigbour(RuntimeServices.UnboxInt32(x), RuntimeServices.UnboxInt32(y), vector))
		{
			gameObject = UnityEngine.Object.Instantiate(exit, pos + new Vector3(vector.x * num, vector.y * num, -0.12f), Quaternion.identity);
			gameObject.transform.parent = transform;
		}
		vector = new Vector2(0f, -1f);
		if (CheckNeigbour(RuntimeServices.UnboxInt32(x), RuntimeServices.UnboxInt32(y), vector))
		{
			gameObject = UnityEngine.Object.Instantiate(exit, pos + new Vector3(vector.x * num, vector.y * num, -0.12f), Quaternion.identity);
			gameObject.transform.parent = transform;
		}
		vector = new Vector2(-1f, 0f);
		if (CheckNeigbour(RuntimeServices.UnboxInt32(x), RuntimeServices.UnboxInt32(y), vector))
		{
			gameObject = UnityEngine.Object.Instantiate(exit, pos + new Vector3(vector.x * num, vector.y * num, -0.12f), Quaternion.identity);
			gameObject.transform.parent = transform;
		}
		vector = new Vector2(1f, 0f);
		if (CheckNeigbour(RuntimeServices.UnboxInt32(x), RuntimeServices.UnboxInt32(y), vector))
		{
			gameObject = UnityEngine.Object.Instantiate(exit, pos + new Vector3(vector.x * num, vector.y * num, -0.12f), Quaternion.identity);
			gameObject.transform.parent = transform;
		}
	}

	public virtual bool CheckNeigbour(int x, int y, Vector2 ToPosition)
	{
		Vector2 vector = new Vector2(x, y);
		Vector2 vector2 = vector + ToPosition;
		return !(vector2.x < 0f) && !(vector2.x >= Global.WorldMax.x) && !(vector2.y < 0f) && vector2.y < Global.WorldMax.y && ((Global.WorldLevel[(int)vector2.x, (int)vector2.y] != null && Global.WorldMap[(int)vector2.x, (int)vector2.y] != "secret") ? true : false);
	}

	public virtual void Main()
	{
	}
}
