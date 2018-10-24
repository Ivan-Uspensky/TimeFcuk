using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	public float speed;
	public Transform Spark;
	public float lifetime = 3;
	TrailRenderer trail;
	MeshRenderer mesh;
	Rigidbody rigidbody;
	public float spreadRange = 0.1f;
	Vector3 spreadVector;
	float moveDistance;

	void Start() {
		Destroy (gameObject, lifetime);

		rigidbody = GetComponent<Rigidbody>();
		
		trail = GetComponent<TrailRenderer>();
		trail.enabled = false;

		mesh = GetComponentInChildren<MeshRenderer>();

		float spreadX = Random.Range (-spreadRange, spreadRange);
		float spreadY = Random.Range (-spreadRange, spreadRange);
		spreadVector = new Vector3(0, 0, 1);
	}
	
	void Update () {
		trail.enabled = Time.timeScale != 1f ? true : false;
		if (!rigidbody.isKinematic) {
			// moveDistance = Time.deltaTime * speed;
			// CheckCollisions (moveDistance);
			// transform.Translate (spreadVector * moveDistance);
			rigidbody.velocity = transform.forward * speed;
		} else {
			mesh.enabled = false;
		}
	}

	// void FixedUpdate() {
	// 	Vector3 destination = transform.forward * moveDistance;
	// 	rigidbody.MovePosition(transform.position + destination);
	// }

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}

	// void CheckCollisions(float moveDistance) {
	// 	Ray ray = new Ray (transform.position, transform.right);
	// 	RaycastHit hit;

	// 	if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
	// 		OnHitObject(hit.collider, hit.point, hit.normal);
	// 	}
	// }

	// void OnHitObject(Collider c, Vector3 hitPoint, Vector3 hitNormal) {
  //   speed = 0;
	// 	Transform hitParticle = Instantiate(Spark, hitPoint, Quaternion.FromToRotation (Vector3.forward, hitNormal)) as Transform;
	// 	Destroy(hitParticle.gameObject, 1f);
	// 	Destroy (gameObject, lifetime);
	// }
	
	// void OnCollisionEnter(Collision collision) {
	// 	// foreach (ContactPoint contact in collision.contacts) {
	// 		speed = 0;
	// 		Transform hitParticle = Instantiate(Spark, collision.contacts[0].point, Quaternion.FromToRotation (Vector3.forward, collision.contacts[0].normal)) as Transform;
	// 		Destroy(hitParticle.gameObject, 1f);
	// 	// }
	// }

	void OnTriggerEnter(Collider hitInfo) {
		speed = 0;
		rigidbody.isKinematic = true;
		// Transform hitParticle = Instantiate(Spark, hitInfo.contacts[0].point, Quaternion.FromToRotation (Vector3.forward, hitInfo.contacts[0].normal)) as Transform;
		Vector3 hitPoint = hitInfo.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
		Transform hitParticle = Instantiate(Spark, hitPoint, Quaternion.FromToRotation (Vector3.forward, hitPoint.normalized)) as Transform;
		Destroy(hitParticle.gameObject, 1f);
	}

	void OnCollisionEnter(Collision collision) {
		// rigidbody.velocity = transform.forward * 0;
		rigidbody.isKinematic = true;
		Transform hitParticle = Instantiate(Spark, collision.contacts[0].point, Quaternion.FromToRotation (Vector3.forward, collision.contacts[0].normal)) as Transform;
		Destroy(hitParticle.gameObject, 2f);
  }
}