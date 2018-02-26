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

	public Transform RealPlayer;
	public float attentionDistance = 10;
	Transform body;
	Vector3 target;
	bool targetIsNear = false;
	
	Vector3 direction;
	Animator animator;
	float animationState;
	
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
	}

	void FixedUpdate() {
		targetIsNear = Vector3.Distance(target, transform.position) <= attentionDistance ? true : false;
		if (targetIsNear) {
			Vector3 lookPos = RealPlayer.position - body.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			body.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * 10);
		} 
		else {
			body.localRotation = Quaternion.identity;
		}
	}

	void Update() {
		// Debug.Log(direction.magnitude + " : " + Vector3.zero);
		
		if (direction.magnitude > 0.1f) {
      animationState = 0.25f;
    } else {
			animationState = 0;
		}

		animator.SetFloat("animationState", animationState);

	}

  Vector3 BehaveModel() {
    Vector3 closestCover = Player.position;
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
				Quaternion targetRotation;
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
	}
}
