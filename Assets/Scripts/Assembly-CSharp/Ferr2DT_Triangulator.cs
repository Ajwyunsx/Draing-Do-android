using System.Collections.Generic;
using LibTessDotNet;
using UnityEngine;

public static class Ferr2DT_Triangulator
{
	public static List<int> GetIndices(ref List<Vector2> aPoints, bool aTreatAsPath, bool aInvert)
	{
		Vector4 bounds = GetBounds(aPoints);
		Tess tess = new Tess();
		ContourVertex[] array = new ContourVertex[aPoints.Count];
		for (int i = 0; i < aPoints.Count; i++)
		{
			array[i] = default(ContourVertex);
			array[i].Position = default(Vec3);
			array[i].Position.X = aPoints[i].x;
			array[i].Position.Y = aPoints[i].y;
			array[i].Position.Z = 0f;
		}
		tess.AddContour(array, ContourOrientation.Original);
		if (aInvert)
		{
			aPoints.Add(new Vector2(bounds.x - (bounds.z - bounds.x) * 1f, bounds.w - (bounds.y - bounds.w) * 1f));
			aPoints.Add(new Vector2(bounds.z + (bounds.z - bounds.x) * 1f, bounds.w - (bounds.y - bounds.w) * 1f));
			aPoints.Add(new Vector2(bounds.z + (bounds.z - bounds.x) * 1f, bounds.y + (bounds.y - bounds.w) * 1f));
			aPoints.Add(new Vector2(bounds.x - (bounds.z - bounds.x) * 1f, bounds.y + (bounds.y - bounds.w) * 1f));
			ContourVertex[] array2 = new ContourVertex[4];
			for (int j = 0; j < 4; j++)
			{
				array2[j] = default(ContourVertex);
				array2[j].Position = default(Vec3);
				array2[j].Position.X = aPoints[aPoints.Count - 4 + j].x;
				array2[j].Position.Y = aPoints[aPoints.Count - 4 + j].y;
				array2[j].Position.Z = 0f;
			}
			tess.AddContour(array2, (!aInvert) ? ContourOrientation.Clockwise : ContourOrientation.CounterClockwise);
		}
		tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);
		List<int> list = new List<int>();
		for (int k = 0; k < tess.Elements.Length; k += 3)
		{
			for (int l = 0; l < 3; l++)
			{
				if (tess.Elements[k + l] == -1)
				{
					continue;
				}
				for (int m = 0; m < aPoints.Count; m++)
				{
					if (aPoints[m].x == tess.Vertices[tess.Elements[k + l]].Position.X && aPoints[m].y == tess.Vertices[tess.Elements[k + l]].Position.Y)
					{
						list.Add(m);
						break;
					}
				}
			}
		}
		return list;
	}

	public static List<int> GetSegmentsUnder(List<Vector2> aPath, float aX, float aY, bool aIgnoreLast)
	{
		List<int> list = new List<int>();
		int num = (aIgnoreLast ? 4 : 0);
		for (int i = 0; i < aPath.Count - num; i++)
		{
			int num2 = ((i + 1 < aPath.Count - num) ? (i + 1) : 0);
			int num3 = ((!(aPath[i].x < aPath[num2].x)) ? num2 : i);
			int num4 = ((!(aPath[i].x > aPath[num2].x)) ? num2 : i);
			if (aPath[num3].x <= aX && aPath[num4].x > aX)
			{
				float num5 = Mathf.Lerp(aPath[num3].y, aPath[num4].y, (aX - aPath[num3].x) / (aPath[num4].x - aPath[num3].x));
				if (aY > num5)
				{
					list.Add(num3);
					list.Add(num4);
				}
			}
		}
		return list;
	}

	public static Vector4 GetBounds(List<Vector2> aPoints)
	{
		if (aPoints.Count <= 0)
		{
			return new Vector4(0f, 0f, 1f, 1f);
		}
		float x = aPoints[0].x;
		float x2 = aPoints[0].x;
		float y = aPoints[0].y;
		float y2 = aPoints[0].y;
		for (int i = 0; i < aPoints.Count; i++)
		{
			if (aPoints[i].x < x)
			{
				x = aPoints[i].x;
			}
			if (aPoints[i].x > x2)
			{
				x2 = aPoints[i].x;
			}
			if (aPoints[i].y > y)
			{
				y = aPoints[i].y;
			}
			if (aPoints[i].y < y2)
			{
				y2 = aPoints[i].y;
			}
		}
		return new Vector4(x, y, x2, y2);
	}

	public static bool PtInTri(Vector2 aTri1, Vector2 aTri2, Vector2 aTri3, Vector2 aPt)
	{
		float num = aPt.x - aTri1.x;
		float num2 = aPt.y - aTri1.y;
		bool flag = (aTri2.x - aTri1.x) * num2 - (aTri2.y - aTri1.y) * num > 0f;
		if ((aTri3.x - aTri1.x) * num2 - (aTri3.y - aTri1.y) * num > 0f == flag)
		{
			return false;
		}
		if ((aTri3.x - aTri2.x) * (aPt.y - aTri2.y) - (aTri3.y - aTri2.y) * (aPt.x - aTri2.x) > 0f != flag)
		{
			return false;
		}
		return true;
	}

	public static Vector2 LineIntersectionPoint(Vector2 aStart1, Vector2 aEnd1, Vector2 aStart2, Vector2 aEnd2)
	{
		float num = aEnd1.y - aStart1.y;
		float num2 = aStart1.x - aEnd1.x;
		float num3 = num * aStart1.x + num2 * aStart1.y;
		float num4 = aEnd2.y - aStart2.y;
		float num5 = aStart2.x - aEnd2.x;
		float num6 = num4 * aStart2.x + num5 * aStart2.y;
		float num7 = num * num5 - num4 * num2;
		return new Vector2((num5 * num3 - num2 * num6) / num7, (num * num6 - num4 * num3) / num7);
	}

	public static bool IsClockwise(Vector2 aPt1, Vector2 aPt2, Vector2 aPt3)
	{
		return (aPt2.x - aPt1.x) * (aPt3.y - aPt1.y) - (aPt3.x - aPt1.x) * (aPt2.y - aPt1.y) > 0f;
	}

	private static Vector2 GetCircumcenter(List<Vector2> aPoints, List<int> aTris, int aTri)
	{
		Vector2 vector = (aPoints[aTris[aTri]] + aPoints[aTris[aTri + 1]]) / 2f;
		Vector2 vector2 = (aPoints[aTris[aTri + 1]] + aPoints[aTris[aTri + 2]]) / 2f;
		Vector2 vector3 = aPoints[aTris[aTri]] - aPoints[aTris[aTri + 1]];
		vector3 = new Vector2(vector3.y, 0f - vector3.x);
		Vector2 vector4 = aPoints[aTris[aTri + 1]] - aPoints[aTris[aTri + 2]];
		return LineIntersectionPoint(aEnd2: vector2 + new Vector2(vector4.y, 0f - vector4.x), aStart1: vector, aEnd1: vector + vector3, aStart2: vector2);
	}

	private static bool EdgeFlip(List<Vector2> aPoints, List<int> aTris, int aTri)
	{
		List<int> list = new List<int>(3);
		List<int> list2 = new List<int>(3);
		List<int> list3 = new List<int>(3);
		List<int> list4 = new List<int>(3);
		list.Clear();
		list.Add(aTris[aTri]);
		list.Add(aTris[aTri + 1]);
		list.Add(aTris[aTri + 2]);
		Vector2 circumcenter = GetCircumcenter(aPoints, aTris, aTri);
		float num = Vector2.SqrMagnitude(aPoints[list[0]] - circumcenter);
		for (int i = 0; i < aTris.Count; i += 3)
		{
			if (i == aTri)
			{
				continue;
			}
			list3.Clear();
			list4.Clear();
			list2.Clear();
			list2.Add(aTris[i]);
			list2.Add(aTris[i + 1]);
			list2.Add(aTris[i + 2]);
			for (int j = 0; j < 3; j++)
			{
				int num2 = 0;
				for (int k = 0; k < 3; k++)
				{
					if (list[j] == list2[k])
					{
						list3.Add(list[j]);
						num2++;
					}
				}
				if (num2 == 0)
				{
					list4.Add(list[j]);
				}
			}
			if (list4.Count == 1 && list3.Count == 2)
			{
				for (int l = 0; l < 3; l++)
				{
					if (list2[l] != list3[0] && list2[l] != list3[1] && list2[l] != list4[0])
					{
						list4.Add(list2[l]);
						break;
					}
				}
			}
			if (list4.Count == 2 && list3.Count == 2 && Vector2.SqrMagnitude(aPoints[list4[1]] - circumcenter) < num)
			{
				aTris[aTri] = list4[0];
				aTris[aTri + 1] = list3[0];
				aTris[aTri + 2] = list4[1];
				aTris[i] = list4[1];
				aTris[i + 1] = list3[1];
				aTris[i + 2] = list4[0];
				return true;
			}
		}
		return false;
	}

	private static int GetSurroundingTri(List<Vector2> aPoints, List<int> aTris, Vector2 aPt)
	{
		for (int i = 0; i < aTris.Count; i += 3)
		{
			if (PtInTri(aPoints[aTris[i]], aPoints[aTris[i + 1]], aPoints[aTris[i + 2]], aPt))
			{
				return i;
			}
		}
		return -1;
	}
}
