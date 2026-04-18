using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Globweal : MonoBehaviour
{
	public object ISFIRST;

	[NonSerialized]
	public static int HP = 5;

	[NonSerialized]
	public static int MaxHP = 5;

	[NonSerialized]
	public static int Boolet = 10;

	[NonSerialized]
	public static float Rainbow;

	[NonSerialized]
	public static int Potion;

	[NonSerialized]
	public static int Gold;

	[NonSerialized]
	public static int GunPower = 1;

	[NonSerialized]
	public static int MaxRainbow = 10;

	[NonSerialized]
	public static int Weather;

	[NonSerialized]
	public static int Clock;

	[NonSerialized]
	public static UnityScript.Lang.Array Place = new UnityScript.Lang.Array();

	[NonSerialized]
	public static UnityScript.Lang.Array Quest = new UnityScript.Lang.Array();

	[NonSerialized]
	public static UnityScript.Lang.Array Stuff = new UnityScript.Lang.Array();

	[NonSerialized]
	[HideInInspector]
	public static bool Pause = true;

	[NonSerialized]
	[HideInInspector]
	public static bool MuteSFX;

	[NonSerialized]
	[HideInInspector]
	public static bool MuteMusic;

	[NonSerialized]
	[HideInInspector]
	public static string LastClickName = string.Empty;

	[NonSerialized]
	[HideInInspector]
	public static GameObject LastCreatedObject;

	public GUISkin newSkin;

	public Texture HP1_Texture;

	public Texture HP2_Texture;

	public virtual void Awake()
	{
		GameObject[] array = null;
		array = GameObject.FindGameObjectsWithTag("GLOBAL");
		if (array.Length > 1)
		{
			int i = 0;
			GameObject[] array2 = array;
			for (int length = array2.Length; i < length; i++)
			{
				if (!RuntimeServices.EqualityOperator(((Global)array2[i].GetComponent(typeof(Global))).ISFIRST, true))
				{
					UnityEngine.Object.Destroy(array2[i]);
				}
			}
		}
		ISFIRST = true;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	public virtual void Start()
	{
	}

	public virtual void FixedUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			GetMyTarget();
		}
		if (HP <= 0)
		{
			HP = MaxHP;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public virtual void GetMyTarget()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 1000f))
		{
			hitInfo.transform.SendMessage("Activate", null, SendMessageOptions.DontRequireReceiver);
			LastClickName = hitInfo.transform.name;
		}
	}

	public virtual void OnGUI()
	{
		GUI.skin = newSkin;
		GUI.color = new Color(0f, 0f, 0f, 1f);
		GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1024f, (float)Screen.height / 768f, 1f));
		float num = 1024f / (float)Screen.width;
		float num2 = 768f / (float)Screen.height;
		Vector3 vector = default(Vector3);
		if (Event.current.type.Equals(EventType.Repaint))
		{
			vector = Camera.main.WorldToScreenPoint(new Vector3(-13f, 0f, 0f));
			for (int i = 0; i < MaxHP; i++)
			{
				if (i < HP)
				{
					Graphics.DrawTexture(new Rect(vector.x * num + (float)(i * 35), 10f, 50f, 50f), HP1_Texture);
				}
				else
				{
					Graphics.DrawTexture(new Rect(vector.x * num + (float)(i * 35), 10f, 50f, 50f), HP2_Texture);
				}
			}
		}
		vector = Camera.main.WorldToScreenPoint(new Vector3(-6.5f, 0f, 0f));
		float x = GUI.skin.GetStyle("label").CalcSize(new GUIContent(Boolet.ToString())).x;
		GUI.Label(new Rect(vector.x * num, 680f, x, 55f), Boolet.ToString());
		vector = Camera.main.WorldToScreenPoint(new Vector3(-0.5f, 0f, 0f));
		x = GUI.skin.GetStyle("label").CalcSize(new GUIContent(Potion.ToString())).x;
		GUI.Label(new Rect(vector.x * num, 680f, x, 55f), Potion.ToString());
		vector = Camera.main.WorldToScreenPoint(new Vector3(6f, 0f, 0f));
		int num3 = (int)Rainbow;
		x = GUI.skin.GetStyle("label").CalcSize(new GUIContent(num3.ToString())).x;
		GUI.Label(new Rect(vector.x * num, 680f, x, 55f), num3.ToString());
	}

	public virtual void Main()
	{
	}
}
