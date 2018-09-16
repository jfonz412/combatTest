using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//the base script for all of our unity personalities
public class UnitReactions : MonoBehaviour
{
    /*
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

    //prevent units from reacting to same attacker/target with each hit
    //private Transform currentAttacker;
    // prevent AlertEveryoneInRange from triggering for everyone in vicinity with every hit
    private Transform currentVictim; 

    [Range(0f,100f)]
    public float courage;
    //private float groupCourage; //will be used to buff or debuff courage when unit's side has more or less alies than the other

    private void Start()
    {
        npcState = transform.GetComponent<NPCInteractionStates>();
        myBrain = GetComponent<Brain>();
        attackController = GetComponent<AttackController>();
        unitController = GetComponent<UnitController>();

        rm = ScriptToolbox.GetInstance().GetUnitReactionManager();
        rm.AddUnitToReactionManager(this);
    }

    public void CheckSurroundings()
    {
        if (doNotReact || myBrain.ActiveStates(engagedStates))
            return; //return if already engaged and reacting to a unit

        Collider2D[] unitsInVicinity = Physics2D.OverlapCircleAll(transform.position, reactionRadius, 1);
        //Debug.LogError(LayerMask.GetMask(new string[] { "Default" }));
        for (int n = 0; n < unitsInVicinity.Length; n++)
        {
            if (CheckThreatLevel(unitsInVicinity[n].transform)) //if something triggers a reaction, exit out of this method
                return;            
        }
    }

    private bool CheckThreatLevel(Transform unit)
    {
        //figure out my relationship with the unit
        //Debug.LogError("Unit is " + unit.name);
        Factions unitFaction = unit.GetComponent<UnitReactions>().myFaction;
        int unitRelationship = 0;

        //dock unit if they are in an enemy faction
        for (int i = 0; i < myEnemyFactions.Length; i++)
        {
            if (unitFaction == myEnemyFactions[i])
                unitRelationship -= 10;
        }

        //add points if unit is in an allied faction
        for (int i = 0; i < myFriendlyFactions.Length; i++)
        {
            if (unitFaction == myFriendlyFactions[i])
                unitRelationship += 10;
        }

        //this should cause this unit to choose the party they are more allied with while ignoring neutral fights
        if (unitRelationship < 0) //&& i like the victim enough)
        {
            //flee if we are not couragous enough
            if (courage <= Random.Range(0, 100))
            {
                //Debug.Log(gameObject.name + " is fleeing!");
                Flee();
            }
            else
            {
                //Debug.Log(gameObject.name + " is fightng!");
                Fight(unit);
            }
            return true; 
        }
        return false; //not a threat
    }

    public void IAmUnderAttack(Transform attacker, Transform victim)
    {
        rm.AlertEveryoneInRange(attacker, victim); //issue here, see currentTarget

        if (doNotReact || myBrain.ActiveStates(engagedStates))
        {
            return;
        }
        else if(courage >= Random.Range(0, 100))
        {
            Fight(attacker);
        }
        else
        {
            Flee();
        }
    }

    public void ReactToViolence(Transform attacker, Transform victim)
    {
        //if we have already reacted to this person being attacked  ..or we are already fighting or fleeing, don't react
        if (doNotReact || myBrain.ActiveStates(engagedStates))
            return;

        //flee if we are not couragous enough
        if (courage < Random.Range(0, 100))
        {
            currentVictim = victim;
            Flee();
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

    public void CourageCheck()
    {
        if (courage <= Random.Range(0, 100))
        {
            Flee();
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

    private void Flee()
    {
        StartCoroutine(RunToNearestExit());       
    }

    #endregion

    #region Reaction Helper methods

    private IEnumerator RunToNearestExit()
    {
        Vector3 closestExit = GetClosestExit();
        myBrain.ToggleState(Brain.State.Fleeing, true);
        float z = transform.position.z;
        Vector3 safelyOutOfRange = new Vector3(100f, 100f, z);

        PathfindingManager.RequestPath(new PathRequest(transform.position, closestExit, unitController.OnPathFound));
        while(Vector3.Distance(transform.position, closestExit) > 0.5f)
        {
            yield return null;
        }
        transform.position = safelyOutOfRange;
        myBrain.ToggleState(Brain.State.Routed, true); //so the enemy stops chasing
        myBrain.ToggleState(Brain.State.Fleeing, false); //got away safely, stop fleeing
      
    }

    private Vector3 GetClosestExit()
    {
        List<ExitScene> exits = LevelManager.sceneExits;
        Vector3 closestExit = exits[0].transform.position; //hardcode to first ext and compare to other exit options

        for (int i = 0; i < exits.Count; i++)
        {
            Vector3 exitPos = exits[i].transform.position;
            if (Vector3.Distance(transform.position, exitPos) < Vector3.Distance(transform.position, closestExit))
            {
                closestExit = exitPos;
            }
        }

        return closestExit;
    }

    private Vector3 GetPositionOppositeOf(Transform attacker)
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

    private Node RandomNode()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        Vector3 position = new Vector3(x + Random.Range(-5.0f, 5.0f), y + Random.Range(-5.0f, 5.0f), z);
        Node node = Grid.instance.NodeAtWorldPosition(position);

        return node;
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


    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reactionRadius);
    }
    */
}
