using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShopState : State
{
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.FightOrFlight,
            UnitStateMachine.UnitState.Dead
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        ShopInventoryUI.instance.HardShopExit();
    }
}

