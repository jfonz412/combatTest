using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float health = 100f;
	private Animator anim;
	private UnitAI unitAI;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		unitAI = gameObject.GetComponent<UnitAI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void TakeDamage(float damage, GameObject attacker){
		if (health > 0.0f){
			health = health - damage;
			unitAI.ReactToDisturbance("Damage Taken", attacker);
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
	/*
	IEnumerator KnockBack(){
		float timer = 0f;
		float knockBackTime = 30.0f;
		
		while(knockBackTime > timer){
			rb2d.isKinematic = false;
			rb2d.AddForce(Vector2.up * .05f);
			timer += Time.deltaTime;
			Debug.Log (timer);
		}
		rb2d.isKinematic = true;
		yield return 0;
	}
	*/
}
