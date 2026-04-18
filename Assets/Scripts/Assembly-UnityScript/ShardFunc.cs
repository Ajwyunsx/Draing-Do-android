using System;
using UnityEngine;

[Serializable]
public class ShardFunc : MonoBehaviour
{
	public GameObject shardObject;

	private bool Once;

	public AudioClip SFX;

	private Transform myTransform;

	public float YMove;

	public float ZMove;

	public Vector2 countFX;

	public ShardFunc()
	{
		ZMove = -2f;
		countFX = new Vector2(7f, 8f);
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void DISAPPEAR()
	{
		if (!(shardObject == null))
		{
			ShardCrush();
		}
	}

	public virtual void ShardCrush()
	{
		if (!Once)
		{
			Once = true;
			gameObject.SendMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
			if ((bool)SFX)
			{
				AudioSource.PlayClipAtPoint(SFX, transform.position);
			}
			for (int i = 0; (float)i < UnityEngine.Random.Range(countFX.x, countFX.y); i++)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(shardObject) as GameObject;
				float x = myTransform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position = Global.LastCreatedObject.transform.position;
				float num = (position.x = x);
				Vector3 vector = (Global.LastCreatedObject.transform.position = position);
				float y = myTransform.position.y + YMove + UnityEngine.Random.Range(-0.25f, 0.25f);
				Vector3 position2 = Global.LastCreatedObject.transform.position;
				float num2 = (position2.y = y);
				Vector3 vector3 = (Global.LastCreatedObject.transform.position = position2);
				float z = myTransform.position.z + ZMove;
				Vector3 position3 = Global.LastCreatedObject.transform.position;
				float num3 = (position3.z = z);
				Vector3 vector5 = (Global.LastCreatedObject.transform.position = position3);
				int num4 = UnityEngine.Random.Range(-180, 180);
				Vector3 eulerAngles = Global.LastCreatedObject.transform.eulerAngles;
				float num5 = (eulerAngles.z = num4);
				Vector3 vector7 = (Global.LastCreatedObject.transform.eulerAngles = eulerAngles);
				Global.LastCreatedObject.SendMessage("FakePartVelocityAndDestroy", 4, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public virtual void Main()
	{
	}
}
