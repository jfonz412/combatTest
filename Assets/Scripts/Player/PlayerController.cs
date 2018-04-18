using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject clickMarker;

    UnitController unitController;
    AttackController attackController;

    IEnumerator movingToInteraction = null;

    PlayerState.PlayerStates[] movementImparingStates = new PlayerState.PlayerStates[] 
    {
        PlayerState.PlayerStates.Shopping,
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Prompt
    };



	void Start () {
        FloatingTextController.Initialize(); //just needs to be initialized somewhere
        unitController = GetComponent<UnitController>();
        attackController = GetComponent<AttackController>();
    }

    
    // Update is called once per frame
    void Update () {
        if (!Incapcitated())
        {
            MovePlayer();
        }

            //FOR DEBUGGING 
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log(PlayerState.currentState);
            }
        
    }

    //this will prevent all movement, popups, and interactions during certain states
    bool Incapcitated()
    {
        return PlayerState.CheckPlayerState(movementImparingStates);
    }
	
	void MovePlayer(){
        if (!EventSystem.current.IsPointerOverGameObject()) //check if mouse is over UI element
        {
            if (Input.GetMouseButtonDown(0))
            {
                ProcessClick(0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                ProcessClick(1);
            }
        }
	}

    void ProcessClick(int mouseButton)
    {
        CloseOpenWindows.instance.DestroyPopupMenus();

        Vector3 mouseClickPos = GetMouseClickPosition();

        if (mouseButton == 0)
        {
            ProcessLeftClick(mouseClickPos);
        }
        else
        {
            ProcessRightClick(mouseClickPos);
        }
    }

    Vector3 GetMouseClickPosition()
    {
        Vector3 mouseClickPos;
        mouseClickPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseClickPos.z = 0f; //make sure this stays the same
        return mouseClickPos;
    }


    /************************************ LEFT AND RIGHT CLICKS *******************************/

    void ProcessLeftClick(Vector3 mouseClickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        CheckHit(hit, mouseClickPos);
        Instantiate(clickMarker, mouseClickPos, Quaternion.identity);
    }


    void ProcessRightClick(Vector3 mouseClickPos) //Brings up interaction options menu
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        Collider2D collider = hit.collider;
        if (collider)
        {
            CheckForInteractableMenu(collider);
        }
    }


    /****************** HELPER FUNCTIONS **************************/
    #region Click Helpers

    // LEFT CLICK HELPERS
    void CheckHit(RaycastHit2D hit, Vector3 location)
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

    void CheckCollider(Collider2D collider, Vector3 location)
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

    // RIGHT CLICK HELPER
    void CheckForInteractableMenu(Collider2D collider)
    {
        Interactable interactable;

        interactable = collider.GetComponent<Interactable>();

        if (interactable)
        {
            interactable.OpenInteractionMenu();
        }     
    }

    void MoveHere(Vector3 location)
    {
        StopPreviousInteraction();
        PathfindingManager.RequestPath(transform.position, location, unitController.OnPathFound);
    }
    #endregion


    #region Movement helpers
    //this will be called from the button in the dropdown interaction menu
    public void InteractWithInteractable(string chosenInteraction, Interactable interactable)
    {
        //don't allow player to attack same target over and over by clicking
        if(interactable.gameObject.transform == attackController.lastKnownTarget)
        {
            return;
        }

        StopPreviousInteraction();
        movingToInteraction = MoveToInteraction(interactable, chosenInteraction);
        StartCoroutine(movingToInteraction);
    }

    IEnumerator MoveToInteraction(Interactable interactable, string interaction)
    {
        while (interactable)
        {
            if (Vector3.Distance(transform.position, interactable.transform.position) > interactable.radius)
            { 
                PathfindingManager.RequestPath(transform.position, interactable.transform.position, unitController.OnPathFound);
                yield return new WaitForSeconds(.2f); //might be able to extend this here? no need to be as precise?
            }
            else
            {
                unitController.StopMoving();
                break;
            }
        }

        if(interactable != null)
        {
            interactable.Interaction(interaction);
        }
        
        yield break;
    }

    void StopPreviousInteraction()
    {
        if (movingToInteraction != null)
        {
            StopCoroutine(movingToInteraction); //stop moving towards previous interaction, if any
        }
           
        attackController.EngageTarget(false); //disengage current target (stops the attacking coroutine)
    }
    #endregion

}
