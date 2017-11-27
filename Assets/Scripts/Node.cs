using UnityEngine;
using System.Collections;

public class Node {
	//https://www.youtube.com/watch?v=nhiFx28e7JY&t=1105s
	public bool walkable;
	public Vector3 worldPos; //might change to Vector2?
	
	//structure
	public Node(bool _walkable, Vector3 _worldPos){
		walkable = _walkable;
		worldPos = _worldPos;
	}
}
