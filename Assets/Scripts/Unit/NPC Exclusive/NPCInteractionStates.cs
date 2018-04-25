using UnityEngine;

public class NPCInteractionStates : MonoBehaviour {

    //public bool isDead = false;
    public enum InteractionState { FleeingPlayer, FleeingNPC, FightingPlayer, FightingNPC, Neutral, Trading, Talking, Dead };
    InteractionState currentState = InteractionState.Neutral; //default
    NPCInteraction npc;

    InteractionState[] validDialogueStates = new InteractionState[] { InteractionState.Neutral };
    InteractionState[] validTradeStates = new InteractionState[]    { InteractionState.Neutral };

    InteractionState[] validInspectStates = new InteractionState[]  { InteractionState.Neutral,
                                                                      InteractionState.FightingPlayer,
                                                                      InteractionState.FleeingPlayer,
                                                                      InteractionState.FightingNPC,
                                                                      InteractionState.FleeingNPC,
                                                                      InteractionState.Dead};

    InteractionState[] validAttackStates = new InteractionState[]   { InteractionState.Neutral,
                                                                      InteractionState.FightingPlayer,
                                                                      InteractionState.FleeingPlayer,
                                                                      InteractionState.FightingNPC,
                                                                      InteractionState.FleeingNPC };

    void Start()
    {
        npc = GetComponent<NPCInteraction>();
    }

    public void SetInteractionState(InteractionState newState)
    {
        switch (newState)
        {
            case InteractionState.Neutral:
                currentState = newState;
                break;
            case InteractionState.Talking:
                currentState = newState;
                break;
            case InteractionState.FightingPlayer:
                currentState = newState;
                npc.defaultInteraction = Interactable.DefaultInteractions.Attack;
                Debug.Log(npc.defaultInteraction);
                break;
            case InteractionState.FightingNPC:
                currentState = newState;
                break;
            case InteractionState.FleeingPlayer:
                currentState = newState;
                npc.defaultInteraction = Interactable.DefaultInteractions.Attack;
                Debug.Log(npc.defaultInteraction);
                break;
            case InteractionState.FleeingNPC:
                currentState = newState;
                break;
            case InteractionState.Trading:
                currentState = newState;
                break;
            case InteractionState.Dead:
                currentState = newState;
                break;
            default:
                Debug.LogError("Unknown player state");
                break;
        }
        //Debug.Log(name + " is: " + currentState);
    }

    public bool ValidateInteraction(string interaction)
    {
        if (interaction == "Attack")
        {
            //Debug.Log("Checking attack");
            return CheckForValidState(validAttackStates);
        }
        else if(interaction == "Talk")
        {
            //Debug.Log("Checking talk");
            return CheckForValidState(validDialogueStates);
        }
        else if (interaction == "Trade")
        {
            //Debug.Log("Checking trade");
            return CheckForValidState(validTradeStates);
        }
        else if (interaction == "Inspect")
        {
            //Debug.Log("Checking Inspect");
            return CheckForValidState(validInspectStates);
        }
        return false;
    }

    bool CheckForValidState(InteractionState[] validStates)
    {
        for (int i = 0; i < validStates.Length; i++)
        {
            if (currentState == validStates[i])
            {
                return true;
            }
        }
        Debug.Log("You cannot do this right now");
        return false;
    }
}
