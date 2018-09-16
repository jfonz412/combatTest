using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State {

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        Time.timeScale = 0;
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        Time.timeScale = 1;
    }
}
