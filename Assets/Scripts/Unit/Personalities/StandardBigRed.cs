using UnityEngine;

public class StandardBigRed : UnitReactions {

    protected override void Start()
    {
        base.Start();
        myEnemyFactions = new Factions[] { Factions.HostileCreature };
    }


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
        return;
    }
}
