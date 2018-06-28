using System.Collections.Generic;
using UnityEngine;


//only the player uses this class
public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    private EquipmentManager equipmentManager;

    private EquipSlot[] slots;
    private Dictionary<EquipmentSlot, EquipSlot> equipSlots = new Dictionary<EquipmentSlot, EquipSlot>
    {
        { EquipmentSlot.Head, null },
        { EquipmentSlot.Chest, null },
        { EquipmentSlot.Hands, null },
        { EquipmentSlot.MainHand, null },
        { EquipmentSlot.OffHand, null },
        { EquipmentSlot.Legs, null },
        { EquipmentSlot.Feet, null }
    };

    private void Start()
    {
        equipmentManager = ScriptToolbox.GetInstance().GetPlayerManager().player.transform.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();
    }

    private void UpdateUI(Equipment oldItem, Equipment newItem)
    {
        if (newItem == null)
        {
            EquipmentSlot slot = oldItem.equipSlot;
            equipSlots[slot].ClearSlot();
        }
        else if(!newItem.naked)
        {
            EquipmentSlot slot = newItem.equipSlot;
            equipSlots[slot].AddEquipment(equipmentManager.EquipmentFromSlot(slot));
        }     
    }

    //bad coding here, but this script is only used by the player right now
    private void AssignSlotNums()
    {
         equipSlots[EquipmentSlot.Head] = slots[0]; 
         equipSlots[EquipmentSlot.Chest] = slots[1];
         equipSlots[EquipmentSlot.Hands] = slots[2];
         equipSlots[EquipmentSlot.MainHand] = slots[3];
         equipSlots[EquipmentSlot.OffHand] = slots[4];
         equipSlots[EquipmentSlot.Legs] = slots[5];
         equipSlots[EquipmentSlot.Feet] = slots[6];
    }
}
