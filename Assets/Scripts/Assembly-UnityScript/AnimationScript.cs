using System;
using UnityEngine;

[Serializable]
public class AnimationScript : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void AnimateObject(string anim)
	{
		GetComponent<Animation>().CrossFade(anim, 0.15f);
	}

	public virtual void Main()
	{
	}
}
