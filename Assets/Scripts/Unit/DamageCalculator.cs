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
}
