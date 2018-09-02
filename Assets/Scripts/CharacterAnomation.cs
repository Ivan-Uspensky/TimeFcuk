using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(GunController))]
public class CharacterAnomation : MonoBehaviour {

  public float runSpeed = 2.5f;
  // public BoxCollider playerCollider;
  public Transform viewCamera;

  public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

  float animationState;
  float playerRotation;
  float currentSpeed;

  float movementAnim;

  float yaw;
	float pitch;

  bool slideState;
  bool attackState;
  bool timeState;
  // float strafeState;

  float attackJourney;
  public float slowdownPlayerFactor;

  public float msBetweenShots = 0.1f;

  LayerMask layerMask;
  Transform modelTrans;
  // PlayerController controller;

  public Transform shoulderTrans;
  public Transform rightShoulder;
  public Transform chest;
  public Transform spine;
  public Vector3 lookPos;
  public Transform fakeTarget;

  public TimeManager timeManager;


  float timeFactor;

  GameObject rsp;
  GunController gunController;

  Vector3 moveInput;
  // Vector3 moveVelocity;
  
  Animator animator;
  Vector3 direction;
  
  public bool weaponRotation;

  void Start () {
    
    animator = GetComponentInChildren<Animator>();
    // controller = GetComponent<PlayerController> ();
    gunController = GetComponent<GunController>();
    timeState = false;
    // collider = GetComponent<BoxCollider>();

    rsp = new GameObject();
    rsp.name = transform.root.name + " Right Shoulder IK Helper";
    // attackJourney = 0.7f;
  }
  
  void Update () {

    moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
    // moveVelocity = moveInput.normalized * runSpeed;

    // Debug.Log(moveInput);
    // controller.Move(moveVelocity);
    
    if (Input.GetKey(KeyCode.LeftControl)) {
      slideState = true;
      // collider.size = new Vector3(1, 1.3f, 1);
      // collider.center = new Vector3(0, 0.65f, 0);
    } else {
      slideState = false;
      // collider.size = new Vector3(1, 1.9f, 1);
      // collider.center = new Vector3(0, 0.95f, 0);
    }
    
    if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftAlt)) {
      // animator.SetBool("attackState", true);
      attackState = true;
    } else {
      // animator.SetBool("attackState", false);
      attackState = false;
    }

    // INCORRECT SLOMO FORMURA OF VALUES
    if (Input.GetKeyDown(KeyCode.Mouse1)) {
      if (!timeState) {
        timeManager.DoSlow();
        currentSpeed = runSpeed * timeManager.slowdownFactor / slowdownPlayerFactor;
        animator.speed = timeManager.slowdownFactor / slowdownPlayerFactor;
      } else {
        timeManager.DoFast();
        currentSpeed = runSpeed;
        animator.speed = 1.6f;
      }
      timeState = !timeState;
    }

    if (moveInput == Vector3.forward) {
      animationState = 0.2f;
    }
    if (moveInput == Vector3.back) {
      animationState = 0.4f;
    }
    if (moveInput == Vector3.left) {
      animationState = 0.6f;
    }
    if (moveInput == Vector3.right) {
      animationState = 0.8f;
    }
    // if (moveInput == new Vector3(Mathf.Abs(1),0,1)) {
    //   strafeState = moveInput.x;
    // } else {
    //   strafeState = 0;
    // }
    // if (slideState && moveInput != Vector3.zero) {
    if (slideState) {
      animationState = 1f;
    }

    if (moveInput == Vector3.zero) {
      animationState = 0;
      currentSpeed = 0;
    } else {
      currentSpeed = runSpeed;
    }
    
    // play animation
    animator.SetFloat("animationState", animationState);
  }

	void FixedUpdate() {
		transform.Translate(transform.TransformVector(moveInput) * currentSpeed * Time.fixedDeltaTime, Space.World);
    
    // camera rotation
    playerRotation = viewCamera.transform.eulerAngles.y;
    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, playerRotation, ref turnSmoothVelocity, turnSmoothTime);

    // if (strafeState != 0) {
    //   chest.eulerAngles  = new Vector3(30 * strafeState, 0, 0);
    // } else {
    //   chest.eulerAngles  = new Vector3(0, 0, 0);
    // }
    // Debug.Log("CHESHT: " + chest.eulerAngles + " : " + chest.rotation + " | SPINE: " + spine.eulerAngles + " : " + spine.rotation);
    // Debug.Log(chest.eulerAngles - spine.eulerAngles);
    // weapon rotation
    HandleAimingPos();
    HandleShoulder();
	}

  void HandleAimingPos() {
    lookPos = GameObject.Find("LaserDot(Clone)").transform.position;
    // lookPos = fakeTarget.transform.position;
  }

  void HandleShoulder() {
    
    if (weaponRotation) {
      // shoulderTrans.Rotate(Vector3.right, 30f);
      shoulderTrans.LookAt(lookPos);
      shoulderTrans.rotation = Quaternion.Euler(30f,shoulderTrans.eulerAngles.y,shoulderTrans.eulerAngles.z);
    } else {
      // shoulderTrans.rotation = Quaternion.identity;
      shoulderTrans.LookAt(lookPos);
    }
    
    Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);

    //shooting and shoulder positioning
    if (attackState) {
      if (Time.time > attackJourney) {
        attackJourney = Time.time + msBetweenShots / 1000;
        gunController.Shoot();
      }
      rsp.transform.position = Vector3.Lerp(rightShoulderPos, new Vector3(rightShoulderPos.x, rightShoulderPos.y, rightShoulderPos.z  - 0.2f), attackJourney - Time.time);
    } else {
      rsp.transform.position = rightShoulderPos;
    }

    rsp.transform.parent = transform;
    shoulderTrans.position = rsp.transform.position;

  }

  void OnGUI () {
    GUI.Box(new Rect(10,10,150,120), "Movement States");  
    string speed = animationState.ToString();
    GUI.Label(new Rect(20,90,80,20), speed);
  }
}
