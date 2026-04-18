using System;
using UnityEngine;

[Serializable]
public class MouseButton : MonoBehaviour
{
	public string Action;

	public bool Waving;

	public string ActionCommand;

	private YesNoScript _script;

	private Vector3 StartScale;

	private Transform myTransform;

	private int inTarget;

	public MouseButton()
	{
		Action = string.Empty;
		ActionCommand = string.Empty;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		StartScale = myTransform.localScale;
	}

	public virtual void Start()
	{
		if ((bool)gameObject.transform.parent)
		{
			_script = (YesNoScript)gameObject.transform.parent.gameObject.GetComponent("YesNoScript");
		}
	}

	public virtual void Update()
	{
		if (Input.GetKey(KeyCode.T) && !Global.Pause)
		{
			Global.SetPauseMenu("MenuTools");
		}
		if (Input.GetKey(KeyCode.C) && !Global.Pause)
		{
			Global.SetPauseMenu("MenuCloths");
		}
		if (inTarget != 0)
		{
			myTransform.localScale = Vector3.Lerp(myTransform.localScale, StartScale * 1.05f, 0.5f);
		}
		else
		{
			myTransform.localScale = Vector3.Lerp(myTransform.localScale, StartScale, 0.5f);
		}
		inTarget = 0;
	}

	public virtual void MenuSlotAction()
	{
		int num = default(int);
		string action = Action;
		if (action == "menu")
		{
			if (!Global.Pause)
			{
				Global.SetPauseMenu("MenuTools");
			}
		}
		else if (action == "tools" && !Global.Pause)
		{
			Global.SetPauseMenu("MenuCloths");
		}
	}

	public virtual void OnMouseDown()
	{
		MenuSlotAction();
		Global.BlockMouseTime = 10;
	}

	public virtual void OnMouseOver()
	{
		inTarget = 2;
		if (Global.MouseTrig)
		{
			Global.BlockMouseTime = 5;
		}
	}

	public virtual void Main()
	{
	}
}
