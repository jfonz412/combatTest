using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	Animator anim;
	
	[HideInInspector]
	public float range; //standard is 1 for melee weapons
	[HideInInspector]
	public float speed;
	
	public float weaponCondition;
	public float softness; //1 is max softness
	public float sharpness;
	public float weight;
	
	void Start(){
		anim = GetComponent<Animator>();
		speed = weight / 2;
	}
	
	public void Attack(GameObject owner, GameObject target){
		GameObject myOwner = owner;
		Health enemyHealth = target.GetComponent<Health>();
		SetAnimationAndPosition(myOwner);
		
		anim.SetTrigger("isAttacking");
		enemyHealth.TakeDamage(CalculateDamageDealt(owner), myOwner);
	}
	
	void SetAnimationAndPosition(GameObject myOwner){
		transform.position = myOwner.transform.position;
		float x = myOwner.GetComponent<UnitController>().inputX;
		float y = myOwner.GetComponent<UnitController>().inputY;
		anim.SetFloat("x", x);
		anim.SetFloat ("y", y);
	}

	float CalculateDamageDealt(GameObject owner){
		//also need to consider weapon condtion? maybe condition just makes it break
		float unitAttack = owner.GetComponent<Stats>().attack;
		return unitAttack + weight + sharpness * softness;	
	}
	
}
