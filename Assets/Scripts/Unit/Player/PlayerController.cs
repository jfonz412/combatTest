using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    UnitController unitController;
    AttackController attackController;
    IEnumerator movingToInteraction = null;
    [HideInInspector]
	public bool incapacitated;


	// Use this for initialization
	void Start () {
        FloatingTextController.Initialize(); 
        unitController = GetComponent<UnitController>();
        attackController = GetComponent<AttackController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(!incapacitated){
			MovePlayer();
		}
	}
	
	void MovePlayer(){
		if (Input.GetMouseButtonDown(0))
        {
			Vector3 mouseClickPos;
            mouseClickPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            mouseClickPos.z = transform.position.z; //to always keep z at 0
			ProcessLeftClick(mouseClickPos);			
		}
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseClickPos;
            mouseClickPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            mouseClickPos.z = transform.position.z; //to always keep z at 0
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
                StopPreviousInteraction();
                movingToInteraction = MoveToInteraction(interactable);
                StartCoroutine(movingToInteraction);
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
    }


    void ProcessRightClick(Vector3 mouseClickPos) //Brings up interaction options menu
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        if (hit.collider)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable)
            {
                //Pull up interaction options menu
                Debug.Log("Interaction Options");
            }
        }
        //if it's not a collider or interactable right click does nothing
    }


    /****************** HELPER FUNCTIONS **************************/

    IEnumerator MoveToInteraction(Interactable interactable)
    {
        while (interactable)
        {
            if (Vector3.Distance(transform.position, interactable.transform.position) > interactable.radius) //and interactable != null ?
            { 
                PathfindingManager.RequestPath(transform.position, interactable.transform.position, unitController.OnPathFound);
                yield return new WaitForSeconds(.2f); //might be able to extend this here? no need to be as precise?
            }
            else
            {
                break;
            }
        }
        interactable.DefaultInteraction();
        yield break;
    }

    void StopPreviousInteraction()
    {
        if (movingToInteraction != null)
            StopCoroutine(movingToInteraction); //stop moving towards previous interaction, if any

        attackController.EngageTarget(false); //disengage current target (stops the attacking coroutine), if any
    }
}
