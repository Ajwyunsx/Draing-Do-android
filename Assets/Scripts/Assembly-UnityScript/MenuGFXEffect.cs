using System;
using UnityEngine;

[Serializable]
public class MenuGFXEffect : MonoBehaviour
{
	public Vector3 turn;

	private Vector3 startPos;

	public float speed;

	public MenuGFXEffect()
	{
		turn = new Vector3(0f, 10f, 0f);
		speed = 0.2f;
	}

	public virtual void Start()
	{
		startPos = transform.localPosition;
		transform.localPosition = startPos + turn;
	}

	public virtual void Update()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, speed * Global.deltaTime * 80f);
	}

	public virtual void Main()
	{
	}
}
