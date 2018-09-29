using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdleState : State
{
    //race cases causing me to ensure these are here before corotunie kicks off
    UnitStateMachine usm;
    UnitRelationships r;
    UnitTraits t;


    public bool wander;
    public float wanderRadiusMax = 1f;
    public float reactionRadius = 10f;
    public bool doNotReact = false;

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        usm = (UnitStateMachine)stateMachine;

        if (wander)
        {
            StartCoroutine(Wander());
        }
        if (!doNotReact)
        {
            StartCoroutine(ScanArea());
        }
    }

    protected override void OnStateExit()
    {
        base.OnStateEnter();
        StopAllCoroutines();
    }

    //should split this class up to shorten this array
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.FightOrFlight,
            StateMachine.States.Incapacitated,
            StateMachine.States.PlayerMove,
            StateMachine.States.InvOpen,
            StateMachine.States.Paused,
            StateMachine.States.Talking,
            StateMachine.States.Shopping,
            StateMachine.States.Harvesting
        };
    }

    private IEnumerator ScanArea()
    {
        while (usm.unitRelationships == null || usm.unitTraits == null)
        {
            yield return new WaitForSeconds(2f);
        }

        r = usm.unitRelationships;
        t = usm.unitTraits;

        while (true)
        {
            if (EnemyDetected())
            {
                stateMachine.RequestChangeState(StateMachine.States.FightOrFlight);
                break;
            }
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }

    #region Check Surroundings
    public bool EnemyDetected()
    {
        Collider2D[] unitsInVicinity = Physics2D.OverlapCircleAll(transform.position, reactionRadius, 1);
        //Debug.LogError(LayerMask.GetMask(new string[] { "Default" }));

        for (int n = 0; n < unitsInVicinity.Length; n++)
        {
            if (CheckThreatLevel(unitsInVicinity[n].transform))
            {
                //Debug.LogError("Found a threat");
                return true;
            }
        }
        return false;
    }

    private bool CheckThreatLevel(Transform unit)
    {
        UnitRelationships ur = unit.GetComponent<UnitRelationships>();
        if (ur == null) //not a collider with relationships or an incapacitated unit
            return false;

        //figure out my relationship with the unit
        UnitRelationships.Factions unitFaction = ur.myFaction;
        int unitRelationship = 0;

        //dock unit if they are in an enemy faction

        for (int i = 0; i < r.myEnemyFactions.Length; i++)
        {
            if (unitFaction == r.myEnemyFactions[i])
                unitRelationship -= 10;
        }

        //add points if unit is in an allied faction
        for (int i = 0; i < r.myFriendlyFactions.Length; i++)
        {
            if (unitFaction == r.myFriendlyFactions[i])
                unitRelationship += 10;
        }

        Debug.Log(gameObject.name + "'s relationship with " + unit.name + " is " + unitRelationship);
        //this should cause this unit to choose the party they are more allied with while ignoring neutral fights
        if (unitRelationship < 0)
        {
            if (!unit.GetComponent<BodyPartController>().Incapacitated())
            {
                usm.currentThreat = unit; //set threat in case we decide to fight
                AlertOthers();
                return true;
            }
        }
        return false; //not a threat
    }

    private void AlertOthers()
    {
        //check who is in the vicinity
        //alert all friends
        //this will simulate "sound" once we move from circle radius' to cones (proper stealth)
    }
    #endregion

    #region Wander
    private IEnumerator Wander()
    {
        while (true)
        {
            Vector3 lastPos = transform.position;
            PathfindingManager.RequestPath(new PathRequest(transform.position, RandomWanderSpot(), usm.unitController.OnPathFound));
            //Debug.Log(gameObject + " is wandering");
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
            PathfindingManager.RequestPath(new PathRequest(transform.position, lastPos, usm.unitController.OnPathFound));
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }

    }

    private Vector3 RandomWanderSpot()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        return new Vector3(x + Random.Range(-wanderRadiusMax, wanderRadiusMax), y + Random.Range(-wanderRadiusMax, wanderRadiusMax), z);
    }
    #endregion
}
