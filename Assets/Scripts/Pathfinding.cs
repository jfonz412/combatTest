using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour {
	//public Transform seeker, target;
	
	PathfindingManager requestManager;
	Grid grid;
	
	void Awake(){
		requestManager = GetComponent<PathfindingManager>();
		grid = GetComponent<Grid>();
	}
	
	//void Update(){
		//FindPath(seeker.position, target.position);
	//}
	
	public void StartFindPath(Vector3 startPos, Vector3 targetPos){
		StartCoroutine(FindPath(startPos, targetPos));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 endPos){
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
	
		Node startNode = grid.NodeAtWorldPosition(startPos);
		Node endNode = grid.NodeAtWorldPosition(endPos);
		
		if(startNode.walkable && endNode.walkable){
		
			Heap<Node> openNodes = new Heap<Node>(grid.MaxSize); 		// Can sort of treat this like a dynamic array...arrays cannot normally be resized
			HashSet<Node> closeNodes = new HashSet<Node>(); // HashSet is a faster list that does not preserve order
			
			openNodes.Add(startNode);
			
			while (openNodes.Count > 0){
				Node currentNode = openNodes.RemoveFirst();
				closeNodes.Add (currentNode);
				
				if (currentNode == endNode){
					pathSuccess = true;
					
					break;
				}
				// for each node reffered to here as 'neighbor' from the function 
				foreach(Node neighbor in grid.GetNeighbors(currentNode)){
					if(!neighbor.walkable || closeNodes.Contains(neighbor)){
						continue; //skip the rest of this loop
					}
					
					// This is where we set the gCost and hCost
					int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
					if(newMovementCostToNeighbor < neighbor.gCost || !openNodes.Contains(neighbor)){
						neighbor.gCost = newMovementCostToNeighbor;
						neighbor.hCost = GetDistance(neighbor, endNode);
						neighbor.parent = currentNode;
						
						if(!openNodes.Contains(neighbor)){
							openNodes.Add(neighbor);
						}else{
							openNodes.UpdateItem(neighbor);
						}	
					}
				}
			}
		}
		yield return null;
		if(pathSuccess){
			waypoints = RetracePath(startNode, endNode);
		}
		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	}
	
	Vector3[] RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while(currentNode != startNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}
	
	Vector3[] SimplifyPath(List<Node> path){
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i<path.Count; i++){
			Vector2 directionNew = new Vector2(path[i-1].x - path[i].x, path[i-1].y - path[i].y);
			if(directionNew != directionOld){
				waypoints.Add(path[i].worldPos);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
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
