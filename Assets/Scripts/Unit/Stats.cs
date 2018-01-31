using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	//[SerializeField]
	public float baseAttack;
	//[SerializeField]
	public float baseDefense;
	//[SerializeField]
	public float baseHp;
	
	//UnitAnimator anim;
    EquipmentManager equipmentManager;

	void Start(){
		//anim = GetComponent<UnitAnimator>();

        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += AdjustStats;
	}

    /************* CALCULATE INCOMING HIT ************************/

	public float DamageAfterDefense(float incomingDamage){
		int bodyPartIndex = PickBodyPart();
        float totalDamage = CalculateTotalDamage(incomingDamage, bodyPartIndex);
        FloatingTextController.CreateFloatingText(totalDamage.ToString(), transform);
        return totalDamage;
    }

    // will eventually check for blocks, parrys, misses, ect.
    float CalculateTotalDamage(float incomingDamage, int bodyPartIndex)
    {
        Armor armor = (Armor)equipmentManager.currentEquipment[bodyPartIndex]; //probably not very effecient
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

    int PickBodyPart()
    {
        //EquipmentSlot { Head, Chest, Legs, MainHand, OffHand, Feet} (the max is exclusive so must be +1)
        int num = Random.Range(1, 3);
        Debug.Log("Attacking bodypart #" + num);
        return num;
    }

    void DamageStats(int bodyPart, float damage)
    {
        //skills deducted depending on damage and bdy part
    }


    /******************** STATS *****************************************/

    //Invoked from EquipmentManager to adjust stats based on newly equipped items
    void AdjustStats(Equipment oldItem, Equipment newItem)
    {
        Debug.Log("ADJUSTING STATS HOMIE!!");
    }

    public float attack {
		get {
			return baseAttack;
		}
	}

}
