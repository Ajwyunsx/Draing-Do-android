using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ferr2DT/Path Terrain")]
[RequireComponent(typeof(Ferr2D_Path))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Ferr2DT_PathTerrain : MonoBehaviour, Ferr2D_IPath
{
	public Ferr2DT_FillMode fill;

	public float fillY;

	public float fillZ = 0.2f;

	public bool splitCorners = true;

	public bool smoothPath;

	public int splitCount = 4;

	public float splitDist = 1f;

	public bool splitMiddle = true;

	public float pixelsPerUnit = 32f;

	public Color vertexColor = Color.white;

	public float stretchThreshold = 0.5f;

	public bool createTangents;

	public bool randomByWorldCoordinates;

	public bool createCollider = true;

	public bool create3DCollider;

	public float depth = 4f;

	public bool smoothSphereCollisions;

	public float[] surfaceOffset = new float[4];

	public PhysicMaterial physicsMaterial;

	public PhysicsMaterial2D physicsMaterial2D;

	public bool collidersLeft = true;

	public bool collidersRight = true;

	public bool collidersTop = true;

	public bool collidersBottom = true;

	public float colliderThickness = 0.1f;

	[SerializeField]
	public List<Ferr2DT_TerrainDirection> directionOverrides;

	[SerializeField]
	private Ferr2DT_TerrainMaterial terrainMaterial;

	private Ferr2D_Path path;

	private Ferr2DT_DynamicMesh dMesh;

	private Vector2 unitsPerUV = Vector2.one;

	public Ferr2DT_TerrainMaterial TerrainMaterial
	{
		get
		{
			return terrainMaterial;
		}
		set
		{
			SetMaterial(value);
		}
	}

	private void Start()
	{
		if (createCollider)
		{
			RecreateCollider();
		}
		for (int i = 0; i < Camera.allCameras.Length; i++)
		{
			Camera.allCameras[i].transparencySortMode = TransparencySortMode.Orthographic;
		}
	}

	public void FromJSON(string aJSON)
	{
		FromJSON(Ferr_JSON.Parse(aJSON));
	}

	public void FromJSON(Ferr_JSONValue aJSON)
	{
		fill = (Ferr2DT_FillMode)Enum.Parse(typeof(Ferr2DT_FillMode), aJSON["fill", "Closed"]);
		fillY = aJSON["fillY", 0f];
		fillZ = aJSON["fillZ", 0.2f];
		splitCorners = aJSON["splitCorners", true];
		smoothPath = aJSON["smoothPath", false];
		splitCount = (int)aJSON["splitDist", 4f];
		pixelsPerUnit = aJSON["pixelsPerUnit", 32f];
		stretchThreshold = aJSON["stretchThreshold", 0.5f];
		vertexColor = Ferr_Color.FromHex(aJSON["vertexColor", "FFFFFF"]);
		createCollider = aJSON["createCollider", true];
		create3DCollider = aJSON["create3DCollider", false];
		depth = aJSON["depth", 4f];
		surfaceOffset[0] = aJSON["surfaceOffset.0", 0f];
		surfaceOffset[1] = aJSON["surfaceOffset.1", 0f];
		surfaceOffset[2] = aJSON["surfaceOffset.2", 0f];
		surfaceOffset[3] = aJSON["surfaceOffset.3", 0f];
		collidersBottom = aJSON["colliders.bottom", true];
		collidersTop = aJSON["colliders.top", true];
		collidersLeft = aJSON["colliders.left", true];
		collidersRight = aJSON["colliders.right", true];
		Ferr_JSONValue ferr_JSONValue = aJSON["directionOverrides"];
		for (int i = 0; i < ferr_JSONValue.Length; i++)
		{
			directionOverrides.Add((Ferr2DT_TerrainDirection)Enum.Parse(typeof(Ferr2DT_TerrainDirection), ferr_JSONValue[i, "None"]));
		}
		path.FromJSON(aJSON["path"]);
	}

	public Ferr_JSONValue ToJSON()
	{
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		ferr_JSONValue["fill"] = string.Empty + fill;
		ferr_JSONValue["fillY"] = fillY;
		ferr_JSONValue["fillZ"] = fillZ;
		ferr_JSONValue["splitCorners"] = splitCorners;
		ferr_JSONValue["smoothPath"] = smoothPath;
		ferr_JSONValue["splitDist"] = splitCount;
		ferr_JSONValue["pixelsPerUnit"] = pixelsPerUnit;
		ferr_JSONValue["stretchThreshold"] = stretchThreshold;
		ferr_JSONValue["vertexColor"] = Ferr_Color.ToHex(vertexColor);
		ferr_JSONValue["createCollider"] = createCollider;
		ferr_JSONValue["create3DCollider"] = create3DCollider;
		ferr_JSONValue["depth"] = depth;
		ferr_JSONValue["surfaceOffset.0"] = surfaceOffset[0];
		ferr_JSONValue["surfaceOffset.1"] = surfaceOffset[1];
		ferr_JSONValue["surfaceOffset.2"] = surfaceOffset[2];
		ferr_JSONValue["surfaceOffset.3"] = surfaceOffset[3];
		ferr_JSONValue["colliders.bottom"] = collidersBottom;
		ferr_JSONValue["colliders.top"] = collidersTop;
		ferr_JSONValue["colliders.left"] = collidersLeft;
		ferr_JSONValue["colliders.right"] = collidersRight;
		ferr_JSONValue["directionOverrides"] = 0f;
		Ferr_JSONValue ferr_JSONValue2 = ferr_JSONValue["directionOverrides"];
		for (int i = 0; i < directionOverrides.Count; i++)
		{
			ferr_JSONValue2[i] = directionOverrides[i].ToString();
		}
		ferr_JSONValue["path"] = path.ToJSON();
		return ferr_JSONValue;
	}

	public void RecreatePath()
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		if (dMesh == null)
		{
			dMesh = new Ferr2DT_DynamicMesh();
		}
		if (terrainMaterial == null)
		{
			Debug.LogWarning("Cannot create terrain without a Terrain Material!");
			return;
		}
		MatchOverrides();
		ForceMaterial(terrainMaterial, true, false);
		dMesh.Clear();
		dMesh.color = vertexColor;
		if (path.Count < 2)
		{
			GetComponent<MeshFilter>().sharedMesh = null;
			return;
		}
		if (terrainMaterial.edgeMaterial.mainTexture != null && terrainMaterial.edgeMaterial.mainTexture != null)
		{
			unitsPerUV.x = (float)terrainMaterial.edgeMaterial.mainTexture.width / pixelsPerUnit;
			unitsPerUV.y = (float)terrainMaterial.edgeMaterial.mainTexture.height / pixelsPerUnit;
		}
		if (fill != Ferr2DT_FillMode.FillOnlyClosed && fill != Ferr2DT_FillMode.FillOnlySkirt)
		{
			List<List<Vector2>> segments = new List<List<Vector2>>();
			List<Ferr2DT_TerrainDirection> aSegDirections = new List<Ferr2DT_TerrainDirection>();
			segments = GetSegments(path.GetVerts(false, splitCount, splitCorners), out aSegDirections);
			if (aSegDirections.Count < segments.Count)
			{
				aSegDirections.Add(directionOverrides[directionOverrides.Count - 1]);
			}
			List<int> list = new List<int>();
			for (int i = 0; i < segments.Count; i++)
			{
				list.Add(i);
			}
			list.Sort(new Ferr2DT_Comparer<int>((int x, int y) => GetDescription(segments[y]).zOffset.CompareTo(GetDescription(segments[x]).zOffset)));
			for (int num = 0; num < list.Count; num++)
			{
				AddSegment(segments[list[num]], list.Count <= 1 && path.closed, smoothPath, aSegDirections[list[num]]);
			}
		}
		int[] currentTriangleList = dMesh.GetCurrentTriangleList();
		if ((fill == Ferr2DT_FillMode.Skirt || fill == Ferr2DT_FillMode.FillOnlySkirt) && terrainMaterial.fillMaterial != null)
		{
			AddFill(true);
		}
		else if ((fill == Ferr2DT_FillMode.Closed || fill == Ferr2DT_FillMode.InvertedClosed || fill == Ferr2DT_FillMode.FillOnlyClosed) && terrainMaterial.fillMaterial != null)
		{
			AddFill(false);
		}
		else if (fill != Ferr2DT_FillMode.None)
		{
		}
		int[] currentTriangleList2 = dMesh.GetCurrentTriangleList(currentTriangleList.Length);
		Mesh aMesh = GetComponent<MeshFilter>().sharedMesh;
		string text = "Ferr2DT_PathMesh_" + base.gameObject.GetInstanceID();
		if (aMesh == null || aMesh.name != text)
		{
			aMesh = (GetComponent<MeshFilter>().sharedMesh = new Mesh());
			aMesh.name = text;
		}
		dMesh.Build(ref aMesh, createTangents);
		aMesh.subMeshCount = 2;
		if (GetComponent<Renderer>().sharedMaterials.Length < 2)
		{
			Material[] sharedMaterials = GetComponent<Renderer>().sharedMaterials;
			GetComponent<Renderer>().sharedMaterials = new Material[2];
			if (sharedMaterials.Length > 0)
			{
				GetComponent<Renderer>().sharedMaterials[0] = sharedMaterials[0];
			}
		}
		aMesh.SetTriangles(currentTriangleList, 1);
		aMesh.SetTriangles(currentTriangleList2, 0);
	}

	public void RecreateCollider()
	{
		if (createCollider)
		{
			if (create3DCollider)
			{
				RecreateCollider3D();
			}
			else
			{
				RecreateCollider2D();
			}
		}
	}

	private void RecreateCollider3D()
	{
		Ferr2DT_DynamicMesh ferr2DT_DynamicMesh = new Ferr2DT_DynamicMesh();
		List<List<Vector2>> colliderVerts = GetColliderVerts();
		for (int i = 0; i < colliderVerts.Count; i++)
		{
			for (int j = 0; j < colliderVerts[i].Count; j++)
			{
				if (path.closed && j == colliderVerts.Count - 1)
				{
					ferr2DT_DynamicMesh.AddVertex(colliderVerts[i][0]);
				}
				else
				{
					ferr2DT_DynamicMesh.AddVertex(colliderVerts[i][j]);
				}
			}
		}
		ferr2DT_DynamicMesh.ExtrudeZ(depth, fill == Ferr2DT_FillMode.InvertedClosed);
		if (!collidersTop)
		{
			ferr2DT_DynamicMesh.RemoveFaces(new Vector3(0f, 1f, 0f), 45f);
		}
		if (!collidersLeft)
		{
			ferr2DT_DynamicMesh.RemoveFaces(new Vector3(-1f, 0f, 0f), 45f);
		}
		if (!collidersRight)
		{
			ferr2DT_DynamicMesh.RemoveFaces(new Vector3(1f, 0f, 0f), 45f);
		}
		if (!collidersBottom)
		{
			ferr2DT_DynamicMesh.RemoveFaces(new Vector3(0f, -1f, 0f), 45f);
		}
		if (GetComponent<MeshCollider>() == null)
		{
			base.gameObject.AddComponent<MeshCollider>();
		}
		if (physicsMaterial != null)
		{
			GetComponent<MeshCollider>().sharedMaterial = physicsMaterial;
		}
		Mesh aMesh = GetComponent<MeshCollider>().sharedMesh;
		string text = "Ferr2DT_PathCollider_" + base.gameObject.GetInstanceID();
		if (aMesh == null || aMesh.name != text)
		{
			aMesh = (GetComponent<MeshCollider>().sharedMesh = new Mesh());
			aMesh.name = text;
		}
		GetComponent<MeshCollider>().sharedMesh = null;
		ferr2DT_DynamicMesh.Build(ref aMesh, createTangents);
		GetComponent<MeshCollider>().sharedMesh = aMesh;
	}

	private void RecreateCollider2D()
	{
		if (GetComponent<PolygonCollider2D>() == null)
		{
			base.gameObject.AddComponent<PolygonCollider2D>();
		}
		if (physicsMaterial2D != null)
		{
			GetComponent<PolygonCollider2D>().sharedMaterial = physicsMaterial2D;
		}
		List<List<Vector2>> colliderVerts = GetColliderVerts();
		PolygonCollider2D component = GetComponent<PolygonCollider2D>();
		component.pathCount = colliderVerts.Count;
		if (colliderVerts.Count > 1)
		{
			for (int i = 0; i < colliderVerts.Count; i++)
			{
				component.SetPath(i, ExpandColliderPath(colliderVerts[i], colliderThickness).ToArray());
			}
		}
		else if (fill == Ferr2DT_FillMode.InvertedClosed)
		{
			Rect bounds = Ferr2D_Path.GetBounds(colliderVerts[0]);
			component.pathCount = 2;
			component.SetPath(0, colliderVerts[0].ToArray());
			component.SetPath(1, new Vector2[4]
			{
				new Vector2(bounds.xMin - bounds.width, bounds.yMax + bounds.height),
				new Vector2(bounds.xMax + bounds.width, bounds.yMax + bounds.height),
				new Vector2(bounds.xMax + bounds.width, bounds.yMin - bounds.height),
				new Vector2(bounds.xMin - bounds.width, bounds.yMin - bounds.height)
			});
		}
		else if (colliderVerts.Count > 0 && colliderVerts[0].Count > 0)
		{
			component.SetPath(0, colliderVerts[0].ToArray());
		}
	}

	private List<Vector2> ExpandColliderPath(List<Vector2> aList, float aAmount)
	{
		int count = aList.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			Vector2 normal = Ferr2D_Path.GetNormal(aList, num, false);
			aList.Add(aList[num] + new Vector2(normal.x * aAmount, normal.y * aAmount));
		}
		return aList;
	}

	public List<List<Vector2>> GetColliderVerts()
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		List<Vector2> verts = path.GetVerts(false, splitCount, splitCorners);
		if ((fill == Ferr2DT_FillMode.Skirt || fill == Ferr2DT_FillMode.FillOnlySkirt) && verts.Count > 0)
		{
			Vector2 vector = verts[0];
			verts.Add(new Vector2(verts[verts.Count - 1].x, fillY));
			verts.Add(new Vector2(vector.x, fillY));
			verts.Add(new Vector2(vector.x, vector.y));
		}
		float aSplitDistance = terrainMaterial.ToUV(terrainMaterial.descriptors[0].body[0]).width * ((float)terrainMaterial.edgeMaterial.mainTexture.width / pixelsPerUnit) / (float)Mathf.Max(1, splitCount) * splitDist;
		List<Ferr2DT_TerrainDirection> aSegDirections = new List<Ferr2DT_TerrainDirection>();
		List<List<Vector2>> list = new List<List<Vector2>>();
		List<List<Vector2>> segments = GetSegments(verts, out aSegDirections);
		List<Vector2> list2 = new List<Vector2>();
		for (int i = 0; i < segments.Count; i++)
		{
			if ((aSegDirections[i] == Ferr2DT_TerrainDirection.Bottom && !collidersBottom) || (aSegDirections[i] == Ferr2DT_TerrainDirection.Left && !collidersLeft) || (aSegDirections[i] == Ferr2DT_TerrainDirection.Top && !collidersTop) || (aSegDirections[i] == Ferr2DT_TerrainDirection.Right && !collidersRight))
			{
				if (list2.Count > 0)
				{
					list.Add(new List<Vector2>(list2));
					list2.Clear();
				}
				continue;
			}
			List<Vector2> list3 = new List<Vector2>(segments[i]);
			int count = list3.Count;
			for (int num = count - 1; num >= 0; num--)
			{
				Vector2 normal = Ferr2D_Path.GetNormal(segments[i], num, false);
				if (fill == Ferr2DT_FillMode.None)
				{
					list3.Add(list3[num] + new Vector2(normal.x * surfaceOffset[0], normal.y * surfaceOffset[0]));
					list3[num] += new Vector2(normal.x * (0f - surfaceOffset[3]), normal.y * (0f - surfaceOffset[3]));
				}
				else
				{
					float num2 = surfaceOffset[(int)aSegDirections[i]];
					Vector2 vector2 = new Vector2(num2, num2);
					list3[num] += new Vector2(normal.x * (0f - vector2.x), normal.y * (0f - vector2.y));
				}
			}
			if (smoothPath && list3.Count > 2)
			{
				list3 = Ferr2D_Path.SmoothSegment(list3, aSplitDistance, false);
			}
			list2.AddRange(list3);
		}
		if (list2.Count > 0)
		{
			list.Add(list2);
		}
		return list;
	}

	private void AddSegment(List<Vector2> aSegment, bool aClosed, bool aSmooth, Ferr2DT_TerrainDirection aDir = Ferr2DT_TerrainDirection.None)
	{
		Ferr2DT_SegmentDescription ferr2DT_SegmentDescription = ((aDir == Ferr2DT_TerrainDirection.None) ? GetDescription(aSegment) : terrainMaterial.GetDescriptor(aDir));
		int seed = UnityEngine.Random.seed;
		float num = terrainMaterial.ToUV(ferr2DT_SegmentDescription.body[0]).width * unitsPerUV.x;
		UnityEngine.Random.seed = (int)(aSegment[0].x * 100000f + aSegment[0].y * 10000f);
		Vector3 vector = aSegment[0];
		for (int i = 0; i < aSegment.Count - 1; i++)
		{
			Vector3 a = vector;
			vector = aSegment[i + 1];
			float num2 = Vector3.Distance(a, vector);
			int num3 = Mathf.Max(1, Mathf.FloorToInt(num2 / num + stretchThreshold));
			for (int j = 0; j < num3; j++)
			{
				SlicedQuad(aSegment, i, (float)j / (float)num3, (float)(j + 1) / (float)num3, Mathf.Max(2, splitCount + 2), aSmooth, aClosed, ferr2DT_SegmentDescription);
			}
		}
		if (!aClosed)
		{
			AddCap(aSegment, ferr2DT_SegmentDescription, -1f);
			AddCap(aSegment, ferr2DT_SegmentDescription, 1f);
		}
		UnityEngine.Random.seed = seed;
	}

	private void SlicedQuad(List<Vector2> aSegment, int aVert, float aStart, float aEnd, int aCuts, bool aSmoothed, bool aClosed, Ferr2DT_SegmentDescription aDesc)
	{
		Vector2[] array = new Vector2[aCuts];
		Vector2[] array2 = new Vector2[aCuts];
		Vector3 vector = Ferr2D_Path.GetNormal(aSegment, aVert, aClosed);
		Vector3 vector2 = Ferr2D_Path.GetNormal(aSegment, aVert + 1, aClosed);
		for (int i = 0; i < aCuts; i++)
		{
			float num = aStart + (float)i / (float)(aCuts - 1) * (aEnd - aStart);
			if (aSmoothed)
			{
				array[i] = Ferr2D_Path.HermiteGetPt(aSegment, aVert, num, aClosed);
				array2[i] = Ferr2D_Path.HermiteGetNormal(aSegment, aVert, num, aClosed);
			}
			else
			{
				array[i] = Vector2.Lerp(aSegment[aVert], aSegment[aVert + 1], num);
				array2[i] = Vector2.Lerp(vector, vector2, num);
			}
		}
		int seed = 0;
		if (randomByWorldCoordinates)
		{
			seed = UnityEngine.Random.seed;
			UnityEngine.Random.seed = (int)(array[0].x * 700000f + array[0].y * 30000f);
		}
		Rect rect = terrainMaterial.ToUV(aDesc.body[UnityEngine.Random.Range(0, aDesc.body.Length)]);
		float num2 = rect.height / 2f * unitsPerUV.y;
		float num3 = ((fill != Ferr2DT_FillMode.InvertedClosed) ? aDesc.yOffset : (0f - aDesc.yOffset));
		if (randomByWorldCoordinates)
		{
			UnityEngine.Random.seed = seed;
		}
		int aV = 0;
		int aV2 = 0;
		int num4 = 0;
		for (int j = 0; j < aCuts; j++)
		{
			float t = (float)j / (float)(aCuts - 1);
			Vector3 vector3 = array[j];
			Vector3 vector4 = array2[j];
			int num5 = dMesh.AddVertex(vector3.x + vector4.x * (num2 + num3), vector3.y + vector4.y * (num2 + num3), aDesc.zOffset, Mathf.Lerp(rect.x, rect.xMax, t), (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.y : rect.yMax);
			int num6 = dMesh.AddVertex(vector3.x - vector4.x * (num2 - num3), vector3.y - vector4.y * (num2 - num3), aDesc.zOffset, Mathf.Lerp(rect.x, rect.xMax, t), (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.yMax : rect.y);
			int num7 = ((!splitMiddle) ? (-1) : dMesh.AddVertex(vector3.x + vector4.x * num3, vector3.y + vector4.y * num3, aDesc.zOffset, Mathf.Lerp(rect.x, rect.xMax, t), Mathf.Lerp(rect.y, rect.yMax, 0.5f)));
			if (j != 0)
			{
				if (!splitMiddle)
				{
					dMesh.AddFace(num6, aV2, aV, num5);
				}
				else
				{
					dMesh.AddFace(num6, aV2, num4, num7);
					dMesh.AddFace(num7, num4, aV, num5);
				}
			}
			aV = num5;
			aV2 = num6;
			num4 = num7;
		}
	}

	private void AddCap(List<Vector2> aSegment, Ferr2DT_SegmentDescription aDesc, float aDir)
	{
		int num = 0;
		Vector2 zero = Vector2.zero;
		if (aDir < 0f)
		{
			num = 0;
			zero = aSegment[0] - aSegment[1];
		}
		else
		{
			num = aSegment.Count - 1;
			zero = aSegment[aSegment.Count - 1] - aSegment[aSegment.Count - 2];
		}
		zero.Normalize();
		Vector2 normal = Ferr2D_Path.GetNormal(aSegment, num, false);
		Vector2 vector = aSegment[num];
		Rect rect = ((fill != Ferr2DT_FillMode.InvertedClosed) ? terrainMaterial.ToUV(aDesc.leftCap) : terrainMaterial.ToUV(aDesc.rightCap));
		Rect rect2 = ((fill != Ferr2DT_FillMode.InvertedClosed) ? terrainMaterial.ToUV(aDesc.rightCap) : terrainMaterial.ToUV(aDesc.leftCap));
		float num2 = ((fill != Ferr2DT_FillMode.InvertedClosed) ? aDesc.yOffset : (0f - aDesc.yOffset));
		if (aDir < 0f)
		{
			float num3 = rect.width * unitsPerUV.x;
			float num4 = rect.height / 2f * unitsPerUV.y;
			int aV = dMesh.AddVertex(vector + zero * num3 + normal * (num4 + num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect.x : rect.xMax, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.y : rect.yMax));
			int aV2 = dMesh.AddVertex(vector + normal * (num4 + num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect.xMax : rect.x, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.y : rect.yMax));
			int aV3 = dMesh.AddVertex(vector - normal * (num4 - num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect.xMax : rect.x, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.yMax : rect.y));
			int aV4 = dMesh.AddVertex(vector + zero * num3 - normal * (num4 - num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect.x : rect.xMax, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect.yMax : rect.y));
			dMesh.AddFace(aV, aV2, aV3, aV4);
		}
		else
		{
			float num5 = rect2.width * unitsPerUV.x;
			float num6 = rect2.height / 2f * unitsPerUV.y;
			int aV5 = dMesh.AddVertex(vector + zero * num5 + normal * (num6 + num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.xMax : rect2.x, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.y : rect2.yMax));
			int aV6 = dMesh.AddVertex(vector + normal * (num6 + num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.x : rect2.xMax, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.y : rect2.yMax));
			int aV7 = dMesh.AddVertex(vector - normal * (num6 - num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.x : rect2.xMax, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.yMax : rect2.y));
			int aV8 = dMesh.AddVertex(vector + zero * num5 - normal * (num6 - num2), aDesc.zOffset, new Vector2((fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.xMax : rect2.x, (fill != Ferr2DT_FillMode.InvertedClosed) ? rect2.yMax : rect2.y));
			dMesh.AddFace(aV8, aV7, aV6, aV5);
		}
	}

	private void AddFill(bool aSkirt)
	{
		float aSplitDist = terrainMaterial.ToUV(terrainMaterial.descriptors[0].body[0]).width * ((float)terrainMaterial.edgeMaterial.mainTexture.width / pixelsPerUnit) / (float)Mathf.Max(1, splitCount) * splitDist;
		List<Vector2> aPoints = GetSegmentsCombined(aSplitDist);
		Vector2 vector = Vector2.one;
		if (terrainMaterial.fillMaterial != null && terrainMaterial.fillMaterial.mainTexture != null)
		{
			vector = new Vector2((float)terrainMaterial.fillMaterial.mainTexture.width / pixelsPerUnit, (float)terrainMaterial.fillMaterial.mainTexture.height / pixelsPerUnit);
		}
		if (aSkirt)
		{
			Vector2 vector2 = aPoints[0];
			Vector2 vector3 = aPoints[aPoints.Count - 1];
			aPoints.Add(new Vector2(vector3.x, fillY));
			aPoints.Add(new Vector2(Mathf.Lerp(vector3.x, vector2.x, 0.33f), fillY));
			aPoints.Add(new Vector2(Mathf.Lerp(vector3.x, vector2.x, 0.66f), fillY));
			aPoints.Add(new Vector2(vector2.x, fillY));
		}
		int vertCount = dMesh.VertCount;
		List<int> indices = Ferr2DT_Triangulator.GetIndices(ref aPoints, true, fill == Ferr2DT_FillMode.InvertedClosed);
		for (int i = 0; i < aPoints.Count; i++)
		{
			dMesh.AddVertex(aPoints[i].x, aPoints[i].y, fillZ, aPoints[i].x / vector.x, aPoints[i].y / vector.y);
		}
		for (int j = 0; j < indices.Count; j += 3)
		{
			try
			{
				dMesh.AddFace(indices[j] + vertCount, indices[j + 1] + vertCount, indices[j + 2] + vertCount);
			}
			catch
			{
			}
		}
	}

	public void SetMaterial(Ferr2DT_TerrainMaterial aMaterial)
	{
		ForceMaterial(aMaterial, false);
	}

	public void ForceMaterial(Ferr2DT_TerrainMaterial aMaterial, bool aForceUpdate, bool aRecreate = true)
	{
		if (terrainMaterial != aMaterial || aForceUpdate)
		{
			terrainMaterial = aMaterial;
			Material[] sharedMaterials = new Material[2] { aMaterial.fillMaterial, aMaterial.edgeMaterial };
			GetComponent<Renderer>().sharedMaterials = sharedMaterials;
			if (aRecreate)
			{
				RecreatePath();
			}
		}
	}

	public int AddPoint(Vector2 aPt, int aAtIndex = -1)
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		if (directionOverrides == null)
		{
			directionOverrides = new List<Ferr2DT_TerrainDirection>();
		}
		if (aAtIndex == -1)
		{
			path.Add(aPt);
			directionOverrides.Add(Ferr2DT_TerrainDirection.None);
			return path.pathVerts.Count;
		}
		path.pathVerts.Insert(aAtIndex, aPt);
		directionOverrides.Insert(aAtIndex, Ferr2DT_TerrainDirection.None);
		return aAtIndex;
	}

	public int AddAutoPoint(Vector2 aPt)
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		int closestSeg = path.GetClosestSeg(aPt);
		return AddPoint(aPt, (closestSeg + 1 != path.pathVerts.Count) ? (closestSeg + 1) : (-1));
	}

	public void RemovePoint(int aPtIndex)
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		if (aPtIndex < 0 || aPtIndex >= path.pathVerts.Count)
		{
			throw new ArgumentOutOfRangeException();
		}
		path.pathVerts.RemoveAt(aPtIndex);
		directionOverrides.RemoveAt(aPtIndex);
	}

	public void ClearPoints()
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		path.pathVerts.Clear();
		directionOverrides.Clear();
	}

	private Ferr2DT_SegmentDescription GetDescription(List<Vector2> aSegment)
	{
		Ferr2DT_TerrainDirection direction = Ferr2D_Path.GetDirection(aSegment, 0, fill == Ferr2DT_FillMode.InvertedClosed);
		return terrainMaterial.GetDescriptor(direction);
	}

	private List<List<Vector2>> GetSegments(List<Vector2> aPath, out List<Ferr2DT_TerrainDirection> aSegDirections)
	{
		List<List<Vector2>> aSegmentList = new List<List<Vector2>>();
		if (splitCorners)
		{
			aSegmentList = Ferr2D_Path.GetSegments(aPath, out aSegDirections, directionOverrides, fill == Ferr2DT_FillMode.InvertedClosed, GetComponent<Ferr2D_Path>().closed);
		}
		else
		{
			aSegDirections = new List<Ferr2DT_TerrainDirection>();
			aSegDirections.Add(Ferr2DT_TerrainDirection.Top);
			aSegmentList.Add(aPath);
		}
		if (path.closed)
		{
			Ferr2D_Path.CloseEnds(ref aSegmentList, ref aSegDirections, splitCorners, fill == Ferr2DT_FillMode.InvertedClosed);
		}
		return aSegmentList;
	}

	private List<Vector2> GetSegmentsCombined(float aSplitDist)
	{
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		List<Ferr2DT_TerrainDirection> aSegDirections = new List<Ferr2DT_TerrainDirection>();
		List<Vector2> list = new List<Vector2>();
		List<List<Vector2>> segments = GetSegments(path.GetVerts(false, splitCount, splitCorners), out aSegDirections);
		for (int i = 0; i < segments.Count; i++)
		{
			if (smoothPath && segments[i].Count > 2)
			{
				list.AddRange(Ferr2D_Path.SmoothSegment(segments[i], aSplitDist, false));
			}
			else
			{
				list.AddRange(segments[i]);
			}
		}
		return list;
	}

	public void MatchOverrides()
	{
		if (directionOverrides == null)
		{
			directionOverrides = new List<Ferr2DT_TerrainDirection>();
		}
		if (path == null)
		{
			path = GetComponent<Ferr2D_Path>();
		}
		for (int i = directionOverrides.Count; i < path.pathVerts.Count; i++)
		{
			directionOverrides.Add(Ferr2DT_TerrainDirection.None);
		}
		if (directionOverrides.Count > path.pathVerts.Count && path.pathVerts.Count > 0)
		{
			int num = directionOverrides.Count - path.pathVerts.Count;
			directionOverrides.RemoveRange(directionOverrides.Count - num - 1, num);
		}
	}
}
