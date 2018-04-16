using System.Collections;
using UnityEngine;

public class SlotClickHelpers : MonoBehaviour {

    #region Singleton

    public static SlotClickHelpers instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SlotClickHelpers found");
            return;
        }
        instance = this;
    }
    #endregion

    #region EquipSlot
    //HELPERS

    public void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");   //or equipment == naked or unarmed?
        Equipment previousItem = slot.equipment;                //save a copy of the slotItem
        slot.equipmentManager.Unequip((int)previousItem.equipSlot); //unequip item currently in equip slot
        mouseSlot.UpdateItem(previousItem);                //place previous item in the mouseSlot (as an item)?
    }

    public void PlaceItemInEmptySlot(MouseSlot mouseSlot, EquipSlot slot)
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

    public void SwapItems(MouseSlot mouseSlot, EquipSlot slot)
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
    public bool CheckItemType()
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

    #region InventorySlot
    //HELPERS 

    public void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;

        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
        Item previousItem = slot.item;             //save a copy of the slotItem
        inventory.Remove(previousItem);       //remove the item in the slot 
        mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot          
    }

    public void PlaceItemInEmptySlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        mouseItem.slotNum = slot.slotNum; //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem); //drop item in slot
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    public void SwapItems(MouseSlot mouseSlot, InventorySlot slot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;
        Item previousItem = slot.item;

        Debug.Log("SWAPPING ITEMS");
        mouseItem.slotNum = slot.slotNum;              //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
        mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot
    }

    //SHOPPPING HELPERS

    public IEnumerator SellItem(Item item)
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
                ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY);
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

        Inventory.instance.CondenseStackables(item, quantity);

        item.quantity = quantity;
        ShopInventory.instance.AddToSoldSlot(item);

        ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
    }

    #endregion

    #region ShopSlot

    public IEnumerator PurchaseItem(Item item)
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
                ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY);
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
            CommitPurchase(item, quantity, price);
        }
        else
        {
            Debug.Log("Insufficient funds, balance: $" + wallet.balance);
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.LOW_GOLD);
        }
    }

    //PURCHASE HELPERS
    void CommitPurchase(Item item, int quantity, float price)
    {
        PlayerWallet wallet = PlayerWallet.instance;
        Item newItem = CreateNewItemForInventory(item, quantity);

        if(Inventory.instance.CheckInventorySpace(newItem))
        {
            AdjustShopStock(item, quantity);
            Inventory.instance.AddItem(newItem);
            wallet.Withdraw(price);
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
        else
        {
            ShopDialogue.instance.SetCurrentMessage(LoadShop.MessageType.INV_FULL);
            //Debug.LogWarning("Item has been added to your inventory even though the inventory is full. You did not pay.");
        }

    }

    void AdjustShopStock(Item item, int quantity)
    {
        if (item.quantity - quantity < 1)
        {
            ShopInventory.instance.Remove(item); //and destroy???
        }
        else
        {
            item.quantity -= quantity;
        }
    }

    Item CreateNewItemForInventory(Item item, int quantity)
    {
        Item newItem = Instantiate(item);
        newItem.quantity = quantity;
        return newItem;
    }

    void CreateNewItemForShop(Item item, int quantity)
    {
        Item newCopyOfItemForShop = Instantiate(item);
        newCopyOfItemForShop.quantity = quantity;
        ShopInventory.instance.AddToSoldSlot(item);
    }

    #endregion
}
