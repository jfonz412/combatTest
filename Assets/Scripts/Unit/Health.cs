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
		currentHealth = currentHealth - myStats.DamageAfterDefense(damage);
		Debug.Log (name + " has taken " + myStats.DamageAfterDefense(damage) + " damage!");
		if (currentHealth > 0.0f){
			unitReactions.ReactToDisturbance("Damage Taken", attacker);
			//anim.SetTrigger("hurt"); or trigger damage indicators
		}else{
			Die();
		}
	}
	
	void Die(){
		//animate death
		Destroy (gameObject);
	}
}
