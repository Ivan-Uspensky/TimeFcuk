using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent (typeof(BotState))]
public class BotMoveHandler : MonoBehaviour {
	public UnityEngine.AI.NavMeshAgent agent;
	public Graph m_Graph;
	BotState State;
	Vector3 prevTargetPosition;
	int m_PathCurrent;
	Vector3 longest;
	Node start;
	Node end;
	Path m_Path = new Path();
	void Start () {
		State = GetComponent<BotState>();
		m_PathCurrent = 0;
		prevTargetPosition = new Vector3(0,0,0);
	}
	void Update () {
		// if (State.GetSeeing()) {
			//  ----   agent.SetDestination(State.GetPlayerPosition());
			CoversGetPath(State.GetPlayerPosition());
			// CoversMovement();
		// } else {
		// 	agent.ResetPath();
		// }
	}
	void CoversGetPath(Vector3 whereToGo) {
		if (whereToGo != prevTargetPosition) {
			start = GetNearestCover(transform.position);
			end = GetNearestCover(whereToGo);
			m_Path = m_Graph.GetShortestPath(start, end);
			prevTargetPosition = whereToGo;
		}
	}
	Node GetNearestCover(Vector3 toObject) {
		float dist = 1000;
		float temp = 0;
		Node nearest = m_Graph.nodes[m_Graph.nodes.Count - 1];
		for (int i = 0; i < m_Graph.nodes.Count - 1; i++ ) {
			temp = (toObject - m_Graph.nodes[i].transform.position).sqrMagnitude;
			if (temp < dist) {
				dist = temp;
				nearest = m_Graph.nodes[i];
			}
		}
		return nearest;
	}
	// void CoversMovement() {
	// 	Vector3 actualCoverPoint = GetCoverSide(m_Path.nodes[m_PathCurrent]);
	// 	agent.SetDestination(actualCoverPoint);
	// 	if ((transform.position - actualCoverPoint).sqrMagnitude <= 1) {
	// 		if (m_PathCurrent < m_Path.nodes.Count - 1) {
	// 			m_PathCurrent++;
	// 		}
	// 	}
	// }
	// Vector3 GetCoverSide(Node node) {
	// 	List<Vector3> coverPoints = node.getCoverPoints();
	// 	longest = new Vector3(0,0,0);
	// 	float longestDist = 0;
	// 	foreach (Vector3 cover in coverPoints) {
	// 		if ((State.GetPlayerPosition() - cover).sqrMagnitude >= longestDist) {
	// 			longestDist = (State.GetPlayerPosition() - cover).sqrMagnitude;
	// 			longest = cover;
	// 		}
	// 	}
	// 	return longest;
	// }

	void OnDrawGizmos() {
  	Handles.color = Color.green;
		Vector3 currentPos = transform.position;
		foreach (Node node in m_Path.nodes) {
			Handles.DrawLine(currentPos, node.transform.position);
			currentPos = node.transform.position;
		}
  }

}
