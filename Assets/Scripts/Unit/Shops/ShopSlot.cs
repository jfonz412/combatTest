﻿using UnityEngine;

public class ShopSlot : InventorySlot
{

    public override void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        item = newItem;
        icon.sprite = item.icon;
        icon.rectTransform.sizeDelta = new Vector2(50f, 50f); //temporary, the long term solution would be to make a custom icon sprite for each item
        icon.enabled = true;
    }

    public override void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    

    public override void SlotHoverOver()
    {
        SlotClick.ShopSlotHoverOver(item);
    }

    public override void SlotRightClicked()
    {
        SlotClick.ShopSlotRightClicked(item); //does nothing, leave it here in case I find a use for this
    }

    public override void SlotLeftClicked()
    {
        SlotClick.ShopSlotLeftClicked(this);
    }
}