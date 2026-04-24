using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class CurrentToolSlot : MonoBehaviour
{
	private string CurrentCount;

	private GUISkin newSkin;

	private bool textON;

	private GameObject GO;

	private int currentToolNumber;

	private int toolsCount;

	private bool neverending;

	public virtual void Start()
	{
		CheckToCreateIcon();
	}

	public virtual void FixedUpdate()
	{
		if (currentToolNumber != Global.CurrentToolNumber)
		{
			CheckToCreateIcon();
		}
		if (!neverending && toolsCount != Global.ToolsCount[Global.CurrentToolNumber])
		{
			toolsCount = Global.ToolsCount[Global.CurrentToolNumber];
			CurrentCount = string.Empty + Global.ToolsCount[Global.CurrentToolNumber];
		}
	}

	public virtual void CheckToCreateIcon()
	{
		UnityEngine.Object.DestroyImmediate(GO);
		neverending = true;
		textON = false;
		string text = Global.Tools[Global.CurrentToolNumber];
		if (text == null)
		{
			text = string.Empty;
		}
		if (!(text != string.Empty))
		{
			return;
		}
		currentToolNumber = Global.CurrentToolNumber;
		GO = UnityEngine.Object.Instantiate(LoadData.HUD(text), new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.005f), this.transform.rotation) as GameObject;
		Global.PrepareUiForOverlay(GO);
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(GO.transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			if (!(obj is Transform))
			{
				obj = RuntimeServices.Coerce(obj, typeof(Transform));
			}
			Transform transform = (Transform)obj;
			transform.GetComponent<Renderer>().material = Global.NoLiteMat;
			UnityRuntimeServices.Update(enumerator, transform);
		}
		GO.transform.parent = this.gameObject.transform;
		if ((bool)GO.GetComponent<Collider>())
		{
			GO.GetComponent<Collider>().enabled = false;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(LoadData.GO(text)) as GameObject;
		if ((bool)gameObject)
		{
			GetToolItem getToolItem = (GetToolItem)gameObject.GetComponent(typeof(GetToolItem));
			neverending = getToolItem.toolOptions.neverending;
			if (!getToolItem.toolOptions.neverending)
			{
				toolsCount = Global.ToolsCount[Global.CurrentToolNumber];
				CurrentCount = string.Empty + Global.ToolsCount[Global.CurrentToolNumber];
				newSkin = Global.DefaultSKIN;
				textON = true;
			}
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	public virtual void OnGUI()
	{
		if (textON)
		{
			GUI.skin = Global.DefaultSKIN;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3((float)Screen.width / 1024f, (float)Screen.height / 768f, 1f));
			float num = 1024f / (float)Screen.width;
			float num2 = 768f / (float)Screen.height;
			Vector3 vector = Camera.main.WorldToScreenPoint(transform.position - new Vector3(gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f, (0f - gameObject.GetComponent<Renderer>().bounds.size.y) * 0.5f, 0f));
			GUI.Box(new Rect(vector.x * num, ((float)Screen.height - vector.y) * num2, 64f, 50f), CurrentCount);
		}
	}

	public virtual void OnMouseDown()
	{
		if (!Global.Pause)
		{
			Global.BlockMouseTime = 10;
			Global.SetPauseMenu("MenuTools");
		}
	}

	public virtual void OnMouseOver()
	{
		if (Global.MouseTrig)
		{
			Global.BlockMouseTime = 5;
		}
	}

	public virtual void Main()
	{
	}
}
