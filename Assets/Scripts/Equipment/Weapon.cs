using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	[HideInInspector]
	public float speed, range; //standard is 1 for melee weapons
	
	public float weaponCondition = 1; //1 is undamaged
	public float softness = 1; //1 is max hardness, the harder the better
	public float sharpness = 1; //the sharper the better
	public float weight = 1; //the heavier the better
	//string attackType = "cast"/"slash"/"stab"
	
	void Start(){
		speed = weight / 2; 
	}
	
	public void Attack(GameObject owner, GameObject target){ //instead of passing GameObject, might be better to retrieve parent's parent?
		Health enemyHealth = target.GetComponent<Health>();
		enemyHealth.TakeDamage(CalculateDamageDealt(owner), owner);
	}

	//Calculate the damage of the weapon
	float CalculateDamageDealt(GameObject owner){
		float unitAttack = owner.GetComponent<Stats>().attack; 
		return unitAttack + weight + sharpness * softness * weaponCondition;	//also need to consider weapon condtion? maybe condition just makes it break
	}
}
