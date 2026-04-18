using System;
using UnityEngine;

[Serializable]
public class VertexMod : MonoBehaviour
{
	public float scale;

	public float speed;

	public float size;

	public bool move_x;

	public bool correct_waving;

	public bool[] block;

	private Vector3[] baseHeight;

	private Vector3[] vertices;

	private Mesh mesh;

	public VertexMod()
	{
		scale = 0.3f;
		speed = 4f;
		size = 0.75f;
		block = new bool[4];
	}

	public virtual void Start()
	{
		mesh = ((MeshFilter)GetComponent(typeof(MeshFilter))).mesh;
		if (baseHeight == null)
		{
			baseHeight = mesh.vertices;
		}
		vertices = new Vector3[baseHeight.Length];
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
			if (!move_x)
			{
				vector.y += Mathf.Sin((Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z + (float)num) * size) * scale;
			}
			else
			{
				vector.x += Mathf.Sin((Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z + (float)num) * size) * scale;
			}
			vertices[i] = vector;
		}
		mesh.vertices = vertices;
	}

	public virtual void Main()
	{
	}
}
