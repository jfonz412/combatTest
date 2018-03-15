using UnityEngine;

public class SlotClick : MonoBehaviour {

    #region Equip Slot Clicks

    public static void EquipSlotRightClicked(EquipSlot slot)
    {
        Debug.Log("Equip slot right clicked");
        if(slot.equipment != null)
        {
            slot.equipmentManager.FastUnequip(slot.equipment);
        }
    }

    public static void EquipSlotHoverOver(EquipSlot slot)
    {
        if (slot.equipment != null)
        {
            slot.equipment.OpenStatWindow();
        }
    }


    public static void EquipSlotLeftClicked(EquipSlot slot)
    {
        MouseSlot mouseSlot = MouseSlot.instance;
        Equipment mouseItem = MouseSlot.instance.currentItem as Equipment; //save a copy of the mouseItem

        if (CheckItemType() == false)
        {
            return;
        }

        if (mouseItem == null && slot.equipment == null) //or slot.equipment == naked or unarmed?
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        if (mouseItem == null && slot.equipment != null)
        {
            PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment == null) //or slot.equipment == naked or unarmed?
        {
            PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment != null) //or slot.equipment == naked or unarmed?
        {
            SwapItems(mouseSlot, slot);
            return;
        }
    }

    //HELPERS

    static void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");   //or equipment == naked or unarmed?
        Equipment previousItem = slot.equipment;                //save a copy of the slotItem
        slot.equipmentManager.Unequip((int)previousItem.equipSlot); //unequip item currently in equip slot
        mouseSlot.UpdateItem(previousItem);                //place previous item in the mouseSlot (as an item)?
    }

    static void PlaceItemInEmptySlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot, slot))
        {
            return;
        }

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        slot.equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    static void SwapItems(MouseSlot mouseSlot, EquipSlot slot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot, slot))
        {
            return;
        }

        Debug.Log("SWAPPING ITEMS");
        Equipment previousItem = slot.equipment;        //save a copy of the slotItem
        slot.equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(previousItem);        //add old item to mouseSlot
    }


    //----------------------------------------------
    static bool CheckItemType()
    {
        if (MouseSlot.instance.currentItem == null)
        {
            return true;
        }
        else if (MouseSlot.instance.currentItem.GetType() == typeof(Armor))
        {
            return true;
        }
        else if (MouseSlot.instance.currentItem.GetType() == typeof(Weapon))
        {
            return true;
        }
        else
        {
            Debug.Log("MOUSEITEM NOT EQUIPMENT");
            return false;
        }
    }

    static bool CheckEquipSlot(int mouseItemEquipSlot, EquipSlot slot)
    {
        if (mouseItemEquipSlot != slot.equipSlot)
        {
            Debug.Log("WRONG EQUIP SLOT ");
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region Inventory Slot Clicks

    public static void InventorySlotRightClicked(Item item)
    {
        Debug.Log("Inventory slot right clicked");
        if (item != null)
        {
            item.Use();
        }           
    }

    public static void InventorySlotHoverOver(Item item)
    {
        if (item != null)
        {
            item.OpenStatWindow();
        }
    }

    public static void InventorySlotLeftClicked(InventorySlot slot)
    {
        MouseSlot mouseSlot = MouseSlot.instance;
        Item mouseItem = MouseSlot.instance.currentItem;

        if (mouseItem == null && slot.item == null)
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        if (mouseItem == null && slot.item != null)
        {
            PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item == null)
        {
            PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item != null)
        {
            SwapItems(mouseSlot, slot);
            return;
        }
    }


    //HELPERS 

    static void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;

        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
        Item previousItem = slot.item;             //save a copy of the slotItem
        inventory.Remove(previousItem);       //remove the item in the slot 
        mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot            //place previous item in the mouseSlot (as an item)?
    }

    static void PlaceItemInEmptySlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        mouseItem.slotNum = slot.slotNum; //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem); //drop item in slot
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    static void SwapItems(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;
        Item previousItem = slot.item;

        Debug.Log("SWAPPING ITEMS");
        mouseItem.slotNum = slot.slotNum;              //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
        mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot
    }

    #endregion
}
