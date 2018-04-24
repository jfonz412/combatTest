using UnityEngine;

public class NPCInteractionStates : MonoBehaviour {

    //public bool isDead = false;
    public enum InteractionState { FleeingPlayer, FleeingNPC, FightingPlayer, FightingNPC, Neutral, Trading, Talking, Dead };
    InteractionState currentState = InteractionState.Neutral; //default
    NPCInteraction npc;

    void Start()
    {
        npc = GetComponent<NPCInteraction>();
    }

    #region States
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
                break;
            case InteractionState.FightingNPC:
                currentState = newState;
                break;
            case InteractionState.FleeingPlayer:
                currentState = newState;
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
    #endregion

    #region StateReactions

    public void CheckNPCInteractionState(string interaction)
    {
        if (currentState == InteractionState.Neutral)
        {
            npc.NeutralInteractions(interaction);
        }
        else if (currentState == InteractionState.FightingPlayer)
        {
            npc.FightingPlayerInteractions(interaction);
        }
        else if (currentState == InteractionState.FleeingPlayer)
        {
            npc.FleeingPlayerInteractions(interaction);
        }
        else if (currentState == InteractionState.FightingNPC)
        {
            npc.FightingNPCInteractions(interaction);
        }
        else if (currentState == InteractionState.FleeingNPC)
        {
            npc.FleeingNPCInteractions(interaction);
        }
        else if (currentState == InteractionState.Talking)
        {
            Debug.Log("Talking NPCInteraction state not implemented");
        }
        else if (currentState == InteractionState.Trading)
        {
            Debug.Log("Trading NPCInteraction state not implemented");
        }
        else if (currentState == InteractionState.Dead)
        {
            return;
        }
    }
    #endregion
}
