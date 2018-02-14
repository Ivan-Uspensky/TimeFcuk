using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float mouseSensitivity = 3;
  public Transform target;
  public Vector2 pitchMinMax = new Vector2 (-60, 60);
  public Transform RedDotPrefab;
  public Transform cameraPosition;
  public RaycastHit hit;

  public float angle = 0f;
  public Vector3 axis = Vector3.zero;

  public Transform RedDot;

  public float distanceToDot = 10;
  
  float yaw;
	float pitch;
  Vector3 targetRotation;
	
  void Start() {
    RedDot = Instantiate( RedDotPrefab, Vector3.zero, new Quaternion(0,0,0,0));
  }

	void LateUpdate () {
		
    transform.position = cameraPosition.position;
    
    yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
    pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.timeScale;
    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

    targetRotation = new Vector3 (pitch, yaw);
    transform.eulerAngles = targetRotation;

    if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToDot)) {
      RedDot.transform.position = hit.point;
    } else {
      transform.rotation.ToAngleAxis(out angle, out axis);
      Vector3 pos = Quaternion.AngleAxis(angle, axis) * Vector3.forward * distanceToDot;
      RedDot.transform.position = transform.position + pos;
    }

  }
}
