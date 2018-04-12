using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : MonoBehaviour {

	public Transform muzzle;
	public Projectile projectile;
	public float msBetweenShots = 100;
	public float muzzleVelocity = 50;

	// public float SpreadRange = 0.1f;
  // Vector3 spreadVector;
	
	float nextShotTime;
	public ParticleSystem muzzleFlash;

  public void Start() {
    // float spreadX = Random.Range (-SpreadRange, SpreadRange);
		// float spreadY = Random.Range (-SpreadRange, SpreadRange);
    // float spreadZ = Random.Range (-SpreadRange, SpreadRange);
		// spreadVector = new Vector3(spreadX, spreadY, spreadZ);
		muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmitting);
  }

	public void Shoot() {
		// if (Time.time > nextShotTime) {
			// nextShotTime = Time.time + msBetweenShots / 1000;
			Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;

      // Projectile newProjectile = Instantiate(projectile, muzzle.position, Quaternion.LookRotation(muzzle.position - spreadVector)) as Projectile;
      muzzleFlash.Play(true);
			newProjectile.SetSpeed (muzzleVelocity);
		// }
	}

	public void botShoot(Vector3 target) {
		if (Time.time > nextShotTime) {
			nextShotTime = Time.time + msBetweenShots / 1000;
			
			float step = 10 * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(muzzle.position, target, step, 0.0f);
			Quaternion rotation = Quaternion.LookRotation(newDir);

			// Debug.Log(rotation);
			Projectile newProjectile = Instantiate(projectile, muzzle.position, rotation) as Projectile;
      muzzleFlash.Play(true);
			newProjectile.SetSpeed (muzzleVelocity);
		}
	}
}
