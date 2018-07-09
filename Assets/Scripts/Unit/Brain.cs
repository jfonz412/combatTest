using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    THIS SCRIPT WILL BE USED TO PULL THE VARIOUS COMPONENTS AND SYSTEMS UNDER SPECIFIC STATES
    BRAINSTATES WILL CONTROL MOVEMENT, ATTACKING, AI AND MORE
*/
public class Brain : MonoBehaviour {
    private UnitController myMovement;
    private AttackController myCombat;
    private UnitReactions myReactions; //will eventually change this to UnitPersonality and UnitAffiliations or UnitRelationships and later UnitRoutine or something

    public enum State
    {
        Shock,
        CantBreathe,
        OvercomeByPain,
        Unconscious,
        Vomitting,
        Downed,
        Rocked,
        OvercomeByFear,
        OvercomeByRage,
        
        Neutral,
        Fighting,
        Fleeing,
        Dead,

        Shopping,
        Talking,
        Prompted,
        Paused,
        BattleReportOpen
    };

    public Dictionary<State, bool> currentStates;
    public Dictionary<State, float> stateTimers;


    // Use this for initialization
    void Start ()
    {
        myCombat = GetComponent<AttackController>();
        myMovement = GetComponent<UnitController>();
        myReactions = GetComponent<UnitReactions>();

        SetDefaultStates();
	}

    public void ToggleState(State state, bool toggle)
    {
        currentStates[state] = toggle;
    }

    public bool ActiveState(State state)
    {
        if (currentStates[state] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ActiveStates(State[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (currentStates[states[i]] == true)
            {
                return true;
            }
        }

        return false;
    }

    public void TriggerTemporaryState(State state, int severityTimer)
    {
        StartCoroutine(TimedState(state, severityTimer));
    }

    private IEnumerator TimedState(State state, float severityTimer)
    {
        ToggleState(state, true);

        while (severityTimer > 0)
        {
            severityTimer -= Time.deltaTime;
            yield return null;
        }

        ToggleState(state, false);
    }

    private void SetDefaultStates()
    {
        currentStates = new Dictionary<State, bool>()
        {
            { State.CantBreathe, false },
            { State.Downed, false },
            { State.OvercomeByFear, false },
            { State.OvercomeByPain, false },
            { State.OvercomeByRage, false },
            { State.Rocked, false },
            { State.Shock, false },
            { State.Unconscious, false },
            { State.Vomitting, false },

            { State.Dead, false },
            { State.Fighting, false },
            { State.Fleeing, false },
            { State.Neutral, false },

            { State.Paused, false },
            { State.BattleReportOpen, false },
            { State.Prompted, false },
            { State.Shopping, false },
            { State.Talking, false }
        };
    }
}
