using System;
using UnityEngine;

[Serializable]
public class Monitor : MonoBehaviour
{
	[NonSerialized]
	public static bool On;

	[NonSerialized]
	public static Vector3 XYZ;

	[NonSerialized]
	public static bool Click;

	[NonSerialized]
	public static bool MouseNo;

	[NonSerialized]
	public static bool ForceNo;

	[NonSerialized]
	public static Transform LastOverTrans;

	[NonSerialized]
	public static bool dist;

	[NonSerialized]
	public static bool DontDrop;

	public GUISkin SkinA;

	public GUISkin SkinB;

	[NonSerialized]
	public static string TextA;

	[NonSerialized]
	public static string TextB;

	private string oldTextA;

	public virtual void Awake()
	{
	}

	public virtual void Update()
	{
		Click = false;
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			Click = true;
		}
		Camera currentCamera = Camera.main;
		Vector3 mousePosition = Input.mousePosition;
		if (currentCamera == null || !IsFinite(mousePosition))
		{
			On = false;
			XYZ = Vector3.zero;
			return;
		}
		int num = 2;
		Vector3 position = transform.position;
		float num2 = (position.z = num);
		Vector3 vector = (transform.position = position);
		int layerMask = 32;
		Ray ray = currentCamera.ScreenPointToRay(mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		On = false;
		if (Physics.Raycast(ray, out hitInfo, 100f, layerMask) && GetComponent<Collider>() == hitInfo.collider && hitInfo.transform.gameObject == gameObject)
		{
			On = true;
		}
		float z = Mathf.Abs(currentCamera.transform.position.z) - 1f;
		if (float.IsNaN(z) || float.IsInfinity(z))
		{
			XYZ = Vector3.zero;
			return;
		}
		XYZ = currentCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, z));
		if (!IsFinite(XYZ))
		{
			XYZ = Vector3.zero;
		}
	}

	public virtual void FixedUpdate()
	{
		dist = false;
		if ((bool)Global.CurrentPlayerObject)
		{
			float num = default(float);
			num = (LastOverTrans ? Vector2.Distance(new Vector2(Global.CurrentPlayerObject.position.x, Global.CurrentPlayerObject.position.y), new Vector2(LastOverTrans.position.x, LastOverTrans.position.y)) : Vector2.Distance(new Vector2(Global.CurrentPlayerObject.position.x, Global.CurrentPlayerObject.position.y), new Vector2(XYZ.x, XYZ.y)));
			if (!(num >= 2f))
			{
				dist = true;
			}
		}
		if (!DontDrop)
		{
			if ((bool)SlotItem.selected)
			{
				TextA = string.Empty;
				TextB = "Drop it!";
			}
		}
		else
		{
			TextA = string.Empty;
			TextB = string.Empty;
		}
		DontDrop = false;
		MouseNo = false;
		ForceNo = false;
	}

	public virtual void OnMouseOver()
	{
		TextA = string.Empty;
		if (!SlotItem.selected)
		{
			TextB = string.Empty;
		}
	}

	public virtual void OnGUI()
	{
		if (TalkPause.IsGameplayBlocked())
		{
			return;
		}
		GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1000f, (float)Screen.height / 563f, 1f));
		GUI.skin = SkinA;
		GUI.color = new Color(0f, 0f, 0f, 0.75f);
		GUI.Label(new Rect(0f, 504f, 1001f, 20f), TextA);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.Label(new Rect(0f, 503f, 1000f, 20f), TextA);
		GUI.skin = SkinB;
		GUI.color = new Color(0f, 0f, 0f, 0.75f);
		if (!ForceNo)
		{
			if (dist || !MouseNo)
			{
				GUI.Label(new Rect(0f, 534f, 1001f, 20f), TextB);
				GUI.color = new Color(1f, 1f, 1f, 1f);
				GUI.Label(new Rect(0f, 533f, 1000f, 20f), TextB);
			}
			else if (!string.IsNullOrEmpty(TextA))
			{
				GUI.Label(new Rect(0f, 534f, 1001f, 20f), "Too far from You!");
				GUI.color = new Color(1f, 0.1f, 0.1f, 1f);
				GUI.Label(new Rect(0f, 533f, 1000f, 20f), "Too far from You!");
			}
		}
	}

	public virtual void Main()
	{
		if (!Global.Pause)
		{
		}
	}

	private bool IsFinite(Vector3 value)
	{
		return !float.IsNaN(value.x) && !float.IsInfinity(value.x) && !float.IsNaN(value.y) && !float.IsInfinity(value.y) && !float.IsNaN(value.z) && !float.IsInfinity(value.z);
	}
}
