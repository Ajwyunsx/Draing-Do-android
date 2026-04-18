using System;
using UnityEngine;

[Serializable]
public class JumpCrush : MonoBehaviour
{
	public bool CrushByClick;

	private bool once;

	public string NameItem;

	public bool NoOnce;

	public JumpCrush()
	{
		CrushByClick = true;
		NameItem = "Box";
	}

	public virtual void OnMouseDown()
	{
		if (CrushByClick && Monitor.dist)
		{
			CrushItNow();
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag != "Player") && !(other.GetComponent<Rigidbody>().velocity.y >= -1f))
		{
			CrushItNow();
			int num = 7;
			Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
			float num2 = (velocity.y = num);
			Vector3 vector = (other.gameObject.GetComponent<Rigidbody>().velocity = velocity);
			other.SendMessage("ReDoubleJump", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void CrushItNow()
	{
		if (!once || NoOnce)
		{
			once = true;
			gameObject.SendMessage("CrushHP", 1, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void OnMouseOver()
	{
		Monitor.TextA = NameItem;
		Monitor.DontDrop = true;
		Global.MouseMode = "just";
		Monitor.MouseNo = true;
	}

	public virtual void Main()
	{
	}
}
