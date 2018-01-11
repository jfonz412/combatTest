using UnityEngine;
using System.Collections;

public class UnitAnimator : MonoBehaviour {
	public int armorState; //this is what determines which armor we are choosing to equip. Can also use this to dictate stats (like an armor ID)
	public int weaponState;
	public Animator[] animators;
	public GameObject loadedArmor;
	public GameObject armorHolder;
	public GameObject loadedWeapon;
	public GameObject weaponHolder;
	
	float inputX = 0f;
	float inputY = -1f;
	
	// Use this for initialization
	void Start () {
		animators = new Animator[3];
		//Get Character Animator
		animators [0] = GetComponent<Animator> ();
		//Load Armor Set
		loadArmorSet();
		loadWeapon();
	}
	
	
	void loadArmorSet(){
		//Get rid of old armor if there is any
		if (loadedArmor != null){
			// If we were going to be doing alot of armor changes we may want to disable the current armor and pool it.
			// If this like an RPG where the player will only be changing armor on upgrades then destory is fine.
			// Because we likely wont be recalling the old armor.
			Destroy (loadedArmor.gameObject);
			loadedArmor = null;
		}
		
		if (armorState == 0){
			//We are doing a resource load instead of having all of the armor on the player but disabled incase there are several 
			//Armor sets so we wont have to worry about loading for exaomple 10 armors all at once.
			//Resources require a special folder, prefabs in that folder, and string dependant loading. 
			//if the armor is in sub folders you will need to path down to them for example.
			// "/armor/AurthorSilverCostume"
			loadedArmor = Instantiate (Resources.Load ("PlateIronTorso"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}
		
		/*
		if (armorState == 1){
			loadedArmor = Instantiate (Resources.Load ("LeatherTorso"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}
		*/
		
		//Attach the armor
		loadedArmor.transform.SetParent (armorHolder.transform);
		
		//Make sure armor is centered and right scale
		loadedArmor.transform.localPosition = new Vector3 (0,0,0);
		//loadedArmor.transform.localScale = new Vector3 (1, 1, 1);
		
		//Grab the animator for the loaded armor
		animators [1] = loadedArmor.GetComponent<Animator> ();
		
		//Now we need to sync animators
		//the best way to do that is to quickly set their animation to idle
		//might need to reset bools too
		
		for (int i = 0; i < animators.Length; i++){
			if (animators [i] != null){
				animators [i].SetFloat ("x", inputX);
				animators [i].SetFloat ("y", inputY);
			}
		}
		
	}
	
	void loadWeapon(){
		if (loadedWeapon != null){
			Destroy (loadedWeapon.gameObject);
			loadedWeapon = null;
		}
		
		if (weaponState == 0){
			loadedWeapon = Instantiate (Resources.Load ("Iron Dagger"), new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		}

		loadedWeapon.transform.SetParent (armorHolder.transform);
		loadedWeapon.transform.localPosition = new Vector3 (0,0,0);

		animators [2] = loadedWeapon.GetComponent<Animator> ();	
	}
	
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
			for (int i = 0; i < animators.Length - 1; i++){ 
				if (animators [i] != null){
					animators [i].SetBool("isWalking", false);
				}
			}
		}
	}
	
	//not triggering player's attack animation
	public void TriggerAttackAnimation(){
		for (int i = 0; i < animators.Length; i++){
			if (animators [i] != null){
				animators [i].SetFloat ("x", inputX);
				animators [i].SetFloat ("y", inputY);
				animators [i].SetTrigger("isAttacking");
			}
		}
	}
}
