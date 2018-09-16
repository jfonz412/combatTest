using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkState : State {

    protected override void Init()
    {
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
        ScriptToolbox.GetInstance().GetDialogueManager().CloseDialogueWindow();
    }
}
