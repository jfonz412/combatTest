using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour {
    UnitController unitController;
    AttackController attackController;
    IEnumerator movingToInteraction = null;

    [HideInInspector]
	public bool incapacitated;

    public GameObject clickMarker;

	// Use this for initialization
	void Start () {
        FloatingTextController.Initialize(); 
        unitController = GetComponent<UnitController>();
        attackController = GetComponent<AttackController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
		if(!incapacitated){
			MovePlayer();
		}
	}
	
	void MovePlayer(){
		if (Input.GetMouseButtonDown(0))
        {
            ProcessClick(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ProcessClick(1);   
        }
	}

    void ProcessClick(int mouseButton)
    {
        if (InteractableMenu.instance != null)
        { 
            InteractableMenu.instance.CloseMenu();
        }

        Vector3 mouseClickPos;
        mouseClickPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseClickPos.z = transform.position.z; //to always keep z at 0

        if(mouseButton == 0)
        {
            ProcessLeftClick(mouseClickPos);
        }
        else
        {
            ProcessRightClick(mouseClickPos);
        }
    }


    /************************************ LEFT AND RIGHT CLICKS *******************************/

    void ProcessLeftClick(Vector3 mouseClickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        if (hit.collider)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable)
            {
                InteractWithInteractable("Default", interactable);
            }
            else
            {
                StopPreviousInteraction();
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            StopPreviousInteraction();
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
        //spawn marker
        Instantiate(clickMarker, mouseClickPos, Quaternion.identity);
    }


    void ProcessRightClick(Vector3 mouseClickPos) //Brings up interaction options menu
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        if (hit.collider)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable)
            {
                interactable.OpenInteractionMenu();
            }
        }
    }


    /****************** HELPER FUNCTIONS **************************/

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
            StopCoroutine(movingToInteraction); //stop moving towards previous interaction, if any

        attackController.EngageTarget(false); //disengage current target (stops the attacking coroutine), if any
    }

    //this will be called from the button in the dropdown interaction menu
    public void InteractWithInteractable(string chosenInteraction, Interactable interactable)
    { 
        StopPreviousInteraction();
        movingToInteraction = MoveToInteraction(interactable, chosenInteraction);
        StartCoroutine(movingToInteraction);
    }
}
