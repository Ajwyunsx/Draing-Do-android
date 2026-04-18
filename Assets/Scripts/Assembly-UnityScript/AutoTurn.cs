using System;
using UnityEngine;

[Serializable]
public class AutoTurn : MonoBehaviour
{
	public Vector3 alwaysTurns;

	public Vector3 alwaysMove;

	private Transform myTransform;

	private int rotateDir;

	public bool rotateDirON;

	public AutoTurn()
	{
		alwaysTurns = Vector3.zero;
		alwaysMove = Vector3.zero;
		rotateDir = 1;
	}

	public virtual void Start()
	{
		myTransform = transform;
		if (rotateDirON)
		{
			rotateDir = (int)Mathf.Sign(myTransform.localScale.x);
		}
	}

	public virtual void FixedUpdate()
	{
		if (alwaysTurns != Vector3.zero)
		{
			myTransform.Rotate(alwaysTurns * rotateDir);
		}
		if (alwaysMove != Vector3.zero)
		{
			transform.Translate(alwaysMove, Space.World);
		}
	}

	public virtual void Main()
	{
	}
}
