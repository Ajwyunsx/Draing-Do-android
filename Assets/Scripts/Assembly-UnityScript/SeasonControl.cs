using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SeasonControl : MonoBehaviour
{
	private int OldDAYTIME;

	public bool Day;

	public bool Night;

	public bool DeleteInRain;

	public bool LiveOnlyRain;

	public bool Invert;

	public bool AllChilds;

	public SeasonControl()
	{
		OldDAYTIME = -100000;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (OldDAYTIME == Global.DAYTIME)
		{
			return;
		}
		OldDAYTIME = Global.DAYTIME;
		bool flag = true;
		if (Day && (Global.DAYTIME < 6 || Global.DAYTIME > 21))
		{
			flag = false;
		}
		if (Night && Global.DAYTIME >= 6 && Global.DAYTIME <= 21)
		{
			flag = false;
		}
		if (DeleteInRain && Global.CheckRain())
		{
			flag = false;
		}
		if (LiveOnlyRain)
		{
			flag = (Global.CheckRain() ? true : false);
		}
		if (Invert)
		{
			flag = !flag;
		}
		if (AllChilds)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (!(obj is Transform))
				{
					obj = RuntimeServices.Coerce(obj, typeof(Transform));
				}
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(flag);
				UnityRuntimeServices.Update(enumerator, transform);
			}
		}
		else
		{
			if ((bool)GetComponent<Renderer>())
			{
				GetComponent<Renderer>().enabled = flag;
			}
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = flag;
			}
		}
	}

	public virtual void Main()
	{
	}
}
