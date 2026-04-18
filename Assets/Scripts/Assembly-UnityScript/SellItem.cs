using System;
using UnityEngine;

[Serializable]
public class SellItem : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void OnMouseUpAsButton()
	{
		if ((bool)SlotItem.selected)
		{
			SlotItem component = SlotItem.selected.GetComponent<SlotItem>();
			if ((bool)component)
			{
				Global.Gold = (int)((float)Global.Gold + (float)component.Price * 0.5f);
				AudioSource.PlayClipAtPoint(LoadData.SFX("buy"), transform.position);
				UnityEngine.Object.Destroy(SlotItem.selected);
			}
		}
	}

	public virtual void Main()
	{
	}
}
