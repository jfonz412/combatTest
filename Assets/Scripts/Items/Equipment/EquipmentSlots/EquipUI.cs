using System.Collections.Generic;
using UnityEngine;


//only the player uses this class
public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    private EquipmentManager equipmentManager;

    private EquipSlot[] slots;
    private Dictionary<Item.EquipmentSlot, EquipSlot> equipSlots = new Dictionary<Item.EquipmentSlot, EquipSlot>
    {
        { Item.EquipmentSlot.Head, null },
        { Item.EquipmentSlot.Chest, null },
        { Item.EquipmentSlot.Hands, null },
        { Item.EquipmentSlot.MainHand, null },
        { Item.EquipmentSlot.OffHand, null },
        { Item.EquipmentSlot.Legs, null },
        { Item.EquipmentSlot.Feet, null }
    };

    private void Start()
    {
        /*
        equipmentManager = ScriptToolbox.GetInstance().GetPlayerManager().player.transform.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;
        */
        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();
    }

    public void UpdateUI(Item newItem, Item.EquipmentSlot slot)
    {
        if (newItem == null) //if new item is null then we are just equipping
        {
            equipSlots[slot].ClearSlot();
        }
        else
        {
            equipSlots[slot].AddEquipment(newItem);
        }     
    }

    //bad coding here, but this script is only used by the player right now
    private void AssignSlotNums()
    {
         equipSlots[Item.EquipmentSlot.Head] = slots[0]; 
         equipSlots[Item.EquipmentSlot.Chest] = slots[1];
         equipSlots[Item.EquipmentSlot.Hands] = slots[2];
         equipSlots[Item.EquipmentSlot.MainHand] = slots[3];
         equipSlots[Item.EquipmentSlot.OffHand] = slots[4];
         equipSlots[Item.EquipmentSlot.Legs] = slots[5];
         equipSlots[Item.EquipmentSlot.Feet] = slots[6];
    }
}
