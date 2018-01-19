using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	[HideInInspector]
	public Transform lastKnownTarget = null;
	
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
			HasTarget(true, GameObject.Find("Player").transform);
		}
	}
	
	//How the player object talks to this script
	public void HasTarget(bool hasTarget, Transform targetTransform = null){
		if(hasTarget){
			lastKnownTarget = targetTransform;
			if (followingEntity != null){
				StopCoroutine(followingEntity); 
			}
			followingEntity = FollowEntity(targetTransform); 
			StartCoroutine(followingEntity); 
		}else{
			if (followingEntity != null){
				StopCoroutine(followingEntity); 
			}
			lastKnownTarget = null;
		}
	}
	
	public void StopMoving(){
        //movementSpeed = 0;
        if (followingPath != null){
			StopCoroutine(followingPath);
			anim.ToggleMovingAnimation(false);
		}else{
			StopCoroutine("FollowPath");
			anim.ToggleMovingAnimation(false);
		}
	}

	
	/******************************************* PRIVATE FUNCTIONS ***************************************************/
	
	IEnumerator FollowEntity(Transform targetTransform){
        while (targetTransform){
			if(Vector3.Distance(transform.position, lastKnownTarget.position) > equippedWeapon.range && targetTransform != null){
                lastKnownTarget = targetTransform;
                PathfindingManager.RequestPath(transform.position, lastKnownTarget.position, OnPathFound);

                //was originally one second, but it took units too long to register they were in range and to stop moving 
                //however if the unit is moved out of range while attacking, this may cause the unit to attack over and over each time it's in range
                //a bool checking to see if the unit's attack is off cooldown may solve this issue
                //could also be an animation issue?
                yield return new WaitForSeconds(.1f); 
			}
            if (Vector3.Distance(transform.position, lastKnownTarget.position) < equippedWeapon.range && targetTransform != null)
            {
                StopAndAttack(targetTransform);
                lastKnownTarget = targetTransform;
				yield return new WaitForSeconds(equippedWeapon.speed +  Random.Range(0.0f, 0.2f));
			}
		}
	}

    void StopAndAttack(Transform targetTransform){
		StopMoving();
		anim.FaceDirection(transform.position, targetTransform.position);
		anim.TriggerAttackAnimation(equippedWeapon.attackType);
		equippedWeapon.Attack(transform, targetTransform);
	}
	
	/********************************** PATHFINDING *********************************************/
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
            movementSpeed = 2f;
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

