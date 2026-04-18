using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
	public AudioClip SoundClip;

	public string NameItem;

	public string Type;

	public int Parameter;

	public string NameParameter;

	public bool JustBulletsParameter;

	public bool correctScale;

	[HideInInspector]
	public int Timer;

	public bool NoLifeTimer;

	public int Life;

	[HideInInspector]
	public bool Delete;

	[HideInInspector]
	public float Alpha;

	[HideInInspector]
	public tk2dSprite sprite;

	[HideInInspector]
	public bool GetItem;

	[HideInInspector]
	public bool _Appear;

	public int Gold;

	public bool ClickForGet;

	private Vector3 GetScale;

	private int ScaleTimer;

	private Transform MyTransform;

	public bool GrowEffects;

	public GameObject ParticlePlus;

	public bool DeleteNow;

	public GameObject priceSign;

	public bool blinked;

	private int blinkTimer;

	private int blinkInt;

	private Color clr;

	private bool onceGet;

	private int BeginTimer;

	private bool falling;

	public bool ScaleByHP;

	public Item()
	{
		correctScale = true;
		Life = 1000;
		Alpha = 1f;
		ScaleTimer = 50;
		GrowEffects = true;
		blinkInt = 300;
		clr = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	}

	public virtual void Awake()
	{
		if (ClickForGet)
		{
			GetComponent<Collider>().enabled = false;
			BeginTimer = 50;
		}
		else
		{
			BeginTimer = 0;
		}
		MyTransform = transform;
		GetScale = MyTransform.localScale;
		if (GrowEffects)
		{
			MyTransform.localScale = new Vector3(0f, 0f, 0f);
		}
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
	}

	public virtual void Start()
	{
		if ((bool)priceSign)
		{
			priceSign = UnityEngine.Object.Instantiate(priceSign, Global.CurrentPlayerObject.position + new Vector3(-20f, 0f, -2f), Quaternion.identity) as GameObject;
			if (Gold > 0)
			{
				priceSign.BroadcastMessage("Rename", Gold + " $", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				priceSign.BroadcastMessage("Rename", "Free", SendMessageOptions.DontRequireReceiver);
			}
			priceSign.transform.parent = transform;
			priceSign.transform.position = new Vector3(transform.position.x, GetComponent<Collider>().bounds.center.y, transform.position.z);
		}
		float x = transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f);
		Vector3 position = transform.position;
		float num = (position.x = x);
		Vector3 vector = (transform.position = position);
	}

	public virtual void FixedUpdate()
	{
		if (BeginTimer > 0)
		{
			BeginTimer--;
			if (BeginTimer <= 0)
			{
				GetComponent<Collider>().enabled = true;
			}
			return;
		}
		if (blinked && !GetItem && (bool)GetComponent<Renderer>())
		{
			blinkTimer++;
			blinkInt = 100;
			if (blinkTimer > blinkInt)
			{
				float num = default(float);
				if (blinkTimer > blinkInt)
				{
					num = 2f;
				}
				if (blinkTimer > blinkInt + 15)
				{
					num = 0.5f;
				}
				if (blinkTimer == blinkInt + 50)
				{
					blinkTimer = 0;
				}
				clr = Color.Lerp(clr, new Color(num, num, num, 0.5f), 0.1f);
				GetComponent<Renderer>().material.SetColor("_TintColor", clr);
			}
		}
		if (GrowEffects && ScaleTimer > 0)
		{
			ScaleTimer--;
			MyTransform.localScale = Vector3.Lerp(MyTransform.localScale, GetScale, 0.1f);
		}
		if (_Appear)
		{
			Alpha += 0.03f;
			float y = transform.position.y + Mathf.Sin(Alpha * 6f) * 0.1f;
			Vector3 position = transform.position;
			float num2 = (position.y = y);
			Vector3 vector = (transform.position = position);
			sprite.color = new Color(1f, 1f, 1f, Alpha);
			GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, Alpha);
			if (!(Alpha < 1f))
			{
				_Appear = false;
				if ((bool)gameObject.GetComponent<Collider>())
				{
					gameObject.GetComponent<Collider>().enabled = true;
				}
			}
			return;
		}
		if (!NoLifeTimer)
		{
			if (Life > 0)
			{
				Life--;
			}
			else
			{
				Delete = true;
			}
		}
		if (!Delete)
		{
			return;
		}
		if (Timer == 0)
		{
			if ((bool)gameObject.GetComponent<Collider>())
			{
				gameObject.GetComponent<Collider>().enabled = false;
			}
			if (!GetItem)
			{
			}
		}
		Timer++;
		if ((bool)sprite)
		{
			Alpha -= 0.01f;
			sprite.color = new Color(1f, 1f, 1f, Alpha);
			GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, Alpha);
		}
		if (Timer >= 60)
		{
			float x = Mathf.Lerp(MyTransform.localScale.x, 0f, Time.deltaTime * 7f);
			Vector3 localScale = MyTransform.localScale;
			float num3 = (localScale.x = x);
			Vector3 vector3 = (MyTransform.localScale = localScale);
			float x2 = MyTransform.localScale.x;
			Vector3 localScale2 = MyTransform.localScale;
			float num4 = (localScale2.y = x2);
			Vector3 vector5 = (MyTransform.localScale = localScale2);
			float x3 = MyTransform.localScale.x;
			Vector3 localScale3 = MyTransform.localScale;
			float num5 = (localScale3.z = x3);
			Vector3 vector7 = (MyTransform.localScale = localScale3);
		}
		if (Timer > 90)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void LateUpdate()
	{
		if (GetItem)
		{
			float y = Mathf.Lerp(MyTransform.position.y, Global.CurrentPlayerObject.position.y + 1.3f, Time.deltaTime * 8f);
			Vector3 position = MyTransform.position;
			float num = (position.y = y);
			Vector3 vector = (MyTransform.position = position);
			float x = Mathf.Lerp(MyTransform.position.x, Global.CurrentPlayerObject.position.x, Time.deltaTime * 15f);
			Vector3 position2 = MyTransform.position;
			float num2 = (position2.x = x);
			Vector3 vector3 = (MyTransform.position = position2);
			float z = Global.CurrentPlayerObject.position.z - 3f;
			Vector3 position3 = MyTransform.position;
			float num3 = (position3.z = z);
			Vector3 vector5 = (MyTransform.position = position3);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (Gold <= 0 && !ClickForGet && other.gameObject.layer == 8)
		{
			GetItemFunction();
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!ClickForGet && Gold <= 0 && other.gameObject.layer == 8)
		{
			GetItemFunction();
		}
	}

	public virtual void OnMouseUpAsButton()
	{
		if ((bool)SlotItem.selected || (Gold <= 0 && !ClickForGet))
		{
			return;
		}
		if (Gold > 0)
		{
			if (Global.Gold < Gold)
			{
				if (!(GetComponent<AudioSource>().clip == Global.SFXNo) || !GetComponent<AudioSource>().isPlaying)
				{
					Global.GoldBarEffect = true;
					GetComponent<AudioSource>().clip = LoadData.SFX("no");
					GetComponent<AudioSource>().Play();
				}
				return;
			}
			if (!(GetComponent<AudioSource>().clip == Global.SFXBuy) || !GetComponent<AudioSource>().isPlaying)
			{
				Global.GoldBarEffect = true;
				GetComponent<AudioSource>().clip = Global.SFXBuy;
				GetComponent<AudioSource>().Play();
			}
			Global.Gold -= Gold;
			Gold = 0;
		}
		GetItemFunction();
	}

	public virtual void GetItemFunction()
	{
		if (BeginTimer > 0 || onceGet)
		{
			return;
		}
		onceGet = true;
		if ((bool)GetComponent<Renderer>() && blinked)
		{
			GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
		}
		switch (Type)
		{
		case "Item":
			Delete = true;
			GetItem = true;
			break;
		case "Invincible":
			Delete = true;
			GetItem = true;
			break;
		case "Strike":
			Delete = true;
			GetItem = true;
			break;
		case "Bouncy":
			Delete = true;
			GetItem = true;
			break;
		case "Freeze":
			Delete = true;
			GetItem = true;
			break;
		case "Companion":
			Delete = true;
			GetItem = true;
			break;
		case "Var":
			Global.Var[NameParameter] = Convert.ToInt32(Global.Var[NameParameter]) + Parameter;
			Delete = true;
			GetItem = true;
			break;
		case "Planka":
			Delete = true;
			GetItem = true;
			break;
		case "Teleport":
			Delete = true;
			GetItem = true;
			break;
		case "Balls":
			Delete = true;
			GetItem = true;
			break;
		case "EasyBall":
			Delete = true;
			GetItem = true;
			break;
		case "Ball":
			Delete = true;
			GetItem = true;
			break;
		case "Star":
			Delete = true;
			GetItem = true;
			break;
		case "Gold":
		case "gold":
			Global.Gold += Parameter;
			Global.LevelGold += Parameter;
			Delete = true;
			GetItem = true;
			Global.CreateText("+ " + Parameter + " Gold", transform.position + new Vector3(0f, 0.75f, -2f), new Color(1f, 1f, 0.8f, 1f), 0f);
			if (Global.Gold < 0)
			{
				Global.Gold = 0;
			}
			break;
		case "HP":
			if (Parameter > 0)
			{
				if (!(Global.HP <= 0f))
				{
					Global.HP += Parameter;
					Delete = true;
					GetItem = true;
					if (!(Global.HP <= Global.MaxHP))
					{
						Global.HP = Global.MaxHP;
					}
				}
			}
			else
			{
				Global.CurrentPlayerObject.SendMessage("OnEnemyCollision", -Parameter, SendMessageOptions.DontRequireReceiver);
				Delete = true;
				GetItem = true;
			}
			break;
		case "Tool":
		case "tool":
			Delete = true;
			GetItem = true;
			break;
		case "cloth":
			Global.GetThatCloth(NameParameter);
			Delete = true;
			GetItem = true;
			break;
		case "DoubleJump":
			Global.DoubleJump = true;
			Global.GameMessageCreate("double jump");
			Delete = true;
			GetItem = true;
			break;
		case "SwimFast":
			Global.SwimFast = true;
			Global.GameMessageCreate("swim fast");
			Delete = true;
			GetItem = true;
			break;
		case "FonarSkill":
			Global.FonarSkill = true;
			Global.GameMessageCreate("light skill");
			Delete = true;
			GetItem = true;
			break;
		case "MaxHP":
			Global.MaxHP += 1f;
			Global.HP += 1f;
			Global.GameMessageCreate("max hp");
			Delete = true;
			GetItem = true;
			break;
		default:
			Delete = true;
			GetItem = true;
			break;
		}
		gameObject.BroadcastMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
		if ((bool)ParticlePlus)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ParticlePlus) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position;
			float z = Global.LastCreatedObject.transform.position.z - 0.3f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
		}
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
		if ((bool)SoundClip && (Life > 0 || NoLifeTimer))
		{
			AudioSource.PlayClipAtPoint(SoundClip, transform.position);
		}
		Global.LastCreatedObject.transform.position = transform.position;
		float z2 = Global.LastCreatedObject.transform.position.z - 0.2f;
		Vector3 position2 = Global.LastCreatedObject.transform.position;
		float num2 = (position2.z = z2);
		Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
		if (DeleteNow)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void OnAppear()
	{
		_Appear = true;
		Alpha = 0f;
		sprite.color = new Color(1f, 1f, 1f, Alpha);
		GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, Alpha);
		if (correctScale)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if ((bool)gameObject.GetComponent<Collider>())
		{
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

	public virtual void TimerOn()
	{
		NoLifeTimer = false;
		Life = 750;
	}

	public virtual void DestroyItemScript()
	{
		MyTransform.localScale = GetScale;
		UnityEngine.Object.Destroy(this);
	}

	public virtual void Falling(Transform prnt)
	{
		if ((bool)prnt)
		{
			transform.parent = prnt;
		}
		falling = true;
		NoLifeTimer = false;
		Life = 150;
	}

	public virtual void StopMiniGame()
	{
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void Detach()
	{
		if ((bool)transform.parent)
		{
			transform.parent = null;
		}
		GetComponent<Collider>().enabled = true;
		int num = 0;
		Vector3 eulerAngles = transform.eulerAngles;
		float num2 = (eulerAngles.z = num);
		Vector3 vector = (transform.eulerAngles = eulerAngles);
	}

	public virtual void ScaleByEnemyHP(float num)
	{
		if (ScaleByHP)
		{
			Parameter = (int)num;
			if (Parameter <= 0)
			{
				Parameter = 1;
			}
			float num2 = 0.4f + 0.019f * (float)Parameter;
			if (!(num2 <= 2f))
			{
				num2 = 2f;
			}
			MonoBehaviour.print("SIZE: " + num2);
			float x = num2;
			Vector3 localScale = transform.localScale;
			float num3 = (localScale.x = x);
			Vector3 vector = (transform.localScale = localScale);
			float y = num2;
			Vector3 localScale2 = transform.localScale;
			float num4 = (localScale2.y = y);
			Vector3 vector3 = (transform.localScale = localScale2);
		}
	}

	public virtual void OnMouseOver()
	{
		if (ClickForGet)
		{
			if (Gold > 0)
			{
				Global.MouseMode = "gold";
			}
			else
			{
				Global.MouseMode = "just";
			}
			Monitor.LastOverTrans = transform;
			Monitor.TextA = NameItem;
			string empty = string.Empty;
			empty = ((Gold <= 0) ? (empty + "L.Mouse to get it ") : ((Global.Gold < Gold) ? ("Price: " + Gold + " gold. You don't have enough gold") : ("Price: " + Gold + " gold. L.Mouse to buy it")));
			Monitor.TextB = empty;
			Monitor.DontDrop = true;
			Monitor.MouseNo = false;
			Global.offBlockTimer = 5;
		}
	}

	public virtual void Main()
	{
	}
}
