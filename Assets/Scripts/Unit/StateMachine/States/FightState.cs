using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : State
{
    IEnumerator engaging;
    BodyPartController targetBody;
    Transform targetTransform;
    List<BodyPart> attack1Parts, attack2Parts;
    UnitStateMachine usm;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.Incapacitated,
            StateMachine.States.PlayerMove
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        usm = (UnitStateMachine)stateMachine;

        engaging = Engage(usm.currentThreat);
        StartCoroutine(engaging);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        usm.currentThreat = null; 
        StopCoroutine(engaging);
        usm.unitController.StopMoving();
    }


#region Fight
    private IEnumerator Engage(Transform _targetTransform)
    {
        //Debug.LogError(gameObject.name + " is engaging " + _targetTransform.name);
        bool inRange;
        targetTransform = _targetTransform;
        targetBody = targetTransform.GetComponent<BodyPartController>();
        attack1Parts = usm.attack1Parts;
        attack2Parts = usm.attack2Parts;

        UnitController uc = usm.unitController;
        BodyPartController opponent = targetTransform.GetComponent<BodyPartController>();
        Collider2D col = GetComponent<Collider2D>();

        usm.unitAnim.FaceDirection(transform.position, targetTransform.position);
        
        while (!opponent.Incapacitated())
        {
            //eventually this will be a function that will check if ranged or melee, then decide if in range or not
            //for ranged we can use a raycast of some sort to check for obstabcles
            inRange = col.IsTouching(targetTransform.GetComponent<Collider2D>());
            
            if (!inRange)
            {
                PathfindingManager.RequestPath(new PathRequest(transform.position, targetTransform.position, uc.OnPathFound));
                yield return new WaitForSeconds(.1f);
            }
            else if (!usm.attackTimer.coolingDown)
            {
                Attack();
            }
            yield return null;
        }
        usm.RequestChangeState(StateMachine.States.Idle);
        yield break;
    }

    private void Attack()
    {

        AttackInfo myAttack;
        AttackTimer attackTimer = usm.attackTimer;
        BodyPartController myBody = usm.bodyController;
        CombatSkills mySkills = usm.combatSkills;

        if (Random.Range(0, 100) <= 75)
        {
            if (!myBody.PartTooInjured(attack1Parts))
            {
                AttackAnimation();
                myAttack = mySkills.RequestAttackInfo(attack1Parts[0].MyWeapon());
            }
            else
            {
                string line = "<color=red>" + gameObject.name + " is too injured to attack with their " + attack1Parts[0].MyWeapon().name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer to prevent attack
                                                  //unit reactions -> run away if not player?
                return;
            }
        }
        else
        {
            if (!myBody.PartTooInjured(attack2Parts))
            {
                AttackAnimation();
                myAttack = mySkills.RequestAttackInfo(attack2Parts[0].MyWeapon());
            }
            else
            {

                string line = "<color=red>" + gameObject.name + " is too injured to attack with their " + attack2Parts[0].MyWeapon().name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer to prevent attack
                                                  //unit reactions -> run away if not player?
                return;
            }
        }
        Debug.Log(gameObject.name + " attacking with reset timer of " + myAttack.speed);
        targetBody.RecieveAttack(myAttack, transform); //should reset back to whatever weapon was used
        attackTimer.ResetAttackTimer(myAttack.speed);
    }

    private void AttackAnimation()
    {
        usm.unitController.StopMoving();

        if (targetTransform != null)
            usm.unitAnim.FaceDirection(transform.position, targetTransform.position);

        usm.unitAnim.Attack(); 
    }
    #endregion
}
