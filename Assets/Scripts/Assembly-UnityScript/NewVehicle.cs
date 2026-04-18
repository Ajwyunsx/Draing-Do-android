using System;
using UnityEngine;

[Serializable]
public class NewVehicle : MonoBehaviour
{
	public AudioClip SFXOuch;

	public AudioClip SFXFall;

	private Transform trans;

	private Vector3 StartScale;

	private Vector3 NeedSpeed;

	public GameObject particleStrike;

	public float speed;

	public GameObject CreateByDead;

	public Vector3 alwaysMove;

	public bool CantControlY;

	public bool ChangeDir;

	private int Dir;

	private bool onceDead;

	public int OxygenTimer;

	public bool CanWaterBreath;

	public NewVehicle()
	{
		speed = 4f;
		Dir = 1;
	}

	public virtual void Start()
	{
		Global.CurrentPlayerObject = transform;
		trans = transform;
		StartScale = trans.localScale;
		if (GetComponent<AudioSource>() == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.Pause)
		{
			return;
		}
		if (ChangeDir && !(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.9f))
		{
			float num = Mathf.Sign(Input.GetAxis("Horizontal"));
			if (num != (float)Dir)
			{
				Dir = (int)num;
				float x = StartScale.x * num;
				Vector3 localScale = trans.localScale;
				float num2 = (localScale.x = x);
				Vector3 vector = (trans.localScale = localScale);
			}
		}
		if (OxygenTimer > 0)
		{
			OxygenTimer--;
		}
		if (Global.Oxygen <= 0 && OxygenTimer <= 0)
		{
			Global.HP -= Global.MaxHP * 0.01f;
		}
		float x2 = Input.GetAxis("Horizontal") * speed + NeedSpeed.x + alwaysMove.x;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num3 = (velocity.x = x2);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity);
		if (!CantControlY)
		{
			float y = Input.GetAxis("Vertical") * speed + NeedSpeed.y + alwaysMove.y;
			Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
			float num4 = (velocity2.y = y);
			Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity2);
		}
		else
		{
			float y2 = NeedSpeed.y + alwaysMove.y;
			Vector3 velocity3 = GetComponent<Rigidbody>().velocity;
			float num5 = (velocity3.y = y2);
			Vector3 vector7 = (GetComponent<Rigidbody>().velocity = velocity3);
		}
		NeedSpeed *= 0.9f;
		if (!onceDead && !(Global.HP > 0f))
		{
			onceDead = true;
			if ((bool)SFXFall)
			{
				AudioSource.PlayClipAtPoint(SFXFall, transform.position);
			}
			Camera.main.SendMessage("FadeOn", null, SendMessageOptions.DontRequireReceiver);
			Global.CheckPointNameTemp = Global.CheckPointName;
			if ((bool)CreateByDead)
			{
				UnityEngine.Object.Instantiate(CreateByDead, trans.position, Quaternion.identity);
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void CrushHP()
	{
		HPMinus(1);
	}

	public virtual void HPMinus(int hp)
	{
		if (Global.InvincibleTimer <= 0)
		{
			if (!(GetComponent<AudioSource>().clip == SFXOuch) || !GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().PlayOneShot(SFXOuch);
			}
			if ((bool)particleStrike)
			{
				UnityEngine.Object.Instantiate(particleStrike, trans.position + new Vector3(0f, 0.1f, -0.5f), Quaternion.identity);
			}
			Global.HP = 0f;
			Global.QuakeStart(75, 30f);
			Global.InvincibleTimer = 75;
		}
	}

	public virtual void OxigenCheck(bool @bool)
	{
		if (@bool)
		{
			Global.Oxygen = Global.MaxOxygen;
		}
		else
		{
			Global.Oxygen = (int)((float)Global.Oxygen - 1f * Time.deltaTime * 50f);
		}
	}

	public virtual void Main()
	{
	}
}
