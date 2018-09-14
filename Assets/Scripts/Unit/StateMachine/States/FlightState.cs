using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightState : State
{
    IEnumerator flee;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Incapacitated,
            //UnitStateMachine.UnitState.FightOrFlight
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        flee = Flee();
        StartCoroutine(flee);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        StopCoroutine(flee); 
        stateMachine.unitController.StopMoving();
    }

    #region Flight
    private IEnumerator Flee()
    {
        Vector3 closestExit = GetClosestExit();
        float z = transform.position.z;
        Vector3 safelyOutOfRange = new Vector3(100f, 100f, z);

        PathfindingManager.RequestPath(new PathRequest(transform.position, closestExit, stateMachine.unitController.OnPathFound));
        while (Vector3.Distance(transform.position, closestExit) > 0.5f)
        {
            yield return null;
        }
        transform.position = safelyOutOfRange;
        stateMachine.RequestChangeState(UnitStateMachine.UnitState.Idle);
    }

    private Vector3 GetClosestExit()
    {
        List<ExitScene> exits = LevelManager.sceneExits;
        Vector3 closestExit = exits[0].transform.position; //hardcode to first ext and compare to other exit options

        for (int i = 0; i < exits.Count; i++)
        {
            Vector3 exitPos = exits[i].transform.position;
            if (Vector3.Distance(transform.position, exitPos) < Vector3.Distance(transform.position, closestExit))
            {
                closestExit = exitPos;
            }
        }

        return closestExit;
    }
    #endregion
}
