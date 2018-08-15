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
  bool targetIsNear = false;
	
	void Start () {
		gunController = GetComponent<GunController>();
		animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {  
    targetIsNear = (Player.position - transform.position).sqrMagnitude <= attentionDistance * attentionDistance ? true : false;
    if (targetIsNear) {
      gunController.botShoot(Player.position);
      animator.SetBool("attackState", true);
    } else {
      animator.SetBool("attackState", false);
    }

	}

}
