using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;
	public Transform[] paths;
	public Animator animator;
	public Transform target;
	public Transform unitBody;
	
	public bool isStopped = false;
	public float animationState;

	int current;
	Vector3 unitDirection;
	Vector3 prevUnitPos;

	bool targetIsNear = false;
	float attentionDistance = 10;
	// float unitRotationSpeed = 10;
  // float unitAngleRotation;

	void Start () {
		current = 0;
		prevUnitPos = transform.position;
	}
	
	void Update () {
		Movement();
		PauseControl();
		AnimationController();
	}

	void FixedUpdate() {
		// model movement with navmesh methods
		agent.SetDestination(paths[current].position);
	}

	void Movement() {
		//circling between two destinations
		if (agent.remainingDistance < 1 && agent.remainingDistance != 0) {
			agent.ResetPath();
			current = current == 0 ? 1 : 0;
		}
	}

	void PauseControl() {
		// pause control
		agent.isStopped = isStopped;
	}

	void AnimationController() {
		//rotation selector 
		targetIsNear = Vector3.Distance(target.position, transform.position) <= attentionDistance ? true : false;
		if (targetIsNear) {
			unitBody.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
		} 
		else {
			unitBody.localRotation = Quaternion.identity;
		}
		//animation selector
		unitDirection = (transform.position - prevUnitPos).normalized;
    prevUnitPos = transform.position;
		if (unitDirection.magnitude > 0.1f) {
			animationState = 0.4f;
    } else {
			animationState = 0;
		}
		//animate
		animator.SetFloat("animationState", animationState);
	}
}
