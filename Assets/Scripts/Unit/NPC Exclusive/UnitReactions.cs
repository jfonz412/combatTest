using UnityEngine;
using System.Collections;

//the base script for all of our unity personalities
public class UnitReactions : MonoBehaviour
{
    private UnitReactionManager rm;
    private AttackController attackController;
    private NPCInteractionStates npcState;
    private Brain myBrain;
    private UnitController unitController;

    public bool doNotReact = false; //prevent unit from reacting via AI

    //gives us faction bonuses or debuffs
    public enum Factions { Townie, BigRedGang, Player, HostileCreature };
    public Factions myFaction; 
    public Factions[] myEnemyFactions;
    public Factions[] myFriendlyFactions;

    private Brain.State[] engagedStates = new Brain.State[] { Brain.State.Fighting, Brain.State.Fleeing };

    public float reactionRadius = 5f;
    //public float criticalHealthThreshold = 0;

    [HideInInspector]
    public bool isDead { get { return myBrain.isDead; } } //provides a shortcut for manager
    private bool runningAway = false;

    //prevent units from reacting to same attacker/target with each hit
    private Transform currentAttacker;
    // prevent AlertEveryoneInRange from triggering for everyone in vicinity with every hit
    private Transform currentVictim; 

    [Range(0f,100f)]
    public float courage;

    private void Start()
    {
        npcState = transform.GetComponent<NPCInteractionStates>();
        myBrain = GetComponent<Brain>();
        attackController = GetComponent<AttackController>();
        unitController = GetComponent<UnitController>();

        rm = ScriptToolbox.GetInstance().GetUnitReactionManager();
        rm.AddUnitToReactionManager(this);
    }

    /* disabling vicinity reactions for now 
    public void ReactToUnitInRaidius(UnitReactions unit)
    {
        for(int i = 0; i < myEnemyFactions.Length; i++)
        {
            if (unit.faction == myEnemyFactions[i])
            {
                //Debug.Log("Enemy in radius!"); //NEEDS REFACTORING*******
                ReactToViolence(unit.transform);
                return;
            }
        }
    }
    */

    public void IAmUnderAttack(Transform attacker, Transform victim)
    {
        rm.AlertEveryoneInRange(attacker, victim); //issue here, see currentTarget

        if (attacker == currentAttacker || doNotReact)
        {
            return;
        }
        else if(courage >= Random.Range(0, 100))
        {
            currentAttacker = attacker;
            Fight(attacker);
        }
        else
        {
            currentAttacker = attacker;
            Flee(attacker);
        }
    }

    public void ReactToViolence(Transform attacker, Transform victim)
    {
        //if we have already reacted to this person being attacked  ..or we are already fighting or fleeing, don't react
        if (victim == currentVictim || doNotReact || myBrain.ActiveStates(engagedStates))
            return;

        //flee if we are not couragous enough
        if (courage < Random.Range(0, 100))
        {
            currentVictim = victim;
            Flee(attacker);
        }
        else
        {
            currentVictim = victim;

            //figure out my relationship with the victim
            Factions victimFaction = victim.GetComponent<UnitReactions>().myFaction;
            int victimRelationship = 0;
            //figure out my relationship with the attacker
            Factions attackerFaction = attacker.GetComponent<UnitReactions>().myFaction;
            int attackerRelationship = 0;
            
            //dock either unit if they are in an enemy faction
            for (int i = 0; i < myEnemyFactions.Length; i++)
            {
                if (victimFaction == myEnemyFactions[i])
                    victimRelationship -= 10;
                if (attackerFaction == myEnemyFactions[i])
                    attackerRelationship -= 10;
            }

            //add points if either unit is in an allied faction
            for (int i = 0; i < myFriendlyFactions.Length; i++)
            {
                if (victimFaction == myFriendlyFactions[i])
                    victimRelationship += 10;
                if (attackerFaction == myFriendlyFactions[i])
                    attackerRelationship += 10;
            }
                     
            //this should cause this unit to choose the party they are more allied with while ignoring neutral fights
            if(victimRelationship > attackerRelationship && victimRelationship > 0) //&& i like the victim enough)
            {
                Fight(attacker);
            }
                                                             
            else if (victimRelationship < attackerRelationship && attackerRelationship > 0) //&& i like the attacker enough
            {
                Fight(victim);
            }
        }
    }

    #region Possible Reactions

    private void Fight(Transform attacker)
    {
        //target the last unit that attacked it while preventing attacking the same target everytime unit is damaged
        if (attackController.lastKnownTarget != attacker)
        {
            attackController.EngageTarget(true, attacker);
        }
    }

    private void Flee(Transform attacker)
    {
        if (!runningAway) //if not already running away
        {
            runningAway = true;
            StartCoroutine(RunFromAttacker(attacker));
        }           
    }

    #endregion

    #region Reaction Helper methods

    private IEnumerator RunFromAttacker(Transform attacker)
    {
        //PathfindingManager.RequestPath(transform.position, GetPosition(attacker), unitController.OnPathFound); //makes for a quick initial reaction
        myBrain.ToggleState(Brain.State.Fleeing, true);
        while (attacker && !isDead)
        {
            if (Vector3.Distance(transform.position, attacker.transform.position) < 3f) //runaway radius hardcoded
            {
                PathfindingManager.RequestPath(new PathRequest(transform.position, GetPosition(attacker), unitController.OnPathFound));
                yield return new WaitForSeconds(3f); //might want to play with this?
            }
            else
            {
                runningAway = false;
                break;
            }
        }
        myBrain.ToggleState(Brain.State.Fleeing, false);
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

    //gets node opposite of attacker to run to
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
}
