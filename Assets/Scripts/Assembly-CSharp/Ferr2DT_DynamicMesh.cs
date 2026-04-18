using System.Collections.Generic;
using UnityEngine;

public class Ferr2DT_DynamicMesh
{
	private List<Vector3> mVerts;

	private List<int> mIndices;

	private List<Vector2> mUVs;

	private List<Color> mColors;

	public Color color = Color.white;

	public int VertCount
	{
		get
		{
			return mVerts.Count;
		}
	}

	public Ferr2DT_DynamicMesh()
	{
		mVerts = new List<Vector3>();
		mUVs = new List<Vector2>();
		mIndices = new List<int>();
		mColors = new List<Color>();
	}

	public void Clear()
	{
		mVerts.Clear();
		mIndices.Clear();
		mUVs.Clear();
		mColors.Clear();
		color = Color.white;
	}

	public void Build(ref Mesh aMesh, bool aCalculateTangents)
	{
		aMesh.Clear();
		aMesh.vertices = mVerts.ToArray();
		aMesh.uv = mUVs.ToArray();
		aMesh.triangles = mIndices.ToArray();
		aMesh.colors = mColors.ToArray();
		aMesh.RecalculateNormals();
		aMesh.RecalculateBounds();
		if (aCalculateTangents)
		{
			RecalculateTangents(aMesh);
		}
		else
		{
			aMesh.tangents = null;
		}
	}

	public void ExtrudeZ(float aDist, bool aInverted)
	{
		int count = mVerts.Count;
		int count2 = mIndices.Count;
		mUVs.AddRange(mUVs.ToArray());
		mColors.AddRange(mColors.ToArray());
		Vector3 vector = new Vector3(0f, 0f, aDist);
		for (int i = 0; i < count; i++)
		{
			mVerts[i] -= vector / 2f;
		}
		for (int j = 0; j < count; j++)
		{
			mVerts.Add(mVerts[j] + vector);
		}
		for (int k = 0; k < count2; k += 3)
		{
			mIndices.Add(mIndices[k + 2] + count);
			mIndices.Add(mIndices[k + 1] + count);
			mIndices.Add(mIndices[k] + count);
		}
		int num = count - 1;
		for (int l = 0; l < num; l++)
		{
			if (aInverted)
			{
				AddFace(l, l + count, l + count + 1, l + 1);
			}
			else
			{
				AddFace(l + 1, l + count + 1, l + count, l);
			}
		}
		if (aInverted)
		{
			AddFace(count - 1, count - 1 + count, count, 0);
		}
		else
		{
			AddFace(0, count, count - 1 + count, count - 1);
		}
	}

	public void RemoveFaces(Vector3 aFacing, float aDegreesTolerance)
	{
		for (int i = 0; i < mIndices.Count; i += 3)
		{
			Vector3 vector = Vector3.Cross(mVerts[mIndices[i + 1]] - mVerts[mIndices[i]], mVerts[mIndices[i + 2]] - mVerts[mIndices[i]]);
			vector.Normalize();
			if (Vector3.Angle(vector, aFacing) < aDegreesTolerance)
			{
				mIndices.RemoveRange(i, 3);
				i -= 3;
			}
		}
	}

	public int[] GetCurrentTriangleList(int aStart = 0)
	{
		int[] array = new int[mIndices.Count - aStart];
		int num = 0;
		for (int i = aStart; i < mIndices.Count; i++)
		{
			array[num] = mIndices[i];
			num++;
		}
		return array;
	}

	private void RecalculateTangents(Mesh aMesh)
	{
		Vector3[] array = new Vector3[aMesh.vertices.Length];
		Vector3[] array2 = new Vector3[aMesh.vertices.Length];
		Vector4[] array3 = new Vector4[aMesh.vertices.Length];
		for (int i = 0; i < aMesh.triangles.Length; i += 3)
		{
			long num = aMesh.triangles[i];
			long num2 = aMesh.triangles[i + 1];
			long num3 = aMesh.triangles[i + 2];
			Vector3 vector = aMesh.vertices[num];
			Vector3 vector2 = aMesh.vertices[num2];
			Vector3 vector3 = aMesh.vertices[num3];
			Vector2 vector4 = aMesh.uv[num];
			Vector2 vector5 = aMesh.uv[num2];
			Vector2 vector6 = aMesh.uv[num3];
			float num4 = vector2.x - vector.x;
			float num5 = vector3.x - vector.x;
			float num6 = vector2.y - vector.y;
			float num7 = vector3.y - vector.y;
			float num8 = vector2.z - vector.z;
			float num9 = vector3.z - vector.z;
			float num10 = vector5.x - vector4.x;
			float num11 = vector6.x - vector4.x;
			float num12 = vector5.y - vector4.y;
			float num13 = vector6.y - vector4.y;
			float num14 = 1f / (num10 * num13 - num11 * num12);
			Vector3 vector7 = new Vector3((num13 * num4 - num12 * num5) * num14, (num13 * num6 - num12 * num7) * num14, (num13 * num8 - num12 * num9) * num14);
			Vector3 vector8 = new Vector3((num10 * num5 - num11 * num4) * num14, (num10 * num7 - num11 * num6) * num14, (num10 * num9 - num11 * num8) * num14);
			array2[num] += vector7;
			array2[num2] += vector7;
			array2[num3] += vector7;
			array[num] += vector8;
			array[num2] += vector8;
			array[num3] += vector8;
		}
		for (int j = 0; j < aMesh.vertices.Length; j++)
		{
			Vector3 normal = aMesh.normals[j];
			Vector3 tangent = array2[j];
			Vector3.OrthoNormalize(ref normal, ref tangent);
			array3[j].x = tangent.x;
			array3[j].y = tangent.y;
			array3[j].z = tangent.z;
			array3[j].w = ((!(Vector3.Dot(Vector3.Cross(normal, tangent), array[j]) < 0f)) ? 1f : (-1f));
		}
		aMesh.tangents = array3;
	}

	public int AddVertex(float aX, float aY, float aZ)
	{
		mVerts.Add(new Vector3(aX, aY, aZ));
		mUVs.Add(new Vector2(0f, 0f));
		mColors.Add(color);
		return mVerts.Count - 1;
	}

	public int AddVertex(float aX, float aY, float aZ, float aU, float aV)
	{
		mVerts.Add(new Vector3(aX, aY, aZ));
		mUVs.Add(new Vector2(aU, aV));
		mColors.Add(color);
		return mVerts.Count - 1;
	}

	public int AddVertex(Vector3 aVert)
	{
		mVerts.Add(aVert);
		mUVs.Add(new Vector2(0f, 0f));
		mColors.Add(color);
		return mVerts.Count - 1;
	}

	public int AddVertex(Vector3 aVert, Vector2 aUV)
	{
		mVerts.Add(aVert);
		mUVs.Add(aUV);
		mColors.Add(color);
		return mVerts.Count - 1;
	}

	public int AddVertex(Vector2 aVert, float aZ, Vector2 aUV)
	{
		mVerts.Add(new Vector3(aVert.x, aVert.y, aZ));
		mUVs.Add(aUV);
		mColors.Add(color);
		return mVerts.Count - 1;
	}

	public void AddFace(int aV1, int aV2, int aV3)
	{
		mIndices.Add(aV1);
		mIndices.Add(aV2);
		mIndices.Add(aV3);
	}

	public void AddFace(int aV1, int aV2, int aV3, int aV4)
	{
		mIndices.Add(aV3);
		mIndices.Add(aV2);
		mIndices.Add(aV1);
		mIndices.Add(aV4);
		mIndices.Add(aV3);
		mIndices.Add(aV1);
	}
}
