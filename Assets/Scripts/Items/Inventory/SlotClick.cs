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
            SellItem(item); //pass quantity in also
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

     void SellItem(Item item)
    {
        float price = PriceChecker.AppraiseItem(item, "Sale");

        Inventory.instance.Remove(item);
        ShopInventory.instance.AddToSoldSlot(item);

        PlayerWallet.instance.Deposit(price);
        Debug.Log("You have been credited $" + price);
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
            StartCoroutine(PurchaseItem(item)); //should have a way to exit this if player is knocked out of shopping during a purchase, no?
        }
    }

    //HELPERS

    IEnumerator PurchaseItem(Item item)
    {
        PlayerWallet wallet = PlayerWallet.instance;
        QuantityPrompt.instance.TriggerPrompt();

        while (QuantityPrompt.instance.waitingForInput) //and playerstate is Prompt
        {
            yield return null; //should pause us
        }

        int quantity = QuantityPrompt.instance.GetQuantity();
        
        //will set to 0 if nothing is entered or cancel is selected
        if(quantity == 0)
        {
            yield break;
        }

        float price = PriceChecker.AppraiseItem(item, "Purchase") * quantity;

        if (price <= wallet.balance)
        {
            wallet.Withdraw(price); 

            //item.quantity = quantity;

            ShopInventory.instance.Remove(item); 
            Inventory.instance.AddItem(item); //must be after the Removal because this reassigns the slotNum
        }
        else
        {
            Debug.Log("Insufficient funds, balance: $" + wallet.balance);
        }
    }

    #endregion

    int PromptForQuantity()
    {
        QuantityPrompt.instance.TriggerPrompt();

        IEnumerator waitForInput = WaitForInput();
        StartCoroutine(waitForInput);
        Debug.Log("exiting coroutine loop");
        return QuantityPrompt.instance.enteredAmount;
    }
    
    IEnumerator WaitForInput()
    {
        while (QuantityPrompt.instance.waitingForInput)
        {
            yield return null;
        }
        Debug.Log("exiting WaitForInput");
    }
}
