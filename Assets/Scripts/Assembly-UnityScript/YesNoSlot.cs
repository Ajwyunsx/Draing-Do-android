using System;
using UnityEngine;

[Serializable]
public class YesNoSlot : MonoBehaviour
{
	public bool YesX;

	public string Action;

	[HideInInspector]
	public YesNoScript _script;

	private Vector3 StartScale;

	private int inTarget;

	public YesNoSlot()
	{
		Action = string.Empty;
	}

	public virtual void Awake()
	{
		StartScale = transform.localScale;
	}

	public virtual void Start()
	{
		_script = (YesNoScript)gameObject.transform.parent.gameObject.GetComponent("YesNoScript");
	}

	public virtual void MenuSlotAction()
	{
		int num = default(int);
		string action = Action;
		if (action == "yes")
		{
			_script.FinalYes(true);
		}
		else if (action == "no")
		{
			_script.FinalYes(false);
		}
	}

	public virtual void RemoveAllOjects()
	{
		GameObject gameObject = this.gameObject;
		GameObject[] array = (GameObject[])UnityEngine.Object.FindSceneObjectsOfType(typeof(GameObject));
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (array2[i] != Global.GlobalObject)
			{
				UnityEngine.Object.Destroy(array2[i]);
			}
		}
	}

	public virtual void ActivateByMouse()
	{
		MenuSlotAction();
	}

	public virtual void Update()
	{
		if (inTarget != 0)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, StartScale * 1.05f, 0.5f);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, StartScale, 0.5f);
		}
		inTarget = 0;
	}

	public virtual void OnMouseOver()
	{
		inTarget = 2;
	}

	public virtual void Main()
	{
	}
}
