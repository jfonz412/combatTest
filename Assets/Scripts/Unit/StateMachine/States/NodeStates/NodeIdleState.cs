
public class NodeIdleState : State
{
    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Harvesting
        };
    }
    protected override void OnStateEnter()
    {
        base.OnStateEnter();

    }

    protected override void OnStateExit()
    {
        base.OnStateEnter();
    }  
}
