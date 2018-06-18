using UnityEngine;

public class ShopSlotClick : MonoBehaviour {

    private SlotClickHelpers slotClickHelper;
    private PlayerWallet playerWallet;

    void Start()
    {
        slotClickHelper = InventoryManager.GetInstance().GetSlotClickHelpers();
        playerWallet = ScriptToolbox.GetInstance().GetPlayerWallet();
    }


    #region Shop Slot Clicks

    public void ShopSlotRightClicked(Item item)
    {
        if (item == null)
            return;

        ShopDialogue shopDialogue = InventoryManager.GetInstance().GetShopDialogue();
        CheckInventorySpace invCheck = InventoryManager.GetInstance().GetInventorySpaceChecker();

        float price = PriceChecker.AppraiseItem(item, "Purchase") * item.quantity;

        if (playerWallet.GetCurrentBalance() >= price && invCheck.CheckItem(item))
        {
            InventoryManager.GetInstance().GetShopInventory().Remove(item);
            InventoryManager.GetInstance().GetInventory().AddItem(item);
            playerWallet.Withdraw(price);
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
        else
        {
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY); //SHOULD BE "GENERIC_NO"
        }
    }

    public void ShopSlotHoverOver(Item item)
    {
        if (item != null)
        {
            item.OpenStatWindow("Shop");
        }
    }

    public void ShopSlotLeftClicked(ShopSlot slot)
    {
        Item item = slot.item;

        if (item != null)
        {
            StartCoroutine(slotClickHelper.PurchaseItem(item));
        }
    }
    #endregion
}
