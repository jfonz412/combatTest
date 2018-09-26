using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : UnitStateMachine
{
    public Dialogue dialogue;

    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(UnitState.Talking, GetComponent<NPCTalkState>());
        states.Add(UnitState.Shopping, GetComponent<NPCShopState>());
    }
}
