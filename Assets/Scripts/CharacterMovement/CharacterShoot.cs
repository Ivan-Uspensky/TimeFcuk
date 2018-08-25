using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour {

	Animator animator;
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			Debug.Log("reloading");
			animator.SetBool("reloadState", true);
			StartCoroutine(ResetReload());
		}
	}

	IEnumerator ResetReload() {
		yield return new WaitForSeconds(1.2f);
		animator.SetBool("reloadState", false);
  }
}
