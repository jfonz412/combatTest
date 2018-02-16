using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public Equipment[] currentEquipment; 
 
    UnitAnimator unitAnim;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;

    public Equipment unarmedMain, unarmedOff, nakedChest, nakedLegs, nakedFeet, nakedHead; //only for player

    void Start () {
        unitAnim = GetComponent<UnitAnimator>();
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    //FIGURE OUT A WAY TO CALL THESE METHODS WHEN THE MOUSESLOT ITEM IS SWAPPED ???

    //IF MOUSESLOT == NULL --> UNEQUIP

    //IF MOUSESLOT == EQUIPMENT --> EQUIP

    //ELSE --> RETURN (INVALID ITEM)

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipSlot;

        //put item in inventory if valid
        if(currentEquipment[slotIndex] != null && currentEquipment[slotIndex].equipmentID != 0)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        unitAnim.LoadEquipment((int)newItem.equipSlot, newItem.equipmentID);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(oldItem, newItem);
        }
    }

    //may need to check for naked unequip
    public void Unequip(int slotIndex)
    {

        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            if (oldItem.equipmentID != 0) 
            {
                inventory.Add(oldItem); 
            }

            Strip(oldItem);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(oldItem, null);    
            }
        }
    }

    void Strip(Equipment oldItem)
    {
        int slotIndex = (int)oldItem.equipSlot;

        switch (oldItem.equipSlot)
        {
            case EquipmentSlot.Head:
                currentEquipment[slotIndex] = nakedHead;
                break;
            case EquipmentSlot.Chest:
                currentEquipment[slotIndex] = nakedChest;
                break;
            case EquipmentSlot.MainHand:
                currentEquipment[slotIndex] = unarmedMain;
                break;
            case EquipmentSlot.OffHand:
                currentEquipment[slotIndex] = unarmedOff;
                break;
            case EquipmentSlot.Legs:
                currentEquipment[slotIndex] = nakedLegs;
                break;
            case EquipmentSlot.Feet:
                currentEquipment[slotIndex] = nakedFeet;
                break;
            default:
                currentEquipment[slotIndex] = null;
                Debug.LogError("Invalid EquipSlot");
                break;
        }
        unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //add naked/unarmed to anim slot
    }
}
