using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator : MonoBehaviour {

    /************* CALCULATE INCOMING HIT ************************/

    public static float CalculateTotalDamage(float incomingDamage, float baseDefense, Armor bodyPart)
    {
        //CheckForMissBlockParry();
        float totalDamage = incomingDamage - (bodyPart.defense + baseDefense);
        if (totalDamage < 0)
        {
            return 0;
        }
        else
        {
            //DamageStats(bodyPart, totalDamage);
            return totalDamage;
        }
    }

    //calculate attack damage
    public static float CalculateDamageDealt(float baseAttack, float weaponAttack)
    {
        //also need to consider weapon condtion? maybe condition just makes it break
        return baseAttack + weaponAttack;
    }
}
