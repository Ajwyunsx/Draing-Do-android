using UnityEngine;

public class Ferr2DT_TerrainMaterial : MonoBehaviour
{
	public Material fillMaterial;

	public Material edgeMaterial;

	public Ferr2DT_SegmentDescription[] descriptors = new Ferr2DT_SegmentDescription[4];

	public Ferr2DT_TerrainMaterial()
	{
		for (int i = 0; i < descriptors.Length; i++)
		{
			descriptors[i] = new Ferr2DT_SegmentDescription();
		}
	}

	public Ferr_JSONValue ToJSON()
	{
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		ferr_JSONValue["fillMaterialName"] = fillMaterial.name;
		ferr_JSONValue["edgeMaterialName"] = edgeMaterial.name;
		ferr_JSONValue["descriptors"] = 0f;
		Ferr_JSONValue ferr_JSONValue2 = ferr_JSONValue["descriptors"];
		for (int i = 0; i < descriptors.Length; i++)
		{
			ferr_JSONValue2[i] = descriptors[i].ToJSON();
		}
		return ferr_JSONValue;
	}

	public void FromJSON(string aJSON)
	{
		FromJSON(Ferr_JSON.Parse(aJSON));
	}

	public void FromJSON(Ferr_JSONValue aJSON)
	{
		Ferr_JSONValue ferr_JSONValue = aJSON["descriptors"];
		for (int i = 0; i < ferr_JSONValue.Length; i++)
		{
			descriptors[i] = new Ferr2DT_SegmentDescription();
			descriptors[i].FromJSON(ferr_JSONValue[i]);
		}
	}

	public Ferr2DT_SegmentDescription GetDescriptor(Ferr2DT_TerrainDirection aDirection)
	{
		for (int i = 0; i < descriptors.Length; i++)
		{
			if (descriptors[i].applyTo == aDirection)
			{
				return descriptors[i];
			}
		}
		if (descriptors.Length > 0)
		{
			return descriptors[0];
		}
		return new Ferr2DT_SegmentDescription();
	}

	public bool Has(Ferr2DT_TerrainDirection aDirection)
	{
		for (int i = 0; i < descriptors.Length; i++)
		{
			if (descriptors[i].applyTo == aDirection)
			{
				return true;
			}
		}
		return false;
	}

	public void Set(Ferr2DT_TerrainDirection aDirection, bool aActive)
	{
		if (aActive)
		{
			if (descriptors[(int)aDirection].applyTo != aDirection)
			{
				descriptors[(int)aDirection] = new Ferr2DT_SegmentDescription();
				descriptors[(int)aDirection].applyTo = aDirection;
			}
		}
		else if (descriptors[(int)aDirection].applyTo != Ferr2DT_TerrainDirection.Top)
		{
			descriptors[(int)aDirection] = new Ferr2DT_SegmentDescription();
		}
	}

	public Rect ToUV(Rect aPixelUVs)
	{
		if (edgeMaterial == null)
		{
			return aPixelUVs;
		}
		return new Rect(aPixelUVs.x / (float)edgeMaterial.mainTexture.width, 1f - aPixelUVs.height / (float)edgeMaterial.mainTexture.height - aPixelUVs.y / (float)edgeMaterial.mainTexture.height, aPixelUVs.width / (float)edgeMaterial.mainTexture.width, aPixelUVs.height / (float)edgeMaterial.mainTexture.height);
	}

	public Rect ToScreen(Rect aPixelUVs)
	{
		if (edgeMaterial == null)
		{
			return aPixelUVs;
		}
		return new Rect(aPixelUVs.x / (float)edgeMaterial.mainTexture.width, aPixelUVs.y / (float)edgeMaterial.mainTexture.height, aPixelUVs.width / (float)edgeMaterial.mainTexture.width, aPixelUVs.height / (float)edgeMaterial.mainTexture.height);
	}
}
