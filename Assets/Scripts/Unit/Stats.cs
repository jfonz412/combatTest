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

    /************* CALCULATE INCOMING HIT ************************/

	public float DamageAfterDefense(float incomingDamage){
		int bodyPartIndex = PickBodyPart();
        float totalDamage = CalculateTotalDamage(incomingDamage, bodyPartIndex);
        FloatingTextController.CreateFloatingText(totalDamage.ToString(), transform);
        return totalDamage;
    }
	
	int PickBodyPart(){
		// cannot be 0 or 1, which are the unit and weapon animators
		return Random.Range(2,4); //2 is torso, 3 is legs (the max is exclusive so must be +1)
	}
	
	void DamageStats(int bodyPart,float damage){
		//skills deducted depending on damage and bdy part
	}

    // will eventually check for blocks, parrys, misses, ect.
    float CalculateTotalDamage(float incomingDamage, int bodyPartIndex)
    {
        Armor armor = anim.animators[bodyPartIndex].gameObject.GetComponent<Armor>();
        float totalDamage = incomingDamage - (armor.defense + baseDefense); 
        if (totalDamage < 0)
        {
            return 0;
        }
        else
        {
            DamageStats(bodyPartIndex, totalDamage);
            return totalDamage;
        }
    }
	
	
	/******************** STATS *****************************************/
	
	public float attack {
		get {
			return baseAttack;
		}
	}
}
