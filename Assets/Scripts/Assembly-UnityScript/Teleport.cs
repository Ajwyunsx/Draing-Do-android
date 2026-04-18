using System;
using UnityEngine;

[Serializable]
public class Teleport : MonoBehaviour
{
	public AudioClip SFX;

	public string level;

	public string checkPointName;

	public int DealyTimer;

	public Transform TeleportTo;

	public Teleport()
	{
		level = string.Empty;
	}

	public virtual void Start()
	{
		if (level == null)
		{
			level = string.Empty;
		}
	}

	public virtual void FixedUpdate()
	{
		if (DealyTimer > 0)
		{
			DealyTimer--;
		}
	}

	public virtual void ActON()
	{
		if (DealyTimer > 0)
		{
			return;
		}
		DealyTimer = 25;
		if ((bool)SFX)
		{
			AudioSource.PlayClipAtPoint(SFX, transform.position);
		}
		if (level == string.Empty)
		{
			if ((bool)TeleportTo)
			{
				Global.CurrentPlayerObject.position = new Vector3(TeleportTo.position.x, TeleportTo.position.y, -1f);
				Global.CurrentPlayerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		}
		else
		{
			Global.LoadLEVEL(level, checkPointName);
		}
	}

	public virtual void Main()
	{
	}
}
