using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class Rainbow : MonoBehaviour
{
	public float Power;

	public int Period;

	public object Zero;

	[HideInInspector]
	public bool GiveMode;

	[HideInInspector]
	public Transform MyTransform;

	public Rainbow()
	{
		Power = 20f;
	}

	public virtual void Awake()
	{
		MyTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (!GiveMode || RuntimeServices.ToBool(Zero))
		{
			return;
		}
		Period++;
		if (Period >= 50)
		{
			Period = 0;
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("particles/raindrop")) as GameObject;
			Global.LastCreatedObject.transform.position = new Vector3(MyTransform.position.x, MyTransform.position.y, -10f);
			Power -= 0.1f;
			Global.Rainbow += 0.1f;
			if (!(Power > 0f))
			{
				Zero = true;
			}
		}
	}

	public virtual void Activate()
	{
		GiveMode = true;
	}

	public virtual void Main()
	{
	}
}
