using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class PeriodGenerator : MonoBehaviour
{
	public GameObject ThatObject;

	private UnityScript.Lang.Array ownObjects;

	public int MaxObjects;

	public float ZPosition;

	public float RndPosition;

	private int timer;

	public int DelayTime;

	public int COUNT;

	private bool Dis;

	private int disappear_timer;

	public GameObject particleStrike;

	private Transform myTransform;

	public PeriodGenerator()
	{
		ownObjects = new UnityScript.Lang.Array();
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (Dis)
		{
			DisFunction();
			return;
		}
		timer++;
		if (timer <= DelayTime)
		{
			return;
		}
		timer = 0;
		for (int i = 0; i < ownObjects.length; i++)
		{
			if (RuntimeServices.EqualityOperator(ownObjects[i], null))
			{
				ownObjects.RemoveAt(i);
			}
		}
		if (ownObjects.length >= MaxObjects)
		{
			return;
		}
		ownObjects.Add(UnityEngine.Object.Instantiate(ThatObject, transform.position + new Vector3(UnityEngine.Random.Range(0f - RndPosition, RndPosition), UnityEngine.Random.Range(0f - RndPosition, RndPosition), ZPosition), Quaternion.identity));
		if (COUNT <= 0)
		{
			return;
		}
		COUNT--;
		if (COUNT <= 0)
		{
			Dis = true;
			disappear_timer = 60;
			if ((bool)particleStrike)
			{
				UnityEngine.Object.Instantiate(particleStrike, myTransform.position + new Vector3(0f, 0f, -0.5f), Quaternion.identity);
			}
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = false;
			}
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public virtual void DisFunction()
	{
		disappear_timer--;
		myTransform.localScale = Vector3.Lerp(myTransform.localScale, Vector3.zero, 0.1f);
		if (disappear_timer <= 0)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
