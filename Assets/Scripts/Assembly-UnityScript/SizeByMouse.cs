using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SizeByMouse : MonoBehaviour
{
	private Vector3 StartScale;

	private Transform myTransform;

	private int inTarget;

	public string DiactivateVariable;

	private bool oldEnable;

	public bool UnActive;

	public SizeByMouse()
	{
		oldEnable = true;
	}

	public virtual void SetOn(bool b)
	{
		if (b == oldEnable)
		{
			return;
		}
		oldEnable = b;
		Transform transform = null;
		if (oldEnable)
		{
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = true;
			}
			if ((bool)GetComponent<Renderer>())
			{
				GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
			}
			if ((bool)GetComponent<SkillDrawHUD>())
			{
				GetComponent<SkillDrawHUD>().OffSkillDraw();
			}
			inTarget = 0;
			return;
		}
		if ((bool)GetComponent<Collider>())
		{
			GetComponent<Collider>().enabled = false;
		}
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0.5f);
		}
		if ((bool)GetComponent<SkillDrawHUD>())
		{
			GetComponent<SkillDrawHUD>().OffSkillDraw();
		}
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is Transform))
			{
				obj = RuntimeServices.Coerce(obj, typeof(Transform));
			}
			transform = (Transform)obj;
			UnityEngine.Object.Destroy(transform.gameObject);
			UnityRuntimeServices.Update(enumerator, transform);
		}
		inTarget = 0;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		StartScale = myTransform.localScale;
		if (DiactivateVariable == null)
		{
			DiactivateVariable = string.Empty;
		}
		if (DiactivateVariable != string.Empty && !Global.CheckStuff(DiactivateVariable))
		{
			SetOn(false);
		}
	}

	public virtual void Update()
	{
		if (inTarget != 0)
		{
			myTransform.localScale = Vector3.Lerp(myTransform.localScale, StartScale * 1.075f, 0.35f);
		}
		else
		{
			myTransform.localScale = Vector3.Lerp(myTransform.localScale, StartScale, 0.35f);
		}
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void OnMouseEnter()
	{
		if (!UnActive)
		{
			inTarget = 2;
		}
	}

	public virtual void OnMouseExit()
	{
		inTarget = 0;
	}

	public virtual void SetOnModify(bool b)
	{
		if (b)
		{
			UnActive = false;
			inTarget = 0;
		}
		else
		{
			UnActive = true;
			inTarget = 0;
		}
	}

	public virtual void Main()
	{
	}
}
