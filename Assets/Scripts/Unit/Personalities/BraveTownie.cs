using UnityEngine;

public class BraveTownie : UnitReactions {

    public override void ReactToAttackAgainstSelf(Transform attacker = null)
    {
        base.ReactToAttackAgainstSelf(attacker);
        Fight(attacker);
    }

    protected override void ReactToFactionAttack(Transform attacker = null)
    {
        base.ReactToFactionAttack(attacker);
        Fight(attacker);
    }

    protected override void ReactToNonFactionAttack(Transform attacker = null)
    {
        base.ReactToNonFactionAttack(attacker);
        //do nothing
        return;
    }
}
