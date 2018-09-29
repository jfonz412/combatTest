public class NPCStateMachine : UnitStateMachine
{
    public Dialogue dialogue;
    public LoadShop myShop;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        myShop = GetComponent<LoadShop>();
        dialogue.unit = this;
    }
    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(States.Talking, GetComponent<NPCTalkState>());
        states.Add(States.Shopping, GetComponent<NPCShopState>());
    }
}
