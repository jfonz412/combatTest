using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	private UnitController unit;
	//do not touch, is calculated
	public float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = GetComponent<Stats>().baseHp;
		unit = GetComponent<UnitController>();
	}
	
	public void TakeDamage(float damage, GameObject attacker){
		//before you subtract damage, filter incoming damage through a DefenseController
		if (currentHealth > 0.0f){
			currentHealth = currentHealth - damage;
			unit.ReactToDisturbance("Damage Taken", attacker);
			//anim.SetTrigger("hurt");
		}
		else{
			Die();
		}
		Debug.Log (name + " has taken " + damage + " damage!");
	}
	
	void Die(){
		//animate death
		Destroy (gameObject);
	}
}
