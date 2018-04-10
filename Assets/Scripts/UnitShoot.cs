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
    // if (Vector3.Distance(Player.position, transform.position) <= attentionDistance) {
      
    // float x_offset = (Mathf.Cos(Mathf.Deg2Rad * (source.eulerAngles.y)));
    // float z_offset =(-Mathf.Sin(Mathf.Deg2Rad * (source.eulerAngles.y)));
    // Physics.Raycast(toEdge_origin.position, new Vector3(x_offset,0,z_offset), out edge_hit, 1.5f);
      
      if (Physics.Raycast (source.position, source.TransformDirection(Vector3.forward), out hit)) {
        if (hit.collider.gameObject == Player.gameObject) { // can see player
          Debug.Log(Player.position + " " + hit.point);
          gunController.botShoot(hit.point);
        //   // animator.SetFloat("Blend", 1);
      	}
      }
      
    // }
	}

  void OnDrawGizmos() {
		Debug.DrawLine(source.position, hit.point, Color.red, 0, false);
	}
}
