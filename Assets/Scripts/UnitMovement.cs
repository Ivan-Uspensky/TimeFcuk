using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;
	public Transform[] paths;
	public Animator animator;
	public Transform target;
	public Transform unitBody;

  public Transform spine;
	
	public bool isStopped = false;
	public float animationState;
	public float attentionDistance;

	int current;
	Vector3 unitDirection;
	Vector3 prevUnitPos;

	bool targetIsNear = false;
	
	// float unitRotationSpeed = 10;
  float unitAngleRotation;
	float bodyRotation;

	// Follower code
	public Graph m_Graph;
	Node start;
	Node end;
	Path m_Path = new Path();
	// int m_PathLength;
	int m_PathCurrent;
	Node m_Current;
	// Follower code
	Node nearestCover;
	Vector3 prevTargetposition;

	void Start () {
		current = 0;
		prevUnitPos = transform.position;
		// m_PathLength = 0;
		m_PathCurrent = 0;
	  prevTargetposition = new Vector3(15,0,15);
	}
	
	// m_Path.nodes[0].transform.position;

	void Update () {
		targetIsNear = (target.position - transform.position).sqrMagnitude <= attentionDistance * attentionDistance ? true : false;
		// if (targetIsNear) {
		// }
		// Movement();
		
		PauseControl();
		AnimationController();
	}

	Node GetNearestCover(Transform toObject) {
		float dist = 100;
		float temp = 0;
		Node nearest = m_Graph.nodes[m_Graph.nodes.Count - 1];
		for (int i = 0; i < m_Graph.nodes.Count - 1; i++ ) {
			temp = (toObject.position - m_Graph.nodes[i].transform.position).sqrMagnitude;
			if (temp < dist) {
				dist = temp;
				nearest = m_Graph.nodes[i];
			}
		}
		return nearest;
	}

	void FixedUpdate() {
		// model movement with navmesh methods
		if (!targetIsNear) {
			// agent.SetDestination(paths[current].transform.position);
			Movement();
		} else {
			CoversGetPath();
			CoversMovement();
		}
	}

	void CoversGetPath() {
		if (target.position != prevTargetposition) {
			start = GetNearestCover(transform);
			end = GetNearestCover(target);
			Debug.Log(start + " - " + end);
			m_Path = m_Graph.GetShortestPath(start, end);
			Debug.Log(m_Path);
			m_PathCurrent = 0;
			prevTargetposition = target.position;
		}
	}

	void CoversMovement() {
		// Debug.Log(m_PathCurrent + "/" + m_Path.nodes.Count + "/" + m_Path.nodes[m_PathCurrent]);
		agent.SetDestination(m_Path.nodes[m_PathCurrent].transform.position);
		if ((transform.position - m_Path.nodes[m_PathCurrent].transform.position).sqrMagnitude <= 0.25f) {
			agent.ResetPath();
			if (m_PathCurrent < m_Path.nodes.Count - 2) {
				m_PathCurrent++;
				// Debug.Log("++: " + m_PathCurrent);
			}
		}
	}

  void LateUpdate() {
    // spine look at
		RotationController();
  }

	void Movement() {
		//circling between two destinations
		if (agent.remainingDistance < 1 && agent.remainingDistance != 0) {
			agent.ResetPath();
			// current = current == 0 ? 1 : 0;
		}
	}

	void PauseControl() {
		// pause control
		agent.isStopped = isStopped;
	}

  void RotationController() {
		if (targetIsNear) {
			unitBody.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
      // spine.LookAt(new Vector3(target.position.x, 2, target.position.z));
		} 
		else {
			unitBody.localRotation = Quaternion.identity;
      // spine.localRotation = Quaternion.identity;
		}
  }

	void AnimationController() {
		unitAngleRotation = spine.localRotation.eulerAngles.y;
		// Debug.Log(unitAngleRotation);
		
		//animation selector
		unitDirection = (transform.position - prevUnitPos).normalized;
    prevUnitPos = transform.position;

    // if (targetIsNear) {
    //   animator.SetBool("attackState", true);
    // } else {
    //   animator.SetBool("attackState", false);
    // }

		if (unitDirection.magnitude > 0.1f) {
			
      // body strafe animation

			// if (unitAngleRotation > 66  && unitAngleRotation < 115) {
			// 	// transform.Rotate(new Vector3(0,-90,0));
			// 	animationState = 0.6f;
			// }
			// if (unitAngleRotation > 116  && unitAngleRotation < 245) {
			// 	// transform.Rotate(new Vector3(0,180,0));
			// 	animationState = 0.8f;
			// }
			// if (unitAngleRotation > 265 && unitAngleRotation < 295) {
			// 	// transform.Rotate(new Vector3(0,90,0));
			// 	animationState = 0.2f;	
			// }
				animationState = 0.4f;
			// Debug.Log(unitAngleRotation + " - " + animationState);

		} else {
			animationState = 0;
		}
		//animate
		animator.SetFloat("animationState", animationState);
	}

}
