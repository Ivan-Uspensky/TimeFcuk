using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	// public Transform weaponHold;
  public Machinegun startingGun;
	Machinegun equippedGun;

	void Start () {
    // if (startingGun != null) {
    //   EquipGun(startingGun);
    // }
		equippedGun = GetComponentInChildren<Machinegun>();
	}

	// public void EquipGun(Machinegun gunToEquip) {
	// 	if (equippedGun != null) {
	// 		Destroy(equippedGun.gameObject);
	// 	}
	// 	equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
  //   equippedGun.transform.parent = weaponHold;
	// }

	public void Shoot() {
		if (equippedGun != null) {
			equippedGun.Shoot();
		}
	}

	public void botShoot(Vector3 target) {
		if (equippedGun != null) {
			equippedGun.botShoot(target);
		}
	}
}
