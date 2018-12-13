using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BotState))]
public class BotMoveHandler : MonoBehaviour {
	public UnityEngine.AI.NavMeshAgent agent;
	BotState State;
	void Start () {
		State = GetComponent<BotState>();
	}
	void Update () {
		if (State.GetSeeing()) {
			agent.SetDestination(State.GetPlayerPosition());
		} else {
			agent.ResetPath();
		}
		
	}
}
