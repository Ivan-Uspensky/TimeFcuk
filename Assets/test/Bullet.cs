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
	MeshRenderer mesh;
	void Start () {
		Destroy (gameObject, lifetime);
		spreadVector = new Vector3(Random.Range (-spreadRange, spreadRange), Random.Range (-spreadRange, spreadRange), 1);
		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position);
		}
		GetComponent<TrailRenderer> ().material.SetColor ("_TintColor", trailColour);
		mesh = GetComponent<MeshRenderer>();
	}
	void Update () {
		if (speed == 0) {
			mesh.enabled = false;
		}
		moveDistance = Time.deltaTime * speed;
		CheckCollisions(moveDistance);
		transform.Translate(spreadVector * moveDistance);
	}
	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
		}
	}
	void OnHitObject(Collider c, Vector3 hitPoint) {
    Debug.Log("HIT");
		speed = 0;
	}
	// void OnTriggerEnter(Collider col) {
	// 	Debug.Log("HIT");
	// 	if(col.gameObject.layer == 10) {
	// 		Destroy(gameObject);
	// 	}
	// }
	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
}
