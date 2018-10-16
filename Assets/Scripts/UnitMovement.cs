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
	
	public bool isStopped;
	public float animationState;
	public float attentionDistance;

	float movementSpeed;
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
	bool isSit;
	Vector3 longest;

	void Start () {
		current = 0;
		prevUnitPos = transform.position;
		// m_PathLength = 0;
		m_PathCurrent = 0;
	  prevTargetposition = new Vector3(15,0,15);
		animator.SetLayerWeight(2, 1.0f);
		movementSpeed = agent.speed;
		isSit = false;
	}
	
	// m_Path.nodes[0].transform.position;

	void Update () {
		targetIsNear = (target.position - transform.position).sqrMagnitude <= attentionDistance * attentionDistance ? true : false;
		PauseControl();
		AnimationController();
	}

	void FixedUpdate() {
		// if (!targetIsNear) {
			// agent.SetDestination(paths[current].transform.position);
			// Movement();
		// } else {
			CoversGetPath();
			CoversMovement();
		// }
	}

	Node GetNearestCover(Transform toObject) {
		float dist = 1000;
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

	void CoversGetPath() {
		if (target.position != prevTargetposition) {
			start = GetNearestCover(transform);
			end = GetNearestCover(target);

// ------- Get correct cover position ------- //
			// List<Vector3> coverPoints = end.getCoverPoints();
			// longest = new Vector3(0,0,0);
			// float longestDist = 0;
			// foreach (Vector3 cover in coverPoints) {
			// 	if ((target.position - cover).sqrMagnitude >= longestDist) {
			// 		longestDist = (target.position - cover).sqrMagnitude;
			// 		longest = cover;
			// 	}
			// }
			// Debug.Log("cover: " + longest);
// ------- ------- ------- ------- ------- //
			
			// Debug.Log(start + " - " + end);
			m_Path = m_Graph.GetShortestPath(start, end);
			// Debug.Log(m_Path);
			m_PathCurrent = 0;
			prevTargetposition = target.position;
		}
	}

	Vector3 GetActualCoverPoint(Node node) {
		List<Vector3> coverPoints = node.getCoverPoints();
		longest = new Vector3(0,0,0);
		float longestDist = 0;
		foreach (Vector3 cover in coverPoints) {
			if ((target.position - cover).sqrMagnitude >= longestDist) {
			longestDist = (target.position - cover).sqrMagnitude;
			longest = cover;
			}
		}
		return longest;
	}

	void CoversMovement() {
		Vector3 actualCoverPoint = GetActualCoverPoint(m_Path.nodes[m_PathCurrent]);
		agent.SetDestination(actualCoverPoint);
		// Debug.Log((transform.position - m_Path.nodes[m_PathCurrent].transform.position).sqrMagnitude);
		if ((transform.position - actualCoverPoint).sqrMagnitude <= 0.7f) {
// ------- Movement through covers ------- //
			agent.speed = 0;
			isSit = true;
			StartCoroutine(WaitNGoNext());
			agent.ResetPath();
// ------- ------- ------- ------- ------- //
			if (m_PathCurrent < m_Path.nodes.Count - 2) {
				m_PathCurrent++;
			}
		}
	}

  void LateUpdate() {
    // spine look at
		RotationController();
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
				animator.SetLayerWeight(1, 1f);
				animationState = 0.4f;
			// Debug.Log(unitAngleRotation + " - " + animationState);

		} else {
				animator.SetLayerWeight(1, 0.4f);
				animationState = 1;
		}
		//animate
		animator.SetFloat("animationState", animationState);
	}

  void OnCollisionEnter(Collision collision) {
	  // Debug.Log(collision.contacts[0].otherCollider.gameObject.layer);
    if (collision.contacts[0].otherCollider.gameObject.layer == 14) {
      // animator.SetBool("hitState", true);
			animator.SetLayerWeight(2, 1.0f);
			animator.Play("Hit",2,0.2f);
			agent.speed = 1f;
			StartCoroutine(StopHit());
			// if (!animator.GetCurrentAnimatorStateInfo(2).IsName("Hit")) {
			// 	animator.SetBool("hitState", false);
			// }
    }
	}
	IEnumerator StopHit() {
    yield return new WaitForSeconds(0.2f);
    animator.SetLayerWeight(2, 0);
		agent.speed = movementSpeed;
  }

	IEnumerator WaitNGoNext() {
		float rand = Random.Range(0.5f, 3.5f);
		yield return new WaitForSeconds(rand);
		isSit = false;
		agent.speed = movementSpeed;
		// animationState = 0.4f;
	}
	
  void OnDrawGizmos() {
    Gizmos.color = Color.black;
    // Gizmos.DrawSphere(longest, 0.5f);
		Gizmos.DrawCube(longest, new Vector3(0.8f, 3, 0.8f));
		
		Gizmos.color = Color.cyan;
		if (end) {
			Gizmos.DrawCube(end.transform.position, new Vector3(2, 5, 2));
		}
  }
}
