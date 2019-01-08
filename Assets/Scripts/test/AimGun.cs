using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGun : MonoBehaviour {
	public Transform LaserDot;
	void Update () {
		// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LaserDot.position - transform.position), 0.2f);
		transform.LookAt(LaserDot.position);
	}
}
