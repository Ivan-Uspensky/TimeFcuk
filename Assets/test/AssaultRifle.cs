using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour {
	public Transform muzzle;
	public Bullet projectile;
	public float msBetweenShots;
	public float muzzleVelocity;
	float nextShotTime;
	Vector3 recoilMoveSmooth;
	float recoilAngle;
	float recoilRotSmooth;
	void LateUpdate() {
		transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilMoveSmooth, .1f);
		// recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmooth, .1f);
		// transform.localEulerAngles = transform.localEulerAngles + Vector3.left * recoilAngle;
	}
	public void Shoot() {
		if (Time.time > nextShotTime) {
			nextShotTime = Time.time + msBetweenShots / 1000;
			transform.localPosition -= Vector3.forward * .2f;
			// recoilAngle += 5;
			// recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);
 			Bullet bullet = Instantiate(projectile, muzzle.position, muzzle.rotation) as Bullet;
			bullet.SetSpeed(muzzleVelocity);
		}
	}
}
