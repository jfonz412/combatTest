using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour {
    public Equipment[] defaultLoadout = new Equipment[6]; //num of equipslots 
    EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        LoadDefaultEquipment();
    }

    void LoadDefaultEquipment()
    {
        for (int i = 0; i < defaultLoadout.Length; i++)
        {
            Debug.Log("Loading: " + defaultLoadout[i].name);
            equipmentManager.Equip(defaultLoadout[i]);
        }
    }
}
