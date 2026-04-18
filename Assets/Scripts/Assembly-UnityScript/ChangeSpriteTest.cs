using System;
using UnityEngine;

[Serializable]
public class ChangeSpriteTest : MonoBehaviour
{
	public string Type;

	[HideInInspector]
	public int Timer;

	public int Life;

	[HideInInspector]
	public bool Delete;

	[HideInInspector]
	public float Alpha;

	[HideInInspector]
	public tk2dSprite sprite;

	[HideInInspector]
	public bool GetItem;

	public ChangeSpriteTest()
	{
		Life = 1000;
		Alpha = 1f;
	}

	public virtual void Start()
	{
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
	}

	public virtual void FixedUpdate()
	{
		if (Life > 0)
		{
			Life--;
		}
		else
		{
			Delete = true;
		}
		if (!Delete)
		{
			return;
		}
		if (Timer == 0)
		{
			float z = transform.position.z - 4f;
			Vector3 position = transform.position;
			float num = (position.z = z);
			Vector3 vector = (transform.position = position);
			if (GetItem)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(Resources.Load("Other/stars")) as GameObject;
				Global.LastCreatedObject.transform.position = transform.position;
			}
		}
		Timer++;
		Alpha -= 0.03f;
		if (GetItem)
		{
			int num2 = 2;
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			float num3 = (velocity.y = num2);
			Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity);
		}
		else
		{
			int num4 = -2;
			Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
			float num5 = (velocity2.y = num4);
			Vector3 vector5 = (GetComponent<Rigidbody>().velocity = velocity2);
		}
		sprite.color = new Color(1f, 1f, 1f, Alpha);
		if (Timer > 30)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void OnCollisionEnter(Collision other)
	{
		sprite.spriteId = 1;
	}

	public virtual void Main()
	{
	}
}
