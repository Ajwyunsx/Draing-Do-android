using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class CrushWalls : MonoBehaviour
{
	public AudioClip SFX;

	[HideInInspector]
	public tk2dSprite sprite;

	[HideInInspector]
	public bool toUnblock;

	public virtual void Start()
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
			if ((bool)transform.GetComponent<Collider>())
			{
				transform.GetComponent<Collider>().enabled = false;
				UnityRuntimeServices.Update(enumerator, transform);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (toUnblock)
		{
			gameObject.BroadcastMessage("FakePartVelocityAndDestroy", 3, SendMessageOptions.DontRequireReceiver);
			if (transform.childCount == 0)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void CrushHP(int hp)
	{
		if (hp >= 2)
		{
			Unblock();
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = false;
			}
		}
	}

	public virtual void Unblock()
	{
		toUnblock = true;
		if ((bool)SFX)
		{
			AudioSource.PlayClipAtPoint(SFX, transform.position);
		}
	}

	public virtual void Main()
	{
	}
}
