using System.Collections;
using UnityEngine;

public class DragRigidbody : MonoBehaviour
{
	public float spring = 50f;

	public float damper = 5f;

	public float drag = 10f;

	public float angularDrag = 5f;

	public float distance = 0.2f;

	public bool attachToCenterOfMass;

	private SpringJoint springJoint;

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Camera camera = FindCamera();
		RaycastHit hitInfo;
		if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
		{
			return;
		}
		if (!hitInfo.rigidbody || hitInfo.rigidbody.isKinematic || hitInfo.rigidbody.gameObject.tag != "Drag")
		{
			return;
		}
		if (!springJoint)
		{
			GameObject gameObject = new GameObject("Rigidbody dragger");
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			springJoint = gameObject.AddComponent<SpringJoint>();
			rigidbody.isKinematic = true;
		}
		springJoint.transform.position = hitInfo.point;
		if (attachToCenterOfMass)
		{
			Vector3 position = transform.TransformDirection(hitInfo.rigidbody.centerOfMass) + hitInfo.rigidbody.transform.position;
			springJoint.anchor = springJoint.transform.InverseTransformPoint(position);
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hitInfo.rigidbody;
		StartCoroutine(DragObject(hitInfo.distance));
	}

	private IEnumerator DragObject(float hitDistance)
	{
		float oldDrag = springJoint.connectedBody.drag;
		float oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		Camera mainCamera = FindCamera();
		while (Input.GetMouseButton(0))
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			springJoint.transform.position = ray.GetPoint(hitDistance);
			yield return null;
		}
		if (springJoint.connectedBody)
		{
			springJoint.connectedBody.drag = oldDrag;
			springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody = null;
		}
	}

	private Camera FindCamera()
	{
		Camera localCamera = GetComponent<Camera>();
		return localCamera ? localCamera : Camera.main;
	}
}
