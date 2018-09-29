using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******NOT CURRENTLY USED*********/
public class PausedState : State {

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle
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
