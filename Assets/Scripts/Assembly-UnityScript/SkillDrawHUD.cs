using System;
using UnityEngine;

[Serializable]
public class SkillDrawHUD : MonoBehaviour
{
	private GameObject Icon;

	private int oldSkill;

	public int SkillNum;

	public SkillDrawHUD()
	{
		oldSkill = -1;
		SkillNum = -1;
	}

	public virtual void Start()
	{
		EquipWeapon();
	}

	public virtual void FixedUpdate()
	{
		if (SkillNum == -1)
		{
			EquipWeapon();
		}
	}

	public virtual void OffSkillDraw()
	{
		UnityEngine.Object.Destroy(this);
	}

	public virtual void EquipWeapon()
	{
		int num = default(int);
		num = ((SkillNum != -1) ? SkillNum : Convert.ToInt32(Global.Var["Skill"]));
		if (oldSkill != num)
		{
			oldSkill = num;
			if ((bool)Icon)
			{
				UnityEngine.Object.Destroy(Icon);
			}
			GameObject skill = SlotLib.GetSkill(oldSkill);
			if (skill == null)
			{
				oldSkill = -1;
				return;
			}
			Icon = UnityEngine.Object.Instantiate(skill, transform.position + new Vector3(0f, 0f, -0.01f), Quaternion.identity);
			if (SkillNum == -1)
			{
				Icon.SendMessage("Custom", "ok", SendMessageOptions.DontRequireReceiver);
			}
			Icon.transform.parent = transform;
			if (SkillNum != -1)
			{
				Icon.transform.localScale = Icon.transform.localScale * 0.125f;
			}
			EnableIconVisuals();
		}
	}

	public virtual void ListUse()
	{
	}

	public virtual void Main()
	{
	}

	private void EnableIconVisuals()
	{
		if (!(Icon != null))
		{
			return;
		}
		Renderer[] componentsInChildren = Icon.GetComponentsInChildren<Renderer>(true);
		foreach (Renderer renderer in componentsInChildren)
		{
			renderer.enabled = true;
		}
		SpriteRenderer[] componentsInChildren2 = Icon.GetComponentsInChildren<SpriteRenderer>(true);
		foreach (SpriteRenderer spriteRenderer in componentsInChildren2)
		{
			spriteRenderer.enabled = true;
			Color color = spriteRenderer.color;
			color.a = 1f;
			spriteRenderer.color = color;
		}
		tk2dBaseSprite[] componentsInChildren3 = Icon.GetComponentsInChildren<tk2dBaseSprite>(true);
		foreach (tk2dBaseSprite tk2dBaseSprite in componentsInChildren3)
		{
			Color color2 = tk2dBaseSprite.color;
			color2.a = 1f;
			tk2dBaseSprite.color = color2;
		}
	}
}
