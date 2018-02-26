using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : MonoBehaviour {

	public Transform muzzle;
	public Projectile projectile;
	// public float msBetweenShots = 100;
	public float muzzleVelocity = 53;

	// public float SpreadRange = 0.1f;
  // Vector3 spreadVector;
	
	float nextShotTime;

  public void Start() {
    // float spreadX = Random.Range (-SpreadRange, SpreadRange);
		// float spreadY = Random.Range (-SpreadRange, SpreadRange);
    // float spreadZ = Random.Range (-SpreadRange, SpreadRange);
		// spreadVector = new Vector3(spreadX, spreadY, spreadZ);
  }

	public void Shoot() {
		// if (Time.time > nextShotTime) {
			// nextShotTime = Time.time + msBetweenShots / 1000;
			Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
      // Projectile newProjectile = Instantiate(projectile, muzzle.position, Quaternion.LookRotation(muzzle.position - spreadVector)) as Projectile;
      newProjectile.SetSpeed (muzzleVelocity);
		// }
	}
}
