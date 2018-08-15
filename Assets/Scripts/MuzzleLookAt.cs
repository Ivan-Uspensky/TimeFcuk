using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLookAt : MonoBehaviour {

  public Transform position;
	public Transform target;

	void Update () {
		transform.position = position.position;
		transform.LookAt(Vector3.zero);
	}

	void OnDrawGizmos() {
		Debug.DrawLine(target.position, position.position, Color.red, 0, false);
	}
}
