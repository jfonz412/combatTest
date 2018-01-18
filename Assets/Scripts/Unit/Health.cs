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
	
	public void TakeDamage(float damage, Transform attacker){		
		currentHealth = currentHealth - myStats.DamageAfterDefense(damage);
		Debug.Log (name + " has taken " + myStats.DamageAfterDefense(damage) + " damage!");
		if (currentHealth > 0.0f){
			unitReactions.ReactToDisturbance("Damage Taken", attacker);
		}else{
			IEnumerator death = Die(attacker);
			StartCoroutine(death);
		}
	}
	
	IEnumerator Die(Transform attacker){
		UnitController myUnit = GetComponent<UnitController>();
		UnitAnimator myAnim = GetComponent<UnitAnimator>();

		//stop the attacker
		attacker.GetComponent<UnitController>().HasTarget(false); 
		
		//stop the target
		myUnit.HasTarget(false); 
		myUnit.StopMoving();
		myAnim.TriggerDeathAnimation();
		
		//stop processing clicks if player
		PlayerController player = myUnit.GetComponent<PlayerController>();
		if(player != null){
			player.incapacitated = true;
		}
		
		//extend death animation before destroy
		yield return new WaitForSeconds(6f);
		Destroy (gameObject);
	}
}
