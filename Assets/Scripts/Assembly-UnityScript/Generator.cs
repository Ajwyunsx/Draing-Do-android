using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Generator : MonoBehaviour
{
	public GameObject ThatObject;

	public PhysicMaterial ThatObjectMaterial;

	private UnityScript.Lang.Array ownObjects;

	public int MaxObjects;

	public float ZPosition;

	public Vector2 RndPosition;

	private int timer;

	public int DelayTime;

	public int COUNT;

	private int OldCount;

	private bool Dis;

	private int disappear_timer;

	public GameObject particleStrike;

	private Transform myTransform;

	private Vector3 startScale;

	private float distance;

	private bool actived;

	private int STARTCOUNT;

	private bool blockThatCreates;

	public bool NoAutoSpawn;

	public bool positionRightOnPlayer;

	public bool DestroyOnDIS;

	public Generator()
	{
		ownObjects = new UnityScript.Lang.Array();
		MaxObjects = 3;
		ZPosition = -0.9f;
		DelayTime = 50;
		distance = 20f;
	}

	public virtual void Awake()
	{
		STARTCOUNT = COUNT;
		OldCount = COUNT;
		myTransform = transform;
		startScale = myTransform.localScale;
	}

	public virtual void FixedUpdate()
	{
		if (!actived)
		{
			if (!(Vector3.Distance(Global.CurrentPlayerObject.position, transform.position) >= distance))
			{
				actived = true;
			}
			return;
		}
		if (Dis)
		{
			DisFunction();
			return;
		}
		if (!NoAutoSpawn)
		{
			timer++;
		}
		if (timer >= DelayTime)
		{
			timer = 0;
			for (int i = 0; i < ownObjects.length; i++)
			{
				if (RuntimeServices.EqualityOperator(ownObjects[i], null))
				{
					ownObjects.RemoveAt(i);
				}
			}
			if (!blockThatCreates && ownObjects.length < MaxObjects)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(ThatObject, new Vector3(transform.position.x, transform.position.y, 0f) + new Vector3(UnityEngine.Random.Range(0f - RndPosition.x, RndPosition.x), UnityEngine.Random.Range(0f - RndPosition.y, RndPosition.y), ZPosition + UnityEngine.Random.Range(-0.00111f, 0.00111f)), Quaternion.identity);
				float zPosition = ZPosition;
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.z = zPosition);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
				if (positionRightOnPlayer)
				{
					float x = Global.CurrentPlayerObject.position.x;
					Vector3 position2 = Global.LastCreatedObject.transform.position;
					float num2 = (position2.x = x);
					Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
					float y = Global.CurrentPlayerObject.position.y - 0.5f;
					Vector3 position3 = Global.LastCreatedObject.transform.position;
					float num3 = (position3.y = y);
					Vector3 vector5 = (Global.LastCreatedObject.transform.position = position3);
				}
				if ((bool)ThatObjectMaterial)
				{
					Global.LastCreatedObject.GetComponent<Collider>().sharedMaterial = ThatObjectMaterial;
				}
				ownObjects.Add(Global.LastCreatedObject);
				if (COUNT > 0)
				{
					COUNT--;
					if (COUNT == 0)
					{
						blockThatCreates = true;
					}
				}
			}
		}
		if (blockThatCreates && ownObjects.length <= 0)
		{
			Dis = true;
			gameObject.BroadcastMessage("Detach", null, SendMessageOptions.DontRequireReceiver);
			disappear_timer = 60;
			if ((bool)particleStrike)
			{
				UnityEngine.Object.Instantiate(particleStrike, myTransform.position + new Vector3(0f, 0f, -0.5f), Quaternion.identity);
			}
			if ((bool)GetComponent<Collider>())
			{
				GetComponent<Collider>().enabled = false;
			}
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public virtual void DisFunction()
	{
		if (disappear_timer > 0)
		{
			disappear_timer--;
			myTransform.localScale = Vector3.Lerp(myTransform.localScale, Vector3.zero, 0.1f);
		}
		if (disappear_timer <= 0)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void SpawnCreate()
	{
		timer = DelayTime;
	}

	public virtual void DISAPPEAR()
	{
		if (DestroyOnDIS)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
