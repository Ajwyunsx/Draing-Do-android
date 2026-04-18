using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GetDropBox : MonoBehaviour
{
	public AudioClip SFX;

	public GameObject[] ObjectChance;

	private bool Once;

	public string BoxName;

	public int MinHPCrush;

	public bool isMystGlass;

	public OptionsDropBox dropBoxOptions;

	public Vector3 local_velocity;

	private Transform myTransform;

	public GameObject shardObject;

	public bool RandomZ;

	public GetDropBox()
	{
		MinHPCrush = 1;
	}

	public virtual void Start()
	{
		if (RandomZ)
		{
			int num = UnityEngine.Random.Range(0, 4) * 90;
			Vector3 localEulerAngles = transform.localEulerAngles;
			float num2 = (localEulerAngles.z = num);
			Vector3 vector = (transform.localEulerAngles = localEulerAngles);
		}
		myTransform = transform;
	}

	public virtual void FixedUpdate()
	{
		if (isMystGlass && Global.BreakGlassTimer > 0 && !(Vector2.Distance(new Vector2(Global.CurrentPlayerObject.position.x, Global.CurrentPlayerObject.position.y), new Vector2(transform.position.x, transform.position.y)) >= 1.5f))
		{
			ShardCrush();
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void GetON(GameObject _myself)
	{
		if (!Global.InTheHandObject)
		{
			_myself.SendMessage("StartGetInTheHand", gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (!(collision.gameObject.tag == "CheckPoint") && !(collision.gameObject.tag == "Ignore") && !(collision.gameObject.tag == "Deep") && collision.gameObject.layer != 11 && !(collision.gameObject.tag == "Player"))
		{
			TouchToOther(collision.gameObject);
		}
	}

	public virtual void OnTriggerEnter(Collider collision)
	{
		if (!(collision.gameObject.tag == "CheckPoint") && !(collision.gameObject.tag == "Ignore") && !(collision.gameObject.tag == "Deep") && !(collision.gameObject.tag == "Player") && collision.gameObject.layer != 11)
		{
			TouchToOther(collision.gameObject);
		}
	}

	public virtual void CrushHP()
	{
		if (!(shardObject == null))
		{
			ShardCrush();
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void TouchToOther(GameObject collision)
	{
		if (shardObject == null || collision.layer == 8)
		{
			return;
		}
		float num = Mathf.Abs(local_velocity.x) + Mathf.Abs(local_velocity.y);
		if (!(num <= 5f))
		{
			if ((bool)shardObject)
			{
				ShardCrush();
				collision.SendMessage("CrushHP", 1, SendMessageOptions.DontRequireReceiver);
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void ShardCrush()
	{
		if (!Once)
		{
			Once = true;
			gameObject.SendMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			if (Extensions.get_length((System.Array)ObjectChance) > 0)
			{
				Chance();
			}
			for (int i = 0; i < 7; i++)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(shardObject) as GameObject;
				float x = myTransform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.x = x);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
				float y = myTransform.position.y + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position2 = Global.LastCreatedObject.transform.position;
				float num2 = (position2.y = y);
				Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
				float z = myTransform.position.z - 0.25f;
				Vector3 position3 = Global.LastCreatedObject.transform.position;
				float num3 = (position3.z = z);
				Vector3 vector5 = (Global.LastCreatedObject.transform.position = position3);
				int num4 = UnityEngine.Random.Range(-180, 180);
				Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
				float num5 = (eulerAngles.z = num4);
				Vector3 vector7 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
				Global.LastCreatedObject.SendMessage("FakePartVelocityAndDestroy", 4, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Chance()
	{
		int num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)ObjectChance));
		if ((bool)ObjectChance[num])
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(ObjectChance[num]) as GameObject;
			Global.LastCreatedObject.transform.position = myTransform.position;
			float z = Global.CurrentPlayerObject.position.z + 0.1f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num2 = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			Global.LastCreatedObject.SendMessage("NOSave", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Main()
	{
	}
}
