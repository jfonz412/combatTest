using System.Collections.Generic;
using UnityEngine;

public class UnitReactionManager : MonoBehaviour {
    [SerializeField]
    private List<UnitReactions> units;

    //this is called by a unit when it is attacked (UnitReactions.ReactToAttackAgainstSelf())
    public void AlertEveryoneInRange(Transform attacker, Transform victim)
    {
        Vector3 location = attacker.position;

        //Debug.Log("Alerting others in range");
        for (int i = 0; i < units.Count; i++)
        {
            UnitReactions unit = units[i];

            //skip if unit is dead
            if (unit.isDead || unit == null)
            {
                units.RemoveAt(i);
                continue;
            }

            if (Vector3.Distance(unit.transform.position, location) < unit.reactionRadius)
            {
                unit.ReactToViolence(attacker, victim);
            }
        }
    }

    /* move this out of unit reaction manager
    private void CheckForRadiusConflicts()
    {
        for(int n = 0; n < units.Count; n++)
        {
            UnitReactions mainUnit = units[n];

            for (int i = 0; i < units.Count; i++)
            {
                UnitReactions otherUnit = units[i];

                if (Vector3.Distance(otherUnit.transform.position, mainUnit.transform.position) < mainUnit.reactionRadius)
                {
                    mainUnit.ReactToUnitInRaidius(otherUnit);
                }
            }
        }
    }
    */

    public void AddUnitToReactionManager(UnitReactions unit)
    {
        //Debug.Log(unit);
        units.Add(unit);
    }
}
