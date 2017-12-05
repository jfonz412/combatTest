using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	float speed = 1f;
	Vector3[] path;
	int targetIndex;
	Animator anim;
		
	void Start(){
		anim = GetComponent<Animator>();
	}
	
	void Update(){
		MovePlayer();
	}
	
	
	void MovePlayer(){
		Vector3 targetPos;
		
		if (Input.GetMouseButtonDown(0)) {
			targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPos.z = transform.position.z; //to always keep z at 0		
			PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
		}
	}
	
	
	// not setting proper x and y values to get correct walking direction
	void SetWalkAngle(Vector2 startPos, Vector2 endPos){
		Vector2 relativePos =  endPos - startPos;
		float inputX = relativePos.x;
		float inputY = relativePos.y;
		
		anim.SetBool("isWalking", true);
		anim.SetFloat("x", inputX);
		anim.SetFloat("y", inputY);
	}
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			targetIndex = 0;
			path = newPath;
			StopCoroutine("FollowPath"); 
			StartCoroutine("FollowPath"); 
		}
	}
	
	IEnumerator FollowPath(){
		Vector3 currentWaypoint = path[0];
		SetWalkAngle(transform.position, currentWaypoint);
		while(true){
			if(transform.position == currentWaypoint){
				targetIndex++;
				if(targetIndex >= path.Length){
					path = null;
					targetIndex = 0;
					anim.SetBool("isWalking", false);
					yield break;
				}
				currentWaypoint = path[targetIndex];
				SetWalkAngle(transform.position, currentWaypoint);
			}
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed  * Time.deltaTime);
			yield return null;
		}
	}
	
	public void OnDrawGizmos(){
		if(path != null){
			for(int i = targetIndex; i < path.Length; i++){
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one/2);
				
				if(i == targetIndex){
					Gizmos.DrawLine(transform.position, path[i]);
				}else{
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
