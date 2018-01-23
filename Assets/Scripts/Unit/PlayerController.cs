using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    UnitController unitController;
    AttackController attackController;
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

    void ProcessLeftClick(Vector3 mouseClickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)

        if (hit.collider)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable)
            {
                interactable.DefaultInteraction(transform); //pass in player's transform
            }
            else
            {
                attackController.EngageTarget(false); //make sure to disengage target if previously fighting
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            attackController.EngageTarget(false);
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
    }


    //Brings up interaction options menu
    void ProcessRightClick(Vector3 mouseClickPos)
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

}
