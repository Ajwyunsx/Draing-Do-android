using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class KillAfterTime : MonoBehaviour
{
	public bool randomAngle;

	public int time;

	public int timeRandom;

	public bool isThunder;

	private int oldTime;

	public AudioClip[] SFXBoom;

	public KillAfterTime()
	{
		time = 10;
	}

	public virtual void Start()
	{
		if (randomAngle)
		{
			float z = transform.eulerAngles.z + (float)UnityEngine.Random.Range(-90, 90);
			Vector3 eulerAngles = transform.eulerAngles;
			float num = (eulerAngles.z = z);
			Vector3 vector = (transform.eulerAngles = eulerAngles);
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				float x = transform.localScale.x * -1f;
				Vector3 localScale = transform.localScale;
				float num2 = (localScale.x = x);
				Vector3 vector3 = (transform.localScale = localScale);
			}
		}
		oldTime = time;
		time += UnityEngine.Random.Range(0, timeRandom);
	}

	public virtual void FixedUpdate()
	{
		time--;
		if (time > 0)
		{
			return;
		}
		if (!isThunder)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		else if (UnityEngine.Random.Range(0, 100) < 30)
		{
			int num = UnityEngine.Random.Range(-180, 180);
			Vector3 eulerAngles = transform.eulerAngles;
			float num2 = (eulerAngles.z = num);
			Vector3 vector = (transform.eulerAngles = eulerAngles);
			transform.position += new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0f);
			time = oldTime + UnityEngine.Random.Range(0, timeRandom);
		}
		else
		{
			if (Extensions.get_length((System.Array)SFXBoom) > 0)
			{
				Global.CreateSFX(SFXBoom[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)SFXBoom))], transform.position, UnityEngine.Random.Range(0.85f, 1.1f), UnityEngine.Random.Range(0.8f, 1f));
			}
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
