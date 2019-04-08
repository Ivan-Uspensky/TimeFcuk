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
	Vector3 actualCoverPoint;
	Node start;
	Node end;
	Path m_Path = new Path();
	int sidePositionCounter;
	
	void Start () {
		State = GetComponent<BotState>();
		m_PathCurrent = 0;
		prevTargetPosition = new Vector3(0,0,0);
		sidePositionCounter = 0;
	}
	void Update () {
		// if (State.GetSeeing()) {
			//  ----   agent.SetDestination(State.GetPlayerPosition());
			CoversGetPath(State.GetPlayerPosition());
			CoversMovement();
		// } else {
		// 	agent.ResetPath();
		// }
	}
	void CoversGetPath(Vector3 whereToGo) {
		if ((whereToGo - prevTargetPosition).sqrMagnitude >= 0.562f) {
			end = GetNearestCover(whereToGo);
			if (m_PathCurrent < m_Path.nodes.Count - 2) {
				m_Path = m_Graph.GetShortestPath(m_Path.nodes[m_PathCurrent], end);
			} else {
				start = GetNearestCover(transform.position);
				m_Path = m_Graph.GetShortestPath(start, end);
			}
			m_PathCurrent = 0;
			prevTargetPosition = whereToGo;
		}
	}
	Node GetNearestCover(Vector3 toObject) {
		float dist = 1000;
		float temp = 0;
		Node nearest = m_Graph.nodes[m_Graph.nodes.Count - 2];
		for (int i = 0; i < m_Graph.nodes.Count - 2; i++ ) {
			temp = (toObject - m_Graph.nodes[i].transform.position).sqrMagnitude;
			if (temp < dist) {
				dist = temp;
				nearest = m_Graph.nodes[i];
			}
		}
		return nearest;
	}
	void CoversMovement() {
		actualCoverPoint = GetCoverSide(m_Path.nodes[m_PathCurrent]);
		actualCoverPoint.y = 0.25f;
		agent.SetDestination(actualCoverPoint);
		
		if ((transform.position - actualCoverPoint).sqrMagnitude <= 0.36f) {
			// agent.ResetPath();
			if (m_PathCurrent < m_Path.nodes.Count - 2) {
				m_PathCurrent++;
			} else {
				List<Transform> positions = State.GetSidePositins();
				// Debug.Log(State.GetSidePositins()[0].position);
				agent.SetDestination(positions[sidePositionCounter].position);
			}
			
			
		}
	}
	Vector3 GetCoverSide(Node node) {
		List<Vector3> coverPoints = node.getCoverPoints();
		longest = new Vector3(0,0,0);
		float longestDist = 0;
		foreach (Vector3 cover in coverPoints) {
			if ((State.GetPlayerPosition() - cover).sqrMagnitude >= longestDist) {
				longestDist = (State.GetPlayerPosition() - cover).sqrMagnitude;
				longest = cover;
			}
		}
		return longest;
	}
	void OnDrawGizmos() {
  	Handles.color = Color.green;
		Vector3 currentPos = transform.position;
		for (int i = m_PathCurrent; i < m_Path.nodes.Count - 1; i++ ) {
			Handles.DrawLine(currentPos, m_Path.nodes[i].transform.position);
			currentPos = m_Path.nodes[i].transform.position;
		}
		Gizmos.color = Color.red;
    Gizmos.DrawSphere(actualCoverPoint, 0.35f);
  }

}
