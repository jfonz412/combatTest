using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentList : MonoBehaviour {
    [SerializeField]
    private Dictionary<int, Equipment> equipment = new Dictionary<int, Equipment>();

	// Use this for initialization
	void Start () {
		
	}
}
