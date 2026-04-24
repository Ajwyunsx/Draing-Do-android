using System;
using UnityEngine;

[Serializable]
public class SinLine : MonoBehaviour
{
	public Color c1;

	public Color c2;

	public int lengthOfLineRenderer;

	public float speed;

	public float length;

	public float step_size;

	public float line_width;

	public LineRenderer lineRenderer;

	public Transform myTransform;

	public SinLine()
	{
		c1 = new Color(1f, 1f, 1f, 0.55f);
		c2 = new Color(1f, 1f, 1f, 0.15f);
		lengthOfLineRenderer = 20;
		speed = 2f;
		length = 0.1f;
		step_size = 0.1f;
		line_width = 0.2f;
	}

	public virtual void Start()
	{
		myTransform = transform;
		lineRenderer = (LineRenderer)gameObject.AddComponent(typeof(LineRenderer));
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.material.color = new Color(1f, 1f, 1f, 0.45f);
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(line_width, line_width);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
	}

	public virtual void Update()
	{
		for (int i = 0; i < lengthOfLineRenderer; i++)
		{
			Vector3 position = myTransform.position + new Vector3((float)i * step_size, Mathf.Sin((float)i + Time.time * speed) * length, 0f);
			lineRenderer.SetPosition(i, position);
		}
	}

	public virtual void Main()
	{
	}
}
