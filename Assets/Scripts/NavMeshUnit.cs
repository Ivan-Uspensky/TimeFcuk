using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshUnit : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;

  public Transform target;
  public Transform player;
  
  public float attentionDistance = 10;

  Animator animator;
  Transform unitBody;

  Quaternion unitRotation;
  Vector3 unitDirection;
  Vector3 lookDirection;
  Vector3 whereToLook;
  
  float unitRotationSpeed = 10;
  float unitAngleRotation;
  bool targetIsNear = false;
  float animationState;
 
  Vector3 prevUnitPos;
  // Vector3 currUnitPos;

	void Start () {
		animator = GetComponentInChildren<Animator>();
    unitBody = this.gameObject.transform.GetChild(0);
    prevUnitPos = transform.position;
	}
	
  void FixedUpdate() {
		targetIsNear = Vector3.Distance(player.position, transform.position) <= attentionDistance ? true : false;
		
		if (targetIsNear) {
			lookDirection = player.position - unitBody.position;
			lookDirection.y = 0;
			unitAngleRotation = Vector3.Angle(unitDirection.normalized, lookDirection.normalized);
			unitRotation = Quaternion.LookRotation(lookDirection);
			unitBody.rotation = Quaternion.Slerp(unitBody.rotation, unitRotation, Time.deltaTime * unitRotationSpeed);
			// spine.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 10);
		} 
		else {
			unitBody.localRotation = Quaternion.identity;
		}
	}

	void Update () {
		unitDirection = (transform.position - prevUnitPos).normalized;
    prevUnitPos = transform.position;
    
    //choose for animation
    whereToLook = unitDirection.normalized - lookDirection.normalized;
    if (unitDirection.magnitude > 0.1f) {
      if (unitAngleRotation > 50) {
				if (whereToLook.x >= 0) {
					animationState = 0.75f;
				} else {
					animationState = 1;
				}
			}
			if (unitAngleRotation <= 50) {
				animationState = 0.25f;
			}
    } else {
			animationState = 0;
		}
    //animate
		animator.SetFloat("animationState", animationState);

    //move with navmesh
    // agent.SetDestination(target.position);
	}
}
