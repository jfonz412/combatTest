public class PlayerTalkState : State
{

    protected override void Init()
    {
        base.Init();
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
        ScriptToolbox.GetInstance().GetDialogueManager().UnitExitingDialogueState(true);
    }
}
