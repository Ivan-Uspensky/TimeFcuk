using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurer : MonoBehaviour {

  public Transform Target;
  public Transform RedDot;
  public Transform DummyPointer;
  public Transform Covers;
  public Transform CoverZero;
  public float offsetDistance = 2f;

  float currentDistance;
  float bestDistance = 999f;
  // Vector3 closestCover;
  Transform closestCover;

	void Start () {
  }
	
	void Update () {
    bestDistance = 999f;
    foreach (Transform child in Covers) {
		  currentDistance = Vector3.Distance(transform.position, child.position);
			if (currentDistance < bestDistance) {
        closestCover = child;
				bestDistance = currentDistance;
      }
    }

    DummyPointer.transform.position = closestCover.position + new Vector3(0f,1.3f,0f);
    Vector3 pos = (closestCover.position - transform.position).normalized * offsetDistance;
    RedDot.transform.position = new Vector3(closestCover.position.x + pos.x, transform.position.y, closestCover.position.z + pos.z);
	}

	// void OnTriggerEnter(Collider other) {
	// 	Debug.Log("Dunmmy is triggered by" + other.gameObject.layer);
  // }
  // void OnTriggerExit(Collider other) {
  //   Debug.Log("Dunmmy is no longer triggered");
	// }

  void OnGUI () {
    GUI.Box(new Rect(10,10,150,120), "Distance");

    // string distance = Vector3.Distance(Target.position, transform.position).ToString();
    // string position = transform.position.ToString();

    GUI.Label(new Rect(20,40,80,20), currentDistance.ToString());
    GUI.Label(new Rect(20,70,140,20), bestDistance.ToString());
    // GUI.Label(new Rect(20,90,80,20), speed);
  }

}