using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMoveHandler : MonoBehaviour {
	PlayerCameraLook playerSees;
	public Transform player; 
	void Start () {
		playerSees = player.GetComponent<PlayerCameraLook>();
	}
	void Update () {
		if (gameObject.name == playerSees.GetHitName()) {
			Debug.Log("AAAAAAAAAAAA!!!");
		}
	}
}
