﻿using UnityEngine;

public class EquipSlotClick : MonoBehaviour {

    SlotClickHelpers slotClickHelper;

    #region Singleton

    public static EquipSlotClick instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SlotClick found");
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        slotClickHelper = SlotClickHelpers.instance;
    }

    #region Equip Slot Clicks

    public void EquipSlotRightClicked(EquipSlot slot)
    {
        Debug.Log("Equip slot right clicked");
        if (slot.equipment != null)
        {
            slot.equipmentManager.FastUnequip(slot.equipment);
        }
    }

    public void EquipSlotHoverOver(EquipSlot slot)
    {
        if (slot.equipment != null)
        {
            slot.equipment.OpenStatWindow("Inventory");
        }
    }


    public void EquipSlotLeftClicked(EquipSlot slot)
    {
        MouseSlot mouseSlot = MouseSlot.instance;
        Equipment mouseItem = MouseSlot.instance.currentItem as Equipment; //save a copy of the mouseItem

        if (slotClickHelper.CheckItemType() == false)
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
            slotClickHelper.PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment == null) //or slot.equipment == naked or unarmed?
        {
            slotClickHelper.PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment != null) //or slot.equipment == naked or unarmed?
        {
            slotClickHelper.SwapItems(mouseSlot, slot);
            return;
        }
    }


    #endregion
}