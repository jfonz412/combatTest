using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            //Vector3 targetWithNegativeZ = new Vector3(target.position.x, target.position.y, -2f);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0, 0, -10); ;
        }
    }
}
