using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class TalkCore : MonoBehaviour
{
	public GameObject TextObject;

	public float timez;

	private float oldTime;

	public TalkCore()
	{
		timez = 0.5f;
	}

	public virtual void Awake()
	{
		Input.ResetInputAxes();
		oldTime = Time.realtimeSinceStartup;
		MonoBehaviour.print("Awake scene!");
	}

	public virtual void SetTalk(string text)
	{
		string[] array = text.Split("/"[0]);
		for (int i = 0; i < Extensions.get_length((System.Array)array); i++)
		{
			Global.LastCreatedObject = UnityEngine.Object.Instantiate(TextObject);
			Global.LastCreatedObject.transform.position = transform.position + new Vector3(0f, -0.2f - (float)i * 0.032f, -0.1f);
			Global.LastCreatedObject.BroadcastMessage("Rename", array[i], SendMessageOptions.DontRequireReceiver);
			Global.LastCreatedObject.transform.parent = transform;
		}
	}

	public virtual void Update()
	{
		if ((Input.GetKeyDown("escape") || Input.GetKeyDown("return") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Strike")) && !(timez > Time.realtimeSinceStartup - oldTime))
		{
			MonoBehaviour.print("escapere");
			Global.Pause = false;
			Global.HUD_ON = true;
			Time.timeScale = 1f;
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public virtual void Main()
	{
	}
}
