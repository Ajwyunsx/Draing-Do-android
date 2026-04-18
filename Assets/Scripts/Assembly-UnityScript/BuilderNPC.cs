using System;
using UnityEngine;

[Serializable]
public class BuilderNPC : MonoBehaviour
{
	private int BuyTimer;

	public GameObject BuildLevelObject;

	public GameObject MenuObject;

	public virtual void Awake()
	{
		if (Global.BuildCastleLevel > 0)
		{
			for (int i = 0; i < Global.BuildCastleLevel; i++)
			{
				Global.LastCreatedObject = UnityEngine.Object.Instantiate(BuildLevelObject, new Vector3(16f, 19f + 5.22f * (float)i, 0f + 0.0001f * (float)i), Quaternion.identity);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (BuyTimer > 0)
		{
			BuyTimer--;
		}
		if (Global.BuildCastleLevel >= 10)
		{
			gameObject.SendMessage("DISAPPEAR", null, SendMessageOptions.DontRequireReceiver);
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void ActON()
	{
		if (BuyTimer <= 0)
		{
			BuyTimer = 50;
			if (Global.Gold >= Global.BuildPrice())
			{
				Global.YesNoObject = gameObject;
				Global.YesMessage = "Want Build Level?";
				Global.CreateMenuWindowObj(MenuObject);
			}
			else
			{
				AudioSource.PlayClipAtPoint(Global.SFXNo, transform.position);
			}
		}
	}

	public virtual void RealActON()
	{
		if ((bool)GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().Play();
		}
		if ((bool)GetComponent<Animation>())
		{
			GetComponent<Animation>().Play();
		}
		AudioSource.PlayClipAtPoint(Global.SFXBuy, transform.position);
		Global.Gold -= Global.BuildPrice();
		Global.BuildCastleLevel++;
		Global.LastCreatedObject = UnityEngine.Object.Instantiate(BuildLevelObject, new Vector3(16f, 19f + 5.22f * (float)(Global.BuildCastleLevel - 1), 0f + 0.0001f * (float)(Global.BuildCastleLevel - 1)), Quaternion.identity);
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}
}
