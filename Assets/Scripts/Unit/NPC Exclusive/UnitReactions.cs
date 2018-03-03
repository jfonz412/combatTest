using UnityEngine;

public class UnitReactions : MonoBehaviour
{
    public enum Factions { Townie, BigRedGang, Player };
    public Factions faction;

    AttackController attackController;
    NPCInteraction interactions;

    public float reactionRadius = 5f;
    //public float criticalHealthThreshold = 0;
    [HideInInspector]
    public bool isDead = false;

    void Start()
    {
        interactions = transform.GetChild(0).GetComponent<NPCInteraction>();
        attackController = GetComponent<AttackController>();
    }

    /***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/

    //called by UnitReactionManager.cs 
    public void ReactToAttackAgainstOther(int factionID, Transform attacker)
    {
        if (factionID == (int)faction)
        {
            ReactToFactionAttack(attacker);
        }
        else
        {
            ReactToNonFactionAttack(attacker);
        }
    }

    //called by my Health.cs when attacked
    public virtual void ReactToAttackAgainstSelf(Transform attacker = null)
    {
        if (name == "Player")
        {
            CloseOpenWindows.instance.KnockPlayerOutOfDialogue();
            return;
        }

        UnitReactionManager.instance.AlertEveryoneInRange((int)faction, attacker);
    }


    public virtual void ReactToFactionAttack(Transform attacker = null)
    {
        if (name == "Player")
        {
            return;
        }
    }

    public virtual void ReactToNonFactionAttack(Transform attacker = null)
    {
        if (name == "Player")
        {
            return;
        }
    }

#region Possible Reactions
    //possibly give these their own script?
    public void Fight(Transform attacker)
    {
        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
            interactions.RemovePeacefulInteractions();
        }
    }
#endregion

    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
}
