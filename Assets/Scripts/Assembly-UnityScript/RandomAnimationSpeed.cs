using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class RandomAnimationSpeed : MonoBehaviour
{
	public AudioClip SFX;

	public float MinAnimSpeed;

	public float MaxAnimSpeed;

	public Vector2 RandomScale;

	public bool StopByDisappear;

	public RandomAnimationSpeed()
	{
		MinAnimSpeed = 0.75f;
		MaxAnimSpeed = 1.25f;
	}

	public virtual void Awake()
	{
		if (RandomScale != Vector2.zero)
		{
			transform.localScale *= UnityEngine.Random.Range(RandomScale.x, RandomScale.y);
		}
		if ((bool)SFX && GetComponent<Renderer>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(GetComponent<Animation>());
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is AnimationState))
			{
				obj = RuntimeServices.Coerce(obj, typeof(AnimationState));
			}
			AnimationState animationState = (AnimationState)obj;
			animationState.speed = UnityEngine.Random.Range(MinAnimSpeed, MaxAnimSpeed);
			UnityRuntimeServices.Update(enumerator, animationState);
		}
	}

	public virtual void EventSound()
	{
		if (!(SFX == null) && GetComponent<Renderer>().isVisible)
		{
			AudioSource.PlayClipAtPoint(SFX, transform.position);
		}
	}

	public virtual void DISAPPEAR()
	{
		if (StopByDisappear)
		{
			GetComponent<Animation>().Stop();
		}
	}

	public virtual void Main()
	{
	}
}
