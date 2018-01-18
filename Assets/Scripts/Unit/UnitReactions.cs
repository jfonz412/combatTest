using UnityEngine;
using System.Collections;

public class UnitReactions : MonoBehaviour {
	UnitController unitController;

	// Use this for initialization
	void Start () {
		unitController = GetComponent<UnitController>();
	}
	
	/***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/
	
	// Might eventually be able to call the appropriate respnse based on variables assigned in the inspector
	public void ReactToDisturbance(string disturbanceType, Transform target = null){
		if(gameObject.name == "Player"){
			if(disturbanceType == "Damage Taken" && unitController.lastKnownTarget == null){
				//do nothing for now
			}
		}else{
			if(disturbanceType == "Damage Taken" && unitController.lastKnownTarget != target){
				unitController.HasTarget(true, target);
			}
		}
	}
	
	void UnitIdle(){
		//do nothing
	}	
	
}
