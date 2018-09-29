using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : StateMachine {
    public UnitTraits unitTraits;
    public UnitRelationships unitRelationships;
    public UnitAnimController unitAnim;
    public UnitController unitController;
    public BodyPartController bodyController;
    public AttackTimer attackTimer;
    public CombatSkills combatSkills;
    public List<BodyPart> attack1Parts, attack2Parts;
    public Transform currentThreat;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        unitTraits = GetComponent<UnitTraits>();
        unitRelationships = GetComponent<UnitRelationships>();
        unitController = GetComponent<UnitController>();
        unitAnim = GetComponent<UnitAnimController>();
        attackTimer = GetComponent<AttackTimer>();
        bodyController = GetComponent<BodyPartController>();
        combatSkills = GetComponent<CombatSkills>();

        attack1Parts = bodyController.attack1Parts; //main 
        attack2Parts = bodyController.attack2Parts; //off
    }

    protected override void LoadStates()
    {
        states.Add(States.FightOrFlight, GetComponent<FightOrFlight>());
        states.Add(States.Fight, GetComponent<FightState>());
        states.Add(States.Flight, GetComponent<FlightState>());
        states.Add(States.Idle, GetComponent<UnitIdleState>());
        states.Add(States.Incapacitated, GetComponent<IncapacitatedState>());
        states.Add(States.PlayerMove, GetComponent<PlayerMoveState>());
        states.Add(States.Dead, GetComponent<DeadState>());
        states.Add(States.Paused, GetComponent<PausedState>());
        states.Add(States.InvOpen, GetComponent<InvOpenState>());
        states.Add(States.Routed, GetComponent<UnitRoutedState>());
    }

    protected override void SetDefaultState()
    {
        base.SetDefaultState();
        states[currentState].ToggleState(true);
    }

    public void TriggerTemporaryState(IncapacitatedState.TemporaryState tempState, float duration)
    {
        if (ValidTransitionState(States.Incapacitated)) //check if we can become incapacitated
        {
            IncapacitatedState s = (IncapacitatedState)states[States.Incapacitated];

            states[currentState].ToggleState(false);
            currentState = States.Incapacitated;
            s.Incapacitate(tempState, duration);         
        }
    }
}
