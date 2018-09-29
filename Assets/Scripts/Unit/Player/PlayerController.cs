  using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    //[SerializeField]
    //private GameObject clickMarker;
    private PlayerStateMachine.ClickInfo clickInfo = new PlayerStateMachine.ClickInfo();
    private UnitController unitController;
    private PlayerStateMachine stateMachine;

    private IEnumerator movingToInteraction = null;

    private void Start ()
    {
        FloatingTextController.Initialize(); //just needs to be initialized somewhere
        unitController = GetComponent<UnitController>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        ListenForMouseClick();
    }

    #region ListenForMouseClick

    private void ListenForMouseClick(){
        if (!EventSystem.current.IsPointerOverGameObject()) //check if mouse is over UI element
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickInfo.mousePos = GetMouseClickPosition();
                clickInfo.clickType = "leftClick";
                stateMachine.clickInfo = clickInfo;
                stateMachine.RequestChangeState(StateMachine.States.PlayerMove);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickInfo.mousePos = GetMouseClickPosition();
                clickInfo.clickType = "rightClick";
                //stateMachine.clickInfo = clickInfo; not yet
                ProcessRightClick(clickInfo.mousePos);
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

    #region Right Click

    private void ProcessRightClick(Vector3 mouseClickPos) //Brings up interaction options menu
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)
        List<Collider2D> interactablesFound = CheckForUnits(mouseClickPos);

        if (interactablesFound.Count <= 1)
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
    //the object passes in it's collider, which we check to see if interactable. 
    //if so we open the objects interaction menu
    public void CheckForInteractableMenu(Collider2D collider)
    {
        Interactable interactable;

        interactable = collider.GetComponent<Interactable>();

        if (interactable)
        {
            interactable.OpenInteractionMenu();
        }     
    }

    //this will be called from the button in the dropdown interaction menu, so it must be public
    public void RequestInteraction(string chosenInteraction, Interactable interactable)
    {
        clickInfo.interaction = chosenInteraction;
        clickInfo.interactable = interactable;
        stateMachine.clickInfo = clickInfo;
        stateMachine.RequestChangeState(StateMachine.States.PlayerMove);
    }

    #endregion


}
