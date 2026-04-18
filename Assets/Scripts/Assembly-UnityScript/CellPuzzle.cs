using System;
using UnityEngine;

[Serializable]
public class CellPuzzle : MonoBehaviour
{
	public float CellX;

	public float CellY;

	public int CellAngZ;

	public int CellAngX;

	public float MagnetPower;

	public Vector2 MaxX;

	public Vector2 MaxY;

	private int LastX;

	private int LastY;

	private int LastAng;

	private Transform trans;

	public CellPuzzle()
	{
		MagnetPower = 0.06f;
	}

	public virtual void Start()
	{
		trans = transform;
		if (CellAngZ != 0)
		{
			CellAngZ = 360 / CellAngZ;
		}
		if (CellAngX != 0)
		{
			CellAngX = 360 / CellAngX;
		}
	}

	public virtual void Update()
	{
		if (CellX != 0f)
		{
			LastX = Convert.ToInt32(trans.localPosition.x / CellX);
			if (!(trans.localPosition.x / CellX >= MaxX.x))
			{
				float x = MaxX.x * CellX;
				Vector3 localPosition = trans.localPosition;
				float num = (localPosition.x = x);
				Vector3 vector = (trans.localPosition = localPosition);
			}
			if (!(trans.localPosition.x / CellX <= MaxX.y))
			{
				float x2 = MaxX.y * CellX;
				Vector3 localPosition2 = trans.localPosition;
				float num2 = (localPosition2.x = x2);
				Vector3 vector3 = (trans.localPosition = localPosition2);
			}
		}
		if (CellY != 0f)
		{
			LastY = Convert.ToInt32(trans.localPosition.y / CellY);
			if (!(trans.localPosition.y / CellY >= MaxY.x))
			{
				float y = MaxY.x * CellY;
				Vector3 localPosition3 = trans.localPosition;
				float num3 = (localPosition3.y = y);
				Vector3 vector5 = (trans.localPosition = localPosition3);
			}
			if (!(trans.localPosition.y / CellY <= MaxY.y))
			{
				float y2 = MaxY.y * CellY;
				Vector3 localPosition4 = trans.localPosition;
				float num4 = (localPosition4.y = y2);
				Vector3 vector7 = (trans.localPosition = localPosition4);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		if (CellX != 0f)
		{
			float x = Mathf.Lerp(trans.localPosition.x, (float)LastX * CellX, MagnetPower);
			Vector3 localPosition = trans.localPosition;
			float num = (localPosition.x = x);
			Vector3 vector = (trans.localPosition = localPosition);
		}
		if (CellY != 0f)
		{
			float y = Mathf.Lerp(trans.localPosition.y, (float)LastY * CellY, MagnetPower);
			Vector3 localPosition2 = trans.localPosition;
			float num2 = (localPosition2.y = y);
			Vector3 vector3 = (trans.localPosition = localPosition2);
		}
		if (CellAngZ != 0)
		{
			LastAng = Convert.ToInt32(trans.eulerAngles.z / (float)CellAngZ);
			float z = Mathf.LerpAngle(trans.eulerAngles.z, LastAng * CellAngZ, MagnetPower);
			Vector3 eulerAngles = trans.eulerAngles;
			float num3 = (eulerAngles.z = z);
			Vector3 vector5 = (trans.eulerAngles = eulerAngles);
		}
		if (CellAngX != 0)
		{
			LastAng = Convert.ToInt32(trans.eulerAngles.x / (float)CellAngX);
			float x2 = Mathf.LerpAngle(trans.eulerAngles.x, LastAng * CellAngX, MagnetPower);
			Vector3 eulerAngles2 = trans.eulerAngles;
			float num4 = (eulerAngles2.x = x2);
			Vector3 vector7 = (trans.eulerAngles = eulerAngles2);
		}
	}

	public virtual void Main()
	{
	}
}
