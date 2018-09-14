using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    [HideInInspector]
    public Transform lastKnownTarget = null;

    private IEnumerator engagingEntity;

    private BodyPartController targetBody;
    private BodyPartController myBody;
    private Brain myBrain;

    private CombatSkills mySkills;

    private UnitAnimController anim;
    private UnitController unit;
    private AttackTimer attackTimer;

    private List<BodyPart> attack1Parts, attack2Parts;

    private Brain.State[] impairingStates = new Brain.State[]
    {
        Brain.State.Downed,
        Brain.State.Rocked,
        Brain.State.Shock,
        Brain.State.Unconscious,
        Brain.State.Vomitting,
        Brain.State.CantBreathe,
        Brain.State.BattleReportOpen,
        Brain.State.Dead,
        Brain.State.Suffocating
    };

    private Brain.State[] neutralized = new Brain.State[]
    {
        Brain.State.Unconscious,
        Brain.State.Shock,
        Brain.State.Dead,
        Brain.State.Suffocating,
        Brain.State.Fleeing, //MAY WANT TO REMOVE BUT FOR NOW WILL FORCE PRIORITIZATION OF MORE AGRESSIVE ENEMIES
        Brain.State.Routed
    };

    private void Start()
    {
        mySkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();
        unit = GetComponent<UnitController>();
        attackTimer = GetComponent<AttackTimer>();
        myBody = GetComponent<BodyPartController>();
        myBrain = GetComponent<Brain>();

        GetAttackPartReferences();
    }

    private void GetAttackPartReferences()
    {
        attack1Parts = myBody.attack1Parts; //main 
        attack2Parts = myBody.attack2Parts; //off
    }

    //How the player object talks to this script
    public void EngageTarget(bool hasTarget, Transform targetTransform = null)
    {
        if (hasTarget && !Impaired())
        {
            lastKnownTarget = targetTransform;
            StopEngagingEnemy();
            EngageNewEnemy(targetTransform);
        }
        else
        {
            StopEngagingEnemy();
            myBrain.ToggleState(Brain.State.Fighting, false);
            lastKnownTarget = null;
        }
    }

    /****************** PRIVATE FUNCTIONS **************************/
    private IEnumerator MoveToEngagement(Transform targetTransform)
    {
        bool inRange;
        myBrain.ToggleState(Brain.State.Fighting, true);
        Brain opponent = targetTransform.GetComponent<Brain>();
        Collider2D c = GetComponent<Collider2D>();
       
        anim.FaceDirection(transform.position, targetTransform.position);

        while (!opponent.ActiveStates(neutralized) && !myBrain.ActiveState(Brain.State.Fleeing))
        {
            //eventually this will be a function that will check if ranged or melee, then decide if in range or not
            inRange = c.IsTouching(targetTransform.GetComponent<Collider2D>()); //this would just be for melee
            //else in range will a raycast of some sort to check for obstabcles
            if (!inRange) 
            { 
                lastKnownTarget = targetTransform;
                
                PathfindingManager.RequestPath(new PathRequest(transform.position, lastKnownTarget.position, unit.OnPathFound));
                yield return new WaitForSeconds(.1f);
            }
            else if(!Impaired() && !attackTimer.coolingDown)
            {
                Attack(targetTransform);
            }
            yield return null;
        }

        myBrain.ToggleState(Brain.State.Fighting, false);
        yield break;
    }

    #region Helper Functions
    private void Attack(Transform targetTransform)
    {
        AttackInfo myAttack;

        if (Random.Range(0, 100) <= 75)
        {
            if (!myBody.PartTooInjured(attack1Parts))
            {
                AttackAnimation(targetTransform);
                myAttack = mySkills.RequestAttackInfo(attack1Parts[0].MyWeapon());
            }
            else
            {
                string line = "<color=red>" + gameObject.name + " is too injured to attack with their " + attack1Parts[0].MyWeapon().name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer
                                                  //unit reactions -> run away if not player?

                return;
            }
        }
        else
        {
            if (!myBody.PartTooInjured(attack2Parts))
            {
                AttackAnimation(targetTransform);
                myAttack = mySkills.RequestAttackInfo(attack2Parts[0].MyWeapon());
            }
            else
            {

                string line = "<color=red>" + gameObject.name + " is too injured to attack with their " + attack2Parts[0].MyWeapon().name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer
                                                  //unit reactions -> run away if not player?
                return;
            }
        }

        targetBody.RecieveAttack(myAttack, transform); //should reset back to whatever weapon was used
        attackTimer.ResetAttackTimer(myAttack.speed);
    }


    private void AttackAnimation(Transform targetTransform)
    {
        unit.StopMoving();

        if(targetTransform != null)
            anim.FaceDirection(transform.position, targetTransform.position);

        anim.Attack(); //equippedWeapon.attackType
    }

    private bool Impaired()
    {
        if (myBrain.ActiveStates(impairingStates))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StopEngagingEnemy()
    {
        if (engagingEntity != null)
        {
            StopCoroutine(engagingEntity);
        }
    }

    private void EngageNewEnemy(Transform targetTransform)
    {
        targetBody = lastKnownTarget.GetComponent<BodyPartController>();
        engagingEntity = MoveToEngagement(targetTransform);

        StartCoroutine(engagingEntity);
    }

    #endregion
}

