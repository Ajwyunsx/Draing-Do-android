using System;
using UnityEngine;

[Serializable]
public class SpriteColorLikeAmbient : MonoBehaviour
{
	public virtual void Recolor()
	{
		tk2dSprite tk2dSprite2 = null;
		tk2dSprite2 = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		if ((bool)tk2dSprite2)
		{
			tk2dSprite2.color = Global.OldAmbient;
			gameObject.GetComponent<Renderer>().material.color = Global.OldAmbient;
		}
	}

	public virtual void Main()
	{
	}
}
