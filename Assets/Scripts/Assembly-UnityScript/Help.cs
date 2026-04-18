using System;
using UnityEngine;

[Serializable]
public class Help : MonoBehaviour
{
	private bool ON;

	private bool oldON;

	private Vector3 startPosition;

	public GameObject TalkObject;

	private GameObject GO;

	private Transform myTransform;

	private float SetTime;

	public int correctScaleX;

	public float CorrectY;

	public float CorrectZ;

	public bool destroy;

	private int timer;

	public string TextRename;

	public float TextScale;

	public bool OpenByCollision;

	public bool OpenByClick;

	public bool OpenByRightClick;

	private int StayActive;

	public bool NoScaleEffect;

	public bool NoTimeRemove;

	public bool RemoveByMoving;

	public bool CheckDistance;

	public bool ActionPlay;

	public Help()
	{
		SetTime = 0.25f;
		correctScaleX = 1;
		CorrectY = 2.1f;
		CorrectZ = -1f;
		TextScale = 1f;
		OpenByCollision = true;
		ActionPlay = true;
	}

	public virtual void Awake()
	{
		if (TalkObject == null)
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		GO = UnityEngine.Object.Instantiate(TalkObject);
		myTransform = GO.transform;
		UpdatePose();
		if (!string.IsNullOrEmpty(TextRename) && TextRename != string.Empty)
		{
			myTransform.BroadcastMessage("Rename", TextRename, SendMessageOptions.DontRequireReceiver);
		}
		if (TextScale != 1f)
		{
			myTransform.BroadcastMessage("TextSetScale", TextScale, SendMessageOptions.DontRequireReceiver);
		}
		if (!NoScaleEffect)
		{
			int num = 0;
			Vector3 localScale = myTransform.localScale;
			float num2 = (localScale.x = num);
			Vector3 vector = (myTransform.localScale = localScale);
			int num3 = 0;
			Vector3 localScale2 = myTransform.localScale;
			float num4 = (localScale2.y = num3);
			Vector3 vector3 = (myTransform.localScale = localScale2);
		}
		GO.SetActive(false);
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (Global.FadeMode == -1)
		{
			StayActive = 0;
		}
		if (StayActive > 0)
		{
			if (!NoTimeRemove)
			{
				StayActive--;
			}
			if (RemoveByMoving && !(Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.2f))
			{
				StayActive -= 20;
				if (StayActive <= 0)
				{
					gameObject.SendMessage("CloseIt", null, SendMessageOptions.DontRequireReceiver);
				}
			}
			ON = true;
		}
		float x = transform.position.x;
		Vector3 position = GO.transform.position;
		float num = (position.x = x);
		Vector3 vector = (GO.transform.position = position);
		if (ON)
		{
			UpdatePose();
			timer = 0;
			if (oldON != ON)
			{
				myTransform.position = transform.position + new Vector3(0f, 0f, CorrectZ);
				GO.SetActive(true);
				Global.GlobalObject.GetComponent<AudioSource>().Play();
			}
			if (!NoScaleEffect)
			{
				float x2 = Mathf.Lerp(myTransform.localScale.x, 0.85f * (float)correctScaleX, SetTime);
				Vector3 localScale = myTransform.localScale;
				float num2 = (localScale.x = x2);
				Vector3 vector3 = (myTransform.localScale = localScale);
				float y = Mathf.Lerp(myTransform.localScale.y, 0.85f, SetTime);
				Vector3 localScale2 = myTransform.localScale;
				float num3 = (localScale2.y = y);
				Vector3 vector5 = (myTransform.localScale = localScale2);
			}
			float y2 = Mathf.Lerp(myTransform.position.y, startPosition.y + CorrectY, SetTime);
			Vector3 position2 = myTransform.position;
			float num4 = (position2.y = y2);
			Vector3 vector7 = (myTransform.position = position2);
			float z = Mathf.Lerp(myTransform.position.z, startPosition.z, SetTime);
			Vector3 position3 = myTransform.position;
			float num5 = (position3.z = z);
			Vector3 vector9 = (myTransform.position = position3);
		}
		else
		{
			if (oldON != ON)
			{
				timer = 1;
			}
			if (timer > 0 && timer < 10)
			{
				timer++;
				if (timer == 10)
				{
					if (!NoScaleEffect)
					{
						int num6 = 0;
						Vector3 localScale3 = myTransform.localScale;
						float num7 = (localScale3.x = num6);
						Vector3 vector11 = (myTransform.localScale = localScale3);
						int num8 = 0;
						Vector3 localScale4 = myTransform.localScale;
						float num9 = (localScale4.y = num8);
						Vector3 vector13 = (myTransform.localScale = localScale4);
					}
					myTransform.position = transform.position + new Vector3(0f, 0f, CorrectZ);
					if (!destroy)
					{
						GO.SetActive(false);
					}
					else
					{
						UnityEngine.Object.Destroy(GO);
						UnityEngine.Object.Destroy(gameObject);
					}
				}
			}
			if (!NoScaleEffect)
			{
				float x3 = Mathf.Lerp(myTransform.localScale.x, 0f, SetTime * 1f);
				Vector3 localScale5 = myTransform.localScale;
				float num10 = (localScale5.x = x3);
				Vector3 vector15 = (myTransform.localScale = localScale5);
				float y3 = Mathf.Lerp(myTransform.localScale.y, 0f, SetTime * 1f);
				Vector3 localScale6 = myTransform.localScale;
				float num11 = (localScale6.y = y3);
				Vector3 vector17 = (myTransform.localScale = localScale6);
			}
			float y4 = Mathf.Lerp(myTransform.position.y, startPosition.y, SetTime);
			Vector3 position4 = myTransform.position;
			float num12 = (position4.y = y4);
			Vector3 vector19 = (myTransform.position = position4);
			float z2 = Mathf.Lerp(myTransform.position.z, startPosition.z, SetTime);
			Vector3 position5 = myTransform.position;
			float num13 = (position5.z = z2);
			Vector3 vector21 = (myTransform.position = position5);
		}
		oldON = ON;
		ON = false;
	}

