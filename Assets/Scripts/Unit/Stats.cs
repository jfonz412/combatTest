using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	[SerializeField]
	public float baseAttack;
	[SerializeField]
	public float baseDefense;
	[SerializeField]
	public float baseHp;
	
	UnitAnimator anim;
	
	void Start(){
		anim = GetComponent<UnitAnimator>();
	}

	public float DamageAfterDefense(float incomingDamage){
		int bodyPartIndex = PickBodyPart();
		Armor armor = anim.animators[bodyPartIndex].gameObject.GetComponent<Armor>();
		
		float totalDamage = incomingDamage - (armor.defense + baseDefense);
		if (totalDamage > 0){
			DamageStats(bodyPartIndex,totalDamage);	
			return totalDamage;
		}else{
			return 0f;
		}
	}
	
	int PickBodyPart(){
		// cannot be 0 or 1, which are the unit and weapon animators
		return 2; //2 is torso, 3 is legs
	}
	
	void DamageStats(int bodyPart,float damage){
		//skills deducted depending on damage and bdy part
	}
	
	
	
	/******************** STATS *****************************************/
	
	public float attack {
		get {
			return baseAttack;
		}
	}
}
