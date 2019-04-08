using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path {

	protected List<Node> m_Nodes = new List<Node> ();
	protected float m_Length = 0f;
	public virtual List<Node> nodes {
		get {
			return m_Nodes;
		}
	}

	public virtual float length {
		get {
			return m_Length;
		}
	}

	public virtual void Bake () {
		List<Node> calculated = new List<Node> ();
		m_Length = 0f;
		for ( int i = 0; i < m_Nodes.Count; i++ ) {
			Node node = m_Nodes [ i ];
			for ( int j = 0; j < node.connections.Count; j++ ) {
				Node connection = node.connections [ j ];
				// Don't calcualte calculated nodes
				if ( m_Nodes.Contains ( connection ) && !calculated.Contains ( connection ) ) {
					// Calculating the distance between a node and connection when they are both available in path nodes list
					m_Length += Vector3.Distance ( node.transform.position, connection.transform.position );
				}
			}
			calculated.Add ( node );
		}
	}

	public void Add(Node element) {
		m_Nodes.Add(element);
	}

	public override string ToString () {
		return string.Format (
			"Nodes: {0}\nLength: {1}",
			string.Join (
				", ",
				nodes.Select ( node => node.name ).ToArray () ),
			length );
	}
	
}
