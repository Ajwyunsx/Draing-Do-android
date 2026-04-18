using System;
using UnityEngine;

[Serializable]
public class PseudoParent : MonoBehaviour
{
	private Transform Parent;

	private Transform trans;

	public virtual void Awake()
	{
		trans = transform;
		Parent = trans.parent.transform;
		trans.parent = null;
	}

	public virtual void LateUpdate()
	{
		if (!(Mathf.Sign(Parent.lossyScale.z) <= 0f))
		{
			int num = 180;
			Vector3 localEulerAngles = trans.localEulerAngles;
			float num2 = (localEulerAngles.y = num);
			Vector3 vector = (trans.localEulerAngles = localEulerAngles);
		}
		else
		{
			int num3 = 0;
			Vector3 localEulerAngles2 = trans.localEulerAngles;
			float num4 = (localEulerAngles2.y = num3);
			Vector3 vector3 = (trans.localEulerAngles = localEulerAngles2);
		}
		trans.position = Parent.position;
		float z = Parent.eulerAngles.z;
		Vector3 eulerAngles = trans.eulerAngles;
		float num5 = (eulerAngles.z = z);
		Vector3 vector5 = (trans.eulerAngles = eulerAngles);
	}

	public virtual void Main()
	{
	}
}
