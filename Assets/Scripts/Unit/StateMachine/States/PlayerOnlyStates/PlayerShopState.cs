using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopState : State
{
    PlayerStateMachine psm;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.Incapacitated,
            StateMachine.States.FightOrFlight, //in case we are attacked
            StateMachine.States.Prompted,
            StateMachine.States.Dead
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
