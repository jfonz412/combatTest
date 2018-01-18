using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	UnitController unitController;
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
			Vector3 targetPos;
			targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPos.z = transform.position.z; //to always keep z at 0
			ProcessLeftClick(targetPos);			
		}
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetPos;
            targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            targetPos.z = transform.position.z; //to always keep z at 0
            ProcessRightClick(targetPos);
        }
	}
	
	void ProcessLeftClick(Vector3 targetPos){	
		RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero); //Vector2.zero == (0,0)
		Transform targetTransform;
		
		if (hit.collider != null) {
			targetTransform = hit.collider.transform;
			if(targetTransform.tag == "Entity"){
				if(unitController.lastKnownTarget != targetTransform)
                { //prevent double-click attacks
                    targetTransform = hit.collider.transform;
					unitController.HasTarget(true, targetTransform);
				}
			}else{
				unitController.HasTarget(false);
				PathfindingManager.RequestPath(transform.position, targetPos, unitController.OnPathFound);
			}
		}else{
			unitController.HasTarget(false);
			PathfindingManager.RequestPath(transform.position, targetPos, unitController.OnPathFound);
		}
	}

    void ProcessRightClick(Vector3 targetPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero); //Vector2.zero == (0,0)
        Transform targetTransform;

        if (hit.collider != null)
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
                PathfindingManager.RequestPath(transform.position, targetPos, unitController.OnPathFound);
            }
        }
        else
        {
            unitController.HasTarget(false);
            PathfindingManager.RequestPath(transform.position, targetPos, unitController.OnPathFound);
        }
    }

}
