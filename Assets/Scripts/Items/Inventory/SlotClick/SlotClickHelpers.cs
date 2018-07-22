using System.Collections;
using UnityEngine;

public class SlotClickHelpers : MonoBehaviour {

    private PlayerWallet playerWallet;
    private ShopDialogue shopDialogue;
    private Inventory inv;
    private ShopInventory shop;
    private QuantityPrompt qntyPrompt;
    private Brain playerBrain;

    void Start()
    {
        playerWallet = ScriptToolbox.GetInstance().GetPlayerWallet();
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();
        inv = InventoryManager.GetInstance().GetInventory();
        shop = InventoryManager.GetInstance().GetShopInventory();
        shopDialogue = InventoryManager.GetInstance().GetShopDialogue();
        qntyPrompt = InventoryManager.GetInstance().GetQuantityPrompt();
    }


    #region EquipSlot
    //HELPERS

    public void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");   //or equipment == naked or unarmed?
        Item previousItem = slot.equipment;                //save a copy of the slotItem
        slot.equipmentManager.Unequip(previousItem.myEquipSlot); //unequip item currently in equip slot
        mouseSlot.UpdateItem(previousItem);                //place previous item in the mouseSlot (as an item)?
    }

    public void PlaceItemInEmptySlot(MouseSlot mouseSlot, EquipSlot slot)
    {
        Item mouseItem = mouseSlot.currentItem;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot(mouseItem.myEquipSlot, slot))
        {
            return;
        }

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        slot.equipmentManager.Equip(mouseItem);
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    public void SwapItems(MouseSlot mouseSlot, EquipSlot slot)
    {
        Item mouseItem = mouseSlot.currentItem;

        //make sure equipment would be going in the correct slot
        if (!CheckEquipSlot(mouseItem.myEquipSlot, slot))
        {
            return;
        }

        Debug.Log("SWAPPING ITEMS");
        Item previousItem = slot.equipment;        //save a copy of the slotItem
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

    bool CheckEquipSlot(Item.EquipmentSlot mouseItemEquipSlot, EquipSlot slot)
    {
        if (mouseItemEquipSlot != slot.slotType)
        {
            Debug.Log("WRONG EQUIP SLOT: " + slot.slotType + " looking for " + mouseItemEquipSlot);
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
        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
        Item previousItem = slot.item;             //save a copy of the slotItem
        inv.Remove(previousItem);       //remove the item in the slot 
        mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot          
    }

    public void PlaceItemInEmptySlot(MouseSlot mouseSlot, InventorySlot slot)
    {
        Item mouseItem = mouseSlot.currentItem;

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        mouseItem.mySlotNum = slot.slotNum; //assign item's slotNum to this slot
        inv.AddToSpecificSlot(mouseItem); //drop item in slot
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    public void SwapItems(MouseSlot mouseSlot, InventorySlot slot)
    {
        Item mouseItem = mouseSlot.currentItem;
        Item previousItem = slot.item;

        Debug.Log("SWAPPING ITEMS");
        mouseItem.mySlotNum = slot.slotNum;              //assign item's slotNum to this slot
        inv.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
        mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot
    }

    //SHOPPPING HELPERS

    public IEnumerator SellItem(Item item)
    {
        int quantity = 1;
        if (item.quantity > 1)
        {
            qntyPrompt.TriggerPrompt();
            while (qntyPrompt.waitingForInput && playerBrain.ActiveState(Brain.State.Prompted))
            {
                yield return null;
            }
            quantity = qntyPrompt.GetQuantity();

            if (quantity < 1 || quantity > item.quantity)
            {
                Debug.Log("Invalid quantity input: " + quantity);
                shopDialogue.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY);
                yield break; //do nothing if quantity is 0 
            }
        }

        CommitSale(item, quantity);
    }


    void CommitSale(Item item, int quantity)
    {
        float price = PriceChecker.AppraiseItem(item, "Sale") * quantity;

        if(shop.CheckSpaceAndGold(item, quantity, price))
        {
            playerWallet.Deposit(price);
            Debug.Log("You have been credited $" + price);

            inv.CondenseStackables(item, quantity);

            item.quantity = quantity;
            shop.AddItem(item);

            shopDialogue.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
        else
        {
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY);
        }
    }

    #endregion

    #region ShopSlot

    public IEnumerator PurchaseItem(Item item)
    {
        int quantity = 1;

        if (item.quantity > 1)
        {
            qntyPrompt.TriggerPrompt();
            while (qntyPrompt.waitingForInput && playerBrain.ActiveState(Brain.State.Prompted))
            {
                yield return null;
            }

            quantity = qntyPrompt.GetQuantity();

            if (quantity < 1 || quantity > item.quantity)
            {
                Debug.Log("Invalid quantity input: " + quantity);
                shopDialogue.SetCurrentMessage(LoadShop.MessageType.INVAL_QNTY);
                yield break; //do nothing if quantity is 0 
            }
        }

        CheckPlayerFunds(item, quantity);
    }

    void CheckPlayerFunds(Item item, int quantity)
    {
        float price = PriceChecker.AppraiseItem(item, "Purchase") * quantity;

        if (price <= playerWallet.GetCurrentBalance())
        {
            CommitPurchase(item, quantity, price);
        }
        else
        {
            Debug.Log("Insufficient funds, balance: $" + playerWallet.GetCurrentBalance());
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.LOW_GOLD);
        }
    }

    //PURCHASE HELPERS
    void CommitPurchase(Item item, int quantity, float price)
    {
        Item newItem = CreateNewItemForInventory(item, quantity);

        if(InventoryManager.GetInstance().GetInventorySpaceChecker().CheckItem(newItem))
        {
            AdjustShopStock(item, quantity);
            inv.AddItem(newItem);
            playerWallet.Withdraw(price);
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.SUCCESS);
        }
        else
        {
            shopDialogue.SetCurrentMessage(LoadShop.MessageType.INV_FULL);
            //Debug.LogWarning("Item has been added to your inventory even though the inventory is full. You did not pay.");
        }

    }

    void AdjustShopStock(Item item, int quantity)
    {
        if (item.quantity - quantity < 1)
        {
            shop.Remove(item); //and destroy???
        }
        else
        {
            item.quantity -= quantity;
        }
    }

    Item CreateNewItemForInventory(Item item, int quantity)
    {
        Debug.Log("need to implement this");
        //Item newItem = Instantiate(item);
        //newItem.quantity = quantity;
        return new Item();
    }

    #endregion
}
