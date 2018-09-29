public class NodeStateMachine : StateMachine
{
    protected override void LoadStates()
    {
        //base.LoadStates();
        //only need states relevant to the node
        states.Add(States.Harvesting, GetComponent<NodeHarvestState>());
        states.Add(States.Idle, GetComponent<NodeIdleState>());
        states.Add(States.Dead, GetComponent<NodeHarvestedState>());
    }
}
