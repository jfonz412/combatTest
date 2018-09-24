using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : State
{
    IEnumerator engaging;
    BodyPartController targetBody;
    Transform targetTransform;
    List<BodyPart> attack1Parts, attack2Parts;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.PlayerMove
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        
        engaging = Engage(stateMachine.currentThreat);
        StartCoroutine(engaging);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        stateMachine.currentThreat = null; 
        StopCoroutine(engaging);
        stateMachine.unitController.StopMoving();
    }


#region Fight
    private IEnumerator Engage(Transform _targetTransform)
    {
        //Debug.LogError(gameObject.name + " is engaging " + _targetTransform.name);
        bool inRange;
        targetTransform = _targetTransform;
        targetBody = targetTransform.GetComponent<BodyPartController>();
        attack1Parts = stateMachine.attack1Parts;
        attack2Parts = stateMachine.attack2Parts;

        UnitController uc = stateMachine.unitController;
        BodyPartController opponent = targetTransform.GetComponent<BodyPartController>();
        Collider2D col = GetComponent<Collider2D>();

        stateMachine.unitAnim.FaceDirection(transform.position, targetTransform.position);
        
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
            else if (!stateMachine.attackTimer.coolingDown)
            {
                Attack();
            }
            yield return null;
        }
        stateMachine.RequestChangeState(UnitStateMachine.UnitState.Idle);
        yield break;
    }

    private void Attack()
    {

        AttackInfo myAttack;
        AttackTimer attackTimer = stateMachine.attackTimer;
        BodyPartController myBody = stateMachine.bodyController;
        CombatSkills mySkills = stateMachine.combatSkills;

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
        stateMachine.unitController.StopMoving();

        if (targetTransform != null)
            stateMachine.unitAnim.FaceDirection(transform.position, targetTransform.position);

        stateMachine.unitAnim.Attack(); 
    }
    #endregion
}
