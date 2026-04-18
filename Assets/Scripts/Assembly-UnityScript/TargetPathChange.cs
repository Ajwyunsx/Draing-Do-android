using System;
using UnityEngine;

[Serializable]
public class TargetPathChange : MonoBehaviour
{
	public string OLDPATH;

	public string NEWPATH;

	public TargetPathChange()
	{
		NEWPATH = string.Empty;
	}

	public virtual void Start()
	{
		if (!(OLDPATH != Global.CurrentMission))
		{
			if (NEWPATH == string.Empty)
			{
				Global.CurrentMission = gameObject.name;
			}
			else
			{
				Global.CurrentMission = NEWPATH;
			}
		}
	}

	public virtual void Main()
	{
	}
}
