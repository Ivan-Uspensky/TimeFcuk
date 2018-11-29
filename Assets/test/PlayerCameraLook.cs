using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : MonoBehaviour {
	
	public Transform RedDot;
	public RaycastHit hit;
	public float distanceToDot;
  public LayerMask layerMask;
	float angle = 0f;
  Vector3 axis = Vector3.zero;
	void Start () {
		
	}
	void Update () {
		if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToDot, layerMask)) {
			//Position red don at obstacle
			RedDot.transform.position = hit.point;
		} else {
			//Position red dot at 'distanceToDot' 
			transform.rotation.ToAngleAxis(out angle, out axis);
      RedDot.transform.position = transform.position + Quaternion.AngleAxis(angle, axis) * Vector3.forward * distanceToDot;
		}

	}
}
