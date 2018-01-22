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
        Transform targetTransform;

        if (hit.collider)
        {
            targetTransform = hit.collider.transform;
            if (targetTransform.tag == "Entity")
            {
                if (attackController.lastKnownTarget != targetTransform)
                { //prevent double-click attacks
                    targetTransform = hit.collider.transform;
                    attackController.EngageTarget(true, targetTransform);
                }
            }
            else
            {
                attackController.EngageTarget(false);
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            attackController.EngageTarget(false);
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
    }

    // USING THIS TO EXPERIMENT WITH INTERACTABLE OBJECTS

    // The plan is to remove all attack controller type stuff from unit controller so it's not so messy to use a totally seperate circut (different from Interact) to engage enemies
    // Maybe instead of Interact for all objects and NPCs, objects will have their own interaction script and NPC will have it's own in order to be able to talk, attack, ect.
    // for now lets get the attacking out of UnitController
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
                if (targetTransform.tag == "Entity") //enemy
                {
                    if (attackController.lastKnownTarget != targetTransform) //prevent double-click attacks
                    { 
                        targetTransform = hit.collider.transform;
                        attackController.EngageTarget(true, targetTransform);
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
                attackController.EngageTarget(false);
                PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
            }
        }
        else
        {
            attackController.EngageTarget(false);
            PathfindingManager.RequestPath(transform.position, mouseClickPos, unitController.OnPathFound);
        }
    }

}
