using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour {
	public LayerMask collisionMask;
	float skinWidth;
	public Color trailColour;
	public float speed;
	public float lifetime;
	public float spreadRange;
	Vector3 spreadVector;
	float moveDistance;
	public MeshRenderer mesh;
	public Transform Spark;
	float tempTimer;
	float tempDistance;
	void Start () {
		Destroy (gameObject, lifetime);
		spreadVector = new Vector3(Random.Range (-spreadRange, spreadRange), Random.Range (-spreadRange, spreadRange), 1);
		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position, transform.position.normalized);
		}
		GetComponent<TrailRenderer> ().material.SetColor ("_TintColor", trailColour);
		// mesh = GetComponent<MeshRenderer>();
		tempTimer = 0;
		tempDistance = 0;
	}
	void Update () {
		if (speed == 0) {
			mesh.enabled = false;
		}
		moveDistance = Time.deltaTime * speed;
		tempTimer = tempTimer + Time.deltaTime;
		tempDistance = tempDistance + moveDistance;
		CheckCollisions(moveDistance);
		transform.Translate(spreadVector * moveDistance);
	}
	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point, hit.normal);
		}
	}
	void OnHitObject(Collider c, Vector3 hitPoint, Vector3 hitNormal) {
		speed = 0;
		Transform hitParticle = Instantiate(Spark, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitNormal)) as Transform;
		Destroy(hitParticle.gameObject, 1f);
	}
	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
}
