﻿using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform target;
	public float speed = 70f;
	public int damage = 50;
	public float explosionRadius = 0f;
	public GameObject impactEffect;
	
	public void Seek (Transform _target) {
		target = _target;
	}

	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}

	void HitTarget () {
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		Destroy(gameObject);
	}

	void Explode () {
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders) {
			Debug.Log(collider.name);
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}