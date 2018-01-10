using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {
	public GameObject equippedWeapon;
	GameObject weaponParent;
	GameObject myWeaponInstance; 
	UnitAnimator anim;
	
	void Start () {
		anim = GetComponent<UnitAnimator>();
		if (equippedWeapon){
			SpawnWeapon();
		}
	}
	
	public void Attack(GameObject target){
		anim.TriggerAttackAnimation();
		myWeaponInstance.GetComponent<Weapon>().Attack(gameObject, target);
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

