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
}
