using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurer : MonoBehaviour {

  public GameObject UnitMovePointer1;
  public GameObject UnitMovePointer2;
  public Transform Covers;
  public Transform CoverZero;
  public float offsetDistance = 2f;

  float firstHighestNumber;
  float secondHighestNumber;
  float currentDistance;
  Transform closestCover;

  Transform first;
  Transform second;

	void Update () {
    
    firstHighestNumber = 9999f;
    secondHighestNumber = 9999f;

    foreach (Transform child in Covers) {
		  currentDistance = (transform.position - child.position).sqrMagnitude;
      if (currentDistance < firstHighestNumber) {
        first = child;
				firstHighestNumber = currentDistance;
      }
    }

    foreach (Transform child in Covers) {
		  currentDistance = (transform.position - child.position).sqrMagnitude;
      if (currentDistance < secondHighestNumber && currentDistance != firstHighestNumber) {
        second = child;
				secondHighestNumber = currentDistance;
      }
    }

    Vector3 pos1 = (first.position - transform.position).normalized * offsetDistance;
    Vector3 pos2 = (second.position - transform.position).normalized * offsetDistance;
    UnitMovePointer1.transform.position = new Vector3(first.position.x + pos1.x, transform.position.y, first.position.z + pos1.z);
    UnitMovePointer2.transform.position = new Vector3(second.position.x + pos2.x, transform.position.y, second.position.z + pos2.z);
  }

  // void OnGUI () {
  //   GUI.Box(new Rect(10,10,250,750), "Data: ");

  //   GUI.Label(new Rect(20,40,200,700), distanceValues.ToString());
  //   // GUI.Label(new Rect(20,70,140,20), followTarget.ToString());
  //   // GUI.Label(new Rect(20,90,80,20), speed);
  // }

}