using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : UnitStateMachine {
    protected override void LoadStates()
    {
        base.LoadStates();
        //override this state with player talking state
        states.Add(UnitState.Talking, GetComponent<PlayerTalkState>());
    }
}
