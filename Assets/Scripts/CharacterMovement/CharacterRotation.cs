using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
	public float mouseSensitivity;
	public Transform target;
	public Vector2 pitchMinMax = new Vector2(-40, 85);
	public float rotationSmoothTime;
	private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;
	
	private float yaw;
	private float pitch;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void LateUpdate () {
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity,
			rotationSmoothTime);
		transform.eulerAngles = currentRotation;
		transform.position = target.position;
	}
}
