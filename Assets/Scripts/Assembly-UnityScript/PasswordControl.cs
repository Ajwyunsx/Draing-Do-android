using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class PasswordControl : MonoBehaviour
{
	public GameObject[] ObjectList;

	public GameObject[] InputPasswordObjects;

	public string PasswordData;

	[HideInInspector]
	public int delay;

	[HideInInspector]
	public bool IsOKPassword;

	public virtual void Start()
	{
		delay = UnityEngine.Random.Range(0, 25);
	}

	public virtual void FixedUpdate()
	{
		if (!IsOKPassword)
		{
			delay++;
			if (delay >= 25)
			{
				delay = 0;
				CheckPassword();
			}
		}
	}

	public virtual void CheckPassword()
	{
		string text = string.Empty;
		for (int i = 0; i < Extensions.get_length((System.Array)InputPasswordObjects); i++)
		{
			if ((bool)(Button)InputPasswordObjects[i].transform.GetComponent(typeof(Button)))
			{
				text = ((!((Button)InputPasswordObjects[i].transform.GetComponent(typeof(Button))).ON) ? (text + "0") : (text + "1"));
			}
			if ((bool)(Tablo)InputPasswordObjects[i].transform.GetComponent(typeof(Tablo)))
			{
				text += ((Tablo)InputPasswordObjects[i].transform.GetComponent(typeof(Tablo))).currentNumber;
			}
		}
		if (text == PasswordData)
		{
			CommandTo();
		}
	}

	public virtual void CommandTo()
	{
		IsOKPassword = true;
		for (int i = 0; i < Extensions.get_length((System.Array)ObjectList); i++)
		{
			ObjectList[i].SendMessage("ByButton", true, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ByButton(bool @bool)
	{
		if (!IsOKPassword)
		{
			CommandTo();
		}
	}

	public virtual void Main()
	{
	}
}
