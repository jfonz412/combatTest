using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	[SerializeField]
	public float baseAttack;
	[SerializeField]
	public float baseDefense;
	[SerializeField]
	public float baseHp;
	

	public float CalculatedBodilyHarm(float incomingDamage){
		int bodyPartID = PickBodyPart(); 
		float totalDamage = AbsorbAttack(incomingDamage, bodyPartID);
		
		if (totalDamage > 0){
			DamageStats(bodyPartID,totalDamage);	
		}
		return totalDamage;
	}
	
	float AbsorbAttack(float incomingDamage, int targetedBodyPartID){
		UnitAnimator anim = GetComponent<UnitAnimator>();
		//Armor armor = anim.loadedArmor.GetComponent<Armor>();
		float totalDamage = incomingDamage; //= 0;
		
		return totalDamage;
	}
	
	int PickBodyPart(){
		//use random bodypart for now with percentages of being selected to determine where blow has landed
		// the int should corrospond with the appropriate armor slot
		return 1;
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
