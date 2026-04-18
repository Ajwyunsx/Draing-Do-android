using System;
using UnityEngine;

[Serializable]
public class Shoter : MonoBehaviour
{
	public AudioClip SFXFall;

	public AudioClip SFXShot;

	public AudioClip SFXHit;

	public int period;

	public int Distance;

	private int timer;

	private Transform myTransform;

	public GameObject ShotGameObject;

	public int CorrectionShotAngle;

	public int HP;

	public int POWER;

	private bool Disappear;

	private int disappear_timer;

	private float Alpha;

	private tk2dSprite sprite;

	private int InvincibleTimer;

	public bool NoStomp;

	public bool Invincible;

	public bool Parent;

	private GameObject goParent;

	private Transform goTransform;

	public GameObject particleStrike;

	private int lastInvincibleTimer;

	private Renderer[] fadeRenderers;

	private SpriteRenderer[] fadeSpriteRenderers;

	private tk2dBaseSprite[] fadeTk2dSprites;

	public Shoter()
	{
		period = 150;
		Distance = 5;
		HP = 1;
		POWER = 1;
		Alpha = 1f;
	}

	public virtual void Start()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		if (GetComponent<Renderer>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}
		myTransform = transform;
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		if (Parent)
		{
			goParent = new GameObject();
			goTransform = goParent.transform;
			goTransform.position = myTransform.position;
			goTransform.parent = transform.parent;
			myTransform.parent = null;
		}
	}

	public virtual void Update()
	{
		if (Parent)
		{
			if ((bool)goParent)
			{
				myTransform.position = goTransform.position;
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (InvincibleTimer > 0)
		{
			InvincibleTimer--;
		}
		if (!Disappear)
		{
			timer++;
			if (timer >= period)
			{
				timer = 0;
				if (GetComponent<Renderer>().isVisible && (!(GetComponent<AudioSource>().clip == SFXShot) || !GetComponent<AudioSource>().isPlaying))
				{
					GetComponent<AudioSource>().clip = SFXShot;
					GetComponent<AudioSource>().Play();
				}
				if (!(Mathf.Abs(Global.CurrentPlayerObject.position.x - myTransform.position.x) + Mathf.Abs(Global.CurrentPlayerObject.position.y - myTransform.position.y) >= (float)Distance))
				{
					Global.LastCreatedObject = UnityEngine.Object.Instantiate(ShotGameObject, myTransform.position, Quaternion.identity) as GameObject;
					int num = -1;
					Vector3 position = Global.LastCreatedObject.transform.position;
					float num2 = (position.z = num);
					Vector3 vector = (Global.LastCreatedObject.transform.position = position);
					Global.LastCreatedObject.transform.eulerAngles = myTransform.transform.eulerAngles;
					float z = Global.LastCreatedObject.transform.eulerAngles.z + (float)CorrectionShotAngle;
					Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
					float num3 = (eulerAngles.z = z);
					Vector3 vector3 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
				}
			}
		}
		else
		{
			Alpha = Mathf.Clamp01((float)disappear_timer / 60f);
			ApplyFade(Alpha);
			disappear_timer--;
			if (disappear_timer <= 0)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (Disappear || other.tag != "Player")
		{
			return;
		}
		if (InvincibleTimer <= 0)
		{
			if (Global.Stomp && !(other.transform.position.y <= GetComponent<Collider>().bounds.center.y))
			{
				CrushHP(1);
				lastInvincibleTimer = UnityEngine.Random.Range(35, 100);
				InvincibleTimer = lastInvincibleTimer;
				other.gameObject.SendMessage("FromEnemyJump", myTransform.position.y, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				other.gameObject.SendMessage("TouchDanger", new Vector3(myTransform.position.x, myTransform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			other.gameObject.SendMessage("FromEnemyJump", myTransform.position.y, SendMessageOptions.DontRequireReceiver);
			if (InvincibleTimer < lastInvincibleTimer - 5)
			{
				other.gameObject.SendMessage("TouchDanger", new Vector3(myTransform.position.x, myTransform.position.y, 0f), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (Disappear || other.gameObject.tag != "Player")
		{
			return;
		}
		if (InvincibleTimer <= 0)
		{
			if (Global.Stomp && !(other.transform.position.y <= GetComponent<Collider>().bounds.center.y) && !NoStomp)
			{
				CrushHP(1);
				lastInvincibleTimer = UnityEngine.Random.Range(35, 100);
				InvincibleTimer = lastInvincibleTimer;
				other.gameObject.SendMessage("FromEnemyJump", myTransform.position.y, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				other.gameObject.SendMessage("TouchDanger", new Vector3(myTransform.position.x, myTransform.position.y, POWER), SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			other.gameObject.SendMessage("FromEnemyJump", myTransform.position.y, SendMessageOptions.DontRequireReceiver);
			if (InvincibleTimer < lastInvincibleTimer - 5)
			{
				other.gameObject.SendMessage("TouchDanger", new Vector3(myTransform.position.x, myTransform.position.y, 0f), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void CrushHP(int hp)
	{
		if (InvincibleTimer > 0)
		{
			return;
		}
		InvincibleTimer = 40;
		if (Invincible || Disappear)
		{
			return;
		}
		if ((bool)particleStrike)
		{
			UnityEngine.Object.Instantiate(particleStrike, myTransform.position + new Vector3(0f, 0.25f, -0.5f), Quaternion.identity);
		}
		HP -= hp;
		if ((bool)SFXHit)
		{
			AudioSource.PlayClipAtPoint(SFXHit, transform.position);
		}
		if (HP <= 0)
		{
			InvincibleTimer = 100;
			if ((bool)SFXFall)
			{
				AudioSource.PlayClipAtPoint(SFXFall, transform.position);
			}
			if ((bool)myTransform.parent)
			{
				myTransform.parent.SendMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
			}
			gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void DISAPPEAR()
	{
		Disappear = true;
		disappear_timer = 60;
		CacheFadeTargets();
		PlayDeathVisual();
		GetComponent<Collider>().enabled = false;
		if (!GetComponent<Rigidbody>())
		{
			Rigidbody rigidbody = (Rigidbody)gameObject.AddComponent(typeof(Rigidbody));
		}
		int num = UnityEngine.Random.Range(-2, 2);
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num2 = (velocity.x = num);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		int num3 = UnityEngine.Random.Range(-7, -4);
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num4 = (velocity2.y = num3);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
		GetComponent<Rigidbody>().useGravity = true;
		int num5 = UnityEngine.Random.Range(-20, 20);
		Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
		float num6 = (angularVelocity.z = num5);
		Vector3 vector5 = (GetComponent<Rigidbody>().angularVelocity = angularVelocity);
		GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)48;
	}

	public virtual void Main()
	{
	}

	private void CacheFadeTargets()
	{
		fadeRenderers = GetComponentsInChildren<Renderer>(true);
		fadeSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
		fadeTk2dSprites = GetComponentsInChildren<tk2dBaseSprite>(true);
	}

	private void ApplyFade(float alpha)
	{
		if (fadeSpriteRenderers != null)
		{
			SpriteRenderer[] array = fadeSpriteRenderers;
			foreach (SpriteRenderer spriteRenderer in array)
			{
				if ((bool)spriteRenderer)
				{
					Color color = spriteRenderer.color;
					color.a = alpha;
					spriteRenderer.color = color;
				}
			}
		}
		if (fadeTk2dSprites != null)
		{
			tk2dBaseSprite[] array2 = fadeTk2dSprites;
			foreach (tk2dBaseSprite tk2dBaseSprite in array2)
			{
				if ((bool)tk2dBaseSprite)
				{
					Color color2 = tk2dBaseSprite.color;
					color2.a = alpha;
					tk2dBaseSprite.color = color2;
				}
			}
		}
		if (fadeRenderers == null)
		{
			return;
		}
		Renderer[] array3 = fadeRenderers;
		foreach (Renderer renderer in array3)
		{
			if (!(renderer == null) && renderer.sharedMaterial != null)
			{
				Color color3 = renderer.material.color;
				color3.a = alpha;
				renderer.material.color = color3;
				if (renderer.material.HasProperty("_TintColor"))
				{
					Color color4 = renderer.material.GetColor("_TintColor");
					color4.a = alpha;
					renderer.material.SetColor("_TintColor", color4);
				}
			}
		}
	}

	private void PlayDeathVisual()
	{
		Animator component = GetComponent<Animator>();
		if ((bool)component)
		{
			TrySetTrigger(component, "death");
			TrySetTrigger(component, "die");
			TrySetTrigger(component, "hurt");
		}
		Animation component2 = GetComponent<Animation>();
		if ((bool)component2)
		{
			AnimationClip animationClip = FindLegacyClip(component2, "death", "die", "hurt", "shock");
			if ((bool)animationClip)
			{
				component2.CrossFade(animationClip.name, 0.1f);
			}
		}
	}

	private void TrySetTrigger(Animator animator, string parameterName)
	{
		AnimatorControllerParameter[] parameters = animator.parameters;
		foreach (AnimatorControllerParameter animatorControllerParameter in parameters)
		{
			if (animatorControllerParameter.type == AnimatorControllerParameterType.Trigger && animatorControllerParameter.name == parameterName)
			{
				animator.SetTrigger(parameterName);
				break;
			}
		}
	}

	private AnimationClip FindLegacyClip(Animation animationComponent, params string[] keywords)
	{
		foreach (string text in keywords)
		{
			if ((bool)animationComponent.GetClip(text))
			{
				return animationComponent.GetClip(text);
			}
		}
		return null;
	}
}
