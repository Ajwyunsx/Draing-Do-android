using System;
using UnityEngine;

[Serializable]
public class ButtonForMouse : MonoBehaviour
{
	public AudioClip SFXNo;

	public string Action;

	public bool Waving;

	public string ActionCommand;

	private YesNoScript _script;

	private Vector3 StartScale;

	private Transform myTransform;

	private int inTarget;

	public ButtonForMouse()
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
		if (action == "enter_level" && (bool)Global.LastClickObject)
		{
			Global.LastClickObject.SendMessage("PlayLevel", null, SendMessageOptions.DontRequireReceiver);
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

	public virtual void Rename(string name)
	{
		if (name == "MAX")
		{
			enabled = false;
		}
	}

	public virtual void OnMouseOver()
	{
		inTarget = 2;
	}

	public virtual void Main()
	{
	}
}
