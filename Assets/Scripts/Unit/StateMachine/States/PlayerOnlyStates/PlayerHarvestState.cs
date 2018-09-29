using System.Collections;
using UnityEngine;

public class PlayerHarvestState : State {
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.FightOrFlight,
            StateMachine.States.Incapacitated,
            StateMachine.States.PlayerMove,
            StateMachine.States.InvOpen,
            StateMachine.States.BattleReportOpen,
            StateMachine.States.Paused
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
        string harvest = "Harvest";

        while (true) //can only be knocked out by another state change request
        {
            stateMachine.anim.SetTrigger(harvest);
            yield return new WaitForSeconds(1f);
        }
    }
}
