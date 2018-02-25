﻿using UnityEngine;
using System.Collections;

public class UnitReactions : MonoBehaviour
{
    public enum Factions { Townie, BigRedGang, Player };
    public Factions faction;

    AttackController attackController;
    NPCInteraction interactions;

    public float reactionRadius = 5f;
    public bool isDead = false;

    void Start()
    {
        interactions = GetComponent<NPCInteraction>();
        attackController = GetComponent<AttackController>();
    }

    /***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/

    public void ReactToAttack(Transform attacker = null)
    {
        if(name == "Player")
        {
            return;
        }

        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
            RemovePeacefulInteractions();
        }

        UnitReactionManager.instance.AlertEveryoneInRange((int)faction, attacker);
    }

    public void ReactToFactionAttack(Transform attacker = null)
    {
        if (name == "Player")
        {
            return;
        }

        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
            RemovePeacefulInteractions();
        }
    }

    public void ReactToNonFactionAttack(Transform attacker = null)
    {
        //do nothing
    }

    void RemovePeacefulInteractions()
    {
        for (int i = 0; i < interactions.myInteractions.Length; i++)
        {
            if (interactions.myInteractions[i] != "Attack")
            {
                interactions.myInteractions[i] = "--";
            }
        }
        interactions.defaultInteraction = Interactable.DefaultInteractions.Attack;
    }

    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
}
