using UnityEngine;
using System.Collections;

public class UnitAnimator : MonoBehaviour {
	public Animator[] animators;
	public GameObject unitBody;
	
	public int torsoID; //this is what determines which armor we are choosing to equip. Can also use this to dictate stats (like an armor ID)
	public int LegID;
	public int weaponID; //consider the weaponID indicating which set of animations should play

    [HideInInspector]
	public GameObject loadedTorsoArmor;
    [HideInInspector]
    public GameObject loadedLegArmor;
    [HideInInspector]
    public GameObject loadedWeapon;
	
	float inputX = 0f;
	float inputY = -1f;
	
	// Use this for initialization
	void Start () {
		animators = new Animator[4];
		//Get Character Animator
		animators [0] = GetComponent<Animator> ();
		//Load Equipment
		loadWeapon();     //1
		loadTorsoArmor(); //2
		loadLegArmor();   //3
	}
	
	
	void loadWeapon(){ //should take an into for the equipment ID
		if (loadedWeapon != null){
			Destroy (loadedWeapon.gameObject);
			loadedWeapon = null;
		}
		
		if (weaponID == 0){
			loadedWeapon = Instantiate (Resources.Load ("Weapons/unarmed"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}else if (weaponID == 1){
			loadedWeapon = Instantiate (Resources.Load ("Weapons/Iron Dagger"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}else if (weaponID == 2){
			loadedWeapon = Instantiate (Resources.Load ("Weapons/Iron Spear"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}
		
		loadedWeapon.transform.SetParent (unitBody.transform);
		loadedWeapon.transform.localPosition = new Vector3 (0,0,0);
		
		animators [1] = loadedWeapon.GetComponent<Animator>();
		ResetAnimators();	
	}
	
	
	void loadTorsoArmor(){
		//Get rid of old armor if there is any
		if (loadedTorsoArmor != null){
			//PutItemBackInInventory(loadedTorsoArmor);
			Destroy (loadedTorsoArmor.gameObject);
			loadedTorsoArmor = null;
		}
		
		if (torsoID == 0){
			loadedTorsoArmor = Instantiate (Resources.Load ("Armor/naked"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}else if (torsoID == 1){
			loadedTorsoArmor = Instantiate (Resources.Load ("Armor/PlateIronTorso"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}
		
		//Attach the armor
		loadedTorsoArmor.transform.SetParent (unitBody.transform);
		
		//Make sure armor is centered and right scale
		loadedTorsoArmor.transform.localPosition = new Vector3 (0,0,0);
		
		//Grab the animator for the loaded armor
		animators [2] = loadedTorsoArmor.GetComponent<Animator> ();
		
		ResetAnimators();
	}
	
	void loadLegArmor(){
		//Get rid of old armor if there is any
		if (loadedLegArmor != null){
			//PutItemBackInInventory(loadedTorsoArmor);
			Destroy (loadedLegArmor.gameObject);
			loadedLegArmor = null;
		}
		
		if (LegID == 0){
			loadedLegArmor = Instantiate (Resources.Load ("Armor/naked"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}else if (LegID == 1){
			loadedLegArmor = Instantiate (Resources.Load ("Armor/PlateIronLegs"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}
		
		//Attach the armor
		loadedLegArmor.transform.SetParent (unitBody.transform);
		
		//Make sure armor is centered and right scale
		loadedLegArmor.transform.localPosition = new Vector3 (0,0,0);
		
		//Grab the animator for the loaded armor
		animators [3] = loadedLegArmor.GetComponent<Animator> ();
		
		ResetAnimators();
	}
	
	
	/************************* PUBLIC FUNCTIONS ****************************************/
	
	public void FaceDirection(Vector2 startPos, Vector2 endPos){	
		Vector2 relativePos =  endPos - startPos;
		inputX = relativePos.x;
		inputY = relativePos.y;
		
		//set all animators 
		for (int i = 0; i < animators.Length; i++){
			if (animators [i] != null){
				animators [i].SetFloat ("x", inputX);
				animators [i].SetFloat ("y", inputY);
			}
		}	
	}
	
	public void ToggleMovingAnimation(bool isMoving){
		if(isMoving){
			for (int i = 0; i < animators.Length; i++){
				if (animators [i] != null){
					animators [i].SetBool("isWalking", true);
				}
			}
		}
		else{
			for (int i = 0; i < animators.Length; i++){ 
				if (animators [i] != null){
					animators [i].SetBool("isWalking", false);
				}
			}
		}
	}
	
	public void TriggerAttackAnimation(string attackType){
		if(attackType == "slash"){
			for (int i = 0; i < animators.Length; i++){
				if (animators [i] != null){
					animators [i].SetFloat ("x", inputX);
					animators [i].SetFloat ("y", inputY);
					animators [i].SetTrigger("isSlashing"); 
				}
			}
		}else if (attackType == "thrust"){
			for (int i = 0; i < animators.Length; i++){
				if (animators [i] != null){
					animators [i].SetFloat ("x", inputX);
					animators [i].SetFloat ("y", inputY);
					animators [i].SetTrigger("isThrusting"); 
				}
			}
		}
	}
	
	public void TriggerDeathAnimation(){ //unless i = 1 which is weapon
		for (int i = 0; i < animators.Length; i++){
			if (animators [i] != null && i != 1){
				animators [i].SetTrigger("deathAnimation");
			}
		}
	}
	
	void ResetAnimators(){
		//might need to reset bools too
		for (int i = 0; i < animators.Length; i++){
			if (animators [i] != null){
				animators [i].SetFloat ("x", inputX);
				animators [i].SetFloat ("y", inputY);
			}
		}
	}
}
