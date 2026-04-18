using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RandomFlyAI : MonoBehaviour
{
	public AudioClip SFXFall;

	public AudioClip SFXHit;

	public int HP;

	public int POWER;

	public float Speed;

	public int ProcentToHeroMove;

	public int toStartPlace;

	private Vector3 StartPlace;

	public int MinTimer;

	public int MaxTimer;

	public float RadiusRandom;

	public int Distance;

	public bool Sleep;

	public bool changeDir;

	private Vector3 Target;

	private Transform myTransform;

	private int InvincibleTimer;

	private bool Disappear;

	private int disappear_timer;

	private int timer;

	private int shockTimer;

	public bool NoStomp;

	public bool Invincible;

	private Vector3 StartScale;

	private int Dir;

	public GameObject particleStrike;

	public AnimationClip AnimationStop;

	public AnimationClip AnimationHurt;

	private int lastInvincibleTimer;

	private Renderer[] fadeRenderers;

	private SpriteRenderer[] fadeSpriteRenderers;

	private tk2dBaseSprite[] fadeTk2dSprites;

	public RandomFlyAI()
	{
		HP = 1;
		POWER = 1;
		Speed = 4f;
		ProcentToHeroMove = 25;
		MinTimer = 50;
		MaxTimer = 150;
		RadiusRandom = 5f;
		Distance = 9;
		Sleep = true;
		Dir = 1;
	}

	public virtual void Awake()
	{
		myTransform = transform;
		StartScale = myTransform.localScale;
		StartPlace = myTransform.position;
		InvincibleTimer = 50;
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
	}

	public virtual void Update()
	{
		if (changeDir)
		{
			float x = (float)Dir * StartScale.x;
			Vector3 localScale = myTransform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (myTransform.localScale = localScale);
		}
	}

	public virtual void FixedUpdate()
	{
		if (Disappear)
		{
			DisFunction();
			return;
		}
		if (InvincibleTimer > 0)
		{
			InvincibleTimer--;
			if (InvincibleTimer == 0 && (bool)AnimationStop)
			{
				GetComponent<Animation>().CrossFade(AnimationStop.name, 0.25f);
			}
		}
		if (!Sleep)
		{
			if (shockTimer <= 0)
			{
				timer--;
				if (timer <= 0)
				{
					if (UnityEngine.Random.Range(1, 100) <= ProcentToHeroMove)
					{
						timer = UnityEngine.Random.Range(MinTimer, MaxTimer);
						if (toStartPlace == 0)
						{
							Target.x = Global.CurrentPlayerObject.position.x + (float)UnityEngine.Random.Range(-1, 1);
							Target.y = Global.CurrentPlayerObject.position.y + (float)UnityEngine.Random.Range(-1, 1);
						}
						else
						{
							Target.x = StartPlace.x + (float)UnityEngine.Random.Range(-toStartPlace, toStartPlace);
							Target.y = StartPlace.y + (float)UnityEngine.Random.Range(-toStartPlace, toStartPlace);
						}
						if (changeDir)
						{
							if (!(myTransform.position.x <= Target.x))
							{
								Dir = 1;
							}
							else
							{
								Dir = -1;
							}
						}
						return;
					}
					timer = UnityEngine.Random.Range(MinTimer, MaxTimer);
					Target.x = myTransform.position.x + UnityEngine.Random.Range(0f - RadiusRandom, RadiusRandom);
					Target.y = myTransform.position.y + UnityEngine.Random.Range(0f - RadiusRandom, RadiusRandom);
					if (changeDir)
					{
						if (!(myTransform.position.x <= Target.x))
						{
							Dir = 1;
						}
						else
						{
							Dir = -1;
						}
					}
				}
				else
				{
					Vector3 normalized = (Target - myTransform.position).normalized;
					float x = normalized.x * Speed;
					Vector3 velocity = GetComponent<Rigidbody>().velocity;
					float num = (velocity.x = x);
					Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
					float y = normalized.y * Speed;
					Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
					float num2 = (velocity2.y = y);
					Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
				}
			}
			else
			{
				shockTimer--;
			}
		}
		else if (!(Mathf.Abs(Global.CurrentPlayerObject.position.x - myTransform.position.x) + Mathf.Abs(Global.CurrentPlayerObject.position.y - myTransform.position.y) >= (float)Distance))
		{
			Sleep = false;
		}
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (!(other.gameObject.tag == "Player"))
		{
			return;
		}
		if (InvincibleTimer <= 0)
		{
			if (Global.Stomp && !(other.transform.position.y <= GetComponent<Collider>().bounds.center.y) && !NoStomp)
			{
				CrushHP(2);
				if ((bool)GetComponent<Rigidbody>())
				{
					int num = UnityEngine.Random.Range(-5, 5);
					Vector3 velocity = GetComponent<Rigidbody>().velocity;
					float num2 = (velocity.x = num);
					Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
					int num3 = UnityEngine.Random.Range(-5, 5);
					Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
					float num4 = (velocity2.y = num3);
					Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
				}
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
			if ((bool)GetComponent<Rigidbody>())
			{
				int num5 = UnityEngine.Random.Range(-5, 5);
				Vector3 velocity3 = GetComponent<Rigidbody>().velocity;
				float num6 = (velocity3.x = num5);
				Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity3);
				int num7 = UnityEngine.Random.Range(-5, 5);
				Vector3 velocity4 = GetComponent<Rigidbody>().velocity;
				float num8 = (velocity4.y = num7);
				Vector3 vector7 = (GetComponent<Rigidbody>().velocity = velocity4);
			}
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
		if ((bool)AnimationHurt)
		{
			GetComponent<Animation>().CrossFade(AnimationHurt.name, 0.25f);
		}
		shockTimer = 50;
		timer = 0;
		int num = UnityEngine.Random.Range(-4, 4);
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num2 = (velocity.x = num);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		int num3 = UnityEngine.Random.Range(-2, 2);
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num4 = (velocity2.y = num3);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
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
			if (!(GetComponent<AudioSource>().clip == SFXFall) || !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().clip = SFXFall;
				GetComponent<AudioSource>().Play();
			}
			gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void DISAPPEAR()
	{
		Global.LevelEnemy++;
		Disappear = true;
		disappear_timer = 60;
		CacheFadeTargets();
		PlayDeathVisual();
		GetComponent<Collider>().enabled = false;
		float y = 0f - myTransform.localScale.y;
		Vector3 localScale = myTransform.localScale;
		float num = (localScale.y = y);
		Vector3 vector = (myTransform.localScale = localScale);
		if (!GetComponent<Rigidbody>())
		{
			Rigidbody rigidbody = (Rigidbody)gameObject.AddComponent(typeof(Rigidbody));
		}
		GetComponent<Rigidbody>().useGravity = true;
		int num2 = UnityEngine.Random.Range(-1, 1);
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num3 = (velocity.x = num2);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity);
		int num4 = UnityEngine.Random.Range(-2, -3);
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num5 = (velocity2.y = num4);
		Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity2);
		int num6 = UnityEngine.Random.Range(-20, 20);
		Vector3 angularVelocity = GetComponent<Rigidbody>().angularVelocity;
		float num7 = (angularVelocity.z = num6);
		Vector3 vector7 = (GetComponent<Rigidbody>().angularVelocity = angularVelocity);
		GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)48;
	}

	public virtual void DisFunction()
	{
		float alpha = Mathf.Clamp01((float)disappear_timer / 60f);
		ApplyFade(alpha);
		disappear_timer--;
		if (disappear_timer <= 0)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
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
			TrySetTrigger(component, "shock");
		}
		Animation component2 = GetComponent<Animation>();
		if ((bool)component2)
		{
			AnimationClip animationClip = FindLegacyClip(component2, "death", "die", "hurt", "shock");
			if ((bool)animationClip)
			{
				component2.CrossFade(animationClip.name, 0.1f);
			}
			else if ((bool)AnimationHurt)
			{
				component2.CrossFade(AnimationHurt.name, 0.1f);
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
		IEnumerator enumerator = animationComponent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.Current;
				string lowerInvariant = animationState.name.ToLowerInvariant();
				foreach (string text2 in keywords)
				{
					if (lowerInvariant.Contains(text2))
					{
						return animationState.clip;
					}
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	public virtual void InWater()
	{
		if (!Global.Pause)
		{
			timer = UnityEngine.Random.Range(10, 25);
			Target.y += UnityEngine.Random.Range(0.5f, 2.5f);
		}
	}

	public virtual void Main()
	{
	}
}
