using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {
	public GameObject equippedWeapon;
	GameObject weaponParent;
	GameObject myWeaponInstance; 
	
	//[HideInInspector]
	//public float inputX, inputY;
	UnitAnimator anim;
	
	//Vector3 mousePos;
	//Vector2 startPos;
	
	// Get these from stats? or Stats --> Unit? Probably just stats, stats can take care of the calculations
	//float attack;
	//float defense;

	void Start () {
		anim = GetComponent<UnitAnimator>();
		if (equippedWeapon){
			SpawnWeapon();
		}
	}
	
	public void Attack(GameObject target){
		anim.TriggerAttackAnimation();
		myWeaponInstance.GetComponent<Weapon>().Attack(gameObject, target);
	/*
		if (!coolingDown) {
			anim.SetTrigger("attackTrigger");
			myWeaponInstance.GetComponent<Weapon>().Attack(gameObject, target);
			coolingDown = true;
			timer = 1.5f;
		} 
		else {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				coolingDown = false;
			}
		}
	*/
	}
	
	void SpawnWeapon(){
		weaponParent = GameObject.Find("Weapons");
		if(!weaponParent){
			weaponParent = new GameObject("Weapons");
		}		
		myWeaponInstance = Instantiate(equippedWeapon) as GameObject;
		myWeaponInstance.transform.SetParent(weaponParent.transform);
	}
}

