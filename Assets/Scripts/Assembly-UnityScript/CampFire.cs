using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class CampFire : MonoBehaviour
{
	public int ID;

	private int timer;

	public int DirectionToRight;

	private int NoSaveTimer;

	private bool onceShow;

	public Light lite;

	public int TIME;

	public bool CheckDist;

	public bool Tent;

	private int Upgrader;

	private int food;

	public bool NO_SAVE;

	private ParticleSystem runtimeFireParticle;

	public CampFire()
	{
		DirectionToRight = 1;
		TIME = 1;
		CheckDist = true;
	}

	public virtual void Awake()
	{
		gameObject.BroadcastMessage("RenameID", ID, SendMessageOptions.DontRequireReceiver);
		EnsureFireVisuals();
		SetFireVisualActive((bool)lite && lite.enabled);
	}

	public virtual void FixedUpdate()
	{
		if (NoSaveTimer != 0)
		{
			NoSaveTimer--;
		}
		if (timer == 0)
		{
			return;
		}
		timer--;
		if (timer > 0)
		{
			return;
		}
		Global.ChangeTime(Global.DAYTIME + TIME, true);
		Global.DAYMINUTES = 0;
		Global.FadeMode = -1;
		if (!Tent)
		{
			Global.Var["food"] = food + UnityEngine.Random.Range(9, 12);
			if (Convert.ToInt32(Global.Var["food"]) > 100)
			{
				Global.Var["food"] = 100;
			}
			Global.Var["mind"] = Convert.ToInt32(Global.Var["mind"]) - UnityEngine.Random.Range(9, 12);
			if (Convert.ToInt32(Global.Var["mind"]) < 0)
			{
				Global.Var["mind"] = 0;
			}
		}
		else
		{
			Global.Var["food"] = 100;
			Global.Var["mind"] = 0;
			Global.Var["poison"] = 0;
			Global.HP = Global.MaxHP;
			float x = transform.position.x;
			Vector3 position = Global.CurrentPlayerObject.position;
			float num = (position.x = x);
			Vector3 vector = (Global.CurrentPlayerObject.position = position);
		}
		Global.CurrentPlayerObject.SendMessage("Cutscene", false, SendMessageOptions.DontRequireReceiver);
		Global.CurrentPlayerObject.SendMessage("IdleAnim", "sit", SendMessageOptions.DontRequireReceiver);
		if (Upgrader > 0)
		{
			Upgrade();
		}
	}

	public virtual void OnMouseDown()
	{
		ActON();
	}

	public virtual void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{
			ActON();
		}
	}

	public virtual void UseThatItem()
	{
		if (!SlotItem.selected)
		{
			ActON();
		}
	}

	public virtual void UpgradeIt(int num)
	{
		Upgrader = num;
	}

	public virtual void Upgrade()
	{
		Upgrader = 0;
		Global.Upgrades[ID] = Global.Upgrades[ID] + Upgrader;
		gameObject.BroadcastMessage("CheckUpgrade", null, SendMessageOptions.DontRequireReceiver);
		SaveIt();
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (NoSaveTimer <= 0 && !(other.tag != "Player"))
		{
			LiteOn();
			SaveIt();
		}
	}

	public virtual void ActON()
	{
		if (NoSaveTimer <= 0)
		{
			SlotItem.EscapeItem = true;
			LiteOn();
			if (Tent)
			{
				float x = transform.position.x;
				Vector3 position = Global.CurrentPlayerObject.position;
				float num = (position.x = x);
				Vector3 vector = (Global.CurrentPlayerObject.position = position);
			}
			Global.CurrentPlayerObject.SendMessage("Cutscene", true, SendMessageOptions.DontRequireReceiver);
			Global.FadeMode = 1;
			food = RuntimeServices.UnboxInt32(Global.Var["food"]);
			if (Upgrader == 0)
			{
				SaveIt();
			}
			timer = 60;
		}
	}

	public virtual void SaveIt()
	{
		if (!NO_SAVE)
		{
			if (!onceShow)
			{
				Global.GameMessageCreateOne("Game saved!");
				onceShow = true;
			}
			if (!RuntimeServices.EqualityOperator(Global.Var["survive"], 1))
			{
				Global.HP = Global.MaxHP;
			}
			SlotItem.EscapeItem = true;
			Global.CheckPointName = gameObject.name;
			Global.CheckPointDirectionToRight = DirectionToRight;
			Global.CheckPoint.x = transform.position.x;
			Global.CheckPoint.y = transform.position.y;
			Global.CheckPoint.z = -1f;
			Global.LEVEL = Application.loadedLevelName;
			Global.SAVE();
		}
	}

	public virtual void LiteOn()
	{
		EnsureFireVisuals();
		if ((bool)lite)
		{
			ParticleEmitter particleEmitter = null;
			if (transform.childCount > 0)
			{
				particleEmitter = (ParticleEmitter)transform.GetChild(0).gameObject.GetComponent(typeof(ParticleEmitter));
			}
			if (!(bool)particleEmitter)
			{
				Transform transform2 = transform.Find("fire");
				if ((bool)transform2)
				{
					particleEmitter = (ParticleEmitter)transform2.gameObject.GetComponent(typeof(ParticleEmitter));
				}
			}
			if ((bool)particleEmitter)
			{
				particleEmitter.emit = true;
			}
			AudioSource component = GetComponent<AudioSource>();
			if ((bool)component)
			{
				component.Play();
			}
			lite.enabled = true;
		}
		SetFireVisualActive(true);
	}

	private void EnsureFireVisuals()
	{
		if ((bool)runtimeFireParticle)
		{
			return;
		}
		Transform transform2 = transform.Find("RuntimeFireFX");
		if ((bool)transform2)
		{
			runtimeFireParticle = transform2.GetComponent<ParticleSystem>();
		}
		if ((bool)runtimeFireParticle)
		{
			return;
		}
		Material material = Resources.Load<Material>("campfire_flame");
		if (!(bool)material)
		{
			return;
		}
		Vector3 localPosition = new Vector3(0f, 0.16f, -0.02f);
		Transform transform3 = transform.Find("fire");
		if ((bool)transform3)
		{
			localPosition = transform3.localPosition;
		}
		GameObject gameObject = new GameObject("RuntimeFireFX");
		gameObject.layer = base.gameObject.layer;
		Transform transform4 = gameObject.transform;
		transform4.parent = transform;
		transform4.localPosition = localPosition;
		transform4.localRotation = Quaternion.identity;
		transform4.localScale = Vector3.one;
		runtimeFireParticle = gameObject.AddComponent<ParticleSystem>();
		ParticleSystemRenderer component = gameObject.GetComponent<ParticleSystemRenderer>();
		ParticleSystem.MainModule main = runtimeFireParticle.main;
		main.loop = true;
		main.playOnAwake = false;
		main.simulationSpace = ParticleSystemSimulationSpace.Local;
		main.maxParticles = 48;
		main.startLifetime = new ParticleSystem.MinMaxCurve(0.6f, 0.95f);
		main.startSpeed = new ParticleSystem.MinMaxCurve(0.08f, 0.22f);
		main.startSize = new ParticleSystem.MinMaxCurve(0.16f, 0.32f);
		main.startRotation = new ParticleSystem.MinMaxCurve(0f, 6.2831855f);
		main.startColor = new Color(1f, 0.95f, 0.85f, 0.95f);
		ParticleSystem.EmissionModule emission = runtimeFireParticle.emission;
		emission.enabled = false;
		emission.rateOverTime = 18f;
		ParticleSystem.ShapeModule shape = runtimeFireParticle.shape;
		shape.enabled = true;
		shape.shapeType = ParticleSystemShapeType.Cone;
		shape.angle = 16f;
		shape.radius = 0.035f;
		ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = runtimeFireParticle.sizeOverLifetime;
		sizeOverLifetime.enabled = true;
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.AddKey(0f, 0.25f);
		animationCurve.AddKey(0.35f, 1f);
		animationCurve.AddKey(1f, 0.15f);
		sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, animationCurve);
		ParticleSystem.ColorOverLifetimeModule colorOverLifetime = runtimeFireParticle.colorOverLifetime;
		colorOverLifetime.enabled = true;
		Gradient gradient = new Gradient();
		gradient.SetKeys(new GradientColorKey[3]
		{
			new GradientColorKey(new Color(1f, 0.95f, 0.85f), 0f),
			new GradientColorKey(new Color(1f, 0.75f, 0.35f), 0.45f),
			new GradientColorKey(new Color(0.85f, 0.2f, 0.05f), 1f)
		}, new GradientAlphaKey[3]
		{
			new GradientAlphaKey(0f, 0f),
			new GradientAlphaKey(0.92f, 0.15f),
			new GradientAlphaKey(0f, 1f)
		});
		colorOverLifetime.color = gradient;
		component.sharedMaterial = material;
		component.renderMode = ParticleSystemRenderMode.Billboard;
		component.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		component.receiveShadows = false;
		component.sortMode = ParticleSystemSortMode.Distance;
	}

	private void SetFireVisualActive(bool active)
	{
		if (!(bool)runtimeFireParticle)
		{
			return;
		}
		ParticleSystem.EmissionModule emission = runtimeFireParticle.emission;
		emission.enabled = active;
		if (active)
		{
			if (!runtimeFireParticle.isPlaying)
			{
				runtimeFireParticle.Play(true);
			}
		}
		else if (runtimeFireParticle.isPlaying)
		{
			runtimeFireParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	public virtual void ActivateByMouse()
	{
		ActON();
	}

	public virtual void Main()
	{
	}
}
