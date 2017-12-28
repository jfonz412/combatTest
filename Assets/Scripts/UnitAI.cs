using UnityEngine;
using System.Collections;

public class UnitAI : MonoBehaviour {
	//Animator anim;
	//public float inputX;
	//public float inputY;
	
	Vector3[] path;
	int targetIndex;
	
	GameObject targetEntity;
	Vector3 targetEntityPos; //track target's last position
	
	bool chasingEntity = false;
	bool fleeingEntity = false;
	bool isWalking = false;
	
	Weapon equippedWeapon;
	float speed = 2f;
	
	void Start(){
		//anim = GetComponent<Animator>();
		equippedWeapon = GetComponent<AttackController>().equippedWeapon.GetComponent<Weapon>();
	}
	
	void Update(){
		if(chasingEntity){
			FollowEntity();
		}else if(fleeingEntity){
			//FleeEntity();
		}else {
			UnitIdle();
		}
	}
	

	// Might eventually be able to export all reactions to it's own script, and then call them via variables assigned in the inspector
	public void ReactToDisturbance(string disturbanceType, GameObject target = null){
		if(disturbanceType == "Damage Taken"){
			Debug.Log("Don't hit me!!!");
			targetEntity = target;
			chasingEntity = true;
		}
	}
	
	void FollowEntity(){	
		if(targetEntity != null ){
			// check if target has moved
			if (Vector3.Distance(targetEntityPos, targetEntity.transform.position) > 1f) { 
				targetEntityPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
			}
			
			//should maybe use a different bool?
			if (isWalking == false){
				//if I am out of range reset the enemy target position and find a path to the target
				if (Vector3.Distance(transform.position, targetEntity.transform.position) > equippedWeapon.range){
					targetEntityPos = targetEntity.transform.position;
					PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
					Debug.Log("Moving towards target");
					//square up with target so we aren't on top of them. Might replace with collision detection in the future
				}else if (Vector3.Distance(transform.position, targetEntity.transform.position) < 0.5f){ 
					transform.position = Vector3.MoveTowards(transform.position, NeighborNode(), speed  * Time.deltaTime);; 
					Debug.Log("In range, setting up next to target at: " + NeighborNode());
					//when I am in position, attack
				}
				if(Vector3.Distance(transform.position, NeighborNode()) < 0.5){
					Debug.Log("In position, attacking");
					GetComponent<AttackController>().Attack(targetEntity);
				}
			}
		}else{
			chasingEntity = false;
		}
	}
	
	void UnitIdle(){
		//do nothing
	}
	
	
	
	Vector3 NeighborNode(){
	
		Vector3 offSet = new Vector3(0f,0f,0f);
	/*
		if (inputX > 0 && inputY > 0){
			offSet = new Vector3(-0.5f, 0f, 0f); //NE
		}else if (inputX < 0 && inputY < 0){
			offSet = new Vector3(0.5f, 0f, 0f); //SW
		}else if (inputX < 0 && inputY > 0){
			offSet = new Vector3(0f, -0.5f, 0f); //NW
		}else if (inputX > 0 && inputY < 0){
			offSet = new Vector3(0f, 0.5f, 0f); //SE
		}
	*/
		return targetEntityPos + offSet;
		
	}
	
	void SetWalkAngle(Vector2 startPos, Vector2 endPos){
		/*
		Vector2 relativePos =  endPos - startPos;
		inputX = relativePos.x;
		inputY = relativePos.y;
		
		isWalking = true;
		anim.SetBool("isWalking", true);
		anim.SetFloat("x", inputX);
		anim.SetFloat("y", inputY);
		*/
	}
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			//Debug.Log(this + " is Starting new path");
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
					isWalking = false;
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
