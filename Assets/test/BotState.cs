using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotState : MonoBehaviour {
	bool isOffensive;
	bool isDefensive;
	bool isHit;
	bool isGunpointed;
	bool isMoving;
	bool isCovered;
	bool isShooting;
	bool isSeeing;
	public Transform Player;
	PlayerCameraLook playerSees;
	PlayerMovement sidePositions;
	void Start () {
		playerSees = Player.GetComponent<PlayerCameraLook>();
		sidePositions = Player.GetComponent<PlayerMovement>();
	}
	public string GetWhoIsGunpointed() {
		return playerSees.GetHitName();
	}
	public void SetOffensive(bool state) {
		isOffensive = state;
	}
	public bool GetOffensive() {
		return isOffensive;
	}
	public void SetDefensive(bool state) {
		isDefensive = state;
	}
	public bool GetDefensive() {
		return isDefensive;
	}
	public void SetHit(bool state) {
		isHit = state;
	}
	public bool GetHit() {
		return isHit;
	}
	public void SetGunpointed(bool state) {
		isGunpointed = state;
	}
	public bool GetGunpointed() {
		return isGunpointed;
	}
	public void SetMoving(bool state) {
		isMoving = state;
	}
	public bool GetMoving() {
		return isMoving;
	}
	public void SetCovered(bool state) {
		isCovered = state;
	}
	public bool GetCovered() {
		return isCovered;
	}
	public void SetShooting(bool state) {
		isShooting = state;
	}
	public bool GetShooting() {
		return isShooting;
	}
	public void SetSeeing(bool state) {
		isSeeing = state;
	}
	public bool GetSeeing() {
		return isSeeing;
	}
	public Vector3 GetPlayerPosition() {
		return Player.position;
	}
	public List<Vector3> GetSidePositins() {
		return sidePositions.GetSidePositins();
	}
	
}
