using UnityEngine;

public class PlayerStateMachine : UnitStateMachine {
    //used to block state changes, keep these type of bools to a minimum 
    public bool prompted = false; 
    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(States.Talking, GetComponent<PlayerTalkState>());
        states.Add(States.Shopping, GetComponent<PlayerShopState>());
        states.Add(States.Prompted, GetComponent<PlayerPromptState>());
        states.Add(States.Harvesting, GetComponent<PlayerHarvestState>());
    }

    //for the player right now but may come in handy if NPCs ever need to know clickPos
    public struct ClickInfo
    {
        public string clickType;
        public Vector3 mousePos;
        public string interaction;
        public Interactable interactable;
    }

    public ClickInfo clickInfo; //player controller will make one of these and send it here

}
