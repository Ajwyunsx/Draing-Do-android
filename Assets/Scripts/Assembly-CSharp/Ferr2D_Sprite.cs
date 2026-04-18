using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Ferr2D_Sprite : MonoBehaviour
{
	public Color spriteColor = Color.white;

	public Rect UV = new Rect(0f, 0f, 1f, 1f);

	public Vector2 meshScale = new Vector2(1f, 1f);

	public float pixelsPerUnit = 32f;

	public bool Edged;

	public float EdgeSize = 0.1f;

	public float Width
	{
		get
		{
			Vector2 vector = new Vector2(1f, 1f);
			if (GetComponent<Renderer>().sharedMaterial.mainTexture != null)
			{
				vector = new Vector2((float)GetComponent<Renderer>().sharedMaterial.mainTexture.width / pixelsPerUnit, (float)GetComponent<Renderer>().sharedMaterial.mainTexture.height / pixelsPerUnit);
			}
			return vector.x * meshScale.x * UV.width / 2f;
		}
	}

	public float Height
	{
		get
		{
			Vector2 vector = new Vector2(1f, 1f);
			if (GetComponent<Renderer>().sharedMaterial.mainTexture != null)
			{
				vector = new Vector2((float)GetComponent<Renderer>().sharedMaterial.mainTexture.width / pixelsPerUnit, (float)GetComponent<Renderer>().sharedMaterial.mainTexture.height / pixelsPerUnit);
			}
			return vector.y * meshScale.y * (UV.height / 2f);
		}
	}

	private void Start()
	{
		Rebuild();
	}

	private string GetMeshName()
	{
		return "SpriteMesh" + base.gameObject.GetInstanceID();
	}

	public void Rebuild()
	{
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		string meshName = GetMeshName();
		if (mesh == null || mesh.name != meshName)
		{
			Mesh mesh2 = new Mesh();
			GetComponent<MeshFilter>().sharedMesh = mesh2;
			mesh = mesh2;
			mesh.name = meshName;
		}
		Vector2 vector = new Vector2(1f, 1f);
		if (GetComponent<Renderer>().sharedMaterial.mainTexture != null)
		{
			vector = new Vector2((float)GetComponent<Renderer>().sharedMaterial.mainTexture.width / pixelsPerUnit, (float)GetComponent<Renderer>().sharedMaterial.mainTexture.height / pixelsPerUnit);
		}
		mesh.Clear();
		if (!Edged)
		{
			mesh.vertices = new Vector3[4]
			{
				new Vector3((0f - UV.width) / 2f * meshScale.x * vector.x, (0f - UV.height) / 2f * meshScale.y * vector.y, 0f),
				new Vector3(UV.width / 2f * meshScale.x * vector.x, (0f - UV.height) / 2f * meshScale.y * vector.y, 0f),
				new Vector3(UV.width / 2f * meshScale.x * vector.x, UV.height / 2f * meshScale.y * vector.y, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x * vector.x, UV.height / 2f * meshScale.y * vector.y, 0f)
			};
			mesh.uv = new Vector2[4]
			{
				new Vector2(UV.x, UV.y),
				new Vector2(UV.xMax, UV.y),
				new Vector2(UV.xMax, UV.yMax),
				new Vector2(UV.x, UV.yMax)
			};
			mesh.colors = new Color[4] { spriteColor, spriteColor, spriteColor, spriteColor };
			mesh.triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
		}
		else
		{
			float num = UV.width * EdgeSize;
			float num2 = UV.height * EdgeSize;
			mesh.vertices = new Vector3[16]
			{
				new Vector3((0f - UV.width) / 2f * meshScale.x, (0f - UV.height) / 2f * meshScale.y, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x + num, (0f - UV.height) / 2f * meshScale.y, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x + num, (0f - UV.height) / 2f * meshScale.y + num2, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x, (0f - UV.height) / 2f * meshScale.y + num2, 0f),
				new Vector3(UV.width / 2f * meshScale.x - num, (0f - UV.height) / 2f * meshScale.y, 0f),
				new Vector3(UV.width / 2f * meshScale.x, (0f - UV.height) / 2f * meshScale.y, 0f),
				new Vector3(UV.width / 2f * meshScale.x, (0f - UV.height) / 2f * meshScale.y + num2, 0f),
				new Vector3(UV.width / 2f * meshScale.x - num, (0f - UV.height) / 2f * meshScale.y + num2, 0f),
				new Vector3(UV.width / 2f * meshScale.x - num, UV.height / 2f * meshScale.y - num2, 0f),
				new Vector3(UV.width / 2f * meshScale.x, UV.height / 2f * meshScale.y - num2, 0f),
				new Vector3(UV.width / 2f * meshScale.x, UV.height / 2f * meshScale.y, 0f),
				new Vector3(UV.width / 2f * meshScale.x - num, UV.height / 2f * meshScale.y, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x, UV.height / 2f * meshScale.y - num2, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x + num, UV.height / 2f * meshScale.y - num2, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x + num, UV.height / 2f * meshScale.y, 0f),
				new Vector3((0f - UV.width) / 2f * meshScale.x, UV.height / 2f * meshScale.y, 0f)
			};
			mesh.uv = new Vector2[16]
			{
				new Vector2(UV.x, UV.y),
				new Vector2(UV.x + num, UV.y),
				new Vector2(UV.x + num, UV.y + num2),
				new Vector2(UV.x, UV.y + num2),
				new Vector2(UV.xMax - num, UV.y),
				new Vector2(UV.xMax, UV.y),
				new Vector2(UV.xMax, UV.y + num2),
				new Vector2(UV.xMax - num, UV.y + num2),
				new Vector2(UV.xMax - num, UV.yMax - num2),
				new Vector2(UV.xMax, UV.yMax - num2),
				new Vector2(UV.xMax, UV.yMax),
				new Vector2(UV.xMax - num, UV.yMax),
				new Vector2(UV.x, UV.yMax - num2),
				new Vector2(UV.x + num, UV.yMax - num2),
				new Vector2(UV.x + num, UV.yMax),
				new Vector2(UV.x, UV.yMax)
			};
			mesh.colors = new Color[16]
			{
				spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor,
				spriteColor, spriteColor, spriteColor, spriteColor, spriteColor, spriteColor
			};
			mesh.triangles = new int[54]
			{
				2, 1, 0, 3, 2, 0, 7, 4, 1, 2,
				7, 1, 6, 5, 4, 7, 6, 4, 9, 6,
				7, 8, 9, 7, 10, 9, 8, 11, 10, 8,
				14, 11, 13, 11, 8, 13, 15, 14, 12, 14,
				13, 12, 12, 13, 3, 13, 2, 3, 13, 8,
				2, 8, 7, 2
			};
		}
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
}
