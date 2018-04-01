using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;
	
  CurrentCoverPosition coverPosition;
  
  public GameObject followTarget;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	public Transform RealPlayer;
	public float attentionDistance = 10;
	Transform body;
	Vector3 target;
	bool targetIsNear = false;
	public Transform Covers;

  Vector3 closestCover;
  float bestDistance = 9999f;
  float currentDistance;

	Vector3 direction;
	Animator animator;
	float animationState;
	Quaternion targetRotation;
	Vector3 unitDirection;
	Vector3 lookDirection;
	float angle;
	public Transform spine;
	
	Path path;
	
	//Behave variables
  // public Transform CoverZero;
  // public Transform covers;
  // Node childNode;
	// Node previousChildNode;
  // CustomGrid grid;
 
	void Start() {
    StartCoroutine (UpdatePath ());
		animator = GetComponentInChildren<Animator>();
		body = this.gameObject.transform.GetChild(0);
    coverPosition = followTarget.GetComponent<CurrentCoverPosition>();
	}

	void FixedUpdate() {
		targetIsNear = Vector3.Distance(RealPlayer.position, transform.position) <= attentionDistance ? true : false;
		
		if (targetIsNear) {
			lookDirection = RealPlayer.position - body.position;
			lookDirection.y = 0;
			
			angle = Vector3.Angle(unitDirection.normalized, lookDirection.normalized);
			// Debug.Log(unitDirection.normalized + " : " + lookDirection.normalized);
			Quaternion rotation = Quaternion.LookRotation(lookDirection);
			// Quaternion rotation = Quaternion.Euler(lookDirection);
			body.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 10);
			// spine.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 10);

		} 
		else {
			body.localRotation = Quaternion.identity;
		}
	}

	void Update() {
		// Debug.Log(direction.magnitude + " : " + Vector3.zero);
		Vector3 side = unitDirection.normalized - lookDirection.normalized;
		if (direction.magnitude > 0.1f) {
      if (angle > 50) {
				if (side.x >= 0) {
					animationState = 0.75f;
				} else {
					animationState = 1;
				}
			}
			if (angle <= 50) {
				// if (side.z >= 0) {
					animationState = 0.25f;
				// } else {
				// 	animationState = 0.5f;
				// }
			}
    } else {
			animationState = 0;
		}

		animator.SetFloat("animationState", animationState);

	}

  Vector3 BehaveModel() {
    closestCover = followTarget.transform.position;

    // Debug.Log((transform.position - followTarget.position).sqrMagnitude);
    // if ((transform.position - followTarget.transform.position).sqrMagnitude < 0.8f ) {
      // Debug.Log("ololo1");
      // bestDistance = 9999f;
      // foreach (Transform child in Covers) {
		  //   currentDistance = (transform.position - child.position).sqrMagnitude;
			  // Debug.Log("currentDistance: " + currentDistance);
      //   if (currentDistance < bestDistance && child.position != coverPosition.currentCoverPosition) {
      //     closestCover = child.position;
			// 	  bestDistance = currentDistance;
      //   }
      // }
      // Vector3 pos = (closestCover - RealPlayer.transform.position).normalized * 2;
      // closestCover = new Vector3(closestCover.x + pos.x, transform.position.y, closestCover.z + pos.z);
    
      // Debug.Log(closestCover + " : " + followTarget.position);
    // }
    
    // Debug.Log(closestCover + " : " + followTarget.transform.position);
    return closestCover;
  }

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath() {
		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds (.3f);
		}
		target = BehaveModel();
    PathRequestManager.RequestPath (new PathRequest(transform.position, target, OnPathFound));
		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target;

		while (true) {
      target = BehaveModel();
			yield return new WaitForSeconds (minPathUpdateTime);
      if ((target - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (new PathRequest(transform.position, target, OnPathFound));
				targetPosOld = target;
			}
		}
	}

	IEnumerator FollowPath() {

		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt (path.lookPoints [0]);

		float speedPercent = 1;

		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {
				unitDirection = path.lookPoints [pathIndex] - transform.position;
				targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
				


				direction = path.lookPoints [pathIndex] - transform.position;
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed, Space.Self);
			}
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}

		Debug.DrawLine(transform.position, transform.position + unitDirection.normalized, Color.red, 0, false);
		Debug.DrawLine(transform.position, transform.position + lookDirection.normalized, Color.yellow, 0, false);

    // Gizmos.color = Color.green;
    // Gizmos.DrawCube(closestCover, Vector3.one);

	}

  // void OnGUI () {
  //   GUI.Box(new Rect(10,10,150,120), "Data: ");

  //   GUI.Label(new Rect(20,40,80,20), closestCover.ToString());
  //   GUI.Label(new Rect(20,70,140,20), followTarget.ToString());
  //   // GUI.Label(new Rect(20,90,80,20), speed);
  // }

}
