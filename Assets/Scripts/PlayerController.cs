using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Camera myCamera;
	public GameObject equippedWeapon;
	GameObject myWeapon;
	
	[HideInInspector]
	GameObject target;
	[HideInInspector]
	public float inputX, inputY;
	
	Animator anim;
	GameObject weaponParent;
	
	Vector3 mousePos;
	Vector2 startPos;
	
	float speed;
	//float attack;
	//float defense;

	float timer = 1.5f; //replace with weapon speed
	bool coolingDown = false;
	
	
	void Start () {
		anim = GetComponent<Animator>();
		speed = GetComponent<Stats>().speed; //will eventually move to CalculateStats()
		
		if (equippedWeapon){
			SpawnWeapon();
		}
	}
	
	void Update () {
		//MovePlayer();
		if (target){
			//ChaseTarget();
		}
		SetAnimationDirections();
	}
	

	/*****MOVING*****/
	
	void MovePlayer(){
		if (Input.GetMouseButtonDown(0)) {
			startPos = transform.position;
			mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition)); // PASS THIS TO PATHFINDING SCRIPT
			mousePos.z = transform.position.z; //to always keep z at 0		
			ProccessClick(); //must be called after mousePos is set
		}
		
		if(!target){											
			transform.position = Vector3.MoveTowards(transform.position,  mousePos   , speed * Time.deltaTime);
		}
	}
	
	
// JUST FOCUS ON ABOVE FOR NOW
	
	
	void ChaseTarget(){
		Vector3 targetPos = target.transform.position;
		targetPos.z = 0f;

		float weaponRange = equippedWeapon.GetComponent<Weapon>().range;
		
		if (Vector3.Distance(transform.position, targetPos) > weaponRange){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		}else {
			PrepareToAttack();
		}
	}
	
	void ProccessClick(){
		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero); //Vector2.zero = (0,0)
		
		if (hit.collider != null) {
			GameObject entity = hit.collider.gameObject;
			// move player towards enemy
			if(entity.gameObject.GetComponent<Enemy>()){ //or anything other than this gameObject
				target = entity;
			}else{
				Debug.Log(entity);
			}
		}else{
			target = null;
		}
	}
	
	void SetAnimationDirections(){
		bool isWalking;
		
		//differentiate between mousePos and target if there is a target
		if (target){
			Vector2 relativePos = GetAngle(startPos, target.transform.position);
			inputX = relativePos.x;
			inputY = relativePos.y;
			
			isWalking = transform.position != target.transform.position;
			anim.SetBool("isWalking", isWalking);
		}else {
			Vector2 relativePos = GetAngle(startPos, mousePos);
			inputX = relativePos.x;
			inputY = relativePos.y;
			
			isWalking = transform.position != mousePos;
			anim.SetBool("isWalking", isWalking);
		}
		
		if(isWalking){
			anim.SetFloat("x", inputX);
			anim.SetFloat("y", inputY);
		}
	}
	
	
	Vector2 GetAngle(Vector2 start, Vector2 finish){
		// This method allows us to turn mousePos into a Vector2 it seems, so I'm keeping it
		Vector2 relativePos =  finish - start;
		return relativePos;
	}
	
	
	
	void PrepareToAttack(){
		if (!coolingDown) {
			anim.SetTrigger("attackTrigger");
			myWeapon.GetComponent<Weapon>().Attack(gameObject, target);
			coolingDown = true;
			timer = 1.5f;
		} 
		else {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				coolingDown = false;
			}
		}
	}
	
	void SpawnWeapon(){
		weaponParent = GameObject.Find("Weapons");
		if(!weaponParent){
			weaponParent = new GameObject("Weapons");
		}		
		myWeapon = Instantiate(equippedWeapon) as GameObject;
		myWeapon.transform.SetParent(weaponParent.transform);
	}
}

