using System;
using UnityEngine;

[Serializable]
public class FluteEscapeAI : MonoBehaviour
{
	public AudioClip SFX;

	private Vector3 InitScale;

	public Vector3 Scale;

	private Vector3 Target;

	private bool Sleep;

	private bool START;

	public FluteEscapeAI()
	{
		Scale = new Vector3(0.1f, 0.1f, 0.1f);
	}

	public virtual void Start()
	{
		if ((bool)SFX)
		{
			if (GetComponent<AudioSource>() == null)
			{
				gameObject.AddComponent<AudioSource>();
			}
			if (GetComponent<Renderer>() == null)
			{
				gameObject.AddComponent<MeshRenderer>();
			}
		}
		InitScale = transform.localScale;
	}

	public virtual void FixedUpdate()
	{
		ReActive();
		if (START)
		{
			if (!(Mathf.Abs(transform.localScale.x - Target.x) > 0.05f) && !(Mathf.Abs(transform.localScale.y - Target.y) > 0.05f))
			{
				START = false;
			}
			transform.localScale = Vector3.Lerp(transform.localScale, Target, 0.1f);
		}
	}

	public virtual void ReActive()
	{
		if (Global.WakeUpTime > 0 && !Sleep)
		{
			if (GetComponent<Renderer>().isVisible && (bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			START = true;
			Target = Scale;
			Sleep = true;
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = false;
			}
		}
		if (Global.WakeUpTime <= 0 && Sleep)
		{
			if (GetComponent<Renderer>().isVisible && (bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			START = true;
			Target = InitScale;
			Sleep = false;
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = true;
			}
		}
	}

	public virtual void Main()
	{
	}
}
