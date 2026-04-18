using System;
using UnityEngine;

[Serializable]
public class Ferr2DT_SegmentDescription
{
	public Ferr2DT_TerrainDirection applyTo;

	public float zOffset;

	public float yOffset;

	public Rect leftCap;

	public Rect rightCap;

	public Rect[] body;

	public float capOffset;

	public Ferr2DT_SegmentDescription()
	{
		body = new Rect[1]
		{
			new Rect(0f, 0f, 50f, 50f)
		};
		applyTo = Ferr2DT_TerrainDirection.Top;
	}

	public Ferr_JSONValue ToJSON()
	{
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		ferr_JSONValue["applyTo"] = (float)applyTo;
		ferr_JSONValue["zOffset"] = zOffset;
		ferr_JSONValue["yOffset"] = yOffset;
		ferr_JSONValue["capOffset"] = capOffset;
		ferr_JSONValue["leftCap.x"] = leftCap.x;
		ferr_JSONValue["leftCap.y"] = leftCap.y;
		ferr_JSONValue["leftCap.xMax"] = leftCap.xMax;
		ferr_JSONValue["leftCap.yMax"] = leftCap.yMax;
		ferr_JSONValue["rightCap.x"] = rightCap.x;
		ferr_JSONValue["rightCap.y"] = rightCap.y;
		ferr_JSONValue["rightCap.xMax"] = rightCap.xMax;
		ferr_JSONValue["rightCap.yMax"] = rightCap.yMax;
		ferr_JSONValue["body"] = 0f;
		Ferr_JSONValue ferr_JSONValue2 = ferr_JSONValue["body"];
		for (int i = 0; i < body.Length; i++)
		{
			Ferr_JSONValue ferr_JSONValue3 = new Ferr_JSONValue();
			ferr_JSONValue3["x"] = body[i].x;
			ferr_JSONValue3["y"] = body[i].y;
			ferr_JSONValue3["xMax"] = body[i].xMax;
			ferr_JSONValue3["yMax"] = body[i].yMax;
			ferr_JSONValue2[i] = ferr_JSONValue3;
		}
		return ferr_JSONValue;
	}

	public void FromJSON(Ferr_JSONValue aJSON)
	{
		Ferr_JSONValue ferr_JSONValue = new Ferr_JSONValue();
		applyTo = (Ferr2DT_TerrainDirection)aJSON["applyTo", 0f];
		zOffset = aJSON["zOffset", 0f];
		yOffset = aJSON["yOffset", 0f];
		capOffset = aJSON["capOffset", 0f];
		leftCap = new Rect(aJSON["leftCap.x", 0f], aJSON["leftCap.y", 0f], aJSON["leftCap.xMax", 0f], aJSON["leftCap.yMax", 0f]);
		rightCap = new Rect(aJSON["rightCap.x", 0f], aJSON["rightCap.y", 0f], aJSON["rightCap.xMax", 0f], aJSON["rightCap.yMax", 0f]);
		Ferr_JSONValue ferr_JSONValue2 = ferr_JSONValue["body"];
		body = new Rect[ferr_JSONValue2.Length];
		for (int i = 0; i < body.Length; i++)
		{
			body[i] = new Rect(ferr_JSONValue2[i]["x", 0f], ferr_JSONValue2[i]["y", 0f], ferr_JSONValue2[i]["xMax", 50f], ferr_JSONValue2[i]["yMax", 50f]);
		}
	}
}
