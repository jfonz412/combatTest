using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimidTownie : UnitReactions  {
    public override void ReactToAttackAgainstSelf(Transform attacker = null)
    {
        base.ReactToAttackAgainstSelf(attacker);
        Fight(attacker);
    }

    public override void ReactToFactionAttack(Transform attacker = null)
    {
        base.ReactToFactionAttack(attacker);
        //Fight(attacker);
    }

    public override void ReactToNonFactionAttack(Transform attacker = null)
    {
        base.ReactToNonFactionAttack(attacker);
        return;
    }
}
