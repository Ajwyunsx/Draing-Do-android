using System;
using UnityEngine;

[Serializable]
public class SinLine2 : MonoBehaviour
{
	public Color c1;

	public Color c2;

	public LineRenderer lineRenderer;

	public SinLine2()
	{
		c1 = Color.yellow;
		c2 = Color.red;
	}

	public virtual void Start()
	{
		lineRenderer = (LineRenderer)gameObject.AddComponent(typeof(LineRenderer));
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2f, 0f);
		lineRenderer.SetVertexCount(2);
	}

	public virtual void Update()
	{
		for (int i = 0; i < 2; i++)
		{
			if (i == 0)
			{
				lineRenderer.SetPosition(i, Global.CurrentPlayerObject.transform.position);
				continue;
			}
			Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f));
			lineRenderer.SetPosition(i, position);
		}
	}

	public virtual void Main()
	{
	}
}
