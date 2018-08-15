using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLookAt : MonoBehaviour {

	// public Transform position;
	public Transform target;
	private Quaternion m_MyQuaternion;


	void Update () {
		transform.LookAt(target);
		//m_MyQuaternion.SetLookRotation(target.position, Vector3.up);
		//transform.rotation = m_MyQuaternion;
	}
}
