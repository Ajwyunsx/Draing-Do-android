using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class HideChildrens : MonoBehaviour
{
	private bool checkColl;

	private float alpha;

	public virtual void Start()
	{
		alpha = 1f;
	}

	public virtual void FixedUpdate()
	{
		if (checkColl)
		{
			if (!(alpha <= 0f))
			{
				alpha -= 0.05f;
				if ((bool)GetComponent<Renderer>())
				{
					float a = alpha;
					Color color = GetComponent<Renderer>().material.color;
					float num = (color.a = a);
					Color color2 = (GetComponent<Renderer>().material.color = color);
				}
				IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (!(obj is Transform))
					{
						obj = RuntimeServices.Coerce(obj, typeof(Transform));
					}
					Transform transform = (Transform)obj;
					if ((bool)transform.GetComponent<Renderer>())
					{
						float a2 = alpha;
						Color color4 = transform.GetComponent<Renderer>().material.color;
						float num2 = (color4.a = a2);
						Color color5 = (transform.GetComponent<Renderer>().material.color = color4);
						UnityRuntimeServices.Update(enumerator, transform);
					}
				}
			}
		}
		else if (!(alpha >= 1f))
		{
			alpha += 0.05f;
			if ((bool)GetComponent<Renderer>())
			{
				float a3 = alpha;
				Color color7 = GetComponent<Renderer>().material.color;
				float num3 = (color7.a = a3);
				Color color8 = (GetComponent<Renderer>().material.color = color7);
			}
			IEnumerator enumerator2 = UnityRuntimeServices.GetEnumerator(this.transform);
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				if (!(obj2 is Transform))
				{
					obj2 = RuntimeServices.Coerce(obj2, typeof(Transform));
				}
				Transform transform2 = (Transform)obj2;
				if ((bool)transform2.GetComponent<Renderer>())
				{
					float a4 = alpha;
					Color color10 = transform2.GetComponent<Renderer>().material.color;
					float num4 = (color10.a = a4);
					Color color11 = (transform2.GetComponent<Renderer>().material.color = color10);
					UnityRuntimeServices.Update(enumerator2, transform2);
				}
			}
		}
		checkColl = false;
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.tag != "Player"))
		{
			checkColl = true;
		}
	}

	public virtual void Main()
	{
	}
}
