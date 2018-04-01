using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

	public Transform OrbitalTarget;

	void Start () {
		
	}
	
	void Update () {
		OrbitAround();
	}

  void OrbitAround() {
    transform.RotateAround(OrbitalTarget.position, Vector3.up, Time.deltaTime * 30);
  }

  void OnDrawGizmos() {
		Debug.DrawLine(transform.position, OrbitalTarget.position, Color.red, 0, false);
	}


}
