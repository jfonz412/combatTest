using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
    IEnumerator wandering;

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
            UnitStateMachine.UnitState.FightOrFlight
        };
    }

    #region Wander
    private IEnumerator Wander()
    {
        //float z = transform.position.z;
        //Vector3 safelyOutOfRange = new Vector3(100f, 100f, z);      
        while (true)
        {
            //PathfindingManager.RequestPath(new PathRequest(transform.position, closestExit, stateMachine.unitController.OnPathFound));
            Debug.Log(gameObject + " is wandering");
            yield return new WaitForSeconds(3f);
        }
    }
    #endregion
}
