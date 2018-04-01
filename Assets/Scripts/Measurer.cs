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

  int rot = 10;

  // List<float> distanceValues = new List<float>();
  // float[] distances = new float[Covers.Count];

	void Start () {
    coverPosition = UnitMovePointer.GetComponent<CurrentCoverPosition>();
  }
	
	void Update () {
    bestDistance = 9999f;
    // distanceValues.Clear();
    foreach (Transform child in Covers) {
		  currentDistance = (transform.position - child.position).sqrMagnitude;
			// distanceValues.Add(currentDistance);
      if (currentDistance < bestDistance) {
        closestCover = child;
				bestDistance = currentDistance;
      }
    }

    // Debug.Log(distanceValues + " : " + distanceValues.Count);

    // SelectedCoverPointer.transform.position = closestCover.position + new Vector3(0f,1.3f,0f);
    
    Vector3 pos = (closestCover.position - transform.position).normalized * offsetDistance;
    UnitMovePointer.transform.position = new Vector3(closestCover.position.x + pos.x, transform.position.y, closestCover.position.z + pos.z);
    // coverPosition.setPosition(closestCover.position);
  }

  // void OnGUI () {
  //   GUI.Box(new Rect(10,10,250,750), "Data: ");

  //   GUI.Label(new Rect(20,40,200,700), distanceValues.ToString());
  //   // GUI.Label(new Rect(20,70,140,20), followTarget.ToString());
  //   // GUI.Label(new Rect(20,90,80,20), speed);
  // }

}