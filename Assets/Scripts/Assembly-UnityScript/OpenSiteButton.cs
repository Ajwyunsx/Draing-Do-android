using System;
using UnityEngine;

[Serializable]
public class OpenSiteButton : MonoBehaviour
{
	private bool isSelected;

	private Vector3 startScale;

	public virtual void Awake()
	{
		startScale = transform.localScale;
	}

	public virtual void Update()
	{
		if (isSelected)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, startScale * 1.1f, 0.1f);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, startScale, 0.1f);
		}
		isSelected = false;
	}

	public virtual void OnMouseOver()
	{
		isSelected = true;
	}

	public virtual void OnMouseUpAsButton()
	{
		GetComponent<AudioSource>().Play();
		Application.OpenURL("https://www.patreon.com/alexmakovsky");
	}

	public virtual void Main()
	{
	}
}
