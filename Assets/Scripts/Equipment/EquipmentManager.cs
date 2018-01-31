using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public Equipment[] currentEquipment; //holds all of our equipment
    UnitAnimator unitAnim;
    Inventory inventory;

    //this will be used to adjust stats on equipment changes
    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged; //this will probably take in a method from Stats.cs to change the stats on equipment changes

	void Start () {
        unitAnim = GetComponent<UnitAnimator>();
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //LoadOut.LoadDefaultLoadout();
    }

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipSlot; //grabs the item's equip slot based on it's enum

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        unitAnim.LoadEquipment((int)newItem.equipSlot, newItem.equipmentID);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //strip player
            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);    
            }
        }
    }

    //TEMPORARY

    public void UnequpAll() 
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequpAll();
        }
    }
}
