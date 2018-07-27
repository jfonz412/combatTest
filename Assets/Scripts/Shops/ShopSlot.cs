using UnityEngine;

public class ShopSlot : InventorySlot
{

    public override void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.Log("Item null, clearing slot in UI " + newItem);
            ClearSlot();
            return;
        }

        Debug.Log("Item being added to UI slot");
        item = newItem;
        Sprite itemSprite = Resources.Load<Sprite>("Images/ItemIcons/" + item.icon);
        Debug.Log(itemSprite);
        icon.sprite = itemSprite;
        icon.enabled = true;

        //Debug.Log(item.name + " has been added to slot " + slotNum + "with a slotnum of " + item.slotNum);
    }

    public override void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    

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