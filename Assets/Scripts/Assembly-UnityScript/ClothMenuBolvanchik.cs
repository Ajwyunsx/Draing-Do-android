using System;
using UnityEngine;

[Serializable]
public class ClothMenuBolvanchik : MonoBehaviour
{
	public virtual void Start()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(LoadData.HUD(Global.Cloths[Global.InTheCloth])) as GameObject;
		if (!(gameObject == null))
		{
			ClothTab clothTab = (ClothTab)gameObject.GetComponent("ClothTab");
			if ((bool)clothTab)
			{
				gameObject.SendMessage("GetData", null, SendMessageOptions.DontRequireReceiver);
				UnityEngine.Object.DestroyImmediate(gameObject);
				tk2dSprite tk2dSprite2 = null;
				Transform transform = null;
				transform = this.transform.Find("Body");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[0]);
				transform = this.transform.Find("Body/Hand1");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[1]);
				transform = this.transform.Find("Body/Hand2");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[2]);
				transform = this.transform.Find("Body/Head");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[3]);
				transform = this.transform.Find("Body/Leg1");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[6]);
				transform = this.transform.Find("Body/Leg2");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[7]);
				transform = this.transform.Find("Body/Leg1/Boot1");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[8]);
				transform = this.transform.Find("Body/Leg2/Boot2");
				tk2dSprite2 = (tk2dSprite)transform.gameObject.GetComponent(typeof(tk2dSprite));
				tk2dSprite2.spriteId = tk2dSprite2.GetSpriteIdByName(Global.clothesSpriteNames[9]);
			}
		}
	}

	public virtual void Main()
	{
	}
}
