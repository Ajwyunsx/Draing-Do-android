using System;
using UnityEngine;

[Serializable]
public class EyeForm : MonoBehaviour
{
	public tk2dSprite EyeObject;

	public Transform EyeTrans;

	public string DefaultEye;

	private Vector3 startScale;

	private bool changeEye;

	public EyeForm()
	{
		DefaultEye = "eye";
	}

	public virtual void Awake()
	{
		startScale = EyeTrans.localScale;
	}

	public virtual void EyeSprite(string name)
	{
		if (!changeEye && (bool)EyeObject)
		{
			if (name != string.Empty)
			{
				EyeObject.spriteId = EyeObject.GetSpriteIdByName(name);
			}
			else
			{
				EyeObject.spriteId = EyeObject.GetSpriteIdByName(DefaultEye);
			}
		}
	}

	public virtual void FixedUpdate()
	{
		int num = 0;
		if (Global.Oxygen <= 0)
		{
			changeEye = true;
			EyeObject.spriteId = EyeObject.GetSpriteIdByName("eye2");
		}
		else
		{
			num++;
		}
		if (!(Global.MP > 0f))
		{
			changeEye = true;
			EyeObject.spriteId = EyeObject.GetSpriteIdByName("eye2");
		}
		else
		{
			num++;
		}
		if (changeEye && num == 2)
		{
			changeEye = false;
			EyeObject.spriteId = EyeObject.GetSpriteIdByName(DefaultEye);
		}
	}

	public virtual void LateUpdate()
	{
		if (changeEye)
		{
			EyeTrans.localScale = startScale;
		}
	}

	public virtual void Main()
	{
	}
}
