public class NPCTalkState : State {
    NPCStateMachine npc;

    protected override void Init()
    {
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.Incapacitated,
            StateMachine.States.FightOrFlight,
            StateMachine.States.Dead
        };
        npc = (NPCStateMachine)stateMachine;
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        //if we are able to enter this state, put the player their too because they requested it
        PlayerStateMachine psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;
        psm.RequestChangeState(StateMachine.States.Talking);

        npc.unitAnim.FaceDirection(transform.position, npc.player.position);
        ScriptToolbox.GetInstance().GetDialogueManager().StartDialogue(npc.dialogue);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        ScriptToolbox.GetInstance().GetDialogueManager().UnitExitingDialogueState();
    }
}
