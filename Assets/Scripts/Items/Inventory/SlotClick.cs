using System.Collections;
using UnityEngine;

//THIS SCRIPT IS GETTING BIG, CONSIDER SPLITTING IT INTO 3 SEPERATE SCRIPTS?
public class SlotClick : MonoBehaviour {

    SlotClickHelpers slotClickHelper;

    #region Singleton

    public static SlotClick instance;

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

    public  void EquipSlotRightClicked(EquipSlot slot)
    {
        Debug.Log("Equip slot right clicked");
        if(slot.equipment != null)
        {
            slot.equipmentManager.FastUnequip(slot.equipment);
        }
    }

    public  void EquipSlotHoverOver(EquipSlot slot)
    {
        if (slot.equipment != null)
        {
            slot.equipment.OpenStatWindow("Inventory");
        }
    }


    public  void EquipSlotLeftClicked(EquipSlot slot)
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

    #region Inventory Slot Clicks

    public  void InventorySlotRightClicked(Item item)
    {
        Debug.Log("Inventory slot right clicked");
        if (item != null)
        {
            item.Use();
        }           
    }

    public  void InventorySlotHoverOver(Item item)
    {
        if (item != null)
        {
            item.OpenStatWindow("Inventory");
        }
    }

    public  void InventorySlotLeftClicked(InventorySlot slot)
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

    public  void LeftClickedToSell(InventorySlot slot)
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
        PlayerWallet.instance.Deposit(price);
        Debug.Log("Item sold");
        
    }


    #endregion

    #region Shop Slot Clicks

    public void ShopSlotRightClicked(Item item)
    {
        float price = PriceChecker.AppraiseItem(item, "Purchase") * item.quantity;

        if (PlayerWallet.instance.balance >= price)
        {
            ShopInventory.instance.Remove(item);
            Inventory.instance.AddItem(item);
            PlayerWallet.instance.Withdraw(price);
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
    }

    public  void ShopSlotHoverOver(Item item)
    {
        if (item != null)
        {
            item.OpenStatWindow("Shop");
        }
    }

    public  void ShopSlotLeftClicked(ShopSlot slot)
    {
        Item item = slot.item;

        if (item != null)
        {
            StartCoroutine(slotClickHelper.PurchaseItem(item));
        }
    }
    #endregion
}
