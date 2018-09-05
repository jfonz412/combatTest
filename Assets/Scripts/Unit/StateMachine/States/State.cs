using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public UnitStateMachine.UnitState[] canTransitionInto; //what this state can transition INTO
    protected UnitStateMachine stateMachine;

    protected void Start()
    {
        stateMachine = GetComponent<UnitStateMachine>(); //will hold all the other gameObject components for us
        Init();
    }

    public virtual void ToggleState(bool toggle)
    {
        if (toggle)
        {
            OnStateEnter();
        }
        else
        {
            OnStateExit();
        }
    }
    protected virtual void OnStateEnter()
    {

    }

    protected virtual void OnStateExit()
    {

    }

    protected virtual void Init()
    {
        
    }
}