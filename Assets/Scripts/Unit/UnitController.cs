using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	[HideInInspector]
	public GameObject lastKnownTarget = null;
	
	UnitAnimator anim;
	float inputX;
	float inputY;	
	
	Weapon equippedWeapon;
	float movementSpeed = 2f;
	
	IEnumerator followingEntity;
	IEnumerator followingPath;
	Vector3[] path;
	int targetIndex;
	
	
	void Start(){
		anim = GetComponent<UnitAnimator>();
		equippedWeapon = anim.loadedWeapon.GetComponent<Weapon>();
	}
	
	void Update(){
		//FOR TESTING!!!
		if(gameObject.name != "Player" && Input.GetKeyDown(KeyCode.A)){
			HasTarget(true, GameObject.Find("Player"));
		}
	}
	
	//How the player object talks to this script
	public void HasTarget(bool hasTarget, GameObject targetEntity = null){
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
	

	
	/******************************************* PRIVATE FUNCTIONS ***************************************************/
	
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
				StopAndAttack(targetEntity);
				lastTargetPos = targetEntity.transform.position;
				yield return new WaitForSeconds(equippedWeapon.speed +  Random.Range(0.0f, 0.2f));
			}
		}
	}
	
	void StopAndAttack(GameObject targetEntity){
		if(followingPath != null){
			StopCoroutine(followingPath);
			anim.ToggleMovingAnimation(false);
		}else{
			StopCoroutine("FollowPath");
			anim.ToggleMovingAnimation(false);
		}
		anim.FaceDirection(transform.position, targetEntity.transform.position);
		anim.TriggerAttackAnimation(); //pass in equippedWeapon.attackType 
		equippedWeapon.Attack(gameObject, targetEntity);
	}

	
	/********************************** PATHFINDING *********************************************/
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			targetIndex = 0;
			path = newPath;	
			if(followingPath != null){
				StopCoroutine(followingPath);
				followingPath = FollowPath();
				StartCoroutine(followingPath); 
			}else{
				StopCoroutine("FollowPath");
				followingPath = FollowPath();
				StartCoroutine(followingPath); 
			}
		}
	}
	
	IEnumerator FollowPath(){
		Vector3 currentWaypoint = path[0];
		anim.FaceDirection(transform.position, currentWaypoint);
		anim.ToggleMovingAnimation(true);
		while(true){
			if(transform.position == currentWaypoint){
				targetIndex++;
				if(targetIndex >= path.Length){
					path = null;
					targetIndex = 0;
					anim.ToggleMovingAnimation(false);
					yield break;
				}
				currentWaypoint = path[targetIndex];
				anim.FaceDirection(transform.position, currentWaypoint);
			}
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, movementSpeed  * Time.deltaTime);
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
