using UnityEngine;

public class UnitRelationships : MonoBehaviour {
    public enum Factions { Townie, BigRedGang, Player, HostileCreature };
    public Factions myFaction;
    public Factions[] myEnemyFactions;
    public Factions[] myFriendlyFactions;
}
