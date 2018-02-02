using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    public GameObject equipmentUI;
    EquipmentManager equipmentManager;
    EquipSlot[] slots;


    // Use this for initialization
    void Start () {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;
        slots = equipParent.GetComponentsInChildren<EquipSlot>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }


    //might need work, currentEquipment is a list, the original function was using a List
    void UpdateUI(Equipment oldItem, Equipment newItem)
    {
        if (newItem != null)
        { 
            int slotNum = (int)newItem.equipSlot;
            slots[slotNum].AddItem(equipmentManager.currentEquipment[slotNum]);
        }
        else
        {
            int slotNum = (int)oldItem.equipSlot;
            slots[slotNum].ClearSlot();
        }
    }
}
