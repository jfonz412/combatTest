public class PlayerStateMachine : UnitStateMachine {
    //used to block state changes, keep these type of bools to a minimum 
    public bool prompted = false; 
    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(UnitState.Talking, GetComponent<PlayerTalkState>());
        states.Add(UnitState.Shopping, GetComponent<PlayerShopState>());
        states.Add(UnitState.Prompted, GetComponent<PlayerPromptState>());
    }
}
