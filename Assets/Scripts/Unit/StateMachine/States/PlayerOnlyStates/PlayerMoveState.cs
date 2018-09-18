using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State {
    private IEnumerator movingToInteraction;
    UnitController uc;
    UnitStateMachine.ClickInfo clickInfo;
    public GameObject clickMarker;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            UnitStateMachine.UnitState.Idle,
            UnitStateMachine.UnitState.Talking,
            UnitStateMachine.UnitState.Incapacitated,
            UnitStateMachine.UnitState.InvOpen,
            UnitStateMachine.UnitState.PlayerMove,
            //UnitStateMachine.UnitState.FightOrFlight, //we don't want the player to automatically react to attacks
            UnitStateMachine.UnitState.Paused,
            UnitStateMachine.UnitState.Fight,
            UnitStateMachine.UnitState.Flight,
            UnitStateMachine.UnitState.Dead,
            UnitStateMachine.UnitState.Shopping
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        uc = stateMachine.unitController;
        clickInfo = stateMachine.clickInfo;
        ProcessClick();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        StopAllCoroutines();
    }

    #region Process Clicks

    private void ProcessClick()
    {
        ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus();

        if (clickInfo.clickType == "leftClick")
        {
            ProcessLeftClick();
        }
        else
        {
            InteractWithInteractable(clickInfo.interaction, clickInfo.interactable);
        }
    }

    private void ProcessLeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(clickInfo.mousePos, Vector2.zero);

        CheckForCollider(hit, clickInfo.mousePos); //will move to Pos if collider not found
        Instantiate(clickMarker, clickInfo.mousePos, Quaternion.identity);
    }

    #endregion

    #region Click Helpers

    // LEFT CLICK HELPERS
    private void CheckForCollider(RaycastHit2D hit, Vector3 location)
    {
        Collider2D collider = hit.collider;
        if (collider) //if collider is found
        {
            CheckCollider(collider, location);
        }
        else
        {
            MoveHere(location);
        }
    }

    private void CheckCollider(Collider2D collider, Vector3 location)
    {
        Interactable interactable;

        interactable = collider.GetComponent<Interactable>();

        if (interactable)
        {
            InteractWithInteractable("Default", interactable);
        }
        else
        {
            MoveHere(location);
        }
    }

    #endregion

    #region Movement helpers

    private void MoveHere(Vector3 location)
    {
        StopMovingToPrevInteraction();
        PathfindingManager.RequestPath(new PathRequest(transform.position, location, uc.OnPathFound));
        stateMachine.RequestChangeState(UnitStateMachine.UnitState.Idle); 
    }

    private void InteractWithInteractable(string chosenInteraction, Interactable interactable)
    {
        StopMovingToPrevInteraction();
        movingToInteraction = MoveToInteraction(interactable, chosenInteraction);
        StartCoroutine(movingToInteraction);
    }

    private IEnumerator MoveToInteraction(Interactable interactable, string interaction)
    {
        Collider2D c = GetComponent<Collider2D>();
        Debug.LogWarning("Moving to interaction " + interactable.ToString());
        while (interactable)
        {
            if (!c.IsTouching(interactable.GetComponent<Collider2D>()))
            {
                PathfindingManager.RequestPath(new PathRequest(transform.position, interactable.transform.position, uc.OnPathFound));
                yield return new WaitForSeconds(.2f); //might be able to extend this here? no need to be as precise?
            }
            else
            {
                uc.StopMoving();
                break;
            }
        }

        //if the interactable is still there when we get there
        if (interactable != null)
        {
            interactable.Interaction(interaction);
        }

        yield break;
    }

    private void StopMovingToPrevInteraction()
    {
        //stop moving towards previous interaction, if any
        if (movingToInteraction != null)
        {
            StopCoroutine(movingToInteraction);
        }
    }
    #endregion
}
