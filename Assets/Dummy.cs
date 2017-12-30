using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {
	float speed = 2f;
	Vector3 newLocation = new Vector3(-2f,-4f,0f);

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)){
			transform.position = Vector3.MoveTowards(transform.position, newLocation, speed * Time.deltaTime);
		}
	}
}
