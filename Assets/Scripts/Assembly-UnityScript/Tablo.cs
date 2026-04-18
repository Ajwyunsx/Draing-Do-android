using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Tablo : MonoBehaviour
{
	public int[] frameNumbers;

	public int currentNumber;

	public bool IsOnlyByON;

	[HideInInspector]
	public tk2dSprite sprite;

	public virtual void Start()
	{
		sprite = (tk2dSprite)gameObject.GetComponent(typeof(tk2dSprite));
		for (int i = 0; i < Extensions.get_length((System.Array)frameNumbers); i++)
		{
			if (frameNumbers[i] < 0)
			{
				frameNumbers[i] = 0;
			}
		}
	}

	public virtual void ByButton(bool @bool)
	{
		if ((!IsOnlyByON || @bool) && Extensions.get_length((System.Array)frameNumbers) != 0)
		{
			currentNumber++;
			if (currentNumber >= Extensions.get_length((System.Array)frameNumbers))
			{
				currentNumber = 0;
			}
			sprite.spriteId = frameNumbers[currentNumber];
		}
	}

	public virtual void Main()
	{
	}
}
