
public class FightState : State
{
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle
            //UnitStateMachine.UnitState.FightOrFlight
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        stateMachine.attackController.EngageTarget(true, stateMachine.currentThreat);
        //StartCoroutine(Engage(currentThreat));
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
    }
}
