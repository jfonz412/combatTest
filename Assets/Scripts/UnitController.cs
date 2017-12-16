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
			//Debug.Log("Clicked a collider");
			// move player towards enemy
			if(targetEntity.gameObject.GetComponent<Enemy>()){ //or anything other than this gameObject
				chasingEntity = true;
				targetEntityPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
				//Debug.Log("The collider was an enemy");
			}else{
				chasingEntity = false;
				PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
				//Debug.Log("The collider was NOT an enemy");
			}
		}else{
			chasingEntity = false;
			PathfindingManager.RequestPath(transform.position, targetPos, OnPathFound);
			//Debug.Log("Did not click a collider");
		}
	
	}
	
	
	void FollowEntity(){	
		if(targetEntity != null ){
			// after x > 6.5 the glitch is triggered w/ nodes set to 0.2
			//when approaching from east to target past 6.5, but when that next path is set to the same/next node he turns around
			
			//player does not go all the way to target node in general, stops a node before
			//neither do dummies with transform targets (how the pathfinding is set up in the video)
			//so do the gizmos
			
			//I suspect this is because the end node is at some point getting lost, so the next best node is used as the last node
			//try logging the actual coordinate of the last node in the Path
			//we are definintly losing the last node, it works when we are only moving one node though
			//try just failing the path if 0 nodes also, it's cleaner
			if (Vector3.Distance(targetEntityPos, targetEntity.transform.position) > 1f) { 
				targetEntityPos = targetEntity.transform.position;
				PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound);
			}
			
			//should probably use a different bool
			if (anim.GetBool("isWalking") == false){
				if (Vector3.Distance(transform.position, targetEntity.transform.position) > equippedWeapon.range){
					targetEntityPos = targetEntity.transform.position;
					//Debug.Log ("CHASING Distance is: " + Vector3.Distance(transform.position, targetEntity.transform.position));
					PathfindingManager.RequestPath(transform.position, targetEntityPos, OnPathFound); //something is still stopping me from going that extra bit
				}else{
					//Debug.Log ("ATTACKING Distance is: " + Vector3.Distance(transform.position, targetEntity.transform.position));
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
