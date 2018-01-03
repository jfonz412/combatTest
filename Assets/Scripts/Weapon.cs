using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	//default for melee weapons is 1
	public float range, speed, condition;
	public string material, type;
	GameObject myOwner;
	Animator anim;
	
	void Start(){
		anim = GetComponent<Animator>();
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
		SetAnimationAndPosition();
		anim.SetTrigger("isAttacking");
		Health enemyHealth = target.GetComponent<Health>();
		enemyHealth.TakeDamage(CalculateDamageDealt(), myOwner);
	}

	float CalculateDamageDealt(){
		Stats stats = myOwner.GetComponent<Stats>();
		return stats.CalculateTotalAttack(material, type, condition);
	}
}
