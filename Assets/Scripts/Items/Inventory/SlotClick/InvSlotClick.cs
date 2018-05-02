using UnityEngine;

public class InvSlotClick : MonoBehaviour {

    SlotClickHelpers slotClickHelper;

    #region Singleton

    public static InvSlotClick instance;

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

    #region Inventory Slot Clicks

    public void InventorySlotRightClicked(Item item)
    {
        Debug.Log("Inventory slot right clicked");
        if (item != null)
        {
            item.Use();
        }
    }

    public void InventorySlotHoverOver(Item item)
    {
        if (item != null)
        {
            item.OpenStatWindow("Inventory");
        }
    }

    public void InventorySlotLeftClicked(InventorySlot slot)
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
            slotClickHelper.PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item == null)
        {
            slotClickHelper.PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item != null)
        {
            slotClickHelper.SwapItems(mouseSlot, slot);
            return;
        }
    }

    public void LeftClickedToSell(InventorySlot slot)
    {
        Item item = slot.item;
        //int quantity = PromptForQuantity();
        if (item != null)
        {
            StartCoroutine(slotClickHelper.SellItem(item));
        }
    }

    public void RightClickedToSell(Item item)
    {
        float price = PriceChecker.AppraiseItem(item, "Sale") * item.quantity;

        Inventory.instance.Remove(item);
        ShopInventory.instance.AddToSoldSlot(item);
        ScriptToolbox.GetInstance().GetPlayerWallet().Deposit(price);
        ScriptToolbox.GetInstance().GetShopDialogue().SetCurrentMessage(LoadShop.MessageType.SUCCESS);
    }


    #endregion
}
