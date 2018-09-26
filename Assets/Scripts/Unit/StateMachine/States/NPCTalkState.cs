using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkState : State {
    NPCStateMachine npc;

    protected override void Init()
    {
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.FightOrFlight,
            UnitStateMachine.UnitState.Dead
        };
        npc = (NPCStateMachine)stateMachine;
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        //if we are able to enter this state, put the player their too because they requested it
        PlayerStateMachine psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;
        psm.RequestChangeState(UnitStateMachine.UnitState.Talking);

        npc.unitAnim.FaceDirection(transform.position, npc.player.position);
        ScriptToolbox.GetInstance().GetDialogueManager().StartDialogue(npc.dialogue);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        ScriptToolbox.GetInstance().GetDialogueManager().CloseDialogueWindow();
    }
}
