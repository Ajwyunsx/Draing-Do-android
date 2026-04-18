using System;
using UnityEngine;

[Serializable]
public class SkillMode : MonoBehaviour
{
	public string skillAnim;

	public float SkillPower;

	public int skillTime;

	public float SkiLLStamina;

	public bool skillNoDelete;

	public SkillMode()
	{
		skillAnim = "Pony Whip";
		SkillPower = 1f;
		skillTime = 25;
		SkiLLStamina = 1f;
	}

	public virtual void Custom(string name)
	{
		Global.skillAnim = skillAnim;
		Global.skillPower = SkillPower;
		Global.skillTime = skillTime;
		Global.skillStamina = SkiLLStamina;
		Global.skillNoDelete = skillNoDelete;
		Global.skillGOName = gameObject.name;
	}

	public virtual void Main()
	{
	}
}
