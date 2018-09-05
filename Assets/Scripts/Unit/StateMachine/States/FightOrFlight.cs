using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOrFlight : State
{
    protected override void OnStateEnter()
    {
        base.OnStateEnter();

        if (stateMachine.unitTraits.courage <= Random.Range(0, 100)) //also check who's around to determine any other threats
        {
            stateMachine.ChangeState(UnitStateMachine.UnitState.Flight);
        }
        else
        {
            stateMachine.ChangeState(UnitStateMachine.UnitState.Fight);
        }

    }

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            //UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Fight,
            UnitStateMachine.UnitState.Flight,
        };
    }
}

