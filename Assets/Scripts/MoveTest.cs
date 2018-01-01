using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

	Animator anim;
	public float inputX;
	public float inputY;
	
	Vector3[] path;
	int targetIndex;
	
	GameObject targetEntity;
	Vector3 lastTargetPos; //track target's last position
	
	bool chasingEntity = false;
	bool fleeingEntity = false;
	
	//Weapon equippedWeapon;
	float speed = 2f;
	
	void Start(){
		anim = GetComponent<Animator>();
		//equippedWeapon = GetComponent<AttackController>().equippedWeapon.GetComponent<Weapon>();
	}
	
	void Update(){
		if (gameObject.name == "Player"){
			MovePlayer();
			if (chasingEntity){
				//FollowEntity();
			}
		}else{
			if(chasingEntity){
				//FollowEntity();
			}else if(fleeingEntity){
				//FleeEntity();
			}else {
				UnitIdle();
			}
		}
	}
	
	
	/********************************** PATHFINDING *********************************************/
	
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
		FaceDirection(transform.position, currentWaypoint);
		ToggleMovingAnimation(true);
		while(true){
			if(transform.position == currentWaypoint){
				targetIndex++;
				if(targetIndex >= path.Length){
					path = null;
					targetIndex = 0;
					ToggleMovingAnimation(false);
					yield break;
				}
				currentWaypoint = path[targetIndex];
				FaceDirection(transform.position, currentWaypoint);
			}
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed  * Time.deltaTime);
			yield return null;
		}
	}
	
	/*** --------------------------------------------PLAYER FUNCTIONS------------------------------------------------------- ***/
	
	void MovePlayer(){
		if (Input.GetMouseButtonDown(0)) {
			Vector3 targetPos;
			targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPos.z = transform.position.z; //to always keep z at 0
			ProcessClick(targetPos);			
		}
	}
	
	void ProcessClick(Vector3 targetPos){
		RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero); //Vector2.zero == (0,0)
		
		if (hit.collider != null) {
			targetEntity = hit.collider.gameObject;
			if(targetEntity.tag == "Entity"){
				chasingEntity = true;
				lastTargetPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, lastTargetPos, OnPathFound);
			}else{
				chasingEntity = false;
				PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
			}
		}else{
			chasingEntity = false;
			PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
		}
		
	}
	
	/***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/
	
	// Might eventually be able to export all reactions to it's own script, and then call them via variables assigned in the inspector
	// Only works on non-player characters right now
	public void ReactToDisturbance(string disturbanceType, GameObject target = null){
		if(gameObject.name != "Player"){
			if(disturbanceType == "Damage Taken"){
				targetEntity = target;
				chasingEntity = true;
			}
		}
	}
	
	void UnitIdle(){
		//do nothing
	}	
	
	
	/******************************************* HELPER FUNCTIONS ***************************************************/
	
	void FollowEntity(){
		if(targetEntity != null ){
			PathfindingManager.RequestPath(transform.position, lastTargetPos, OnPathFound);
		}else{
			chasingEntity = false;
		}
	}
	
	void FaceDirection(Vector2 startPos, Vector2 endPos){	
		Vector2 relativePos =  endPos - startPos;
		inputX = relativePos.x;
		inputY = relativePos.y;
		
		anim.SetFloat("x", inputX);
		anim.SetFloat("y", inputY);		
	}
	
	void ToggleMovingAnimation(bool isMoving){
		if(isMoving){
			anim.SetBool("isWalking", true);
		}
		else{
			anim.SetBool("isWalking", false);
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
