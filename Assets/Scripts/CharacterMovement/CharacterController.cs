using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float runSpeed;
	public float turningSmoothTime;

	public float speedSmoothTime;

	private float speedSmoothVelocity;
	private float currentSpeed;
	private float turnSmoothVelocity;
	private Animator animator;
	private Transform camera;
	
	private float animationSpeedRate;
	private float targetSpeed;
	private Vector3 input;
	private float targetRotation;
	
	void Start () {
		animator = GetComponent<Animator>();
		camera = Camera.allCameras[1].transform;
	}
	
	void Update () {
		input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		
		targetRotation = camera.eulerAngles.y;
		transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turningSmoothTime);

		targetSpeed = runSpeed * input.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
		
		if (input == Vector3.forward) {
			animationSpeedRate = 0.2f;
		}
		if (input == Vector3.back) {
			animationSpeedRate = 0.4f;
		}
		if (input == Vector3.left) {
			animationSpeedRate = 0.8f;
		}
		if (input == Vector3.right) {
			animationSpeedRate = 0.6f;
		}
		if (input == Vector3.zero) {
			animationSpeedRate = 0;
		}
		
		animator.SetFloat("speedRate", animationSpeedRate, speedSmoothTime, Time.deltaTime);
	}

	void FixedUpdate() {
		transform.Translate(transform.TransformVector(input) * currentSpeed * Time.deltaTime, Space.World);
	}
}
