using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	public float attack;
	public float defense;
	public float baseHp;
	
	//need functions or getters/setters to calculate skills after items, status', ect.
	
	public void LevelUp(string skill){
		//skill + 1
	}
	public float AbsoarbAttack(float incomingDamage){
		float damage = incomingDamage;
		return damage;
			
	}
	public void DamageStats(string bodyPart,float damage){
		//skills deducted depending on damage and bdy part
	}
}
