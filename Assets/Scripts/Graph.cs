using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour {
	[SerializeField]
	protected List<Node> m_Nodes = new List<Node> ();
	public virtual List<Node> nodes {
		get {
			return m_Nodes;
		}
	}

	public virtual Path GetShortestPath (Node start, Node end) {
    if ( start == null || end == null ) {
			throw new ArgumentNullException ();
		}
		Path path = new Path ();
		if ( start == end ) {
			path.nodes.Add ( start );
			return path;
		}
		List<Node> unvisited = new List<Node> ();
		Dictionary<Node, Node> previous = new Dictionary<Node, Node> ();
		Dictionary<Node, float> distances = new Dictionary<Node, float> ();
		
		for ( int i = 0; i < m_Nodes.Count; i++ ) {
			Node node = m_Nodes [ i ];
			unvisited.Add(node);
			distances.Add(node, float.MaxValue);
		}
		
		distances [ start ] = 0f;
		while ( unvisited.Count != 0 ) {
			unvisited = unvisited.OrderBy ( node => distances [ node ] ).ToList ();
			Node current = unvisited [ 0 ];
			unvisited.Remove ( current );
			if ( current == end ) {
				while (previous.ContainsKey(current)) {
					path.nodes.Insert ( 0, current );
					current = previous [ current ];
				}
				path.nodes.Insert ( 0, current );
				break;
			}

			for ( int i = 0; i < current.connections.Count; i++ ) {
        Node neighbor = current.connections [ i ];
        float length = Vector3.Distance ( current.transform.position, neighbor.transform.position );
				float alt = distances [ current ] + length;
				if ( alt < distances [ neighbor ] ) {
					distances [ neighbor ] = alt;
					previous [ neighbor ] = current;
				}
			}
		}
		path.Bake ();
		return path;
	}
	
}
