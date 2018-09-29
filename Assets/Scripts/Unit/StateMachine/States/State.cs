using UnityEngine;

public class State : MonoBehaviour
{
    public StateMachine.States[] canTransitionInto; //what this state can transition INTO
    protected StateMachine stateMachine;

    protected void Awake()
    {
        stateMachine = GetComponent<StateMachine>(); //will hold all the other gameObject components for us
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