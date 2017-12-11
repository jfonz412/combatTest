using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float weaponAttackDamage;
	public float range = 1.5f;
	GameObject myOwner;
	Animator anim;
	
	void Start(){
		anim = GetComponent<Animator>();
	}
	
	void Update(){
		if(myOwner){
			SetAnimationAndPosition();
		}
	}
	
	void SetAnimationAndPosition(){
		transform.position = myOwner.transform.position;
		float x = myOwner.GetComponent<UnitController>().inputX;
		float y = myOwner.GetComponent<UnitController>().inputY;
		anim.SetFloat("x", x);
		anim.SetFloat ("y", y);
	}

	public void Attack(GameObject owner, GameObject target){
		myOwner = owner;
		anim.SetTrigger("isAttacking");
		Enemy enemy = target.GetComponent<Enemy>(); // might need to change this if you want to use this script for enemy vs player
		if (!enemy){
			return;
		}else{
			Health enemyHealth = target.GetComponent<Health>();
			enemyHealth.TakeDamage(CalculateDamageDealt());
		}
	}

	float CalculateDamageDealt(){
		Stats stats = myOwner.GetComponent<Stats>(); //get stats from owner
		return stats.attack + weaponAttackDamage;
	}
}
