using UnityEngine;

public class HostileCreature : UnitReactions
{
    protected override void Start()
    {
        base.Start();
        myEnemyFactions = new Factions[] { Factions.Player, Factions.Townie, Factions.BigRedGang };
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

