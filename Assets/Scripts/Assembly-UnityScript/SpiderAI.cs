using System;
using UnityEngine;

[Serializable]
public class SpiderAI : MonoBehaviour
{
	private Vector3 startPosition;

	public float speed;

	public bool IsAnimSprite;

	private tk2dSpriteAnimator animatedSprite;

	private string action;

	public GameObject COCON;

	private GameObject cocon;

	private float alph;

	private Renderer rend;

	private int GameOverTimer;

	public SpiderAI()
	{
		speed = 1f;
	}

	public virtual void Awake()
	{
		if (IsAnimSprite)
		{
			animatedSprite = (tk2dSpriteAnimator)gameObject.GetComponent(typeof(tk2dSpriteAnimator));
		}
	}

	public virtual void Start()
	{
		startPosition = transform.position;
	}

	public virtual void FixedUpdate()
	{
		if ((bool)Cobweb.Catch)
		{
			transform.position = Vector3.MoveTowards(transform.position, Cobweb.Catch.position + new Vector3(0f, 0.5f, -0.1f), speed * 0.25f);
			if ((bool)cocon)
			{
				alph += 0.01f;
			}
		}
		else
		{
			if ((bool)cocon)
			{
				rend = null;
				if ((bool)Global.CurrentPlayerObject)
				{
					UnityEngine.Object.Destroy(cocon);
				}
				else
				{
					cocon = null;
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * 0.5f);
		}
		if (!cocon)
		{
			Animate("spider");
		}
		else
		{
			Animate("zamot");
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if ((bool)Cobweb.Catch && !(other.gameObject.tag != "Player"))
		{
			MakeCocon();
		}
	}

	public virtual void Animate(string anim)
	{
		if (action != anim)
		{
			action = anim;
			animatedSprite.Play(anim);
		}
	}

	public virtual void MakeCocon()
	{
		if (!Global.CurrentPlayerObject)
		{
			return;
		}
		if (cocon == null)
		{
			cocon = UnityEngine.Object.Instantiate(COCON, Global.CurrentPlayerObject.position, Quaternion.identity);
			rend = cocon.GetComponent<Renderer>();
			float a = 0f;
			Color color = rend.material.color;
			float num = (color.a = a);
			Color color2 = (rend.material.color = color);
			alph = 0f;
			return;
		}
		float a2 = alph;
		Color color4 = rend.material.color;
		float num2 = (color4.a = a2);
		Color color5 = (rend.material.color = color4);
		if (!(alph < 1f))
		{
			Global.HP = 0f;
			CameraFade.SpeedFactor = 0.25f;
			Global.CurrentPlayerObject.SendMessage("DeadPony", null, SendMessageOptions.DontRequireReceiver);
			UnityEngine.Object.Destroy(Global.CurrentPlayerObject.gameObject);
		}
	}

	public virtual void Update()
	{
		if ((bool)cocon && (bool)Global.CurrentPlayerObject)
		{
			cocon.transform.position = Global.CurrentPlayerObject.position + new Vector3(0f, 0f, -0.02f);
		}
	}

	public virtual void Main()
	{
	}
}
