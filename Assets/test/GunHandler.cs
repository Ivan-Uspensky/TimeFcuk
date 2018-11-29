using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour {
	public AssaultRifle gun;
	void Start () {
		
	}
	public void Shoot() {
		if (gun != null) {
			gun.Shoot();
		}
	}
}
