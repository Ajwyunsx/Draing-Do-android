using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class RastenieEffect : MonoBehaviour
{
	public int Generation;

	private float LifeTimer;

	private string Mode;

	private float AngleSpeed;

	public int MinChilds;

	public int MaxChilds;

	public float ZOffset;

	public bool ChildRandomPosition;

	public int LifeTime;

	public float RandomMove;

	public Rasteny[] Rastenys;

	private bool Init;

	public bool MainParent;

	public RastenieEffect()
	{
		Generation = 5;
		MaxChilds = 1;
		LifeTime = 25;
		RandomMove = 0.5f;
	}

	public virtual void Start()
	{
		transform.localScale = Vector3.zero;
		AngleSpeed = UnityEngine.Random.Range(-0.7f, 0.7f);
		LifeTimer = (float)LifeTime * UnityEngine.Random.Range(0.9f, 1.1f);
		float z = transform.position.z + (UnityEngine.Random.Range(-0.00555f, -0.00222f) + ZOffset);
		Vector3 position = transform.position;
		float num = (position.z = z);
		Vector3 vector = (transform.position = position);
	}

	public virtual void SendGeneration(int generation)
	{
		Generation = generation;
		Init = true;
		MainParent = false;
		if (MaxChilds > 0)
		{
			float z = transform.eulerAngles.z + (float)UnityEngine.Random.Range(-90, 90);
			Vector3 eulerAngles = transform.eulerAngles;
			float num = (eulerAngles.z = z);
			Vector3 vector = (transform.eulerAngles = eulerAngles);
		}
	}

	public virtual void SendMode(string modd)
	{
		Mode = modd;
	}

	public virtual void FixedUpdate()
	{
		if (MainParent && !Init && !(Vector3.Distance(Global.CurrentPlayerObject.position, transform.position) >= 6f))
		{
			Init = true;
			LifeTimer = LifeTime;
		}
		if (!Init)
		{
			return;
		}
		LifeTimer -= 1f;
		if (!(LifeTimer > 0f))
		{
			if (Generation > 0)
			{
				if (Rastenys == null || Extensions.get_length((System.Array)Rastenys) == 0)
				{
					UnityEngine.Object.Destroy(this);
					return;
				}
				if (Mode != "stop")
				{
					int num = default(int);
					int min = MinChilds;
					if (MinChilds < 1 && MainParent)
					{
						min = 1;
					}
					int num2 = UnityEngine.Random.Range(min, MaxChilds);
					for (int i = 0; i < num2; i++)
					{
						num = UnityEngine.Random.Range(0, Extensions.get_length((System.Array)Rastenys));
						Global.LastCreatedObject = UnityEngine.Object.Instantiate(Rastenys[num].@object);
						Global.LastCreatedObject.transform.parent = transform;
						int num3 = 0;
						Vector3 localEulerAngles = Global.LastCreatedObject.transform.localEulerAngles;
						float num4 = (localEulerAngles.z = num3);
						Vector3 vector = (Global.LastCreatedObject.transform.localEulerAngles = localEulerAngles);
						if (!ChildRandomPosition)
						{
							Global.LastCreatedObject.transform.localPosition = new Vector3(0f, 0.325f, 0f);
						}
						else
						{
							Global.LastCreatedObject.transform.localPosition = new Vector3(UnityEngine.Random.Range(0f - RandomMove, RandomMove), UnityEngine.Random.Range(0f - RandomMove, RandomMove), 0f);
						}
						Global.LastCreatedObject.transform.parent = null;
						Global.LastCreatedObject.SendMessage("SendGeneration", Generation - 1, SendMessageOptions.DontRequireReceiver);
						Global.LastCreatedObject.SendMessage("SendMode", Rastenys[num].mode, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			if (!MainParent)
			{
				float z = transform.eulerAngles.z + AngleSpeed;
				Vector3 eulerAngles = transform.eulerAngles;
				float num5 = (eulerAngles.z = z);
				Vector3 vector3 = (transform.eulerAngles = eulerAngles);
			}
			transform.localScale += new Vector3(0.08f, 0.08f, 0.08f);
		}
	}

	public virtual void Main()
	{
	}
}
