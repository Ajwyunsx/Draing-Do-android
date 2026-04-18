using System;
using UnityEngine;

[Serializable]
public class FaceTalk : MonoBehaviour
{
	public bool isFace1;

	[NonSerialized]
	public static GameObject Face1;

	[NonSerialized]
	public static GameObject Face2;

	private GameObject FaceObject;

	public FaceTalk()
	{
		isFace1 = true;
	}

	public virtual void Start()
	{
		if (isFace1)
		{
			if ((bool)Face1)
			{
				FaceObject = UnityEngine.Object.Instantiate(Face1) as GameObject;
				FaceObject.transform.position = transform.position;
				FaceObject.transform.parent = transform;
			}
		}
		else if ((bool)Face2)
		{
			FaceObject = UnityEngine.Object.Instantiate(Face2) as GameObject;
			FaceObject.transform.position = transform.position;
			FaceObject.transform.parent = transform;
			float x = FaceObject.transform.localScale.x * -1f;
			Vector3 localScale = FaceObject.transform.localScale;
			float num = (localScale.x = x);
			Vector3 vector = (FaceObject.transform.localScale = localScale);
		}
	}

	public virtual void Main()
	{
	}
}
