using System.Collections;
using UnityEngine;

//THIS SCRIPT IS GETTING BIG, CONSIDER SPLITTING IT INTO 3 SEPERATE SCRIPTS?
public class SlotClick : MonoBehaviour {

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

        if (CheckItemType() == false)
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
            PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment == null) //or slot.equipment == naked or unarmed?
        {
            PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.equipment != null) //or slot.equipment == naked or unarmed?
        {
            SwapItems(mouseSlot, slot);
            return;
        }
    }

    //HELPERS

     void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");   //or equipment == naked or unarmed?
        Equipment previousItem = slot.equipment;                //save a copy of the slotItem
        slot.equipmentManager.Unequip((int)previousItem.equipSlot); //unequip item currently in equip slot
        mouseSlot.UpdateItem(previousItem);                //place previous item in the mouseSlot (as an item)?
    }

     void PlaceItemInEmptySlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot, slot))
        {
            return;
        }

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        slot.equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

     void SwapItems(MouseSlot mouseSlot, EquipSlot slot)
    {
        Equipment mouseItem = mouseSlot.currentItem as Equipment;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot((int)mouseItem.equipSlot, slot))
        {
            return;
        }

        Debug.Log("SWAPPING ITEMS");
        Equipment previousItem = slot.equipment;        //save a copy of the slotItem
        slot.equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(previousItem);        //add old item to mouseSlot
    }


    //----------------------------------------------
     bool CheckItemType()
    {
        if (MouseSlot.instance.currentItem == null)
        {
            return true;
        }
        else if (MouseSlot.instance.currentItem.GetType() == typeof(Armor))
        {
            return true;
        }
        else if (MouseSlot.instance.currentItem.GetType() == typeof(Weapon))
        {
            return true;
        }
        else
        {
            Debug.Log("MOUSEITEM NOT EQUIPMENT");
            return false;
        }
    }

     bool CheckEquipSlot(int mouseItemEquipSlot, EquipSlot slot)
    {
        if (mouseItemEquipSlot != slot.equipSlot)
        {
            Debug.Log("WRONG EQUIP SLOT ");
            return false;
        }
        else
        {
            return true;
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
            PickUpItemIntoEmptyMouseSlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item == null)
        {
            PlaceItemInEmptySlot(mouseSlot, slot);
            return;
        }

        if (mouseItem != null && slot.item != null)
        {
            SwapItems(mouseSlot, slot);
            return;
        }
    }

    public  void LeftClickedToSell(InventorySlot slot)
    {
        Item item = slot.item;
        //int quantity = PromptForQuantity();
        if (item != null)
        {
            StartCoroutine(SellItem(item));
        }
    }

    //HELPERS 

     void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;

        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
        Item previousItem = slot.item;             //save a copy of the slotItem
        inventory.Remove(previousItem);       //remove the item in the slot 
        mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot            //place previous item in the mouseSlot (as an item)?
    }

     void PlaceItemInEmptySlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        mouseItem.slotNum = slot.slotNum; //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem); //drop item in slot
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

     void SwapItems(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;
        Item previousItem = slot.item;

        Debug.Log("SWAPPING ITEMS");
        mouseItem.slotNum = slot.slotNum;              //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
        mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot
    }

    //SHOP HELPERS

    IEnumerator SellItem(Item item)
    {
        int quantity = 1;
        if (item.quantity > 1)
        {
            QuantityPrompt.instance.TriggerPrompt();
            while (QuantityPrompt.instance.waitingForInput && PlayerState.currentState == PlayerState.PlayerStates.Prompt)
            {
                yield return null;
            }
            quantity = QuantityPrompt.instance.GetQuantity();

            if (quantity < 1 || quantity > item.quantity)
            {
                Debug.Log("Invalid quantity input: " + quantity);
                yield break; //do nothing if quantity is 0 
            }
        }

        CommitSale(item, quantity);     
    }


    void CommitSale(Item item, int quantity)
    {
        float price = PriceChecker.AppraiseItem(item, "Sale") * quantity;
        PlayerWallet.instance.Deposit(price);
        Debug.Log("You have been credited $" + price);

        CondenseStackables(item, quantity);

        item.quantity = quantity;
        ShopInventory.instance.AddToSoldSlot(item);
    }

    void CondenseStackables(Item item, int quantity)
    {
        Inventory inv = Inventory.instance;
        if (item.quantity - quantity < 1)
        {
            inv.Remove(item);
        }
        else
        {
            Item newCopyOfItemForInventory = Instantiate(item);
            newCopyOfItemForInventory.quantity -= quantity;

            inv.Remove(item);
            inv.AddItem(newCopyOfItemForInventory);
        }
    }

    #endregion

    #region Shop Slot Clicks

    public  void ShopSlotRightClicked(Item item)
    {
        Debug.Log("Shop slot right clicked (does nothing)");
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
            StartCoroutine(PurchaseItem(item));
        }
    }

    //HELPERS

    IEnumerator PurchaseItem(Item item)
    {
        int quantity = 1;

        if(item.quantity > 1)
        {
            QuantityPrompt.instance.TriggerPrompt();
            while (QuantityPrompt.instance.waitingForInput && PlayerState.currentState == PlayerState.PlayerStates.Prompt)
            {
                yield return null;
            }

            quantity = QuantityPrompt.instance.GetQuantity();

            if (quantity < 1 || quantity > item.quantity)
            {
                Debug.Log("Invalid quantity input: " + quantity);
                yield break; //do nothing if quantity is 0 
            }
        }

        CheckPlayerFunds(item, quantity);
    }

    void CheckPlayerFunds(Item item, int quantity)
    {
        PlayerWallet wallet = PlayerWallet.instance;

        float price = PriceChecker.AppraiseItem(item, "Purchase") * quantity;

        if (price <= wallet.balance)
        {
            wallet.Withdraw(price);
            CommitPurchase(item, quantity);
        }
        else
        {
            Debug.Log("Insufficient funds, balance: $" + wallet.balance);
        }
    }

    //PURCHASE HELPERS
    void CommitPurchase(Item item, int quantity)
    {

        if (item.quantity - quantity < 1)
        {
            ShopInventory.instance.Remove(item);
        }
        else
        {
            item.quantity -= quantity;
        }

        CreateNewItemForInventory(item, quantity);
    }

    void CreateNewItemForInventory(Item item, int quantity)
    {
        Item newCopyOfItemForInventory = Instantiate(item);
        newCopyOfItemForInventory.quantity = quantity;
        Inventory.instance.AddItem(newCopyOfItemForInventory);
    }

    void CreateNewItemForShop(Item item, int quantity)
    {
        Item newCopyOfItemForShop = Instantiate(item);
        newCopyOfItemForShop.quantity = quantity;
        ShopInventory.instance.AddToSoldSlot(item);
    }

    #endregion
}
