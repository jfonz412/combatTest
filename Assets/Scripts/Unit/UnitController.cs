using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	UnitAnimator anim;
	
	IEnumerator followingPath;
	Vector3[] path;
	int targetIndex;

    float movementSpeed = 2f; //standard for npcs, make this public?


    void Start(){
		anim = GetComponent<UnitAnimator>();
	}
	
	void Update(){
		//FOR TESTING!!!
		if(gameObject.name != "Player" && Input.GetKeyDown(KeyCode.A)){ //and not dead or otherwise incapacitated...
            GetComponent<AttackController>().EngageTarget(true, GameObject.Find("Player").transform);
		}
	}


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

    //used by other scripts to stop the pathfinding on command
    public void StopMoving()
    {
        //movementSpeed = 0;
        if (followingPath != null)
        {
            StopCoroutine(followingPath);
            anim.ToggleMovingAnimation(false);
        }
        else
        {
            StopCoroutine("FollowPath");
            anim.ToggleMovingAnimation(false);
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

