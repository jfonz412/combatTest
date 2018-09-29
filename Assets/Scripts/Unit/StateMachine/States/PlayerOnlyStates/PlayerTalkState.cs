public class PlayerTalkState : State
{

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.Incapacitated,
            StateMachine.States.FightOrFlight,
            StateMachine.States.Dead
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
