using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(GunHandler))]
public class PlayerMovement : MonoBehaviour {
	public bool lockCursor;
	public float gravity;
	public float jumpForce;
	public float moveSpeed;
	public Vector2 mouseSensitivity;
	public Vector2 verticalLookMinMax;
	public Transform cam;
	public float strafeHeadAngle;
	CharacterController controller;
	GunHandler gunController;
	float pitch;
	float velocityY;
	Vector3 dirOld;
	Vector3 moveDir;
	bool attackState;
	void Start () {
		controller = GetComponent<CharacterController>();
		gunController = GetComponent<GunHandler>();
		
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	void Update () {
		//Movement input handler
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"),0,Input.GetAxisRaw ("Vertical"));
		// Debug.Log("moveInput: " + moveInput);
		moveDir = Vector3.ClampMagnitude(moveInput, 1);
		// Debug.Log("moveDir: " + moveDir);
		Vector2 mouseInput = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		//Jump input handler
		if (controller.isGrounded) {
			velocityY = 0;
			if (Input.GetKeyDown (KeyCode.Space)) {
				velocityY = jumpForce;
			}
		} else {
			moveDir = dirOld;
		}
		//Shoot input handler
		if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftAlt)) {
      attackState = true;
    } else {
      attackState = false;
    }
		//Rotation handler
		transform.Rotate (Vector3.up * mouseInput.x * mouseSensitivity.x);
		pitch += mouseInput.y * mouseSensitivity.y;
		pitch = Mathf.Clamp (pitch, verticalLookMinMax.x, verticalLookMinMax.y);
		//Camera handler
		Quaternion yQuaternion = Quaternion.AngleAxis (pitch, Vector3.left);
		cam.localRotation =  yQuaternion;
		if (moveInput.x == 1) {
			cam.Rotate(0,0,strafeHeadAngle, Space.Self);
		}
		if (moveInput.x == -1) {
			cam.Rotate(0,0,-strafeHeadAngle, Space.Self);
		}

		//Player jump execution
		velocityY -= gravity * Time.deltaTime;	
		//Player movement execution
		Vector3 velocity = transform.TransformDirection(moveDir) * moveSpeed + Vector3.up * velocityY;
		controller.Move (velocity * Time.deltaTime);
		dirOld = moveDir;
		//Player shoot execution
		if (attackState) {
      gunController.Shoot();
    }
	}
}
