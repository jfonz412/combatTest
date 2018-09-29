using UnityEngine;

public class FightOrFlight : State
{
    protected override void OnStateEnter()
    {
        base.OnStateEnter();

        UnitStateMachine usm = (UnitStateMachine)stateMachine;
        if (usm.unitTraits.courage <= Random.Range(0, 100)) //also check who's around to determine any other threats
        {
            stateMachine.RequestChangeState(StateMachine.States.Flight);
        }
        else
        {
            stateMachine.RequestChangeState(StateMachine.States.Fight);
        }

    }

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Idle,
            StateMachine.States.Fight,
            StateMachine.States.Flight,
            StateMachine.States.Incapacitated
        };
    }
}

