﻿using UnityEngine;
using System.Collections;

//the base script for all of our unity personalities
public class UnitReactions : MonoBehaviour
{
    public enum Factions { Townie, BigRedGang, Player };
    public Factions faction;

    private AttackController attackController;
    private NPCInteractionStates npcState;
    private UnitController unitController;

    public float reactionRadius = 5f;
    //public float criticalHealthThreshold = 0;

    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool runningAway = false; //make protected?

    void Start()
    {
        npcState = transform.GetComponent<NPCInteractionStates>();
        attackController = GetComponent<AttackController>();
        unitController = GetComponent<UnitController>();
        ScriptToolbox.GetInstance().GetUnitReactionManager().AddUnitToReactionManager(this);
    }

    /***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/

    //called by UnitReactionManager.cs 
    public void ReactToAttackAgainstOther(int factionID, Transform attacker)
    {
        if (factionID == (int)faction)
        {
            ReactToFactionAttack(attacker); // or an ally faction
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
            ScriptToolbox.GetInstance().GetWindowCloser().KnockPlayerOutOfDialogue();
            ScriptToolbox.GetInstance().GetUnitReactionManager().AlertEveryoneInRange((int)faction, attacker);
            return;
        }

        //code smells here, unit is not actually reacting to itself being attacked but it's own faction being attacked
        ScriptToolbox.GetInstance().GetUnitReactionManager().AlertEveryoneInRange((int)faction, attacker);
    }


    protected virtual void ReactToFactionAttack(Transform attacker = null)
    {
        if (name == "Player")
        {
            return;
        }
    }

    protected virtual void ReactToNonFactionAttack(Transform attacker = null)
    {
        if (name == "Player")
        {
            return;
        }
    }

    //possibly give these their own script?
    #region Possible Reactions

    protected void Fight(Transform attacker)
    {
        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
            if(attacker.name == "Player")
            {
                npcState.SetInteractionState(NPCInteractionStates.InteractionState.FightingPlayer);
            }
            else
            {
                npcState.SetInteractionState(NPCInteractionStates.InteractionState.FightingNPC);
            }
        }
    }

    protected void RunAwayFromFactionAttack(Transform attacker)
    {
        if (!runningAway)
        {
            runningAway = true;

            if (attacker.name == "Player")
            {
                npcState.SetInteractionState(NPCInteractionStates.InteractionState.FleeingPlayer);
            }
            else
            {
                npcState.SetInteractionState(NPCInteractionStates.InteractionState.FleeingNPC);
            }

            StartCoroutine(RunFromAttacker(attacker));
        }           
    }

    //if general violence occurs, unit will run away and will not interact with trading or talking
    protected void RunAwayFromNonFactionAttack(Transform attacker)
    {
        if (!runningAway)
        {
            runningAway = true;
            npcState.SetInteractionState(NPCInteractionStates.InteractionState.FleeingNPC);
            StartCoroutine(RunFromAttacker(attacker));
        }
    }

    private IEnumerator RunFromAttacker(Transform attacker)
    {
        //PathfindingManager.RequestPath(transform.position, GetPosition(attacker), unitController.OnPathFound); //makes for a quick initial reaction

        while (attacker && !isDead)
        {
            if (Vector3.Distance(transform.position, attacker.transform.position) < 3f) //runaway radius hardcoded
            {
                PathfindingManager.RequestPath(transform.position, GetPosition(attacker), unitController.OnPathFound);
                yield return new WaitForSeconds(3f); //might want to play with this?
            }
            else
            {
                runningAway = false;
                break;
            }
        }
        yield break;
    }

    private Vector3 GetPosition(Transform attacker)
    {
        Node node;
        int attempts = 0;

        //check if we can run directly away from attacker first     
        //causes units to run in opposite direciton, looks more natural
        node = NodeOppositeAttacker(attacker);
        if (node.walkable)
        {
            //return node.worldPos; //implement raycast detection first so they all don't stack together. Then we don't need to deviate
        }
        
        //otherwise run wherever you can
        while (true)
        {
            node = RandomNode();

            if (node.walkable || attempts >= 10)
            {
                break;
            }

            attempts++;          
        }

        return node.worldPos;     
    }

    private Node NodeOppositeAttacker(Transform attacker)
    {
        float deviation = Random.Range(0f, 0.9f); 

        float x = -attacker.position.x * deviation;
        float y = -attacker.position.y * deviation;
        float z = -attacker.position.z;

        Vector3 position = new Vector3(x, y, z);
        Node node = Grid.instance.NodeAtWorldPosition(position);

        return node;
    }

    private Node RandomNode()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        Vector3 position = new Vector3(x + Random.Range(-5.0f, 5.0f), y + Random.Range(-5.0f, 5.0f), z);
        Node node = Grid.instance.NodeAtWorldPosition(position);

        return node;
    }

    #endregion

    //debuggin' purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
}
