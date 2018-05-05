using System.Collections;
using UnityEngine;

public class NPCDeath : Death {

    private NPCInteractionStates myState;

    private void Start()
    {
        myState = GetComponent<NPCInteractionStates>();
    }

    override public void StopAllCombat(Transform myAttacker)
    {
        base.StopAllCombat(myAttacker);
        myState.SetInteractionState(NPCInteractionStates.InteractionState.Dead);
    }
}
