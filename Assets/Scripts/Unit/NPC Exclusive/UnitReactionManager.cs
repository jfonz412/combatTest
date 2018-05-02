using UnityEngine;

public class UnitReactionManager : MonoBehaviour {

    public UnitReactors units;

    //this is called by a unit when it is attacked (UnitReactions.ReactToAttackAgainstSelf())
    public void AlertEveryoneInRange(int factionID, Transform attacker)
    {
        Vector3 location = attacker.position;

        //Debug.Log("Alerting others in range");
        for(int i = 0; i < units.unitReactors.Length; i++)
        {
            UnitReactions unit = units.unitReactors[i];

            if (unit.isDead || unit == null)
            {
                continue;
            }

            if (Vector3.Distance(unit.transform.position, location) < unit.reactionRadius)
            {
                Debug.Log(unit.name + "is in the attack radius!!!");
                unit.ReactToAttackAgainstOther(factionID, attacker);
                Debug.Log("Reacting");
            }
        }
    }
}
