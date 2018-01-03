using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	Animator anim;
	public float inputX;
	public float inputY;
	public int attackerCount = 0; //keeps track of how many enemies can target this object at a time
	
	Vector3[] path;
	int targetIndex;
	
	
	GameObject lastKnownTarget = null;	
	IEnumerator followingEntity;
	IEnumerator followingPath;
	
	Weapon equippedWeapon;
	float speed = 2f;
	
	void Start(){
		anim = GetComponent<Animator>();
		equippedWeapon = GetComponent<AttackController>().equippedWeapon.GetComponent<Weapon>();
	}
	
	void Update(){
		// Player
		if (gameObject.name == "Player"){
			MovePlayer();
		}//test
		else if(Input.GetKeyDown(KeyCode.A)){
			HasTarget(true, GameObject.Find("Player"));
		}
	}
	
	/********************************** PATHFINDING *********************************************/
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			targetIndex = 0;
			path = newPath;	
			if(followingPath != null){
				StopCoroutine(followingPath);
			}
			followingPath = FollowPath(); 
			StartCoroutine(followingPath); 
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
		GameObject targetEntity;
		
		if (hit.collider != null) {
			targetEntity = hit.collider.gameObject;
			if(targetEntity.tag == "Entity"){
				if(lastKnownTarget != targetEntity){ //prevent double-click attacks
					targetEntity = hit.collider.gameObject;
					HasTarget(true, targetEntity);
				}
			}else{
				HasTarget(false);
				PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
			}
		}else{
			HasTarget(false);
			PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
		}
	}
	
	
	/***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/
	
	// Might eventually be able to export all reactions to it's own script, and then call them via variables assigned in the inspector
	// Only works on non-player characters right now
	public void ReactToDisturbance(string disturbanceType, GameObject target = null){
		if(gameObject.name != "Player"){
			if(disturbanceType == "Damage Taken" && lastKnownTarget != target){
				HasTarget(true, target);
			}
		}
	}
	
	void UnitIdle(){
		//do nothing
	}	
	
	
	/******************************************* SHARED FUNCTIONS ***************************************************/
	
	IEnumerator FollowEntity(GameObject targetEntity){
		Vector3 lastTargetPos = Vector3.one;
		
		if(targetEntity != null){
			lastTargetPos = targetEntity.transform.position;
		}	
		
		while(targetEntity){
			if(Vector3.Distance(transform.position, lastTargetPos) > equippedWeapon.range && targetEntity != null){
				lastTargetPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, lastTargetPos, OnPathFound);
				yield return new WaitForSeconds(1f); 	
			}
			if(Vector3.Distance(transform.position, lastTargetPos) < equippedWeapon.range && targetEntity != null){
				FaceDirection(transform.position, lastTargetPos);
				ToggleMovingAnimation(false);
				if(followingPath != null){
					StopCoroutine(followingPath);
				}
				lastTargetPos = targetEntity.transform.position;
				GetComponent<AttackController>().Attack(targetEntity);
				yield return new WaitForSeconds(equippedWeapon.speed);
			}
		}
	}

	void HasTarget(bool hasTarget, GameObject targetEntity = null){
		if(hasTarget){
			lastKnownTarget = targetEntity;
			if (followingEntity != null){
				StopCoroutine(followingEntity); 
			}
			followingEntity = FollowEntity(targetEntity); 
			StartCoroutine(followingEntity); 
		}else{
			if (followingEntity != null){
				StopCoroutine(followingEntity); 
			}
			lastKnownTarget = null;
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
