using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour {
    //might be able to move all of this into a UnitToolbox
    public UnitTraits unitTraits;
    public UnitRelationships unitRelationships;
    public UnitAnimController unitAnim;
    public Animator anim;
    public UnitController unitController;
    public BodyPartController bodyController;
    //public AttackController attackController;
    public AttackTimer attackTimer;
    public CombatSkills combatSkills;
    public SpriteRenderer spriteRend;
    public Transform player;
    public List<BodyPart> attack1Parts, attack2Parts;
    public Transform currentThreat;

    //for the player right now but may come in handy if NPCs ever need to know clickPos
    public struct ClickInfo
    {
        public string clickType;
        public Vector3 mousePos;
        public string interaction;
        public Interactable interactable;
    }

    public ClickInfo clickInfo; //player controller will make one of these and send it here

    public enum UnitState
    {
        Incapacitated,

        Idle,
        FightOrFlight,
        Fight,
        Flight,
        Routed, // "left" the area
        Dead,

        Shopping,
        Talking,
        Prompted,
        Paused,
        BattleReportOpen,

        PlayerMove,
        InvOpen
    };
    protected Dictionary<UnitState, State> states = new Dictionary<UnitState, State>();

    public UnitState currentState = UnitState.Idle;

    protected virtual void Start()
    {
        unitTraits = GetComponent<UnitTraits>();
        unitRelationships = GetComponent<UnitRelationships>();
        unitController = GetComponent<UnitController>();
        unitAnim = GetComponent<UnitAnimController>();
        anim = GetComponent<Animator>();
        attackTimer = GetComponent<AttackTimer>();
        bodyController = GetComponent<BodyPartController>();
        combatSkills = GetComponent<CombatSkills>();
        spriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;

        LoadStates();

        attack1Parts = bodyController.attack1Parts; //main 
        attack2Parts = bodyController.attack2Parts; //off

        //idle by default
        states[currentState].ToggleState(true);
    }

    protected virtual void LoadStates()
    {
        states.Add(UnitState.FightOrFlight, GetComponent<FightOrFlight>());
        states.Add(UnitState.Fight, GetComponent<FightState>());
        states.Add(UnitState.Flight, GetComponent<FlightState>());
        states.Add(UnitState.Idle, GetComponent<IdleState>());
        states.Add(UnitState.Incapacitated, GetComponent<IncapacitatedState>());
        states.Add(UnitState.PlayerMove, GetComponent<PlayerMoveState>());
        states.Add(UnitState.Dead, GetComponent<DeadState>());
        states.Add(UnitState.Paused, GetComponent<PausedState>());
        states.Add(UnitState.InvOpen, GetComponent<InvOpenState>());
    }

    public virtual void RequestChangeState(UnitState state)
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

    public void TriggerTemporaryState(IncapacitatedState.TemporaryState tempState, float duration)
    {
        if (ValidTransitionState(UnitState.Incapacitated)) //check if we can become incapacitated
        {
            IncapacitatedState s = (IncapacitatedState)states[UnitState.Incapacitated];

            states[currentState].ToggleState(false);
            currentState = UnitState.Incapacitated;
            s.Incapacitate(tempState, duration);         
        }
    }

    private bool ValidTransitionState(UnitState state)
    {
        //Debug.Log(currentState.ToString());
        UnitState[] validTransitionStates = states[currentState].canTransitionInto; //states the current state can transition to
        for (int i = 0; i < validTransitionStates.Length; i++)
        {
            Debug.Log(gameObject.name + " " + state.ToString() + "and " + validTransitionStates[i]);
            if (state == validTransitionStates[i])
            {
                return true;
            }
            //Debug.Log(gameObject.name + " " + state.ToString() + "and " + validTransitionStates[i]); 
        }
        return false;
    }
}
