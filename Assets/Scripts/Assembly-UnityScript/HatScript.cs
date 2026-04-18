using System;
using UnityEngine;

[Serializable]
public class HatScript : MonoBehaviour
{
	private GameObject HatObject;

	private int oldHatHero;

	public GameObject[] HatVariants;

	public Transform ScaleCorrectObject;

	public HatScript()
	{
		oldHatHero = -1;
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (oldHatHero != Global.HatHero)
		{
			oldHatHero = Global.HatHero;
			if ((bool)HatObject)
			{
				UnityEngine.Object.Destroy(HatObject);
			}
			HatObject = UnityEngine.Object.Instantiate(HatVariants[Global.HatHero], transform.position, Quaternion.identity);
			if ((bool)ScaleCorrectObject)
			{
				HatObject.transform.localScale = HatObject.transform.localScale * Mathf.Abs(ScaleCorrectObject.localScale.x);
			}
			float x = HatObject.transform.localScale.x * Mathf.Sign(Global.CurrentPlayerObject.localScale.x);
			Vector3 localScale = HatObject.transform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (HatObject.transform.localScale = localScale);
			HatObject.transform.parent = transform;
		}
	}

	public virtual void Main()
	{
	}
}
