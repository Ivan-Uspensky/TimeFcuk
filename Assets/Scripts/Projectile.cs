using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	public float speed = 10;
	public Transform Spark;
  public float lifetime = 3;
	TrailRenderer trail;
  MeshRenderer mesh;
	public float spreadRange = 0.1f;
	Vector3 spreadVector;
	
	void Start() {
		Destroy (gameObject, lifetime);

		trail = GetComponent<TrailRenderer>();
		trail.enabled = false;

    mesh = GetComponentInChildren<MeshRenderer>();

		float spreadX = Random.Range (-spreadRange, spreadRange);
		float spreadY = Random.Range (-spreadRange, spreadRange);
		spreadVector = new Vector3(0, 0, 1);
	}
	
	void Update () {
		trail.enabled = Time.timeScale != 1f ? true : false;
    if (speed != 0) {
		  float moveDistance = Time.deltaTime * speed;
		  //CheckCollisions (moveDistance);
		  transform.Translate (spreadVector * moveDistance);
    } else {
      mesh.enabled = false;
    }
    // transform.Translate (spreadVector * moveDistance);
    // transform.Rotate(spreadVector);
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}

	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.right);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point, hit.normal);
		}
	}

	void OnHitObject(Collider c, Vector3 hitPoint, Vector3 hitNormal) {
    speed = 0;
		Transform hitParticle = Instantiate(Spark, hitPoint, Quaternion.FromToRotation (Vector3.forward, hitNormal)) as Transform;
		Destroy(hitParticle.gameObject, 1f);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			speed = 0;
			Transform hitParticle = Instantiate(Spark, contact.point, Quaternion.FromToRotation (Vector3.forward, contact.normal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}
	}
}