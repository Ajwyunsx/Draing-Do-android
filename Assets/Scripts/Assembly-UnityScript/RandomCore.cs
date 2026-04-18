using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[AddComponentMenu("Alex Code/Random Core")]
public class RandomCore : MonoBehaviour
{
	public GameObject[] levels;

	public virtual void Awake()
	{
		Creator();
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Creator()
	{
		int num = default(int);
		Vector2 worldPosition = Global.WorldPosition;
		if (Global.MapIndex[(int)worldPosition.x, (int)worldPosition.y] == null)
		{
			num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)levels));
			Global.MapIndex[(int)worldPosition.x, (int)worldPosition.y] = string.Empty + num;
		}
		else
		{
			num = Convert.ToInt32(Global.MapIndex[(int)worldPosition.x, (int)worldPosition.y]);
		}
		if ((bool)levels[num])
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(levels[num]) as GameObject;
			Global.LastCreatedObject.name = levels[num].name;
			Global.LastCreatedObject.transform.position = transform.position;
			int num2 = 0;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num3 = (position.z = num2);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
		}
	}

	public virtual void Main()
	{
	}
}
