using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public Equipment[] currentEquipment; 
 
    UnitAnimator unitAnim;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;

	void Start () {
        unitAnim = GetComponent<UnitAnimator>();
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipSlot;

        Debug.Log(slotIndex);

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
            onEquipmentChanged.Invoke(newItem, oldItem);
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
            
            unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //add naked/unarmed to anim slot

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
