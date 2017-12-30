using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float weaponAttackDamage;
	public float range,speed;
	GameObject myOwner;
	Animator anim;
	
	void Start(){
		anim = GetComponent<Animator>();
	}
	
	void Update(){
		//is contantly setting position, might be too expensive
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
		Health enemyHealth = target.GetComponent<Health>();
		enemyHealth.TakeDamage(CalculateDamageDealt(), myOwner);
	}

	float CalculateDamageDealt(){
		Stats stats = myOwner.GetComponent<Stats>(); //get stats from owner
		return stats.attack + weaponAttackDamage;
	}
}
