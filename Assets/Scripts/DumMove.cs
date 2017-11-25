using UnityEngine;
using System.Collections;

public class DumMove : MonoBehaviour {
	Vector3 moveSpot = new Vector3(3f,3f,0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.F)){
			transform.position = moveSpot;
			ChangeMoveSpot();
		}
	
	}
	
	void ChangeMoveSpot(){
		moveSpot.x += 2;
		moveSpot.y -=2;
	}
}
