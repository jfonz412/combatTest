using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHarvestState : State
{
    public float harvestTimer = 5f;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Dead,
            StateMachine.States.Idle
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        StartCoroutine(HarvestNode());
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        StopAllCoroutines();
    }

    private IEnumerator HarvestNode()
    {
        PlayerStateMachine psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;
        string shake = "Shake";

        //while player is harvesting, count down timer
        while (psm.currentState == StateMachine.States.Harvesting)
        {
            stateMachine.anim.SetTrigger(shake); //something here is null
            yield return new WaitForSeconds(1f);
            harvestTimer -= 1;

            if(harvestTimer == 0) //if timer hits 0, die and send player to idle, exit coroutine
            {
                stateMachine.anim.ResetTrigger(shake);
                stateMachine.RequestChangeState(StateMachine.States.Dead);
                psm.RequestChangeState(StateMachine.States.Idle);
                yield return null;
            }
        }

        //if while loop is ended by player changing states, go back to idle because player was interrupted
        stateMachine.anim.ResetTrigger(shake);
        stateMachine.RequestChangeState(StateMachine.States.Idle);
        yield break;
    }
}