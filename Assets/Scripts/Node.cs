using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour {
	[SerializeField]
	public List<Node> m_Connections = new List<Node> ();
	public virtual List<Node> connections {
		get {
			return m_Connections;
		}
	}
	List<Vector3> coverPoints;
	public Node this [ int index ] {
		get {
			return m_Connections [ index ];
		}
	}
	void Start () {
		coverPoints = new List<Vector3>();
		CountChildren(transform);
	}
	void OnValidate () {
		// Removing duplicate elements
		m_Connections = m_Connections.Distinct ().ToList ();
	}
  void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.25f);
  }
	void CountChildren(Transform a) {
		foreach (Transform child in a) {
			coverPoints.Add(child.position);
		}
  }
	public List<Vector3> getCoverPoints() {
		return coverPoints;
	}
}
