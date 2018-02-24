using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public float baseAttack;
    public float baseDefense;
    public float baseHp;

    EquipmentManager equipmentManager; //might not be necessary to cache

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += AdjustStats;
    }

    static void DamageStats(Armor bodyPart, float damage)
    {
        //skills deducted depending on damage and bdy part
    }

    /**************************** STATS *****************************************/

    //Invoked from EquipmentManager to adjust stats based on newly equipped items
    void AdjustStats(Equipment oldItem, Equipment newItem)
    {
        //Debug.Log("ADJUSTING STATS HOMIE!!");
    }

    public float attack
    {
        get
        {
            return baseAttack;
        }
    }

    /*

    public float DamageAfterDefense(float incomingDamage)
    {
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
        //EquipmentSlot { Head, Chest, Legs, MainHand, OffHand, Feet}
        int[] validChoices = new int[] { 0, 1, 2, 5 };
        int num = Random.Range(0, validChoices.Length);

        Debug.Log("Attacking bodypart #" + validChoices[num]);
        return validChoices[num];
    }

    void DamageStats(int bodyPart, float damage)
    {
        //skills deducted depending on damage and bdy part
    }
    */

}
