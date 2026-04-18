using System;
using UnityEngine;

[Serializable]
public class Looker : MonoBehaviour
{
	private Vector3 startPosition;

	[NonSerialized]
	public static Transform target;

	private float correct;

	private float correctX;

	public bool canDown;

	public bool startLooker;

	public virtual void Awake()
	{
		target = transform;
		startPosition = transform.localPosition;
	}

	public virtual void Start()
	{
		if (startLooker)
		{
			target = transform;
		}
	}

	public virtual void FixedUpdate()
	{
		if (Global.Pause || TalkPause.IsGameplayBlocked() || !canDown)
		{
			return;
		}
		if (!(Input.GetAxis("Vertical") >= -0.2f))
		{
			correct += 0.2f;
			if (!(correct <= 4f))
			{
				correct = 4f;
			}
			float y = startPosition.y - correct;
			Vector3 localPosition = transform.localPosition;
			float num = (localPosition.y = y);
			Vector3 vector = (transform.localPosition = localPosition);
		}
		else
		{
			correct = 0f;
			float y2 = Mathf.Lerp(transform.localPosition.y, startPosition.y, 0.25f);
			Vector3 localPosition2 = transform.localPosition;
			float num2 = (localPosition2.y = y2);
			Vector3 vector3 = (transform.localPosition = localPosition2);
		}
		if (!(Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.2f))
		{
			correctX -= 0.05f;
			if (!(Mathf.Abs(correctX) <= 4.5f))
			{
				correctX = Mathf.Sign(correctX) * 4.5f;
			}
			float x = startPosition.x - correctX;
			Vector3 localPosition3 = transform.localPosition;
			float num3 = (localPosition3.x = x);
			Vector3 vector5 = (transform.localPosition = localPosition3);
		}
		else
		{
			correctX = 0f;
			float x2 = Mathf.Lerp(transform.localPosition.x, startPosition.x, 0.1f);
			Vector3 localPosition4 = transform.localPosition;
			float num4 = (localPosition4.x = x2);
			Vector3 vector7 = (transform.localPosition = localPosition4);
		}
	}

	public virtual void Main()
	{
	}
}
