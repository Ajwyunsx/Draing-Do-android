using System;
using UnityEngine;

[Serializable]
public class SpawnEnemy : MonoBehaviour
{
	public int timer;

	private int NewMaxTimer;

	public bool ZAxis;

	public Vector2 MaxTimer;

	public Vector2 RandomAngle;

	public Vector2 RandomScale;

	public GameObject SpawnObject;

	public Vector3 Speed;

	public int Layer;

	public string DeadLevel;

	public int NewLifeTime;

	public SpawnEnemy()
	{
		MaxTimer = new Vector2(50f, 100f);
		RandomAngle = new Vector2(0f, 0f);
		RandomScale = new Vector2(1f, 1f);
		Speed = new Vector3(0f, -0.1f, 0f);
		Layer = -1;
		NewLifeTime = 300;
	}

	public virtual void Start()
	{
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().enabled = false;
		}
	}

	public virtual void SetMaxTimer(Vector2 mt)
	{
		MaxTimer = mt;
	}

	public virtual void SetMaxSpeed(Vector3 sp)
	{
		Speed = sp;
	}

	public virtual void FixedUpdate()
	{
		timer++;
		if (NewMaxTimer == 0)
		{
			NewMaxTimer = (int)UnityEngine.Random.Range(MaxTimer.x, MaxTimer.y);
		}
		if (timer >= NewMaxTimer)
		{
			timer = 0;
			NewMaxTimer = 0;
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(SpawnObject) as GameObject;
			Global.LastCreatedObject.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(0f - GetComponent<Renderer>().bounds.extents.x, GetComponent<Renderer>().bounds.extents.x), UnityEngine.Random.Range(0f - GetComponent<Renderer>().bounds.extents.y, GetComponent<Renderer>().bounds.extents.y), 0f);
			if (ZAxis)
			{
				float z = transform.position.z + UnityEngine.Random.Range(0f - GetComponent<Renderer>().bounds.extents.z, GetComponent<Renderer>().bounds.extents.z);
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.z = z);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
			}
			float z2 = UnityEngine.Random.Range(RandomAngle.x, RandomAngle.y);
			Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
			float num2 = (eulerAngles.z = z2);
			Vector3 vector3 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
			Global.LastCreatedObject.transform.localScale = Global.LastCreatedObject.transform.localScale * UnityEngine.Random.Range(RandomScale.x, RandomScale.y);
			Global.LastCreatedObject.AddComponent<ScriptFly>();
			if (Layer != -1)
			{
				Global.LastCreatedObject.layer = Layer;
			}
			Global.LastCreatedObject.SendMessage("SetDeadLevel", DeadLevel, SendMessageOptions.DontRequireReceiver);
			Global.LastCreatedObject.SendMessage("ChangeSpeed", Speed, SendMessageOptions.DontRequireReceiver);
			if (NewLifeTime > 0)
			{
				Global.LastCreatedObject.SendMessage("SetLifeTime", NewLifeTime, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
