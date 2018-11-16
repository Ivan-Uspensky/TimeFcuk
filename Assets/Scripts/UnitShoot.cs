using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour {

	
  GunController gunController;
	Animator animator;
  public Transform weaponMuzzle;
  public Transform Player;
  public float attentionDistance = 10;
  // RaycastHit hit;
  float rateAbility;
  bool targetIsNear = false;
  UnitMovement movement;
  Vector3 unitDirection;
  float rateFire;
  RaycastHit hit;
  public LayerMask layerMask;
	
	void Start () {
		gunController = GetComponent<GunController>();
		animator = GetComponentInChildren<Animator>();
    movement = GetComponentInChildren<UnitMovement>();
	}
	
	void Update () {  
    targetIsNear = (Player.position - transform.position).sqrMagnitude <= attentionDistance * attentionDistance ? true : false;
    if (targetIsNear) {
      
      unitDirection = movement.GetUnitDirection();

      if (Physics.Raycast(transform.position, Player.position, out hit, Mathf.Infinity, layerMask)) {
      //  Debug.Log(hit.collider.gameObject.layer);
       if (hit.collider.gameObject.layer == 10) {
          // Debug.Log("I see my target");
          if (unitDirection.magnitude > 0.1f) {
            rateAbility = 0.5f;
          } else {
            rateAbility = 0.8f;
          }
          rateFire = Random.value;
          if (rateFire <= rateAbility) {
            Debug.Log(rateFire + " - attack state: true");
            gunController.botShoot(Player.position);
            animator.SetBool("attackState", true);
          }
        } else {
          Debug.Log(rateFire + " - attack state: false");
          animator.SetBool("attackState", false);
        }
      }
    }
	}
  void OnGUI () {
    GUI.Box(new Rect(10,10,150,120), "Movement States");  
    string rateFireString = rateFire.ToString();
    GUI.Label(new Rect(20,90,80,20), rateFireString);
  }

  void OnDrawGizmos() {
		Debug.DrawLine(transform.position, Player.position, Color.red, 0, false);
	}

}
