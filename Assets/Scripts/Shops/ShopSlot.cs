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
        if(psm.currentState == UnitStateMachine.UnitState.Shopping)
            InventoryManager.GetInstance().GetShopSlotClick().ShopSlotRightClicked(item); 
    }

    public override void SlotLeftClicked()
    {
        if (psm.currentState == UnitStateMachine.UnitState.Shopping)
            InventoryManager.GetInstance().GetShopSlotClick().ShopSlotLeftClicked(this);
    }
}