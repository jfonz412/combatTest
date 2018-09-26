public class NPCStateMachine : UnitStateMachine
{
    public Dialogue dialogue;
    public LoadShop myShop;

    protected override void Start()
    {
        base.Start();
        myShop = GetComponent<LoadShop>();
    }
    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(UnitState.Talking, GetComponent<NPCTalkState>());
        states.Add(UnitState.Shopping, GetComponent<NPCShopState>());
    }
}
