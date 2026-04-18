using System;
using UnityEngine;

[Serializable]
public class TentacleAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	private Vector3 Pose;

	private float Speed;

	public float speed;

	public float gravityPower;

	public bool OnlyInWater;

	public GameObject PowerFX;

	public bool MASTER;

	public bool CanCatch;

	public int ChanceNoCatch;

	private int CatchPower;

	public float regen;

	private bool InTheWater;

	public TentacleAI()
	{
		speed = 12f;
		gravityPower = 10f;
		ChanceNoCatch = 97;
	}

	public virtual void Start()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
		core.NewAI("idle", string.Empty, 50, 55);
		core.Layer("fly");
		Speed = speed * UnityEngine.Random.Range(0.9f, 1.1f);
		if (!MASTER)
		{
			Pose = new Vector3(UnityEngine.Random.Range(-3.1f, 3.1f), UnityEngine.Random.Range(-3.1f, 3.1f), 0f);
		}
	}

	public virtual void Update()
	{
		if (!Global.Pause && !TalkPause.IsGameplayBlocked() && !(core.ai != "catch"))
		{
			float x = core.trans.position.x;
			Vector3 position = Global.CurrentPlayerObject.position;
			float num = (position.x = x);
			Vector3 vector = (Global.CurrentPlayerObject.position = position);
			float y = core.trans.position.y - 0.25f;
			Vector3 position2 = Global.CurrentPlayerObject.position;
			float num2 = (position2.y = y);
			Vector3 vector3 = (Global.CurrentPlayerObject.position = position2);
			if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
			{
				CatchDown();
			}
			if (Input.GetButtonDown("Shift") || Input.GetButtonDown("Strike"))
			{
				CatchDown();
			}
			if (CatchPower <= 0)
			{
				core.NewAI("idle", string.Empty, 50, 55);
				core.Layer("fly");
			}
		}
	}

	public virtual void CatchDown()
	{
		CatchPower--;
		core.rigid.AddForce(new Vector3(0f, Input.GetAxis("Horizontal") * 400f, 0f));
		Global.MP -= Global.MaxHP * 0.1f;
	}

	public virtual void FixedUpdate()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		Vector3 vector = default(Vector3);
		float num = default(float);
		string ai = core.ai;
		if (ai == "catch")
		{
			Global.CatchTimer = 100;
			vector = transform.parent.position - transform.position;
			num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.rotation = Quaternion.AngleAxis(num + 180f, Vector3.forward);
		}
		else if (ai == "idle")
		{
			core.HP += regen;
			if (!(core.HP <= core.MaxHP))
			{
				core.HP = core.MaxHP;
			}
			bool flag = true;
			if (OnlyInWater && !InTheWater)
			{
				core.rigid.AddForce(new Vector3(0f, 0f - gravityPower, 0f));
				flag = false;
			}
			if (flag)
			{
				if (!MASTER && UnityEngine.Random.Range(0, 100) == 98)
				{
					Pose = new Vector3(UnityEngine.Random.Range(-4.1f, 4.1f), UnityEngine.Random.Range(0.1f, 2.1f), 0f);
				}
				Vector3 vector2 = Global.CurrentPlayerObject.position + Pose - transform.position;
				core.rigid.AddForceAtPosition(vector2.normalized * Speed, transform.position);
			}
			vector = Global.CurrentPlayerObject.position - transform.position;
			num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.rotation = Quaternion.AngleAxis(num + 180f, Vector3.forward);
		}
		InTheWater = false;
	}

	public virtual void DISAPPEAR()
	{
		core.StartDeathSequence(100);
	}

	public virtual void InWater()
	{
		InTheWater = true;
	}

	public virtual void Effect()
	{
		if ((bool)PowerFX)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(PowerFX);
			gameObject.transform.position = transform.position;
		}
	}

	public virtual void Insert()
	{
		if ((bool)PowerFX)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(PowerFX);
			gameObject.transform.position = transform.position;
		}
		UnityEngine.Object.Destroy(this.gameObject);
	}

	public virtual void OnCollisionStay(Collision other)
	{
		if (!(other.gameObject.tag != "Player") && CanCatch && Global.CatchTimer <= 0 && UnityEngine.Random.Range(0, 100) >= ChanceNoCatch)
		{
			core.NewAI("catch", string.Empty, 50, 55);
			core.Layer("shift");
			CatchPower = UnityEngine.Random.Range(5, 20);
		}
	}

	public virtual void Main()
	{
	}
}
