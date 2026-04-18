using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class Vehicle2 : MonoBehaviour
{
	public float jumpHeight;

	public float gravity;

	public bool CanFly;

	public bool CanSwim;

	public Vector3 AutoSpeed;

	public Vector3 AutoDirection;

	public bool CanHorizontal;

	public bool CanVertical;

	public bool CanRotate;

	public bool CanFastSlow;

	public Vector3 Direction;

	public GameObject Passager;

	public bool ON;

	public float speed;

	[HideInInspector]
	public Joystick moveTouchPad;

	[HideInInspector]
	public Joystick jumpTouchPad;

	[HideInInspector]
	public Joystick actionTouchPad;

	[HideInInspector]
	public bool InTheWater;

	[HideInInspector]
	public bool OldInTheWater;

	[HideInInspector]
	public bool BeenInWater;

	[HideInInspector]
	public Vector3 Speed;

	[HideInInspector]
	public Vector3 NeedSpeed;

	[HideInInspector]
	public Vector3 OldNeedSpeed;

	[HideInInspector]
	public float SpeedY;

	[HideInInspector]
	public int PushTimer;

	[HideInInspector]
	public object IsNoFalling;

	private Transform myTransform;

	public Vehicle2()
	{
		jumpHeight = 2f;
		gravity = 16f;
		CanFly = true;
		CanHorizontal = true;
		CanVertical = true;
		Direction = new Vector3(1f, 0f, 0f);
		speed = 3f;
	}

	public virtual void Awake()
	{
		Global.CurrentPlayerObject = transform;
		myTransform = transform;
	}

	public virtual void Start()
	{
		moveTouchPad = (Joystick)GameObject.Find("MoveJoystick").GetComponent(typeof(Joystick));
		jumpTouchPad = (Joystick)GameObject.Find("JumpJoystick").GetComponent(typeof(Joystick));
		actionTouchPad = (Joystick)GameObject.Find("ActionJoystick").GetComponent(typeof(Joystick));
	}

	public virtual void FixedUpdate()
	{
		float x = NeedSpeed.x;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num = (velocity.x = x);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		float y = NeedSpeed.y;
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num2 = (velocity2.y = y);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
		if (Global.BlockControl <= 0)
		{
			float num3 = 0f;
			if (CanHorizontal)
			{
				num3 = ((Mathf.Abs(moveTouchPad.position.x) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.x) : 0f);
				if (PushTimer == 0)
				{
					float x2 = GetComponent<Rigidbody>().velocity.x + (Input.GetAxis("Horizontal") + num3) * speed;
					Vector3 velocity3 = GetComponent<Rigidbody>().velocity;
					float num4 = (velocity3.x = x2);
					Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity3);
				}
				else
				{
					float x3 = GetComponent<Rigidbody>().velocity.x + (Input.GetAxis("Horizontal") + num3) * speed * 0.75f;
					Vector3 velocity4 = GetComponent<Rigidbody>().velocity;
					float num5 = (velocity4.x = x3);
					Vector3 vector7 = (GetComponent<Rigidbody>().velocity = velocity4);
				}
			}
			if (CanVertical)
			{
				HorizonalSpeed();
			}
		}
		SpeedY = 0f;
		if (!RuntimeServices.ToBool(IsNoFalling))
		{
			SpeedY -= 0.32f;
		}
		SpeedY -= 0.14f;
		float y2 = GetComponent<Rigidbody>().velocity.y + SpeedY - OldNeedSpeed.y + NeedSpeed.y;
		Vector3 velocity5 = GetComponent<Rigidbody>().velocity;
		float num6 = (velocity5.y = y2);
		Vector3 vector9 = (GetComponent<Rigidbody>().velocity = velocity5);
		NeedSpeed.x *= 0.95f;
		NeedSpeed.y *= 0.95f;
		if (!CanFly && !InTheWater && !(NeedSpeed.y <= -10f))
		{
			NeedSpeed.y -= 0.25f;
		}
		OldNeedSpeed = NeedSpeed;
		if (!InTheWater)
		{
			if (PushTimer > 0)
			{
				PushTimer--;
			}
			if (PushTimer < 0)
			{
				PushTimer++;
			}
		}
		OldInTheWater = InTheWater;
		InTheWater = false;
		if (BeenInWater && !InTheWater && !OldInTheWater)
		{
			BeenInWater = false;
			if ((Input.GetAxis("Vertical") >= 1f || jumpTouchPad.IsFingerDown() || !(moveTouchPad.position.y <= 0.1f)) && Global.BlockControl <= 0)
			{
				NeedSpeed.y = CalculateJumpVerticalSpeed() * 0.25f;
			}
		}
	}

	public virtual void HorizonalSpeed()
	{
		MonoBehaviour.print("InTheWater: " + InTheWater);
		if (InTheWater || CanFly)
		{
			float num = 0f;
			num = ((Mathf.Abs(moveTouchPad.position.y) >= 0.4f) ? Mathf.Sign(moveTouchPad.position.y) : 0f);
			if (PushTimer == 0)
			{
				float y = (Input.GetAxis("Vertical") + num) * speed;
				Vector3 velocity = GetComponent<Rigidbody>().velocity;
				float num2 = (velocity.y = y);
				Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
			}
			else
			{
				float y2 = (Input.GetAxis("Vertical") + num) * speed * 0.75f;
				Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
				float num3 = (velocity2.y = y2);
				Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
			}
		}
	}

	public virtual void StreamSpeed(Vector3 needspeed)
	{
		NeedSpeed = Vector3.Lerp(NeedSpeed, needspeed, 0.25f);
	}

	public virtual void InWater()
	{
		IsNoFalling = true;
		BeenInWater = true;
		InTheWater = true;
	}

	public virtual float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2f * jumpHeight * gravity);
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		if (!(other.gameObject.tag == "Danger"))
		{
		}
	}

	public virtual void Main()
	{
	}
}
