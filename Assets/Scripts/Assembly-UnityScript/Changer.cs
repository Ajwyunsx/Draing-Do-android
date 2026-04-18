using System;
using UnityEngine;

[Serializable]
public class Changer : MonoBehaviour
{
	public tk2dSprite leg1A;

	public tk2dSprite leg2A;

	public string legAName;

	public tk2dSprite leg1B;

	public tk2dSprite leg2B;

	public string legBName;

	public tk2dSprite leg1C;

	public tk2dSprite leg2C;

	public string legCName;

	public tk2dSprite hand1A;

	public tk2dSprite hand2A;

	public string handAName;

	public tk2dSprite hand1B;

	public tk2dSprite hand2B;

	public string handBName;

	public tk2dSprite hand1C;

	public tk2dSprite hand2C;

	public string handCName;

	public tk2dSprite head;

	public string headName;

	public tk2dSprite mouth;

	public string mouthName;

	public tk2dSprite wing1;

	public tk2dSprite wing2;

	public string wingName;

	public tk2dSprite body;

	public string bodyName;

	public tk2dSprite neck;

	public string neckName;

	public tk2dSprite ear1;

	public tk2dSprite ear2;

	public string earName;

	public GameObject HairUp;

	public tk2dSprite Hair1;

	public string Hair1S;

	public tk2dSprite Hair2;

	public string Hair2S;

	public tk2dSprite Tail;

	public string TailS;

	public tk2dSprite Eye;

	public string EyeS;

	public GameObject cutiemark;

	public virtual void Start()
	{
	}

	public virtual void Change()
	{
		leg1A.spriteId = leg1A.GetSpriteIdByName(legAName);
		leg2A.spriteId = leg2A.GetSpriteIdByName(legAName);
		leg1B.spriteId = leg1B.GetSpriteIdByName(legBName);
		leg2B.spriteId = leg2B.GetSpriteIdByName(legBName);
		leg1C.spriteId = leg1C.GetSpriteIdByName(legCName);
		leg2C.spriteId = leg2C.GetSpriteIdByName(legCName);
		hand1A.spriteId = hand1A.GetSpriteIdByName(handAName);
		hand2A.spriteId = hand2A.GetSpriteIdByName(handAName);
		hand1B.spriteId = hand1B.GetSpriteIdByName(handBName);
		hand2B.spriteId = hand2B.GetSpriteIdByName(handBName);
		hand1C.spriteId = hand1C.GetSpriteIdByName(handCName);
		hand2C.spriteId = hand2C.GetSpriteIdByName(handCName);
		head.spriteId = head.GetSpriteIdByName(headName);
		mouth.spriteId = mouth.GetSpriteIdByName(mouthName);
		wing1.spriteId = wing1.GetSpriteIdByName(wingName);
		wing2.spriteId = wing2.GetSpriteIdByName(wingName);
		body.spriteId = body.GetSpriteIdByName(bodyName);
		neck.spriteId = neck.GetSpriteIdByName(neckName);
		ear1.spriteId = ear1.GetSpriteIdByName(earName);
		ear2.spriteId = ear2.GetSpriteIdByName(earName);
		HairUp.SetActive(true);
		Hair1.spriteId = Hair1.GetSpriteIdByName(Hair1S);
		Hair2.spriteId = Hair2.GetSpriteIdByName(Hair2S);
		Tail.spriteId = Tail.GetSpriteIdByName(TailS);
		Eye.spriteId = Eye.GetSpriteIdByName(EyeS);
		cutiemark.SetActive(true);
	}

	public virtual void Main()
	{
	}
}
