using UnityEngine;

public class TimidTownie : UnitReactions  {
    public override void ReactToAttackAgainstSelf(Transform attacker = null)
    {
        base.ReactToAttackAgainstSelf(attacker);
        RunAway(attacker);
    }

    public override void ReactToFactionAttack(Transform attacker = null)
    {
        base.ReactToFactionAttack(attacker);
        RunAway(attacker);
    }

    public override void ReactToNonFactionAttack(Transform attacker = null)
    {
        base.ReactToNonFactionAttack(attacker);
        RunAway(attacker);
    }

}
