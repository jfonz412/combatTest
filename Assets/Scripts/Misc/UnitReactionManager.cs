using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReactionManager : MonoBehaviour {

    public UnitReactors units;

    #region Singleton
    public static UnitReactionManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion


    //in the future I can make this specific to violent attacks and have other functions alert Units 
    //to other things like non-violent crimes
    public void AlertEveryoneInRange(int factionID, Transform attacker)
    {
        Vector3 location = attacker.position;

        Debug.Log("Alerting others in range");
        for(int i = 0; i < units.unitReactors.Length; i++)
        {
            UnitReactions unit = units.unitReactors[i];
            if (Vector3.Distance(unit.transform.position, location) < unit.reactionRadius)
            {
                if (factionID == (int)unit.faction)
                {
                    unit.ReactToFactionAttack(attacker);
                }
                else
                {
                    unit.ReactToNonFactionAttack(attacker);
                }   
            }
        }
    }
}
