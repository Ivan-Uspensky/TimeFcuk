using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshUnit : MonoBehaviour {

	public Transform target; 
  public UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(target.position);
	}
}
