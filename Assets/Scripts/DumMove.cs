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
		moveSpot.x += .02f;
		moveSpot.y += .02f;
		transform.position = moveSpot;
	}
}
