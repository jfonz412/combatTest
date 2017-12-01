using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {
	public Transform seeker, target;
	
	Grid grid;
	
	void Awake(){
		grid = GetComponent<Grid>();
	}
	
	void Update(){
		FindPath(seeker.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 endPos){
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node endNode = grid.NodeFromWorldPoint(endPos);
		
		//difference between the two types?
		List<Node> openNodes = new List<Node>(); 
		HashSet<Node> closeNodes = new HashSet<Node>();
		
		openNodes.Add(startNode);
		
		while (openNodes.Count > 0){
			Node currentNode = openNodes[0];
			//start with i = 1 so we skip openNode[0] aka our startNode
			for (int i = 1; i < openNodes.Count; i++){
				if(openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost && openNodes[i].hCost < currentNode.hCost) {
					currentNode = openNodes[i];
				}
			}
			
			openNodes.Remove(currentNode);
			closeNodes.Add (currentNode);
			
			if (currentNode == endNode){
				RetracePath(startNode, endNode);
				return;
			}
			// for each node reffered to here as 'neighbor' from the function 
			foreach(Node neighbor in grid.GetNeighbors(currentNode)){
				if(!neighbor.walkable || closeNodes.Contains(neighbor)){
					continue; //skip the rest of this loop
				}
				
				int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
				if(newMovementCostToNeighbor < neighbor.gCost || !openNodes.Contains(neighbor)){
					neighbor.gCost = newMovementCostToNeighbor;
					neighbor.hCost = GetDistance(neighbor, endNode);
					neighbor.parent = currentNode;
					
					if(!openNodes.Contains(neighbor)){
						openNodes.Add(neighbor);
					}
				}
			}
		}
	}
	
	void RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while(currentNode != startNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		
		grid.path = path;
	}
	
	int GetDistance(Node nodeA, Node nodeB){
		int distanceX = Mathf.Abs(nodeA.x - nodeB.x);
		int distanceY = Mathf.Abs(nodeA.y - nodeB.y);
		
		if (distanceX > distanceY){
			return 14 * distanceY + 10 * (distanceX - distanceY);
		}//else...
		return 14 * distanceX + 10 * (distanceY - distanceX);
		//what happens if they are ==?
	}
	
}
