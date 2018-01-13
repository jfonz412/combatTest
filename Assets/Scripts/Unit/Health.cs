using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float currentHealth; //do not touch, just so I can see it
	UnitReactions unitReactions;
	Stats myStats;

	void Start () {
		myStats = GetComponent<Stats>();
		currentHealth = myStats.baseHp;
		unitReactions = GetComponent<UnitReactions>();
	}
	
	public void TakeDamage(float damage, GameObject attacker){		
		currentHealth = currentHealth - CalculatedBodilyHarm(damage);
		if (currentHealth > 0.0f){
			unitReactions.ReactToDisturbance("Damage Taken", attacker);
			//anim.SetTrigger("hurt");
		}else{
			Die();
		}
		Debug.Log (name + " has taken " + damage + " damage!");
	}
	
	float CalculatedBodilyHarm(float damage){
		damage = myStats.AbsoarbAttack(damage);
		//use Indicator class to show messages for damage, blocks, ect (called from myStats)
		
		if (damage > 0){
			string bodyPart = PickBodyPart();
			myStats.DamageStats(bodyPart,damage);	
		}
		return damage;
	}
	
	string PickBodyPart(){
		//use random bodypart for now with percentages of being selected to determine where blow has landed
		return null;
	}
	
	void Die(){
		//animate death
		Destroy (gameObject);
	}
}
