using UnityEngine;
using System.Collections;

//the base script for all of our unity personalities
public class UnitReactions : MonoBehaviour
{
    public enum Factions { Townie, BigRedGang, Player };
    public Factions faction;

    AttackController attackController;
    NPCInteraction interactions;
    UnitController unitController;

    public float reactionRadius = 5f;
    //public float criticalHealthThreshold = 0;

    //I might be able to rectify these with the similar variables in NPCInteraction.cs via an NPCState.cs of some sort
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool runningAway = false;

    void Start()
    {
        interactions = transform.GetComponent<NPCInteraction>();
        attackController = GetComponent<AttackController>();
        unitController = GetComponent<UnitController>();
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
            CloseOpenWindows.instance.KnockPlayerOutOfDialogue();
            return;
        }

        //code smells here, unit is not actually reacting to itself being attacked but it's own faction being attacked
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

    //possibly give these their own script?
    #region Possible Reactions

    public void Fight(Transform attacker)
    {
        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
            if(attacker.name == "Player")
            {
                interactions.SetInteractionState(NPCInteraction.InteractionState.FightingPlayer);
            }
            else
            {
                interactions.SetInteractionState(NPCInteraction.InteractionState.FightingNPC);
            }
        }
    }

    public void RunAway(Transform attacker)
    {
        if (!runningAway)
        {
            runningAway = true;
            StartCoroutine(RunFromAttacker(attacker));
        }           
    }

    //----------------------------------------

    IEnumerator RunFromAttacker(Transform attacker)
    {
        //PathfindingManager.RequestPath(transform.position, GetPosition(attacker), unitController.OnPathFound); //makes for a quick initial reaction

        if (attacker.name == "Player")
        {
            interactions.SetInteractionState(NPCInteraction.InteractionState.FleeingPlayer);
        }
        else
        {
            interactions.SetInteractionState(NPCInteraction.InteractionState.FleeingNPC);
        }

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

    Vector3 GetPosition(Transform attacker)
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

    Node NodeOppositeAttacker(Transform attacker)
    {
        float deviation = Random.Range(0f, 0.9f); 

        float x = -attacker.position.x * deviation;
        float y = -attacker.position.y * deviation;
        float z = -attacker.position.z;

        Vector3 position = new Vector3(x, y, z);
        Node node = Grid.instance.NodeAtWorldPosition(position);

        return node;
    }

    Node RandomNode()
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
}
