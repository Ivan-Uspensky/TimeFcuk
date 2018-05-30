using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour {

	
  GunController gunController;
	Animator animator;
	public Transform source;
  public Transform weaponMuzzle;
  public Transform Player;
  public float attentionDistance = 10;
  RaycastHit hit;
	
	void Start () {
		gunController = GetComponent<GunController>();
		animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {  
    if (Physics.Raycast (source.position, source.TransformDirection(Vector3.forward), out hit)) {
      if (hit.collider.gameObject == Player.gameObject) { // can see player
        gunController.botShoot(hit.point);
        animator.SetBool("attackState", true);
      } else {
        animator.SetBool("attackState", false);
      }
    }
	}

  void OnDrawGizmos() {
		Debug.DrawLine(source.position, hit.point, Color.red, 0, false);
	}
}
