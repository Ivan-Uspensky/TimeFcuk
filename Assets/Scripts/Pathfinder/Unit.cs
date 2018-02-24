using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public Transform Player;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	//Behave variables
  // public Transform CoverZero;
  // public Transform covers;
  // Node childNode;
	// Node previousChildNode;
  Vector3 target;
  // CustomGrid grid;
	bool targetIsNear = false;

	Path path;

	void Start() {
		// grid = GameObject.Find ("Pathfinder").GetComponent<CustomGrid>();
    StartCoroutine (UpdatePath ());
	}

	void OnTriggerEnter(Collider other) {
    
		Debug.Log(other.gameObject.layer);

		if (other.gameObject.layer == 9) {
    	targetIsNear = true;
    }
  }
  void OnTriggerExit(Collider other) {
    targetIsNear = false;
	}

	void OnCollisionEnter(Collision other) {
    
		Debug.Log(other.gameObject.layer);

		if (other.gameObject.layer == 9) {
    	targetIsNear = true;
    }
  }
  void OnCollisionExit(Collision other) {
    targetIsNear = false;
	}

  Vector3 BehaveModel() {
    // float currentDistance;
		// float bestDistance = 999f;
		// Vector3 closestCover = CoverZero.position;
    Vector3 closestCover = Player.position;

    // if (previousChildNode == null){
		// 	previousChildNode = grid.NodeFromWorldPoint(closestCover);
		// }

    // if (Vector3.Distance(Player.position, transform.position) > 5f) {
    //   closestCover = Player.position + (Vector3.right * 5f);
    // } else {
      // foreach (Transform child in covers) {
			// 	currentDistance = Vector3.Distance(Player.position, child.position);
			// 	childNode = grid.NodeFromWorldPoint(child.position);
			// 	if (currentDistance < bestDistance) {
			// 		closestCover = child.position;
			// 		bestDistance = currentDistance;
			// 		previousChildNode = childNode;

      //     if (Player.position.x - child.position.x < 0) {
      //       closestCover.x += 0.5f;
      //     } else {
      //       closestCover.x -= 0.5f;
      //     }
        
      //     if (Player.position.z - child.position.z < 0) {
      //       closestCover.z += 0.5f;
      //     } else {
      //       closestCover.x -= 0.5f;
      //     }
			// 	}
			// }

    // }

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

				// if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
				// 	speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
				// 	if (speedPercent < 0.01f) {
				// 		followingPath = false;
				// 	}
				// }
				Quaternion targetRotation;

				if (!targetIsNear) {
					targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
					Debug.Log("usual rotation" + targetRotation);
				} else {
					targetRotation = Quaternion.LookRotation (target - transform.position);
					Debug.Log("pointed rotation" + targetRotation);
				}
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}
}
