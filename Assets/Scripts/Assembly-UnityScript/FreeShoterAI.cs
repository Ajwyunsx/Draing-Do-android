using System;
using UnityEngine;

[Serializable]
public class FreeShoterAI : MonoBehaviour
{
	public AudioClip SFX;

	public int period;

	public int ShotPower;

	public int Distance;

	private int timer;

	private Transform myTransform;

	public GameObject ShotGameObject;

	private bool Disappear;

	public bool ShotByObjectAngle;

	public bool lockShot;

	private int disappearTimer;

	private Renderer[] fadeRenderers;

	private SpriteRenderer[] fadeSpriteRenderers;

	private tk2dBaseSprite[] fadeTk2dSprites;

	public FreeShoterAI()
	{
		period = 150;
		Distance = 7;
	}

	public virtual void Start()
	{
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (Disappear)
		{
			UpdateDisappear();
			return;
		}
		if (lockShot)
		{
			return;
		}
		timer++;
		if (timer < period)
		{
			return;
		}
		timer = 0;
		if (!(Mathf.Abs(Global.CurrentPlayerObject.position.x - myTransform.position.x) + Mathf.Abs(Global.CurrentPlayerObject.position.y - myTransform.position.y) >= (float)Distance))
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ShotGameObject, myTransform.position, Quaternion.identity) as GameObject;
			if (!ShotByObjectAngle)
			{
				float z = Mathf.Atan2(Global.CurrentPlayerObject.position.y - myTransform.position.y, Global.CurrentPlayerObject.position.x - myTransform.position.x) * 57.29578f + 90f + UnityEngine.Random.Range(-15f, 15f);
				Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
				float num = (eulerAngles.z = z);
				Vector3 vector = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
			}
			else if (!(transform.parent.transform.lossyScale.x <= 0f))
			{
				float z2 = transform.eulerAngles.z;
				Vector3 eulerAngles2 = Global.LastCreatedObject.transform.eulerAngles;
				float num2 = (eulerAngles2.z = z2);
				Vector3 vector3 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles2);
			}
			else
			{
				float z3 = transform.eulerAngles.z + 180f;
				Vector3 eulerAngles3 = Global.LastCreatedObject.transform.eulerAngles;
				float num3 = (eulerAngles3.z = z3);
				Vector3 vector5 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles3);
			}
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			if (ShotPower != 0)
			{
				Global.LastCreatedObject.SendMessage("ShotPower", ShotPower, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void DISAPPEAR()
	{
		if (Disappear)
		{
			return;
		}
		Disappear = true;
		disappearTimer = 60;
		CacheFadeTargets();
		PlayDeathVisual();
		Collider component = GetComponent<Collider>();
		if ((bool)component)
		{
			component.enabled = false;
		}
		Collider[] componentsInChildren = GetComponentsInChildren<Collider>(true);
		foreach (Collider collider in componentsInChildren)
		{
			collider.enabled = false;
		}
	}

	public virtual void LockShot(bool @bool)
	{
		lockShot = @bool;
	}

	public virtual void Main()
	{
	}

	private void UpdateDisappear()
	{
		ApplyFade(Mathf.Clamp01((float)disappearTimer / 60f));
		disappearTimer--;
		if (disappearTimer <= 0)
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
