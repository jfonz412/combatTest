using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	UnitController unitController;

	// Use this for initialization
	void Start () {
		unitController = GetComponent<UnitController>();
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer();
	}
	
	void MovePlayer(){
		if (Input.GetMouseButtonDown(0)) {
			Vector3 targetPos;
			targetPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPos.z = transform.position.z; //to always keep z at 0
			ProcessClick(targetPos);			
		}
	}
	
	void ProcessClick(Vector3 targetPos){	
		RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero); //Vector2.zero == (0,0)
		GameObject targetEntity;
		
		if (hit.collider != null) {
			targetEntity = hit.collider.gameObject;
			if(targetEntity.tag == "Entity"){
				if(unitController.lastKnownTarget != targetEntity){ //prevent double-click attacks
					targetEntity = hit.collider.gameObject;
					unitController.HasTarget(true, targetEntity);
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
	
}
