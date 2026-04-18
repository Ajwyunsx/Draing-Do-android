using System;
using UnityEngine;

[Serializable]
public class Meteorites : MonoBehaviour
{
	public int timer;

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		timer++;
		if (!((float)timer <= 35f - Global.MaxHP * 4f))
		{
			timer = 0;
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("Other/Meteorite")) as GameObject;
			Global.LastCreatedObject.transform.position = new Vector3(UnityEngine.Random.Range(-7f, 22f), 7f, 0f);
			Global.LastCreatedObject.GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-5, 5), -1f, 0f);
		}
	}

	public virtual void Main()
	{
	}
}
