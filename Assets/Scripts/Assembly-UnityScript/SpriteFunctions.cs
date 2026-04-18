using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SpriteFunctions : MonoBehaviour
{
	private float Alpha;

	private bool toDelete;

	private bool toAppear;

	private Vector3 StartScale;

	public string message;

	private Color StartColor;

	public float AlphaStart;

	public SpriteFunctions()
	{
		Alpha = 1f;
		AlphaStart = 2f;
	}

	public virtual void Awake()
	{
		StartScale = transform.localScale;
		StartColor = GetComponent<Renderer>().material.color;
		if (!string.IsNullOrEmpty(message))
		{
			gameObject.SendMessage(message, 0.075f, SendMessageOptions.DontRequireReceiver);
		}
	}

	public virtual void Start()
	{
		Alpha = AlphaStart;
		GetComponent<Renderer>().material.color = new Color(StartColor.r, StartColor.g, StartColor.b, Alpha);
	}

	public virtual void FixedUpdate()
	{
		if (!(Alpha <= 0f) && (toDelete || toAppear))
		{
			Alpha -= 0.01f;
			GetComponent<Renderer>().material.color = new Color(StartColor.r, StartColor.g, StartColor.b, Alpha);
			if (toDelete && !(Alpha > 0f))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void DeleteTimer()
	{
		toDelete = true;
	}

	public virtual IEnumerator SprayON()
	{
		toAppear = true;
		yield return new WaitForSeconds(0.175f);
		Alpha = 0.55f;
	}

	public virtual void SprayOFF()
	{
	}

	public virtual void FakePartVelocityAndDestroy(float _speed)
	{
		toDelete = true;
		if ((bool)GetComponent<Rigidbody>() && _speed != 0f)
		{
			GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)56;
			GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(0f - _speed, _speed), UnityEngine.Random.Range(0f - _speed, _speed), 0f);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, UnityEngine.Random.Range(0f - _speed, _speed));
		}
		Alpha = 3f;
		transform.parent = null;
	}

	public virtual void SplashStrike()
	{
		toDelete = true;
	}

	public virtual void Main()
	{
	}
}
