using System;
using UnityEngine;

[Serializable]
public class TileControl : MonoBehaviour
{
	public AudioClip SFX;

	public GameObject GFX;

	public string Mode;

	private float Speed;

	private int fall;

	private bool once;

	private int PreTimer;

	public virtual void Start()
	{
		if ((bool)SFX && GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		switch (Mode)
		{
		case "hide":
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().isTrigger = true;
			}
			break;
		case "show":
			RendererChild(false);
			break;
		}
	}

	public virtual void FixedUpdate()
	{
		if (!(Mode == "fall"))
		{
			return;
		}
		if (PreTimer > 0)
		{
			PreTimer++;
			if (PreTimer > 15)
			{
				PreTimer = 0;
				fall = 1;
				gameObject.layer = 19;
			}
		}
		if (fall != 0)
		{
			Speed += 0.003f * (float)fall;
			float y = transform.position.y - Speed;
			Vector3 position = transform.position;
			float num = (position.y = y);
			Vector3 vector = (transform.position = position);
			if (!(Mathf.Abs(transform.position.y - Global.CurrentPlayerObject.position.y) < 20f))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (!once && !(collision.gameObject.tag != "Player"))
		{
			CheckMode(collision.gameObject);
		}
	}

	public virtual void OnTriggerEnter(Collider collision)
	{
		if (!once && !(collision.gameObject.tag != "Player"))
		{
			CheckMode(collision.gameObject);
		}
	}

	public virtual void CheckMode(GameObject go)
	{
		once = true;
		if (Mode != "fall" && (bool)GFX)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(GFX) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position + new Vector3(0f, 0f, -1f);
		}
		if ((bool)SFX)
		{
			AudioSource.PlayClipAtPoint(SFX, transform.position);
		}
		switch (Mode)
		{
		case "fall":
			PreTimer = 1;
			break;
		case "hide":
			UnityEngine.Object.Destroy(gameObject);
			break;
		case "show":
			RendererChild(true);
			break;
		}
	}

	public virtual void RendererChild(bool @bool)
	{
		int i = 0;
		Component[] componentsInChildren = GetComponentsInChildren(typeof(Renderer));
		for (int length = componentsInChildren.Length; i < length; i++)
		{
			((Renderer)componentsInChildren[i]).enabled = @bool;
		}
	}

	public virtual void Main()
	{
	}
}