	public virtual void OnMouseDown()
	{
		if ((!CheckDistance || Monitor.dist) && OpenByClick)
		{
			int stayActive = StayActive;
			gameObject.SendMessage("CloseIt", null, SendMessageOptions.DontRequireReceiver);
			if (stayActive <= 0)
			{
				StayActive = 500;
			}
			if ((bool)Global.CurrentPlayerObject && ActionPlay)
			{
				Global.CurrentPlayerObject.SendMessage("ActionHero", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void OnMouseOver()
	{
		if ((!CheckDistance || Monitor.dist) && OpenByRightClick && Input.GetMouseButtonDown(1))
		{
			int stayActive = StayActive;
			gameObject.SendMessage("CloseIt", null, SendMessageOptions.DontRequireReceiver);
			if (stayActive <= 0)
			{
				StayActive = 500;
			}
		}
	}

	public virtual void OpenIt()
	{
		StayActive = 500;
	}

	public virtual void CloseIt()
	{
		StayActive = 0;
	}

	public virtual void DISAPPEAR()
	{
		UnityEngine.Object.Destroy(GO);
		UnityEngine.Object.Destroy(gameObject);
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (OpenByCollision && !(other.gameObject.tag != "Player"))
		{
			ON = true;
		}
	}

	public virtual void Rename(string name)
	{
		UpdatePose();
		GO.SetActive(true);
		TextRename = name;
		myTransform.BroadcastMessage("Rename", name, SendMessageOptions.DontRequireReceiver);
		GO.SetActive(false);
	}

	public virtual void UpdatePose()
	{
		startPosition = transform.position + new Vector3(0f, 0f, CorrectZ);
	}

	public virtual void Main()
	{
	}
}
