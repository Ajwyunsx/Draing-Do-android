using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class LoadData : MonoBehaviour
{
	public RESOURCE[] DataGO;

	[NonSerialized]
	public static RESOURCE[] SDataGO;

	public RESOURCE[] DataGFX;

	[NonSerialized]
	public static RESOURCE[] SDataGFX;

	public RESOURCE[] DataHUD;

	[NonSerialized]
	public static RESOURCE[] SDataHUD;

	public SFXRESOURCE[] DataSFX;

	[NonSerialized]
	public static SFXRESOURCE[] SDataSFX;

	public TEXRESOURCE[] DataTEX;

	[NonSerialized]
	public static TEXRESOURCE[] SDataTEX;

	[NonSerialized]
	public static int num;

	public virtual void Awake()
	{
		SDataGO = DataGO;
		SDataGFX = DataGFX;
		SDataHUD = DataHUD;
		SDataSFX = DataSFX;
		SDataTEX = DataTEX;
	}

	public static GameObject GO(string name)
	{
		num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)SDataGO))
			{
				if (SDataGO[num].name == name)
				{
					result = SDataGO[num].@object;
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("GO: " + name + " NULL");
			result = null;
			break;
		}
		return (GameObject)result;
	}

	public static GameObject GFX(string name)
	{
		num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)SDataGFX))
			{
				if (SDataGFX[num].name == name)
				{
					result = SDataGFX[num].@object;
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("GFX: " + name + " NULL");
			result = null;
			break;
		}
		return (GameObject)result;
	}

	public static GameObject HUD(string name)
	{
		num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)SDataHUD))
			{
				if (SDataHUD[num].name == name)
				{
					result = SDataHUD[num].@object;
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("HUD: " + name + " NULL");
			result = null;
			break;
		}
		return (GameObject)result;
	}

	public static AudioClip SFX(string name)
	{
		num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)SDataSFX))
			{
				if (SDataSFX[num].name == name)
				{
					result = SDataSFX[num].sound;
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("SFX: " + name + " NULL");
			result = null;
			break;
		}
		return (AudioClip)result;
	}

	public static Texture TEX(string name)
	{
		num = 0;
		object result;
		while (true)
		{
			if (num < Extensions.get_length((System.Array)SDataTEX))
			{
				if (SDataTEX[num].name == name)
				{
					result = SDataTEX[num].texture;
					break;
				}
				num++;
				continue;
			}
			MonoBehaviour.print("TEX: " + name + " NULL");
			result = null;
			break;
		}
		return (Texture)result;
	}

	public virtual void Main()
	{
	}
}
