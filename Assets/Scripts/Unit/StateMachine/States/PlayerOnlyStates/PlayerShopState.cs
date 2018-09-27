using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopState : State
{
    PlayerStateMachine psm;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.FightOrFlight, //in case we are attacked
            UnitStateMachine.UnitState.Prompted,
            UnitStateMachine.UnitState.Dead
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        psm = (PlayerStateMachine)stateMachine;
        InventoryManager.GetInstance().GetInventoryToggle().OpenInventory();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();

        //do not knock out of shop screen if player is being prompted
        if(!psm.prompted)
            ShopInventoryUI.instance.UnitExitingShopState(true); //unit is player
    }
}
