using System;
using UnityEngine;

[Serializable]
public class EscapeAI : MonoBehaviour
{
	public AudioClip SFX;

	private Vector3 StartPosition;

	private bool Sleep;

	public int Mode;

	public float distance;

	public float max;

	private int SleepTimer;

	public int SleepTimerMax;

	public float speed;

	public float randomSpeed;

	private float currentRandomSpeed;

	public EscapeAI()
	{
		Sleep = true;
		Mode = 1;
		distance = 2f;
		max = 12f;
		SleepTimerMax = 100;
		speed = 5f;
		randomSpeed = 1f;
	}

	public virtual void Start()
	{
		StartPosition = transform.position;
	}

	public virtual void FixedUpdate()
	{
		if (SleepTimer > 0)
		{
			SleepTimer--;
		}
		else if (Sleep)
		{
			float num = Vector3.Distance(Global.CurrentPlayerObject.position, transform.position);
			if (!(num >= distance))
			{
				Sleep = false;
				currentRandomSpeed = UnityEngine.Random.Range(0f - randomSpeed, randomSpeed);
				float z = Mathf.Atan2(Global.CurrentPlayerObject.position.y - transform.position.y, Global.CurrentPlayerObject.position.x - transform.position.x) * 57.29578f - 90f + UnityEngine.Random.Range(-15f, 15f);
				Vector3 eulerAngles = transform.eulerAngles;
				float num2 = (eulerAngles.z = z);
				Vector3 vector = (transform.eulerAngles = eulerAngles);
			}
		}
		else
		{
			float y = transform.localPosition.y + (speed + currentRandomSpeed);
			Vector3 localPosition = transform.localPosition;
			float num3 = (localPosition.y = y);
			Vector3 vector3 = (transform.localPosition = localPosition);
			float num = default(float);
			if (!(num <= max))
			{
				Sleep = true;
				SleepTimer = SleepTimerMax;
			}
		}
	}

	public virtual void Main()
	{
	}
}
