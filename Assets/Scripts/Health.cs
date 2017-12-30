using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float health = 100f;
	//private Animator anim;
	private UnitController unit;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator>();
		unit = gameObject.GetComponent<UnitController>();
	}
	
	public void TakeDamage(float damage, GameObject attacker){
		if (health > 0.0f){
			health = health - damage;
			unit.ReactToDisturbance("Damage Taken", attacker);
			//anim.SetTrigger("hurt");
		}
		if (health <= 0.0f){
			Die();
		}
		Debug.Log (name + " has taken " + damage + " damage!");
	}
	
	void Die(){
		//animate death
		Destroy (gameObject);
	}
}
