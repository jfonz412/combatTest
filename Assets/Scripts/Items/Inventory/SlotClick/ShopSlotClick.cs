using UnityEngine;

public class ShopSlotClick : MonoBehaviour {
    SlotClickHelpers slotClickHelper;

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
    }


    #region Shop Slot Clicks

    public void ShopSlotRightClicked(Item item)
    {
        float price = PriceChecker.AppraiseItem(item, "Purchase") * item.quantity;

        if (PlayerWallet.instance.balance >= price && CheckInventorySpace.CheckItem(item))
        {
            ShopInventory.instance.Remove(item);
            Inventory.instance.AddItem(item);
            PlayerWallet.instance.Withdraw(price);
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
        else
        {
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY); //SHOULD BE "GENERIC_NO"
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
