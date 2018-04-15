﻿using UnityEngine;

public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    EquipmentManager equipmentManager;
    EquipSlot[] slots;

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;
        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();
    }

    void UpdateUI(Equipment oldItem, Equipment newItem)
    {
        if (newItem != null)
        { 
            int slotNum = (int)newItem.equipSlot;
            slots[slotNum].AddEquipment(equipmentManager.currentEquipment[slotNum]);
        }
        else
        {
            int slotNum = (int)oldItem.equipSlot;
            slots[slotNum].ClearSlot();
        }
    }

    void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].equipSlot = i;
        }
    }
}
