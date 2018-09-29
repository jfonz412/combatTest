using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvOpenState : State {
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            //this is in case we are attacked while inventory is open
            StateMachine.States.Incapacitated,
            StateMachine.States.Dead
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
