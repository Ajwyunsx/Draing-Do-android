using System;
using UnityEngine;

[Serializable]
public class GetToolItem : MonoBehaviour
{
	public ToolOptions toolOptions;

	[HideInInspector]
	public bool NoItemButTool;

	[HideInInspector]
	public int timer;

	[HideInInspector]
	public bool play;

	[HideInInspector]
	public int start;

	[HideInInspector]
	public int finish;

	[HideInInspector]
	public bool toHide;

	[HideInInspector]
	public int _HideTimer;

	private int PreWakeUptimer;

	public string command;

	public float commandCount;

	private bool turn;

	private Transform myTransform;

	public bool onlyEnemy;

	public bool spray;

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void DestroyItemScript()
	{
		Item item = (Item)GetComponent(typeof(Item));
		if ((bool)item)
		{
			UnityEngine.Object.Destroy(item);
		}
		NoItemButTool = true;
	}

	public virtual void FixedUpdate()
	{
		if (turn)
		{
			myTransform.Rotate(new Vector3(0f, 0f, Mathf.Sign(GetComponent<Rigidbody>().velocity.x) * 10f));
		}
		if (PreWakeUptimer > 0)
		{
			PreWakeUptimer--;
			if (PreWakeUptimer == 0)
			{
				Global.WakeUpTime = 240;
			}
		}
		if (play)
		{
			timer++;
			if (timer == start)
			{
				GetComponent<Collider>().enabled = true;
			}
			if (timer >= finish)
			{
				GetComponent<Collider>().enabled = false;
				if (spray)
				{
					gameObject.BroadcastMessage("SprayOFF", SendMessageOptions.DontRequireReceiver);
				}
				play = false;
				timer = 0;
			}
		}
		if (toHide)
		{
			_HideTimer--;
			if (_HideTimer <= 0)
			{
				toHide = false;
				GetComponent<Renderer>().enabled = true;
			}
		}
	}

	public virtual void DisableCollider(Vector3 timeVector3)
	{
		play = true;
		start = (int)timeVector3.x;
		finish = (int)timeVector3.y;
		timer = 0;
	}

	public virtual void DisableSpray(Vector3 timeVector3)
	{
		if (spray)
		{
			gameObject.BroadcastMessage("SprayON", SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (!NoItemButTool)
		{
			return;
		}
		if (command == "CrushHP")
		{
			Global.CurrentPlayerObject.SendMessage("BackSpeed", other.transform.position, SendMessageOptions.DontRequireReceiver);
		}
		if (!string.IsNullOrEmpty(command))
		{
			if (!onlyEnemy)
			{
				other.gameObject.SendMessage(command, commandCount, SendMessageOptions.DontRequireReceiver);
			}
			else if (other.gameObject.layer == 17 || other.gameObject.layer == 12)
			{
				other.gameObject.SendMessage(command, commandCount + Mathf.Floor(Global.ToolsCount[Global.CurrentToolNumber] / 10), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void HideWithTimer(int num)
	{
		_HideTimer = num;
		toHide = true;
		GetComponent<Renderer>().enabled = false;
	}

	public virtual void WakeUps()
	{
		PreWakeUptimer = 60;
	}

	public virtual void Turn()
	{
		turn = true;
	}

	public virtual void Dark()
	{
		if (Global.TimerNoVehicle <= 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(LoadData.GO("Vehicles/dark")) as GameObject;
			gameObject.transform.position = Global.CurrentPlayerObject.position;
			gameObject.SendMessage("EnterVehicle", false, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("Directions", 1, SendMessageOptions.DontRequireReceiver);
			Global.Passager_OFF = true;
		}
	}

	public virtual void Potion()
	{
		if (!(Global.HP >= Global.MaxHP))
		{
			Global.HP += 10f;
			if (!(Global.HP <= Global.MaxHP))
			{
				Global.HP = Global.MaxHP;
			}
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(LoadData.GFX("stars")) as GameObject;
			Global.LastCreatedObject.transform.position = myTransform.position;
			float z = Global.LastCreatedObject.transform.position.z - 0.2f;
			Vector3 position = Global.LastCreatedObject.transform.position;
			float num = (position.z = z);
			Vector3 vector = (Global.LastCreatedObject.transform.position = position);
		}
		else
		{
			Global.ToolsCount[Global.CurrentToolNumber] = Global.ToolsCount[Global.CurrentToolNumber] + 1;
		}
	}

	public virtual void Main()
	{
	}
}
