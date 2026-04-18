using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class Rope_Line : MonoBehaviour
{
	public Transform target;

	public bool IsHorizontal;

	public int RightJumpIH;

	public int LeftJumpIH;

	public float resolution;

	public float ropeDrag;

	public float RopeMass;

	public float ropeColRadius;

	private Vector3[] segmentPos;

	private GameObject[] joints;

	private LineRenderer line;

	private int segments;

	private bool rope;

	public string Tag;

	public int Layer;

	public Vector3 swingAxiS;

	public float lowTwistLimit;

	public float highTwistLimit;

	public float swing1Limit;

	public Rope_Line()
	{
		RightJumpIH = 5;
		LeftJumpIH = 5;
		resolution = 1.45f;
		ropeDrag = 0.1f;
		RopeMass = 1f;
		ropeColRadius = 0.5f;
		segments = 4;
		Tag = "Rope";
		Layer = 9;
		swingAxiS = new Vector3(0f, 0f, 0f);
		swing1Limit = 20f;
	}

	public virtual void Awake()
	{
		BuildRope();
	}

	public virtual void Update()
	{
		if (rope)
		{
			for (int i = 0; i < segments; i++)
			{
				if (i == 0)
				{
					line.SetPosition(i, transform.position);
				}
				else if (i == segments - 1)
				{
					if ((bool)target)
					{
						line.SetPosition(i, target.transform.position);
					}
				}
				else
				{
					line.SetPosition(i, joints[i].transform.position);
				}
			}
			line.enabled = true;
		}
		else
		{
			line.enabled = false;
		}
	}

	public virtual void BuildRope()
	{
		line = (LineRenderer)gameObject.GetComponent("LineRenderer");
		segments = (int)(Vector3.Distance(transform.position, target.position) * resolution);
		line.SetVertexCount(segments);
		segmentPos = new Vector3[segments];
		joints = new GameObject[segments];
		segmentPos[0] = transform.position;
		segmentPos[segments - 1] = target.position;
		int num = segments - 1;
		Vector3 vector = (target.position - transform.position) / num;
		for (int i = 1; i < segments; i++)
		{
			Vector3 vector2 = default(Vector3);
			vector2 = vector * i + transform.position;
			segmentPos[i] = vector2;
			AddJointPhysics(i);
		}
		CharacterJoint characterJoint = target.gameObject.AddComponent<CharacterJoint>();
		characterJoint.connectedBody = joints[Extensions.get_length((System.Array)joints) - 1].transform.GetComponent<Rigidbody>();
		characterJoint.swingAxis = swingAxiS;
		float num2 = lowTwistLimit;
		SoftJointLimit softJointLimit = characterJoint.lowTwistLimit;
		float num3 = (softJointLimit.limit = num2);
		SoftJointLimit softJointLimit2 = (characterJoint.lowTwistLimit = softJointLimit);
		float num5 = highTwistLimit;
		SoftJointLimit softJointLimit4 = characterJoint.highTwistLimit;
		float num6 = (softJointLimit4.limit = num5);
		SoftJointLimit softJointLimit5 = (characterJoint.highTwistLimit = softJointLimit4);
		float num8 = swing1Limit;
		SoftJointLimit softJointLimit7 = characterJoint.swing1Limit;
		float num9 = (softJointLimit7.limit = num8);
		SoftJointLimit softJointLimit8 = (characterJoint.swing1Limit = softJointLimit7);
		target.parent = transform;
		rope = true;
		LineRenderer lineRenderer = (LineRenderer)GetComponent("LineRenderer");
		float x = (float)num * 0.85f;
		Vector2 mainTextureScale = lineRenderer.material.mainTextureScale;
		float num11 = (mainTextureScale.x = x);
		Vector2 vector3 = (lineRenderer.material.mainTextureScale = mainTextureScale);
	}

	public virtual void AddJointPhysics(int n)
	{
		joints[n] = new GameObject("Joint_" + n);
		joints[n].transform.parent = transform;
		Rigidbody rigidbody = joints[n].AddComponent<Rigidbody>();
		SphereCollider sphereCollider = joints[n].AddComponent<SphereCollider>();
		CharacterJoint characterJoint = joints[n].AddComponent<CharacterJoint>();
		characterJoint.swingAxis = swingAxiS;
		float num = lowTwistLimit;
		SoftJointLimit softJointLimit = characterJoint.lowTwistLimit;
		float num2 = (softJointLimit.limit = num);
		SoftJointLimit softJointLimit2 = (characterJoint.lowTwistLimit = softJointLimit);
		float num4 = highTwistLimit;
		SoftJointLimit softJointLimit4 = characterJoint.highTwistLimit;
		float num5 = (softJointLimit4.limit = num4);
		SoftJointLimit softJointLimit5 = (characterJoint.highTwistLimit = softJointLimit4);
		float num7 = swing1Limit;
		SoftJointLimit softJointLimit7 = characterJoint.swing1Limit;
		float num8 = (softJointLimit7.limit = num7);
		SoftJointLimit softJointLimit8 = (characterJoint.swing1Limit = softJointLimit7);
		joints[n].transform.position = segmentPos[n];
		joints[n].layer = Layer;
		joints[n].tag = Tag;
		rigidbody.constraints = (RigidbodyConstraints)56;
		rigidbody.drag = ropeDrag;
		rigidbody.mass = RopeMass;
		sphereCollider.radius = ropeColRadius;
		if (n == 1)
		{
			characterJoint.connectedBody = transform.GetComponent<Rigidbody>();
		}
		else
		{
			characterJoint.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
		}
	}

	public virtual void DestroyRope()
	{
		rope = false;
		for (int i = 0; i < Extensions.get_length((System.Array)joints); i++)
		{
			UnityEngine.Object.Destroy(joints[i]);
		}
		segmentPos = new Vector3[0];
		joints = new GameObject[0];
		segments = 0;
	}

	public virtual void Main()
	{
	}
}
