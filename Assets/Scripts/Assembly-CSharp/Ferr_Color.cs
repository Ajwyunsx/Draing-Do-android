using System;
using UnityEngine;

public static class Ferr_Color
{
	public static Color FromHex(string aHex)
	{
		if (aHex.Length != 8)
		{
			return Color.red;
		}
		return new Color((float)Convert.ToInt32(string.Empty + aHex[0] + aHex[1]) / 255f, (float)Convert.ToInt32(string.Empty + aHex[2] + aHex[3]) / 255f, (float)Convert.ToInt32(string.Empty + aHex[4] + aHex[5]) / 255f, (float)Convert.ToInt32(string.Empty + aHex[6] + aHex[7]) / 255f);
	}

	public static string ToHex(Color aColor)
	{
		return string.Format("{0:X}{1:X}{2:X}{3:X}", (int)(aColor.r * 255f), (int)(aColor.g * 255f), (int)(aColor.b * 255f), (int)(aColor.a * 255f));
	}
}
