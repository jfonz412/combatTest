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
            StateMachine.States.Dead
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

        while (psm.currentState == StateMachine.States.Harvesting && harvestTimer > 0)
        {
            stateMachine.anim.SetTrigger(shake); //something here is null
            yield return new WaitForSeconds(1f);
            harvestTimer -= 1;
            Debug.Log("Harvest timer is " + harvestTimer);
        }

        stateMachine.anim.ResetTrigger(shake);
        psm.RequestChangeState(StateMachine.States.Idle);
        stateMachine.RequestChangeState(StateMachine.States.Dead);
        yield break;
    }
}