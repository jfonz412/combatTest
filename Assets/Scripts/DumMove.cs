using UnityEngine;
using System.Collections;

public class DumMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//if(Input.GetKeyDown(KeyCode.F)){
			
			ChangeMoveSpot();
		//}
	
	}
	
	void ChangeMoveSpot(){
		Vector3 moveSpot = transform.position;
		moveSpot.x += .005f;
		moveSpot.y += .005f;
		transform.position = moveSpot;
	}
}
