using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurer : MonoBehaviour {

  public GameObject UnitMovePointer;
  public Transform SelectedCoverPointer;
  public Transform Covers;
  public Transform CoverZero;
  public float offsetDistance = 2f;

  float currentDistance;
  float bestDistance = 9999f;
  Transform closestCover;
  CurrentCoverPosition coverPosition;

	void Start () {
    coverPosition = UnitMovePointer.GetComponent<CurrentCoverPosition>();
  }
	
	void Update () {
    bestDistance = 9999f;
    foreach (Transform child in Covers) {
		  currentDistance = (transform.position - child.position).sqrMagnitude;
			if (currentDistance < bestDistance) {
        closestCover = child;
				bestDistance = currentDistance;
      }
    }

    // SelectedCoverPointer.transform.position = closestCover.position + new Vector3(0f,1.3f,0f);
    Vector3 pos = (closestCover.position - transform.position).normalized * offsetDistance;
    UnitMovePointer.transform.position = new Vector3(closestCover.position.x + pos.x, transform.position.y, closestCover.position.z + pos.z);
    coverPosition.setPosition(closestCover.position);
  }

}