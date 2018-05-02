using UnityEngine;

public class ShopSlotClick : MonoBehaviour {

    private SlotClickHelpers slotClickHelper;
    private PlayerWallet playerWallet;

    #region Singleton

    public static ShopSlotClick instance;

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
        playerWallet = ScriptToolbox.GetInstance().GetPlayerWallet();
    }


    #region Shop Slot Clicks

    public void ShopSlotRightClicked(Item item)
    {
        ShopDialogue shopDialogue = ScriptToolbox.GetInstance().GetShopDialogue();

        float price = PriceChecker.AppraiseItem(item, "Purchase") * item.quantity;

        if (playerWallet.balance >= price && CheckInventorySpace.CheckItem(item))
        {
            ShopInventory.instance.Remove(item);
            Inventory.instance.AddItem(item);
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
