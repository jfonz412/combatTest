using UnityEngine;

public class TimidTownie : UnitReactions  {

    protected override void Start()
    {
        base.Start();
        myEnemyFactions = new Factions[] { Factions.HostileCreature };
    }

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
