using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {
/*
	void OldMovePlayer(){
		if (Input.GetKey(KeyCode.S)){
			direction = "walk south";
			stopDirection = "idleSouth";
			attackDirection = "attackSouth";
			anim.Play (direction);
			transform.position += new Vector3(0f,-speed * Time.deltaTime,0f);;
		}else if (Input.GetKey(KeyCode.W)){
			direction  = "walking north";
			stopDirection = "idleNorth";
			attackDirection = "attackNorth";
			anim.Play (direction);
			transform.position += new Vector3(0f,speed * Time.deltaTime,0f);
		}else if (Input.GetKey(KeyCode.A)){
			direction  = "walking west";
			stopDirection = "idleWest";
			attackDirection = "attackWest";
			anim.Play (direction);
			transform.position += new Vector3(-speed * Time.deltaTime,0f,0f);
		}else if (Input.GetKey(KeyCode.D)){
			direction = "walking east";
			stopDirection = "idleEast";
			attackDirection = "attackEast";
			anim.Play (direction);
			transform.position += new Vector3(speed * Time.deltaTime,0f,0f);
		}else{
			anim.Play (stopDirection);
		}
	}
*/ 

/*
	// this is my version of PlayerController.AngleBetweenVector2
	float GetAngle(Vector2 start, Vector2 finish){
		// SOH CAH TOA
		Vector2 relativePos =  finish - start;
		float adjacent = relativePos.x;
		float opposite = relativePos.y;
		float oppositeOverAdjacent   = opposite / adjacent;
		return Mathf.Atan(oppositeOverAdjacent) * 180 / Mathf.PI;
	}
	
	void SetAnimationDirectionsOLD(float movementAngle){
		if (movementAngle >= 22.5f && movementAngle < 157.5f){
			stopDirection = "idleNorth";
			attackDirection = "attackNorth";
			walkDirection  = "walkNorth";
		} else if (movementAngle >= 157.5f && movementAngle <= 180 || movementAngle <= -157.5f && movementAngle >= -180){ 
			stopDirection = "idleWest";
			attackDirection = "attackWest";
			walkDirection  = "walkWest";
		} else if (movementAngle <= -22.5f && movementAngle > -157.5f){ 
			stopDirection = "idleSouth";
			attackDirection = "attackSouth";
			walkDirection = "walkSouth";
		} else {
			stopDirection = "idleEast";
			attackDirection = "attackEast";
			walkDirection = "walkEast"; 
		}
	}
	
	
	float AngleBetweenVector2(Vector2 start, Vector2 end)
	{
		Vector2 difference = end - start;
		float sign = (end.y < start.y)? -1.0f : 1.0f;
		return Vector2.Angle(Vector2.right, difference) * sign;
	}
*/	
}
