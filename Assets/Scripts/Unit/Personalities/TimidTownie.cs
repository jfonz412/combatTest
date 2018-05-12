using UnityEngine;

public class TimidTownie : UnitReactions  {
    public override void ReactToAttackAgainstSelf(Transform attacker = null)
    {
        base.ReactToAttackAgainstSelf(attacker);
        RunAwayFromFactionAttack(attacker);
    }

    protected override void ReactToFactionAttack(Transform attacker = null)
    {
        base.ReactToFactionAttack(attacker);
        RunAwayFromFactionAttack(attacker);
    }

    protected override void ReactToNonFactionAttack(Transform attacker = null)
    {
        base.ReactToNonFactionAttack(attacker);
        RunAwayFromNonFactionAttack(attacker);
    }

}
