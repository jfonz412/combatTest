using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	UnitController unitController;
    [HideInInspector]
	public bool incapacitated;


	// Use this for initialization
	void Start () {
        FloatingTextController.Initialize();
		unitController = GetComponent<UnitController>();
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
        Transform targetTransform;

        if (hit.collider)
        {
            targetTransform = hit.collider.transform;
            if (targetTransform.tag == "Entity")
            {
                if (unitController.lastKnownTarget != targetTransform)
                { //prevent double-click attacks
                    targetTransform = hit.collider.transform;
                    unitController.HasTarget(true, targetTransform);
                }
            }
            else
            {
                unitController.HasTarget(false);
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            unitController.HasTarget(false);
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
    }

    // USING THIS TO EXPERIMENT WITH INTERACTABLE OBJECTS
    void ProcessRightClick(Vector3 mouseClickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseClickPos, Vector2.zero); //Vector2.zero == (0,0)
        Transform targetTransform;

        if (hit.collider)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable)
            {
                targetTransform = hit.collider.transform;
                if (targetTransform.tag == "Entity") 
                {
                    if (unitController.lastKnownTarget != targetTransform)
                    { //prevent double-click attacks
                        targetTransform = hit.collider.transform;
                        unitController.HasTarget(true, targetTransform);
                    }
                }
                else if (targetTransform.tag == "Pickup")
                {
                    //do nothing for now
                    PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound); //pathfind player to radius, not target's transform (or as it is, mousePos)
                }
                else if (targetTransform.tag == "WorldItem") //crafting stations, ect.
                {
                    //do nothing for now
                }
            }
            else //else let pathfinding handle the rest (pathfinding won't let you walk on unwalkable tagged objects
            {
                unitController.HasTarget(false);
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            unitController.HasTarget(false);
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
    }

}
