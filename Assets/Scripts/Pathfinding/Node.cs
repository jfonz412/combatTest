using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>{
	//https://www.youtube.com/watch?v=nhiFx28e7JY&t=1105s
	public bool walkable;
	public Vector3 worldPos; //might change to Vector2?
	
	//Node's x and y position on the grid
	public int x; 
	public int y;
	
	public int gCost;
	public int hCost;
	public Node parent;
	int heapIndex;
	
	public Node(bool _walkable, Vector3 _worldPos, int _x, int _y){
		walkable = _walkable;
		worldPos = _worldPos;
		x = _x;
		y = _y;
	}
	
	//simple 'getter' (?) so you don't need a third var. Works just the same
	public int fCost {
		get {
			return gCost + hCost;
		}
	}
	
	public int HeapIndex{
		get{
			return heapIndex;
		}
		set{
			heapIndex = value;
		}
	}
	
	public int CompareTo(Node nodeToCompare){
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if(compare == 0){
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}
