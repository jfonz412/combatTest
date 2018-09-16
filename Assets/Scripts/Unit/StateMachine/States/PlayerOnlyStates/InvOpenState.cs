using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvOpenState : State {
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            //this is in case we are attacked while inventory is open
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.Dead
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        InventoryManager.GetInstance().GetInventoryToggle().OpenInventory();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        InventoryManager.GetInstance().GetInventoryToggle().CloseInventory();
    }
}
