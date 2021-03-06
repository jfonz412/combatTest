﻿using UnityEngine;

public class EquipSlotClick : MonoBehaviour {

    SlotClickHelpers slotClickHelper;


    void Start()
    {
        slotClickHelper = InventoryManager.GetInstance().GetSlotClickHelpers();
    }

    #region Equip Slot Clicks

    public void EquipSlotRightClicked(EquipSlot slot)
    {
        Debug.Log("Equip slot right clicked");
        if (slot.Equipment() != null)
        {
            slot.EquipmentManager().FastUnequip(slot.Equipment());
        }
    }

    public void EquipSlotHoverOver(EquipSlot slot)
    {
        if (slot.Equipment() != null)
        {
            slot.Equipment().OpenStatWindow("Inventory");
        }
    }


    public void EquipSlotLeftClicked(EquipSlot slot)
    {
        MouseSlot mouseSlot = MouseSlot.instance;
        Item mouseItem = MouseSlot.instance.Item();

        if (slotClickHelper.CheckItemType() == false)
        {
            return;
        }

        if (mouseItem == null && slot.Equipment() == null) //or slot.Equipment() == naked or unarmed?
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        if (mouseItem == null && slot.Equipment() != null)
        {
            slotClickHelper.PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.Equipment() == null) //or slot.Equipment() == naked or unarmed?
        {
            slotClickHelper.PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.Equipment() != null) //or slot.Equipment() == naked or unarmed?
        {
            slotClickHelper.SwapItems(mouseSlot, slot);
            return;
        }
    }


    #endregion
}
