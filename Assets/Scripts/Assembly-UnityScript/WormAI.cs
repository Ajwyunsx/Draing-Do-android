using System;
using UnityEngine;

[Serializable]
public class WormAI : MonoBehaviour
{
	private AI core;

	private Collider coll;

	public int ChanceNoCatch;

	private int CatchPower;

	private int CatchTimer;

	public int BiteTimer;

	public bool CatchNow;

	private Vector3 catchPos;

	private float lastGravity;

	private int biteTimer;

	public WormAI()
	{
		ChanceNoCatch = 80;
		BiteTimer = 100;
	}

	public virtual void Awake()
	{
		core = GetComponent<AI>();
		coll = GetComponent<Collider>();
	}

	public virtual void Start()
	{
		core.Coords = transform.position;
		if (CatchNow && Global.CurrentPlayerObject != null && core.Distance(Global.CurrentPlayerObject.position, transform.position) <= 0.75f)
		{
			CatchIt(Global.CurrentPlayerObject);
			return;
		}
		core.NewAI("crawl", string.Empty, 50, 55);
		core.Layer("plat");
	}

	public virtual void Update()
	{
		if (!Global.Pause && !TalkPause.IsGameplayBlocked() && !(core.ai != "catch"))
		{
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
				DropIt();
			}
		}
	}

	public virtual void CatchDown()
	{
		CatchPower--;
		core.rigid.AddForce(new Vector3(0f, Input.GetAxis("Horizontal") * 200f, 0f));
		Global.MP -= Global.MaxHP * 0.1f;
	}

	public virtual void FixedUpdate()
	{
		if (core.target == null && Global.CurrentPlayerObject != null)
		{
			core.target = Global.CurrentPlayerObject;
		}
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		if (CatchTimer > 0)
		{
			CatchTimer--;
			if (CatchTimer <= 0)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		string ai = core.ai;
		if (ai == "catch")
		{
			if (!core.trans.parent)
			{
				DropIt();
				return;
			}
			if (biteTimer > 0)
			{
				biteTimer--;
				if (biteTimer <= 0)
				{
					core.StrikeIt(core.trans.parent.gameObject);
					core.TrySetTrigger("bite");
					biteTimer = BiteTimer;
				}
			}
			float x = Mathf.Lerp(core.trans.localPosition.x, catchPos.x, 0.25f);
			Vector3 localPosition = core.trans.localPosition;
			float num = (localPosition.x = x);
			Vector3 vector = (core.trans.localPosition = localPosition);
			float y = Mathf.Lerp(core.trans.localPosition.y, catchPos.y, 0.25f);
			Vector3 localPosition2 = core.trans.localPosition;
			float num2 = (localPosition2.y = y);
			Vector3 vector3 = (core.trans.localPosition = localPosition2);
		}
		else if (ai == "recover")
		{
			if (core.land || core.timer <= 0)
			{
				core.gravity = lastGravity;
				core.Layer("plat");
				core.NewAI("crawl", string.Empty, 50, 55);
			}
		}
		else if (ai == "idle" || ai == "crawl")
		{
			if (CatchNow && core.target != null && core.Distance(core.target.position, core.trans.position) <= 0.75f)
			{
				CatchNow = false;
				CatchIt(core.target);
				return;
			}
			if (core.target != null)
			{
				core.LookTo(core.target.position, 1);
				core.MoveToX(core.target.position.x, 0.01f);
				core.Walk();
			}
			if (core.timer <= 0)
			{
				core.NewAI("crawl", string.Empty, 150, 200);
			}
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!(other.gameObject.tag != "Player") && CatchTimer <= 0 && UnityEngine.Random.Range(0, 100) >= ChanceNoCatch)
		{
			CatchIt(other.gameObject.transform);
		}
	}

	public virtual void CatchIt(Transform tr)
	{
		core.NewAI("catch", string.Empty, 50, 55);
		core.Layer("shift");
		CatchPower = UnityEngine.Random.Range(15, 30);
		core.trans.parent = tr;
		catchPos.x = UnityEngine.Random.Range(-0.5f, 0.5f);
		catchPos.y = UnityEngine.Random.Range(-0.25f, 0.25f);
		lastGravity = core.gravity;
		core.gravity = 0f;
		int num = UnityEngine.Random.Range(0, 360);
		Vector3 localEulerAngles = core.trans.localEulerAngles;
		float num2 = (localEulerAngles.z = num);
		Vector3 vector = (core.trans.localEulerAngles = localEulerAngles);
		coll.isTrigger = true;
		biteTimer = BiteTimer;
	}

	public virtual void DropIt()
	{
		CatchTimer = 100;
		core.NewAI("recover", string.Empty, 35, 35);
		core.Layer("fly");
		core.trans.parent = null;
		core.gravity = 0.5f;
		core.land = false;
		core.landTimer = 10;
		core.Speed.x = UnityEngine.Random.Range(-2f, 2f);
		int num = -2;
		Vector3 position = core.trans.position;
		float num2 = (position.z = num);
		Vector3 vector = (core.trans.position = position);
	}

	public virtual void DISAPPEAR()
	{
	}

	public virtual void Main()
	{
	}
}
