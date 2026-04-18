using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class HairScript : MonoBehaviour
{
	private GameObject HairObject;

	private int oldHairHero;

	private Color oldHairColor;

	public GameObject[] HairVariants;

	public Transform ScaleCorrectObject;

	public HairScript()
	{
		oldHairHero = -1;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (oldHairHero != Global.HairHero)
		{
			oldHairHero = Global.HairHero;
			if ((bool)HairObject)
			{
				UnityEngine.Object.Destroy(HairObject);
			}
			oldHairColor = new Color(0f, 0f, 0f, 0f);
			HairObject = UnityEngine.Object.Instantiate(HairVariants[Global.HairHero], this.transform.position, Quaternion.identity);
			if ((bool)ScaleCorrectObject)
			{
				HairObject.transform.localScale = HairObject.transform.localScale * Mathf.Abs(ScaleCorrectObject.localScale.x);
			}
			float x = HairObject.transform.localScale.x * Mathf.Sign(Global.CurrentPlayerObject.localScale.x);
			Vector3 localScale = HairObject.transform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (HairObject.transform.localScale = localScale);
			HairObject.transform.parent = this.transform;
		}
		if (!HairObject || !(oldHairColor != Global.HairColor))
		{
			return;
		}
		oldHairColor = Global.HairColor;
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(HairObject.transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is Transform))
			{
				obj = RuntimeServices.Coerce(obj, typeof(Transform));
			}
			Transform transform = (Transform)obj;
			transform.gameObject.GetComponent<Renderer>().material.color = Global.HairColor;
			UnityRuntimeServices.Update(enumerator, transform);
		}
	}

	public virtual void Main()
	{
	}
}
