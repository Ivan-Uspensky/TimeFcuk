using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSight : MonoBehaviour {
	float heightMultiplier;
	float sightDist;
	float damping;
	Vector3 playerPosition;
	float playerAngle;
	public float seenDistance;
	public BotState State;
	void Start () {
		heightMultiplier = 0.25f;
		sightDist = 10;
		damping = 6.0f;
		State = gameObject.GetComponentInParent(typeof(BotState)) as BotState;
	}
	void Update () {
		// Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
		// Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
		// Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
		playerAngle = Vector3.Angle(transform.position - State.GetPlayerPosition(), transform.forward);
		// if ((State.GetPlayerPosition() - transform.position).sqrMagnitude <= seenDistance * seenDistance && playerAngle > 130 && playerAngle < 181) {
		if ((State.GetPlayerPosition() - transform.position).sqrMagnitude <= seenDistance * seenDistance) {
			SmoothLook(new Vector3(State.GetPlayerPosition().x, transform.position.y, State.GetPlayerPosition().z));
			State.SetSeeing(true);
		} else {
			State.SetSeeing(false);
		}
		// Debug.Log(State.GetWhoIsGunpointed());
		if (gameObject.name == State.GetWhoIsGunpointed()) {
			State.SetGunpointed(true);
		} else {
			State.SetGunpointed(false);
		}
		Debug.Log("SEEN: " + State.GetSeeing() + " POINTED: " + State.GetGunpointed());
	}
	void SmoothLook(Vector3 hitPoint) {
		Quaternion rotation = Quaternion.LookRotation(hitPoint - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}
}
