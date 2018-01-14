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
		currentHealth = currentHealth - myStats.CalculatedBodilyHarm(damage);
		if (currentHealth > 0.0f){
			unitReactions.ReactToDisturbance("Damage Taken", attacker);
			//anim.SetTrigger("hurt");
		}else{
			Die();
		}
		Debug.Log (name + " has taken " + damage + " damage!");
	}
	
	void Die(){
		//animate death
		Destroy (gameObject);
	}
}
