﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    //[SerializeField]
    //private GameObject clickMarker;
    private UnitStateMachine.ClickInfo clickInfo = new UnitStateMachine.ClickInfo();
    private UnitController unitController;
    private UnitStateMachine stateMachine;

    private IEnumerator movingToInteraction = null;

    private void Start ()
    {
        FloatingTextController.Initialize(); //just needs to be initialized somewhere
        unitController = GetComponent<UnitController>();
        stateMachine = GetComponent<UnitStateMachine>();
    }

    #region Move Player
    private void Update () {
        MovePlayer();
    }

    private void MovePlayer(){
        if (!EventSystem.current.IsPointerOverGameObject()) //check if mouse is over UI element
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickInfo.mousePos = GetMouseClickPosition();
                clickInfo.clickType = "leftClick";
                stateMachine.clickInfo = clickInfo;
                stateMachine.RequestChangeState(UnitStateMachine.UnitState.PlayerMoveState);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickInfo.mousePos = GetMouseClickPosition();
                clickInfo.clickType = "rightClick";
                stateMachine.clickInfo = clickInfo;
                stateMachine.RequestChangeState(UnitStateMachine.UnitState.PlayerMoveState); 
            }
        }
	}

    private Vector3 GetMouseClickPosition()
    {
        Vector3 mouseClickPos;
        mouseClickPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseClickPos.z = 0f; //make sure this stays the same
        return mouseClickPos;
    }
    #endregion

    /*
     * 
     * 
    #region Process Clicks

        
    private void ProcessClick(int mouseButton)
    {
        ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus();
        
        Vector3 mouseClickPos = GetMouseClickPosition(); //send to state machine

        if (mouseButton == 0)
        {
            ProcessLeftClick(mouseClickPos);
            stateMachine.RequestChangeState(UnitStateMachine.UnitState.PlayerMoveState); //request change state 
        }
        else
        {
            ProcessRightClick(mouseClickPos); //need to eventually sent to state machine?  
        }
        
    }


    private void ProcessLeftClick(Vector3 mouseClickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        CheckForCollider(hit, mouseClickPos); //will move to Pos if collider not found
        Instantiate(clickMarker, mouseClickPos, Quaternion.identity);
    }

    private void ProcessRightClick(Vector3 mouseClickPos) //Brings up interaction options menu
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)
        List<Collider2D> interactablesFound = CheckForUnits(mouseClickPos);

        if(interactablesFound.Count <= 1)
        {
            Collider2D collider = hit.collider;
            if (collider)
            {
                CheckForInteractableMenu(collider);
            }
        }
        else
        {
            SelectInteractable.SpawnMenu(interactablesFound);
        }

    }
    #endregion


    #region Click Helpers

    // LEFT CLICK HELPERS
    private void CheckForCollider(RaycastHit2D hit, Vector3 location)
    {
        Collider2D collider = hit.collider;
        if (collider)
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

    // RIGHT CLICK HELPERS

    private List<Collider2D> CheckForUnits(Vector3 mouseClickPos)
    {
        List<Collider2D> interactables = new List<Collider2D>();
        //Debug.Log("Checking for interactables");
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseClickPos, Vector2.zero);
        
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D col = hits[i].collider;
            if (col != null)
            {
                interactables.Add(col);
            }
        }

        return interactables;
    }


    //made public to expose to Unit Menu buttons, which will pass the collider into this method 
    public void CheckForInteractableMenu(Collider2D collider)
    {
        Interactable interactable;

        interactable = collider.GetComponent<Interactable>();

        if (interactable)
        {
            interactable.OpenInteractionMenu();
        }     
    }

    #endregion


    #region Movement helpers

    private void MoveHere(Vector3 location)
    {
        StopMovingToPrevInteraction();
        PathfindingManager.RequestPath(new PathRequest(transform.position, location, unitController.OnPathFound));
    }

    //this will be called from the button in the dropdown interaction menu, so it must be public
    public void InteractWithInteractable(string chosenInteraction, Interactable interactable)
    {
        StopMovingToPrevInteraction();
        movingToInteraction = MoveToInteraction(interactable, chosenInteraction);
        StartCoroutine(movingToInteraction);
    }

    private IEnumerator MoveToInteraction(Interactable interactable, string interaction)
    {
        Collider2D c = GetComponent<Collider2D>();
        stateMachine.RequestChangeState(UnitStateMachine.UnitState.PlayerMoveState); //request change state

        while (interactable)
        {
            if (!c.IsTouching(interactable.GetComponent<Collider2D>())) 
            { 
                PathfindingManager.RequestPath(new PathRequest(transform.position, interactable.transform.position, unitController.OnPathFound));
                yield return new WaitForSeconds(.2f); //might be able to extend this here? no need to be as precise?
            }
            else
            {
                unitController.StopMoving();
                break;
            }
        }

        //if the interactable is still there when we get there
        if(interactable != null)
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
    */
}
