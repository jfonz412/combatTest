using UnityEngine;

public class EquipSlot : InventorySlot {

    EquipmentManager equipmentManager;
    Equipment equipment;
    public int equipSlot;

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
    }

    public void AddEquipment(Equipment newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        equipment = newItem;
        icon.sprite = equipment.icon;
        icon.rectTransform.sizeDelta = new Vector2(50f, 50f); //temporary, the long term solution would be to make a custom icon sprite for each item
        icon.enabled = true;
    }

    public override void ClearSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    #region Slot Clicks
    //consider moving the insides of this function to it's own SlotClick.EquipSlotRightClicked(this);
    public override void SlotRightClicked()
    {
        if (equipment != null)
        {
            equipment.OpenStatWindow();
        }
    }

    //consider moving the insides of this function to it's own SlotClick.EquipSlotLeftClicked(this);
    public override void SlotLeftClicked()
    {
        MouseSlot mouseSlot = MouseSlot.instance;
        Equipment mouseItem = MouseSlot.instance.currentItem as Equipment; //save a copy of the mouseItem

        if(CheckItemType() == false)
        {
            return;
        }

        if (mouseItem == null && equipment == null) //or equipment == naked or unarmed?
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        if (mouseItem == null && equipment != null)
        {
            PickUpItemIntoEmptyMouseSlot(mouseSlot);
            return;
        }

        if (mouseItem != null && equipment == null) //or equipment == naked or unarmed?
        {
            PlaceItemInEmptySlot(mouseSlot);
            return;
        }

        if (mouseItem != null && equipment != null) //or equipment == naked or unarmed?
        {
            SwapItems(mouseSlot);
            return;
        }
    }
    #endregion

    #region Click Helpers

    void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot)
    {
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");   //or equipment == naked or unarmed?
        Equipment previousItem = equipment;                //save a copy of the slotItem
        equipmentManager.Unequip((int)previousItem.equipSlot); //unequip item currently in equip slot
        mouseSlot.UpdateItem(previousItem);                //place previous item in the mouseSlot (as an item)?
    }

    void PlaceItemInEmptySlot(MouseSlot mouseSlot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot))
        {
            return;
        }

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    void SwapItems(MouseSlot mouseSlot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot))
        {
            return;
        }

        Debug.Log("SWAPPING ITEMS");
        Equipment previousItem = equipment;        //save a copy of the slotItem
        equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(previousItem);        //add old item to mouseSlot
    }


    //----------------------------------------------
    bool CheckItemType()
    {
        if(MouseSlot.instance.currentItem == null)
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

    bool CheckEquipSlot(int mouseItemEquipSlot)
    {
        if (mouseItemEquipSlot != equipSlot)
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
}

