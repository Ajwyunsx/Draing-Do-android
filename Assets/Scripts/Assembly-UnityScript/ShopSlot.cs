using System;
using UnityEngine;

[Serializable]
public class ShopSlot : MonoBehaviour
{
	public GameObject Item;

	public bool Once;

	private bool once;

	public float YPosOnStart;

	private GameObject CreatedItem;

	private Vector3 targetScale;

	private int delayTimer;

	private int ScaleTimer;

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		CreateItem();
		if (delayTimer > 0)
		{
			delayTimer--;
		}
		if (ScaleTimer > 0 && (bool)CreatedItem)
		{
			ScaleTimer--;
			if (ScaleTimer > 0)
			{
				CreatedItem.transform.localScale = Vector3.Lerp(CreatedItem.transform.localScale, targetScale, 0.1f);
			}
			else
			{
				CreatedItem.transform.localScale = targetScale;
			}
		}
	}

	public virtual void RemoveIt()
	{
		CreatedItem.transform.localScale = targetScale;
		CreatedItem = null;
		ScaleTimer = 0;
		delayTimer = 50;
	}

	public virtual void CreateItem()
	{
		if (delayTimer <= 0 && !CreatedItem && (!Once || !once))
		{
			once = true;
			CreatedItem = UnityEngine.Object.Instantiate(Item) as GameObject;
			CreatedItem.name = Item.name;
			CreatedItem.transform.position = transform.position;
			float z = Global.CurrentPlayerObject.position.z + 0.015f;
			Vector3 position = CreatedItem.transform.position;
			float num = (position.z = z);
			Vector3 vector = (CreatedItem.transform.position = position);
			float y = CreatedItem.transform.position.y + YPosOnStart;
			Vector3 position2 = CreatedItem.transform.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (CreatedItem.transform.position = position2);
			targetScale = CreatedItem.transform.localScale;
			CreatedItem.transform.localScale = CreatedItem.transform.localScale * 0f;
			ScaleTimer = 100;
			CreatedItem.transform.parent = transform;
			CreatedItem.SendMessage("MakeBuy", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
