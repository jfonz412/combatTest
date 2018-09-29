using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    public Animator anim;
    public SpriteRenderer spriteRend;
    public Transform player;

    public enum States
    {
        Incapacitated,

        Idle,
        FightOrFlight,
        Fight,
        Flight,
        Routed, 
        Dead,

        Shopping,
        Talking,
        Harvesting,
        Prompted,
        Paused,
        BattleReportOpen,

        PlayerMove,
        InvOpen
    };

    protected Dictionary<States, State> states = new Dictionary<States, State>();

    public States currentState = States.Idle;

    private void Start()
    {
        LoadComponents();
        LoadStates();
        SetDefaultState();
    }

    //will be used by child classes to Start() state machine
    protected virtual void LoadComponents()
    {
        spriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
        anim = GetComponent<Animator>();
    }
    protected virtual void SetDefaultState()
    {

    }
    protected virtual void LoadStates()
    {

    }


    public virtual void RequestChangeState(States state)
    {
        //Debug.LogError("Requested change state to " + state.ToString());
        if (ValidTransitionState(state))
        {
            states[currentState].ToggleState(false);
            currentState = state;
            //Debug.LogError(state.ToString() + " is a valid transition state");
            states[currentState].ToggleState(true);
        }
        //Debug.LogError(state.ToString() + " is NOT a valid transition state for " + currentState.ToString());
    }

    protected bool ValidTransitionState(States state)
    {
        //Debug.Log(currentState.ToString());
        States[] validTransitionStates = states[currentState].canTransitionInto; //states the current state can transition to
        for (int i = 0; i < validTransitionStates.Length; i++)
        {
            //Debug.Log(gameObject.name + " " + state.ToString() + "and " + validTransitionStates[i]);
            if (state == validTransitionStates[i])
            {
                return true;
            }
            //Debug.Log(gameObject.name + " " + state.ToString() + "and " + validTransitionStates[i]); 
        }
        Debug.Log(state.ToString() + " cannot transistion from " + currentState.ToString());
        return false;
    }
}
