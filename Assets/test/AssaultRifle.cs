using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour {
	public Transform muzzle;
	public Bullet projectile;
	public float msBetweenShots;
	public float muzzleVelocity;
	float nextShotTime;
	void Start () {
		
	}
	public void Shoot() {
		if (Time.time > nextShotTime) {
			nextShotTime = Time.time + msBetweenShots / 1000;
			Bullet bullet = Instantiate(projectile, muzzle.position, muzzle.rotation) as Bullet;
			bullet.SetSpeed(muzzleVelocity);
		}
	}
}
