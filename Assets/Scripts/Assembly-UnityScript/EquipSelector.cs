using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class EquipSelector : MonoBehaviour
{
	public string VarName;

	public Transform[] Lists;

	private int oldVarName;

	private Vector3 target;

	private Transform trans;

	private bool onceCheck;

	public GameObject MasterObject;

	public EquipSelector()
	{
		oldVarName = -111;
	}

	public virtual void Awake()
	{
		trans = transform;
	}

	public virtual void Update()
	{
		if (!onceCheck && (bool)MasterObject)
		{
			onceCheck = true;
			int num = default(int);
			for (int i = 0; i < Extensions.get_length((System.Array)Lists); i++)
			{
				if (Lists[i].GetComponent<Collider>().enabled)
				{
					num++;
				}
			}
			if (num <= 1)
			{
				UnityEngine.Object.Destroy(MasterObject);
			}
		}
		int num2 = Convert.ToInt32(Global.Var[VarName]);
		if (oldVarName != num2)
		{
			oldVarName = num2;
			target = Lists[num2].localPosition + new Vector3(0f, 0f, -0.35f);
			trans.localPosition = target;
		}
	}

	public virtual void Main()
	{
	}
}
