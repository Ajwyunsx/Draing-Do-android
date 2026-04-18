using System;
using UnityEngine;

[Serializable]
public class MapSelector : MonoBehaviour
{
	public GameObject button;

	public GameObject tablo;

	private bool activ;

	private bool enter;

	private bool activate;

	private Transform myTransform;

	private string CheckPointName;

	private GameObject oldLastClickObject;

	private int mini_timer;

	public virtual void Awake()
	{
		button.SetActiveRecursively(false);
		tablo.SetActiveRecursively(false);
		Global.Pause = true;
		Global.MenuPause = true;
		CheckPointName = Global.CheckPointName;
		if (CheckPointName == string.Empty || CheckPointName == null)
		{
			CheckPointName = "level";
		}
		myTransform = transform;
		GameObject[] array = null;
		array = GameObject.FindGameObjectsWithTag("MapSlot");
		int i = 0;
		GameObject[] array2 = array;
		for (int length = array2.Length; i < length; i++)
		{
			if (array2[i].name == CheckPointName)
			{
				myTransform.position = array2[i].transform.position;
				break;
			}
		}
	}

	public virtual void Update()
	{
		if (!activate)
		{
			enter = false;
		}
		activate = false;
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0f;
		float x = Input.GetAxis("Horizontal") * 3f;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float num = (velocity.x = x);
		Vector3 vector = (GetComponent<Rigidbody>().velocity = velocity);
		float y = Input.GetAxis("Vertical") * 3f;
		Vector3 velocity2 = GetComponent<Rigidbody>().velocity;
		float num2 = (velocity2.y = y);
		Vector3 vector3 = (GetComponent<Rigidbody>().velocity = velocity2);
		if (!(Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")) <= 0f))
		{
			Global.LastClickObject = null;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.LastClickObject == null && activ)
		{
			activ = false;
			button.SetActiveRecursively(false);
			tablo.SetActiveRecursively(false);
		}
		if (mini_timer > 0)
		{
			mini_timer--;
		}
		if (!Global.LastClickObject)
		{
			return;
		}
		Transform transform = Global.LastClickObject.transform;
		if (!(transform.tag == "MapSlot"))
		{
			return;
		}
		if (Global.LastClickObject != oldLastClickObject)
		{
			mini_timer = 15;
			activ = true;
			Global.LastClickObject.SendMessage("GetText", null, SendMessageOptions.DontRequireReceiver);
			button.SetActiveRecursively(true);
			tablo.SetActiveRecursively(true);
			oldLastClickObject = Global.LastClickObject;
			if ((bool)GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().Play();
			}
		}
		myTransform.position = Vector3.Lerp(myTransform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.0075f), 0.2f);
	}

	public virtual void OnTriggerStay(Collider collision)
	{
		if (mini_timer > 0 || collision.gameObject.tag != "MapSlot")
		{
			return;
		}
		if (GetComponent<Rigidbody>().velocity == Vector3.zero)
		{
			if (collision.gameObject != oldLastClickObject)
			{
				activ = true;
				mini_timer = 15;
				button.SetActiveRecursively(true);
				tablo.SetActiveRecursively(true);
				oldLastClickObject = collision.gameObject;
				Global.LastClickObject = collision.gameObject;
				Global.LastClickObject.SendMessage("GetText", null, SendMessageOptions.DontRequireReceiver);
				if ((bool)GetComponent<AudioSource>())
				{
					GetComponent<AudioSource>().Play();
				}
			}
		}
		else
		{
			Global.LastClickObject = null;
			oldLastClickObject = null;
		}
		if (enter)
		{
			collision.gameObject.SendMessage("PlayLevel", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void ActivateByMouse()
	{
		activate = true;
		enter = true;
	}

	public virtual void Main()
	{
	}
}
