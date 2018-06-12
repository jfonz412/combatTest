using UnityEngine;

public class Stats : MonoBehaviour
{
    //these are public just so we can tweak them in the editor, they are not public to be touched by other items
    public float baseAttack;
    public float baseDefense;
    public float baseHp;
    public float baseSpeed;

    EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += AdjustStats;
    }

    static void DamageStats(Armor bodyPart, float damage)
    {
        //skills deducted depending on damage and bdy part
        //results in modifiers
    }

    static void BuffStats(string stat, float amount, string buff)
    {
        //skills buffed depending on damage and bdy part
        //results in modifiers
        // this is just a prototype/placeholder
    }

    /**************************** STATS *****************************************/

    public float attack
    {
        get
        {
            return baseAttack; //* modifiers
        }
    }

    public float defense
    {
        get
        {
            return baseDefense; //* modifiers
        }
    }

    public float health
    {
        get
        {
            return baseHp; //* modifiers
        }
    }

    public float speed
    {
        get
        {
            return baseSpeed; //* modifiers
        }
    }

    //Invoked from EquipmentManager to adjust stats based on newly equipped items
    void AdjustStats(Equipment oldItem, Equipment newItem)
    {
        //Debug.Log("ADJUSTING STATS HOMIE!!");
    }

}
