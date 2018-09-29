using System.Collections;
using UnityEngine;

public class PlayerPromptState : State
{
    private bool waitingForInput;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Shopping
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        Time.timeScale = 0;
        waitingForInput = true;
        StartCoroutine(WaitForInput());
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        Time.timeScale = 1;
        waitingForInput = false;
        StopAllCoroutines(); //just in case
    }

    private IEnumerator WaitForInput()
    {
        while (waitingForInput)
        {
            yield return null;
        }
    }
}
