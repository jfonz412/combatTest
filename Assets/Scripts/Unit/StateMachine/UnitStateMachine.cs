using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour {
    public UnitTraits unitTraits;
    public UnitRelationships unitRelationships;
    public UnitController unitController;
    public AttackController attackController;
    public Transform currentThreat;

    public enum UnitState
    {
        Shock,
        Suffocating,
        CantBreathe,
        OvercomeByPain,
        Unconscious,
        Vomitting,
        Downed,
        Rocked,
        OvercomeByFear,
        OvercomeByRage,

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
        BattleReportOpen
    };
    private Dictionary<UnitState, State> states = new Dictionary<UnitState, State>();

    UnitState currentState = UnitState.Idle;

    protected void Start()
    {
        unitTraits = GetComponent<UnitTraits>();
        unitRelationships = GetComponent<UnitRelationships>();
        unitController = GetComponent<UnitController>();
        attackController = GetComponent<AttackController>();

        LoadStates();

        //idle by default
        states[currentState].ToggleState(true);
    }

    private void LoadStates()
    {
        states.Add(UnitState.FightOrFlight, GetComponent<FightOrFlight>());
        states.Add(UnitState.Fight, GetComponent<FightState>());
        states.Add(UnitState.Flight, GetComponent<FlightState>());
        states.Add(UnitState.Idle, GetComponent<IdleState>());
    }

    public virtual void ChangeState(UnitState state)
    {
        if (ValidTransitionState(state))
        {
            states[currentState].ToggleState(false);
            currentState = state;
            states[currentState].ToggleState(true);    
        }         
    }

    //to recieve info on the threat from BodyPartController.cs
    public void ThreatInfo(Transform threat)
    {
        currentThreat = threat;
    }

    private bool ValidTransitionState(UnitState state)
    {
        UnitState[] validTransitionStates = states[currentState].canTransitionInto; //states the current state can transition to
        for (int i = 0; i < validTransitionStates.Length; i++)
        {
            if (state == validTransitionStates[i])
            {
                return true;
            }

        }
        return false;
    }
}
