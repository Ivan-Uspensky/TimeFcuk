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

	public Node this [ int index ] {
		get {
			return m_Connections [ index ];
		}
	}

	void OnValidate () {
		// Removing duplicate elements
		m_Connections = m_Connections.Distinct ().ToList ();
	}

  void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.5f);
  }
}
