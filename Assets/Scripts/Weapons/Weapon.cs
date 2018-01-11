using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	[HideInInspector]
	public float speed, range; //standard is 1 for melee weapons
	
	public float weaponCondition;
	public float softness; //1 is max softness
	public float sharpness;
	public float weight;
	
	void Start(){
		speed = weight / 2; 
	}
	
	public void Attack(GameObject owner, GameObject target){
		GameObject myOwner = owner;
		Health enemyHealth = target.GetComponent<Health>();
		enemyHealth.TakeDamage(CalculateDamageDealt(owner), myOwner);
	}

	float CalculateDamageDealt(GameObject owner){
		float unitAttack = owner.GetComponent<Stats>().attack; 
		return unitAttack + weight + sharpness * softness;	//also need to consider weapon condtion? maybe condition just makes it break
	}
}
