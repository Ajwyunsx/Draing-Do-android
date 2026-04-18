using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ferr2D/Path")]
public class Ferr2D_Path : MonoBehaviour
{
	public bool closed;

	public List<Vector2> pathVerts = new List<Vector2>();

	public int Count
	{
		get
		{
			return pathVerts.Count;
		}
	}

	public void FromJSON(string aJSON)
	{
		FromJSON(Ferr_JSON.Parse(aJSON));
	}

	public void FromJSON(Ferr_JSONValue aJSON)
	{
		closed = aJSON["closed", false];
		pathVerts = new List<Vector2>();
		object[] array = aJSON["verts", new object[0]];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is Ferr_JSONValue)
			{
				Ferr_JSONValue ferr_JSONValue = array[i] as Ferr_JSONValue;
				pathVerts.Add(new Vector2(ferr_JSONValue[0, 0f], ferr_JSONValue[1, 0f]));
			}
		}
	}

	public Ferr_JSONValue ToJSON()
	{
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		ferr_JSONValue["closed"] = closed;
		object[] array = new object[pathVerts.Count];
		for (int i = 0; i < pathVerts.Count; i++)
		{
			Ferr_JSONValue ferr_JSONValue2 = new Ferr_JSONValue();
			ferr_JSONValue2[0] = pathVerts[i].x;
			ferr_JSONValue2[1] = pathVerts[i].y;
			array[i] = ferr_JSONValue2;
		}
		ferr_JSONValue["verts"] = array;
		return ferr_JSONValue;
	}

	public void ReCenter()
	{
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < pathVerts.Count; i++)
		{
			zero += pathVerts[i];
		}
		zero = zero / pathVerts.Count + new Vector2(base.transform.position.x, base.transform.position.y);
		Vector2 vector = zero - new Vector2(base.gameObject.transform.position.x, base.gameObject.transform.position.y);
		for (int j = 0; j < pathVerts.Count; j++)
		{
			pathVerts[j] -= vector;
		}
		base.gameObject.transform.position = new Vector3(zero.x, zero.y, base.gameObject.transform.position.z);
		UpdateDependants();
	}

	public void UpdateDependants()
	{
		Component[] components = base.gameObject.GetComponents(typeof(Ferr2D_IPath));
		for (int i = 0; i < components.Length; i++)
		{
			(components[i] as Ferr2D_IPath).RecreatePath();
		}
	}

	public void Add(Vector2 aPoint)
	{
		pathVerts.Add(aPoint);
	}

	public int GetClosestSeg(Vector2 aPoint)
	{
		if (pathVerts.Count <= 0)
		{
			return -1;
		}
		float num = float.MaxValue;
		int result = -1;
		int num2 = ((!closed) ? (pathVerts.Count - 1) : pathVerts.Count);
		for (int i = 0; i < num2; i++)
		{
			int index = ((i != pathVerts.Count - 1) ? (i + 1) : 0);
			Vector2 closetPointOnLine = GetClosetPointOnLine(pathVerts[i], pathVerts[index], aPoint, true);
			float num3 = (aPoint - closetPointOnLine).SqrMagnitude();
			if (num3 < num)
			{
				num = num3;
				result = i;
			}
		}
		if (!closed)
		{
			float num4 = (aPoint - pathVerts[pathVerts.Count - 1]).SqrMagnitude();
			if (num4 <= num)
			{
				result = pathVerts.Count - 1;
			}
			num4 = (aPoint - pathVerts[0]).SqrMagnitude();
			if (num4 <= num)
			{
				result = pathVerts.Count - 1;
			}
		}
		return result;
	}

	public List<Vector2> GetVerts(bool aSmoothed, float aSplitDistance, bool aSplitCorners)
	{
		if (aSmoothed)
		{
			return GetVertsSmoothed(aSplitDistance, aSplitCorners, false);
		}
		return GetVertsRaw();
	}

	public List<Vector2> GetVertsRaw()
	{
		return new List<Vector2>(pathVerts);
	}

	public List<Vector2> GetVertsSmoothed(float aSplitDistance, bool aSplitCorners, bool aInverted)
	{
		List<Vector2> list = new List<Vector2>();
		if (aSplitCorners)
		{
			List<Ferr2DT_TerrainDirection> aSegDirections;
			List<List<Vector2>> aSegmentList = GetSegments(pathVerts, out aSegDirections);
			if (closed)
			{
				CloseEnds(ref aSegmentList, ref aSegDirections, aSplitCorners, aInverted);
			}
			if (aSegmentList.Count > 1)
			{
				for (int i = 0; i < aSegmentList.Count; i++)
				{
					aSegmentList[i] = SmoothSegment(aSegmentList[i], aSplitDistance, false);
					if (i != 0 && aSegmentList[i].Count > 0)
					{
						aSegmentList[i].RemoveAt(0);
					}
					list.AddRange(aSegmentList[i]);
				}
			}
			else
			{
				list = SmoothSegment(pathVerts, aSplitDistance, closed);
				if (closed)
				{
					list.Add(pathVerts[0]);
				}
			}
		}
		else
		{
			list = SmoothSegment(pathVerts, aSplitDistance, closed);
			if (closed)
			{
				list.Add(pathVerts[0]);
			}
		}
		return list;
	}

	public static Vector2 GetNormal(List<Vector2> aSegment, int i, bool aClosed)
	{
		if (aSegment.Count < 2)
		{
			return Vector2.up;
		}
		Vector2 vector = ((!aClosed || i != aSegment.Count - 1) ? aSegment[i] : aSegment[0]);
		Vector2 zero = Vector2.zero;
		zero = ((i - 1 >= 0) ? aSegment[i - 1] : ((!aClosed) ? (vector - (aSegment[i + 1] - vector)) : aSegment[aSegment.Count - 2]));
		Vector2 zero2 = Vector2.zero;
		zero2 = ((i + 1 <= aSegment.Count - 1) ? aSegment[i + 1] : ((!aClosed) ? (vector - (aSegment[i - 1] - vector)) : aSegment[1]));
		zero -= vector;
		zero2 -= vector;
		zero.Normalize();
		zero2.Normalize();
		zero = new Vector2(0f - zero.y, zero.x);
		zero2 = new Vector2(zero2.y, 0f - zero2.x);
		Vector2 result = (zero + zero2) / 2f;
		result.Normalize();
		return result;
	}

	public static Vector2 CubicGetNormal(List<Vector2> aSegment, int i, float aPercentage, bool aClosed)
	{
		Vector2 vector = CubicGetPt(aSegment, i, aPercentage, aClosed);
		Vector2 vector2 = CubicGetPt(aSegment, i, aPercentage + 0.01f, aClosed);
		Vector2 vector3 = vector2 - vector;
		vector3.Normalize();
		return new Vector2(vector3.y, 0f - vector3.x);
	}

	public static Vector2 CubicGetPt(List<Vector2> aSegment, int i, float aPercentage, bool aClosed)
	{
		int index = ((!aClosed) ? Mathf.Clamp(i - 1, 0, aSegment.Count - 1) : ((i - 1 >= 0) ? (i - 1) : (aSegment.Count - 1)));
		int index2 = ((!aClosed) ? Mathf.Clamp(i + 1, 0, aSegment.Count - 1) : ((i + 1) % (aSegment.Count - 1)));
		int index3 = ((!aClosed) ? Mathf.Clamp(i + 2, 0, aSegment.Count - 1) : ((i + 2) % (aSegment.Count - 1)));
		return new Vector2(Cubic(aSegment[index].x, aSegment[i].x, aSegment[index2].x, aSegment[index3].x, aPercentage), Cubic(aSegment[index].y, aSegment[i].y, aSegment[index2].y, aSegment[index3].y, aPercentage));
	}

	public static Vector2 HermiteGetNormal(List<Vector2> aSegment, int i, float aPercentage, bool aClosed, float aTension = 0f, float aBias = 0f)
	{
		Vector2 vector = HermiteGetPt(aSegment, i, aPercentage, aClosed, aTension, aBias);
		Vector2 vector2 = HermiteGetPt(aSegment, i, aPercentage + 0.01f, aClosed, aTension, aBias);
		Vector2 vector3 = vector2 - vector;
		vector3.Normalize();
		return new Vector2(vector3.y, 0f - vector3.x);
	}

	public static Vector2 HermiteGetPt(List<Vector2> aSegment, int i, float aPercentage, bool aClosed, float aTension = 0f, float aBias = 0f)
	{
		int index = ((!aClosed) ? Mathf.Clamp(i - 1, 0, aSegment.Count - 1) : ((i - 1 >= 0) ? (i - 1) : (aSegment.Count - 2)));
		int index2 = ((!aClosed) ? Mathf.Clamp(i + 1, 0, aSegment.Count - 1) : ((i + 1) % aSegment.Count));
		int index3 = ((!aClosed) ? Mathf.Clamp(i + 2, 0, aSegment.Count - 1) : ((i + 2) % aSegment.Count));
		return new Vector2(Hermite(aSegment[index].x, aSegment[i].x, aSegment[index2].x, aSegment[index3].x, aPercentage, aTension, aBias), Hermite(aSegment[index].y, aSegment[i].y, aSegment[index2].y, aSegment[index3].y, aPercentage, aTension, aBias));
	}

	private static float Cubic(float v1, float v2, float v3, float v4, float aPercentage)
	{
		float num = aPercentage * aPercentage;
		float num2 = v4 - v3 - v1 + v2;
		float num3 = v1 - v2 - num2;
		float num4 = v3 - v1;
		return num2 * aPercentage * num + num3 * num + num4 * aPercentage + v2;
	}

	private static float Linear(float v1, float v2, float aPercentage)
	{
		return v1 + (v2 - v1) * aPercentage;
	}

	private static float Hermite(float v1, float v2, float v3, float v4, float aPercentage, float aTension, float aBias)
	{
		float num = aPercentage * aPercentage;
		float num2 = num * aPercentage;
		float num3 = (v2 - v1) * (1f + aBias) * (1f - aTension) / 2f;
		num3 += (v3 - v2) * (1f - aBias) * (1f - aTension) / 2f;
		float num4 = (v3 - v2) * (1f + aBias) * (1f - aTension) / 2f;
		num4 += (v4 - v3) * (1f - aBias) * (1f - aTension) / 2f;
		float num5 = 2f * num2 - 3f * num + 1f;
		float num6 = num2 - 2f * num + aPercentage;
		float num7 = num2 - num;
		float num8 = -2f * num2 + 3f * num;
		return num5 * v2 + num6 * num3 + num7 * num4 + num8 * v3;
	}

	public static Ferr2DT_TerrainDirection GetDirection(Vector2 aOne, Vector2 aTwo)
	{
		Vector2 vector = aOne - aTwo;
		vector = new Vector2(0f - vector.y, vector.x);
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			if (vector.x < 0f)
			{
				return Ferr2DT_TerrainDirection.Left;
			}
			return Ferr2DT_TerrainDirection.Right;
		}
		if (vector.y < 0f)
		{
			return Ferr2DT_TerrainDirection.Bottom;
		}
		return Ferr2DT_TerrainDirection.Top;
	}

	public static Ferr2DT_TerrainDirection GetDirection(List<Vector2> aSegment, int i, bool aInvert, bool aClosed = false, List<Ferr2DT_TerrainDirection> aOverrides = null)
	{
		if (aSegment.Count <= 0)
		{
			return Ferr2DT_TerrainDirection.Top;
		}
		int num = i + 1;
		if (i < 0)
		{
			if (aClosed)
			{
				i = aSegment.Count - 2;
				num = 0;
			}
			else
			{
				i = 0;
				num = 1;
			}
		}
		if (aOverrides != null && aOverrides.Count >= aSegment.Count && aOverrides[i] != Ferr2DT_TerrainDirection.None)
		{
			return aOverrides[i];
		}
		Vector2 vector = aSegment[(num <= aSegment.Count - 1) ? num : ((!aClosed) ? (i - 1) : (aSegment.Count - 1))] - aSegment[i];
		vector = new Vector2(0f - vector.y, vector.x);
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
		{
			if (vector.x < 0f)
			{
				return (!aInvert) ? Ferr2DT_TerrainDirection.Left : Ferr2DT_TerrainDirection.Right;
			}
			return aInvert ? Ferr2DT_TerrainDirection.Left : Ferr2DT_TerrainDirection.Right;
		}
		if (vector.y < 0f)
		{
			return (!aInvert) ? Ferr2DT_TerrainDirection.Bottom : Ferr2DT_TerrainDirection.Top;
		}
		return aInvert ? Ferr2DT_TerrainDirection.Bottom : Ferr2DT_TerrainDirection.Top;
	}

	public static bool IsSplit(List<Vector2> aSegment, int i, List<Ferr2DT_TerrainDirection> aOverrides = null)
	{
		if (i == 0 || i == aSegment.Count - 1)
		{
			return false;
		}
		if (aOverrides != null && aOverrides.Count < aSegment.Count)
		{
			aOverrides = null;
		}
		Ferr2DT_TerrainDirection ferr2DT_TerrainDirection = ((aOverrides == null) ? GetDirection(aSegment[i], aSegment[i - 1]) : ((aOverrides[i - 1] != Ferr2DT_TerrainDirection.None) ? aOverrides[i - 1] : GetDirection(aSegment[i], aSegment[i - 1])));
		Ferr2DT_TerrainDirection ferr2DT_TerrainDirection2 = ((aOverrides == null) ? GetDirection(aSegment[i + 1], aSegment[i]) : ((aOverrides[i] != Ferr2DT_TerrainDirection.None) ? aOverrides[i] : GetDirection(aSegment[i + 1], aSegment[i])));
		return ferr2DT_TerrainDirection != ferr2DT_TerrainDirection2;
	}

	public static List<List<Vector2>> GetSegments(List<Vector2> aPath, out List<Ferr2DT_TerrainDirection> aSegDirections, List<Ferr2DT_TerrainDirection> aOverrides = null, bool aInvert = false, bool aClosed = false)
	{
		List<List<Vector2>> list = new List<List<Vector2>>();
		List<Vector2> list2 = new List<Vector2>();
		aSegDirections = new List<Ferr2DT_TerrainDirection>();
		int i = 0;
		for (int j = 0; j < aPath.Count; j++)
		{
			list2.Add(aPath[j]);
			if (IsSplit(aPath, j, aOverrides))
			{
				list.Add(list2);
				aSegDirections.Add(GetDirection(aPath, i, aInvert, aClosed, aOverrides));
				list2 = new List<Vector2>();
				list2.Add(aPath[j]);
				i = j;
			}
		}
		list.Add(list2);
		aSegDirections.Add(GetDirection(aPath, i, aInvert, aClosed, aOverrides));
		return list;
	}

	public static List<Vector2> SmoothSegment(List<Vector2> aSegment, float aSplitDistance, bool aClosed)
	{
		List<Vector2> list = new List<Vector2>(aSegment);
		int num = 0;
		int num2 = ((!aClosed) ? (aSegment.Count - 1) : aSegment.Count);
		for (int i = 0; i < num2; i++)
		{
			int index = ((i != num2 - 1) ? (i + 1) : ((!aClosed) ? (aSegment.Count - 1) : 0));
			int num3 = (int)(Vector2.Distance(aSegment[i], aSegment[index]) / aSplitDistance);
			for (int j = 0; j < num3; j++)
			{
				float aPercentage = (float)(j + 1) / (float)(num3 + 1);
				list.Insert(num + 1, HermiteGetPt(aSegment, i, aPercentage, aClosed));
				num++;
			}
			num++;
		}
		return list;
	}

	public static bool CloseEnds(ref List<List<Vector2>> aSegmentList, ref List<Ferr2DT_TerrainDirection> aSegmentDirections, bool aCorners, bool aInverted)
	{
		Vector2 vector = aSegmentList[0][0];
		Vector2 aTwo = aSegmentList[0][1];
		Vector2 vector2 = aSegmentList[aSegmentList.Count - 1][aSegmentList[aSegmentList.Count - 1].Count - 1];
		Vector2 aOne = aSegmentList[aSegmentList.Count - 1][aSegmentList[aSegmentList.Count - 1].Count - 2];
		if (!aCorners)
		{
			aSegmentList[0].Add(vector);
			return true;
		}
		bool flag = GetDirection(aOne, vector2) != GetDirection(vector2, vector);
		bool flag2 = GetDirection(vector2, vector) != GetDirection(vector, aTwo);
		if (flag && flag2)
		{
			List<Vector2> list = new List<Vector2>();
			list.Add(vector2);
			list.Add(vector);
			aSegmentList.Add(list);
			Ferr2DT_TerrainDirection ferr2DT_TerrainDirection = GetDirection(vector, vector2);
			if (aInverted && ferr2DT_TerrainDirection == Ferr2DT_TerrainDirection.Top)
			{
				ferr2DT_TerrainDirection = Ferr2DT_TerrainDirection.Bottom;
			}
			if (aInverted && ferr2DT_TerrainDirection == Ferr2DT_TerrainDirection.Bottom)
			{
				ferr2DT_TerrainDirection = Ferr2DT_TerrainDirection.Top;
			}
			if (aInverted && ferr2DT_TerrainDirection == Ferr2DT_TerrainDirection.Right)
			{
				ferr2DT_TerrainDirection = Ferr2DT_TerrainDirection.Left;
			}
			if (aInverted && ferr2DT_TerrainDirection == Ferr2DT_TerrainDirection.Left)
			{
				ferr2DT_TerrainDirection = Ferr2DT_TerrainDirection.Right;
			}
			aSegmentDirections.Add(ferr2DT_TerrainDirection);
		}
		else if (flag && !flag2)
		{
			aSegmentList[0].Insert(0, vector2);
		}
		else if (!flag && flag2)
		{
			aSegmentList[aSegmentList.Count - 1].Add(vector);
		}
		else
		{
			aSegmentList[0].InsertRange(0, aSegmentList[aSegmentList.Count - 1]);
			aSegmentList.RemoveAt(aSegmentList.Count - 1);
			aSegmentDirections.RemoveAt(aSegmentDirections.Count - 1);
		}
		return true;
	}

	public static Vector2 GetClosetPointOnLine(Vector2 aStart, Vector2 aEnd, Vector2 aPoint, bool aClamp)
	{
		Vector2 vector = aPoint - aStart;
		Vector2 vector2 = aEnd - aStart;
		float num = vector2.x * vector2.x + vector2.y * vector2.y;
		float num2 = vector.x * vector2.x + vector.y * vector2.y;
		float num3 = num2 / num;
		if (aClamp)
		{
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			else if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		return aStart + vector2 * num3;
	}

	public static Rect GetBounds(List<Vector2> aFrom)
	{
		float num = float.MaxValue;
		float num2 = float.MinValue;
		float num3 = float.MinValue;
		float num4 = float.MaxValue;
		for (int i = 0; i < aFrom.Count; i++)
		{
			if (aFrom[i].x > num2)
			{
				num2 = aFrom[i].x;
			}
			if (aFrom[i].x < num)
			{
				num = aFrom[i].x;
			}
			if (aFrom[i].y < num4)
			{
				num4 = aFrom[i].y;
			}
			if (aFrom[i].y > num3)
			{
				num3 = aFrom[i].y;
			}
		}
		return new Rect(num, num3, num2 - num, num4 - num3);
	}
}
