using UnityEngine;

public class ShopSlot : InventorySlot
{
    public override void SlotHoverOver()
    {
        InventoryManager.GetInstance().GetInventoryToggle().OpenInventory();
        InventoryManager.GetInstance().GetShopSlotClick().ShopSlotHoverOver(item);
    }

    public override void SlotRightClicked()
    {
        InventoryManager.GetInstance().GetShopSlotClick().ShopSlotRightClicked(item); 
    }

    public override void SlotLeftClicked()
    {
        InventoryManager.GetInstance().GetShopSlotClick().ShopSlotLeftClicked(this);
    }
}