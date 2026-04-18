using System;
using System.Collections.Generic;

[Serializable]
public class Ferr2D_Animation
{
	public string name;

	public string next;

	public float maxTime;

	public List<Ferr2D_Frame> frames;

	public Ferr2D_LoopMode loop;

	public Ferr2D_Animation(string aName)
	{
		name = aName;
		next = string.Empty;
		frames = new List<Ferr2D_Frame>();
	}

	public int GetFrame(float aTime)
	{
		if (loop == Ferr2D_LoopMode.Loop)
		{
			aTime %= maxTime;
		}
		float num = 0f;
		for (int i = 0; i < frames.Count; i++)
		{
			num += frames[i].duration;
			if (aTime < num)
			{
				return frames[i].index;
			}
		}
		return frames[frames.Count - 1].index;
	}

	public bool UpdateTime(ref float aTime)
	{
		if (aTime > maxTime)
		{
			if (loop == Ferr2D_LoopMode.Loop)
			{
				aTime -= maxTime;
			}
			if (loop == Ferr2D_LoopMode.Stop)
			{
				aTime = maxTime;
			}
			return true;
		}
		return false;
	}

	public void CalcMax()
	{
		maxTime = 0f;
		for (int i = 0; i < frames.Count; i++)
		{
			maxTime += frames[i].duration;
		}
	}
}
