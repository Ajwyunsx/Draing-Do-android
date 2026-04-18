using System;
using UnityEngine;

[Serializable]
public class ClothLib : MonoBehaviour
{
	public bool WearLikePlayer;

	public GameObject[] body;

	public GameObject[] hat;

	public GameObject[] Mask;

	public GameObject[] Sock;

	public virtual void Awake()
	{
		if (WearLikePlayer)
		{
			Global.CurrentPlayerObject = transform;
			WearBody(Convert.ToInt32(Global.Var["Body"]));
			WearHat(Convert.ToInt32(Global.Var["Hat"]));
			WearMask(Convert.ToInt32(Global.Var["Mask"]));
			WearSock(Convert.ToInt32(Global.Var["Sock"]));
		}
	}

	public virtual void WorkWithWearData(GameObject obj)
	{
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(obj, new Vector3(0f, 0f, 0f), Quaternion.identity);
		Global.LastCreatedObject.SendMessage("WearClothScript", gameObject, SendMessageOptions.DontRequireReceiver);
		UnityEngine.Object.Destroy(Global.LastCreatedObject);
	}

	public virtual void WearBody(int num)
	{
		GameObject gameObject = body[num];
		if ((bool)gameObject)
		{
			if (WearLikePlayer)
			{
				Global.Var["Body"] = num;
			}
			WorkWithWearData(gameObject);
		}
	}

	public virtual void WearHat(int num)
	{
		GameObject gameObject = hat[num];
		if ((bool)gameObject)
		{
			if (WearLikePlayer)
			{
				Global.Var["Hat"] = num;
			}
			WorkWithWearData(gameObject);
		}
	}

	public virtual void WearMask(int num)
	{
		GameObject gameObject = Mask[num];
		if ((bool)gameObject)
		{
			if (WearLikePlayer)
			{
				Global.Var["Mask"] = num;
			}
			WorkWithWearData(gameObject);
		}
	}

	public virtual void WearSock(int num)
	{
		GameObject gameObject = Sock[num];
		if ((bool)gameObject)
		{
			if (WearLikePlayer)
			{
				Global.Var["Sock"] = num;
			}
			WorkWithWearData(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
