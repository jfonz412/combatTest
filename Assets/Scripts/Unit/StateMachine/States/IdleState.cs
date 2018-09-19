using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
    IEnumerator wandering;
    public bool wander;

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        wandering = Wander();
        StartCoroutine(wandering);
    }

    protected override void OnStateExit()
    {
        base.OnStateEnter();
        StopCoroutine(wandering);
    }

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.FightOrFlight,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.PlayerMove,
            UnitStateMachine.UnitState.InvOpen,
            UnitStateMachine.UnitState.Paused,
            UnitStateMachine.UnitState.Talking,
            UnitStateMachine.UnitState.Shopping
        };
    }

    #region Wander
    private IEnumerator Wander()
    {
        if (wander)
        {
            if (stateMachine == null)
                stateMachine = GetComponent<UnitStateMachine>();

            while (true)
            {
                PathfindingManager.RequestPath(new PathRequest(transform.position, RandomWanderSpot(), stateMachine.unitController.OnPathFound));
                Debug.Log(gameObject + " is wandering");
                yield return new WaitForSeconds(3f);
            }
        }
    }

    private Vector3 RandomWanderSpot()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        return new Vector3(x + Random.Range(-1.0f, 1.0f), y + Random.Range(-1.0f, 1.0f), z);
    }
    #endregion
}
