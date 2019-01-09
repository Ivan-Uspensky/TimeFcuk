using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoverEnlightment : MonoBehaviour {
	public bool isCustomColor;
  public Color customColor;
  Color currentColor;
  void Start() {
    if (isCustomColor) {
      currentColor = customColor;
    } else {
      currentColor = Color.yellow;
    }
  }
  void OnDrawGizmos() {
    Gizmos.color = currentColor;
    Gizmos.DrawSphere(transform.position, 0.25f);
  }
}
