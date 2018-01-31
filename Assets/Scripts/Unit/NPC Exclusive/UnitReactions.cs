using UnityEngine;
using System.Collections;

public class UnitReactions : MonoBehaviour {
	AttackController attackController;

	// Use this for initialization
	void Start () {
		attackController = GetComponent<AttackController>();
	}
	
	/***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/
	
	// Might eventually be able to call the appropriate respnse based on variables assigned in the inspector
	public void ReactToDisturbance(string disturbanceType, Transform target = null){
		if(gameObject.name == "Player"){
			if(disturbanceType == "Damage Taken" && attackController.lastKnownTarget == null){
				//do nothing for now
			}
		}else{
			if(disturbanceType == "Damage Taken" && attackController.lastKnownTarget != target){
				attackController.EngageTarget(true, target);
			}
		}
	}
	
	void UnitIdle(){
		//do nothing
	}	
	
}
