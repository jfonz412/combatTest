using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	//https://www.youtube.com/watch?v=nhiFx28e7JY&t=1105s
	// Modified to work for 2D, could use further modification (Vector3 -> Vector2)
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize; //wire box
	public float nodeRadius;
	public bool displayGridGizmos;
	Node[,] grid; //2D array of nodes
	
	float nodeDiameter;
	int nodeCountX, nodeCountY; // # of nodes on x and y axis

    public static Grid instance;

    void Awake(){
		nodeDiameter = nodeRadius*2;
		// gets us how many nodes we can fit into grid's x and y
		nodeCountX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); //rounding to Int prevents partial-nodes
		nodeCountY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();

        instance = this;
    }
	
	public int MaxSize{
		get {
			return nodeCountX * nodeCountY;
		}
	}
	
	void CreateGrid(){
		grid = new Node[nodeCountX, nodeCountY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2; //see work note for math
		
		for (int x  = 0; x < nodeCountX; x++) {
			for (int y  = 0; y < nodeCountY; y++) {
				//x or y * nodeDiameter will increment our nodes by one node per iteration across the game world/grid
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask)); //checks for unwalkableMask layer at worldPoint of node
				grid[x,y] = new Node(walkable, worldPoint, x, y); //populate coordinate of x,y with our location and walkability
			}
		}
	}
	
	public List<Node> GetNeighbors(Node node){
		List<Node> neighbors = new List<Node>();
		
		//searches in a 3x3 block (-1, 0, 1)
		for (int x = -1; x <= 1; x++){
			for (int y = -1; y <= 1; y++){
				if (x == 0 && y == 0){
					continue;
				}
				int checkX = node.x + x; //adds -1, 0, 1 to get at nodes next to this one
				int checkY = node.y + y;
				
				// make sure these coordinates aren't outside of our grid
				// grid will never be less than 0 or more than the node count on each axis
				if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY){
					neighbors.Add (grid[checkX,checkY]);
				}
			}
		}
		
		return neighbors;
	}
	
	//find the node we are on based on the worldPos passed in
	public Node NodeAtWorldPosition(Vector3 worldPosition){
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;  // 0  +  (15/2)  /  15  =  0.5 (half grid size)
		float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
		
		//clamp between 0-1 just to make sure if we are outside of grid we don't get error
		percentX = Mathf.Clamp01(percentX); 
		percentY = Mathf.Clamp01(percentY);
		 
		int x = Mathf.RoundToInt((nodeCountX-1)*percentX); //number of nodes on x axis - 1 * 0.5 (in above case)
		int y = Mathf.RoundToInt((nodeCountY-1)*percentY);
		return grid[x,y];
	}
	
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
		if (grid != null && displayGridGizmos){
			//Node playerNode = NodeAtWorldPosition(player.position); //get player's node position thru it's world pos
			foreach(Node n in grid){
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}
