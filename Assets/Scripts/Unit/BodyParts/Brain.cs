using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {
    public bool isDead { get { return currentStates[State.Dead]; } set { ToggleState(State.Dead, value); } } 

    public enum State
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

        Neutral,
        Fighting,
        Fleeing,
        Routed, // "left" the area
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
    void Start()
    {

        ResetStates();
    }

    public void ToggleState(State state, bool toggle)
    {
        if(currentStates == null)
        {
            ResetStates();
        }
        currentStates[state] = toggle;
    }

    public void TriggerTemporaryState(State state, int severityTimer)
    {
        StartCoroutine(TimedState(state, severityTimer));
    }

    public void Die()
    {
        ResetStates();
        currentStates[State.Dead] = true;
        GetComponent<Death>().Die();
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

    private void ResetStates()
    {
        currentStates = new Dictionary<State, bool>()
        {
            { State.Suffocating, false },
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
            { State.Routed, false },
            { State.Neutral, false },

            { State.Paused, false },
            { State.BattleReportOpen, false },
            { State.Prompted, false },
            { State.Shopping, false },
            { State.Talking, false }
        };
    }
}