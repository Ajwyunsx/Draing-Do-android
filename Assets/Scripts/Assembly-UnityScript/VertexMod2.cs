using System;
using UnityEngine;

[Serializable]
public class VertexMod2 : MonoBehaviour
{
	public float scale;

	public float speed;

	public float size;

	public bool move_x;

	public bool correct_waving;

	private float[] mass;

	private Vector3[] baseHeight;

	private Vector3[] vertices;

	private Mesh mesh;

	private int ALL;

	public float START;

	public float END;

	private float MIN;

	private float MAX;

	private float ALLInside;

	public float Correction;

	public VertexMod2()
	{
		scale = 0.3f;
		speed = 4f;
		size = 0.75f;
		END = 100f;
	}

	public virtual void Start()
	{
		mesh = ((MeshFilter)GetComponent(typeof(MeshFilter))).mesh;
		if (baseHeight == null)
		{
			baseHeight = mesh.vertices;
		}
		vertices = new Vector3[baseHeight.Length];
		mass = new float[vertices.Length];
		ALL = vertices.Length - 1;
		MIN = (float)ALL / 100f * START;
		MAX = (float)ALL / 100f * END;
		ALLInside = MAX - MIN;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (!((float)i >= MIN))
			{
				mass[i] = Correction - 0f;
				continue;
			}
			if (!((float)i >= MAX))
			{
				mass[i] = Correction - 1f;
				continue;
			}
			if (ALLInside == 0f)
			{
				break;
			}
			mass[i] = (Correction - (float)i) / ALLInside;
		}
	}

	public virtual void Update()
	{
		int num = 0;
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vector = baseHeight[i];
			if (correct_waving)
			{
				num = i;
			}
			int num2 = i;
			if (i % 2 == 1)
			{
				num2 = i - 1;
			}
			if (!move_x)
			{
				vector.y += Mathf.Sin(Time.time * speed + baseHeight[num2].x + baseHeight[num2].y + baseHeight[num2].z + (float)num * 0.75f * size) * scale * mass[i];
			}
			else
			{
				vector.x += Mathf.Sin(Time.time * speed + baseHeight[num2].x + baseHeight[num2].y + baseHeight[num2].z + (float)num * 0.75f * size) * scale * mass[i];
			}
			vertices[i] = vector;
		}
		mesh.vertices = vertices;
	}

	public virtual void SetNewSpeed(float s)
	{
	}

	public virtual void SetNewScale(float s)
	{
		scale = s;
	}

	public virtual void Main()
	{
	}
}
