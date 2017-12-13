using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	Animator anim;
	Vector3[] path;
	int targetIndex;
	
	GameObject targetEntity;
	Vector3 targetEntityPos; //track target's last position
	bool chasingEntity = false;
	
	//for blend tree params
	public float inputX;
	public float inputY;
	
	Weapon equippedWeapon;
	float speed = 2f;
		
	void Start(){
		anim = GetComponent<Animator>();
		equippedWeapon = GetComponent<AttackController>().equippedWeapon.GetComponent<Weapon>();
		Debug.Log("Weapon Range is: " + equippedWeapon.range);
	}
	
	void Update(){
		MovePlayer();
		
		if (chasingEntity){
			FollowEntity();
		}
	}
	
	
	void MovePlayer(){
		Vector3 targetPos;
		
		if (Input.GetMouseButtonDown(0)) {
			targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPos.z = transform.position.z; //to always keep z at 0
			ProcessClick(targetPos);			
		}
	}
	
	void ProcessClick(Vector3 targetPos){
		RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero); //Vector2.zero == (0,0)
		
		if (hit.collider != null) {
			targetEntity = hit.collider.gameObject;
			Debug.Log("Clicked a collider");
			// move player towards enemy
			if(targetEntity.gameObject.GetComponent<Enemy>()){ //or anything other than this gameObject
				chasingEntity = true;
				targetEntityPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
				Debug.Log("The collider was an enemy");
			}else{
				chasingEntity = false;
				PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
				Debug.Log("The collider was NOT an enemy");
			}
		}else{
			chasingEntity = false;
			PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
			Debug.Log("Did not click a collider");
		}
	
	}
	
	
	void FollowEntity(){	
		//check if target has moved since we last saved it's pos
		if(targetEntity != null ){
		
		/*
			(((will run every frame)))
			if target has moved
				request a new path
			else
				don't and continue on until path is finished
			
			if path is finished
				if I'm not in weapon range
					close the gap 
				else
					attack
		*/
			if (Vector3.Distance(targetEntityPos, targetEntity.transform.position) > 1f) { 
				targetEntityPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
			}
			
			//should probably use a different bool
			if (anim.GetBool("isWalking") == false){
				if (Vector3.Distance(transform.position, targetEntity.transform.position) > equippedWeapon.range){
					targetEntityPos = targetEntity.transform.position;
					Debug.Log ("Distance is " + Vector3.Distance(transform.position, targetEntity.transform.position));
					PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound); //something is still stopping me from going that extra bit
				}else{
					Debug.Log ("Distance is WHILE ATTCKING " + Vector3.Distance(transform.position, targetEntity.transform.position));
					GetComponent<AttackController>().Attack(targetEntity);
				}
			}
		}
	}
	
	
	// not setting proper x and y values to get correct walking direction
	void SetWalkAngle(Vector2 startPos, Vector2 endPos){
		Vector2 relativePos =  endPos - startPos;
		inputX = relativePos.x;
		inputY = relativePos.y;
		
		anim.SetBool("isWalking", true);
		anim.SetFloat("x", inputX);
		anim.SetFloat("y", inputY);
	}
	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful){
		if(pathSuccessful){
			Debug.Log(this + " is Starting new path");
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
