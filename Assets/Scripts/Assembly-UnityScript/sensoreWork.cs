using System;
using UnityEngine;

[Serializable]
public class sensoreWork : MonoBehaviour
{
	[HideInInspector]
	public Transform _parent;

	[HideInInspector]
	public bool _ONCE;

	[HideInInspector]
	public PonyControl _compo;

	[HideInInspector]
	public GameObject OBJ11;

	[HideInInspector]
	public GameObject OBJ12;

	public virtual void Start()
	{
		_parent = transform.parent.transform;
		_compo = (PonyControl)_parent.GetComponent("PonyControl");
	}

	public virtual void FixedUpdate()
	{
		if (_ONCE)
		{
			if (!_compo.ToDropBox)
			{
				if ((bool)OBJ12)
				{
					_compo.SensoreOnTriggerStay(OBJ12);
				}
				else
				{
					_compo.SensoreOnTriggerStay(OBJ11);
				}
			}
			else if ((bool)OBJ11)
			{
				_compo.SensoreOnTriggerStay(OBJ11);
			}
			OBJ11 = null;
			OBJ12 = null;
			_ONCE = false;
		}
		transform.position = new Vector3(_parent.position.x + (float)_compo.Direction * 0.2f, _parent.position.y, _parent.position.z);
	}

	public virtual void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 12)
		{
			OBJ12 = other.gameObject;
			_ONCE = true;
		}
		if (other.gameObject.layer == 11)
		{
			OBJ11 = other.gameObject;
			_ONCE = true;
		}
	}

	public virtual void Main()
	{
	}
}
