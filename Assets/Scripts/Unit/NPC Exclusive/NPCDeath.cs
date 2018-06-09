using UnityEngine;

public class NPCDeath : Death {

    private NPCInteractionStates myState;

    private void Start()
    {
        myState = GetComponent<NPCInteractionStates>();
    }

    override public void StopAllCombat()
    {
        base.StopAllCombat();
        myState.SetInteractionState(NPCInteractionStates.InteractionState.Dead);
    }
}
