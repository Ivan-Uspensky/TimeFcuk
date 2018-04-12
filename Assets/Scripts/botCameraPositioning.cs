using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botCameraPositioning : MonoBehaviour {

  public Transform head;

	void Update () {
		transform.position = head.position;
	}
}
