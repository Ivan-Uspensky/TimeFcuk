using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePlacing : MonoBehaviour {

  public Transform muzzlePosition;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = muzzlePosition.position;
	}
}
