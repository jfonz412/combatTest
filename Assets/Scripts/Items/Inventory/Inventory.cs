using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int inventorySpace;
    public List<Item> items = new List<Item>();


    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    void Start()
    {
        //fill inventory with null spaces
        for(int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }

    //returns the amount of leftover items that could not be stacked
    public int AddItem(Item item)
    {
        //item = ConvertToInventoryItem(item);

        int leftovers = AttemptToStackItem(item);

        if(leftovers == 0)
        {
            return 0;
        }
        else if(AddToFirstEmptySlot(item))
        {
            return 0;
        }
        else
        {
            return leftovers;
        }
    }

    public void AddToSpecificSlot(Item item)
    {
        int slotNum = item.slotNum.GetValueOrDefault();

        items.RemoveAt(slotNum);     
        items.Insert(slotNum, item);

        Callback();
    }

    //called from slotclick after an item is sold from inventory 
    public void CondenseStackables(Item item, int amountRemoved)
    {
        if (item.quantity - amountRemoved < 1)
        {
            Remove(item);
        }
        else
        {
            Item newCopyOfItemForInventory = Instantiate(item);
            newCopyOfItemForInventory.quantity -= amountRemoved;

            Remove(item); //and destroy?
            AddItem(newCopyOfItemForInventory);
        }
    }

    //Removes but doesn't destroy the item
    public void Remove(Item item) 
    {
        int itemIndex = item.slotNum.GetValueOrDefault();
        Debug.Log(itemIndex);
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);

        Callback();
    }

    //for trashcan
    public void RemoveAndDestroy(Item item)
    {
        Remove(item);
        Destroy(item); 
    }

    //for PlayerSaveData to save inventory
    public SavedItem[] GetItemInfo()
    {
        SavedItem[] itemInfo = new SavedItem[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                itemInfo[i].fileName = items[i].GetResourcePath();
                itemInfo[i].quantity = items[i].quantity;
            }
        }
        return itemInfo;
    }

    #region private methods

    private int AttemptToStackItem(Item newItem)
    {
        if (!newItem.stackable)
            return newItem.quantity;

        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] != null)
            {
                if (items[i].name == newItem.name)
                {
                    newItem.quantity = AddQuantityToSlot(i, newItem.quantity);
                }
            }
        }

        return newItem.quantity;
    }

    private int AddQuantityToSlot(int slot, int newItemQuantity)
    {
        int maxQ = items[slot].maxQuantity;

        while (items[slot].quantity != maxQ && newItemQuantity != 0)
        {
            items[slot].quantity++;
            newItemQuantity--;
        }

        return newItemQuantity;
    }

    private bool AddToFirstEmptySlot(Item item)
    {
        //check if there are any empty slots/items
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                item = ConvertToInventoryItem(item);

                InsertItemIntoEmptySlot(item, i);

                Callback();
                return true;
            }
        }

        Debug.Log("Inventory is full");
        return false;
    }

    private void InsertItemIntoEmptySlot(Item item, int slotNum)
    {
        items.RemoveAt(slotNum); //remove the null item
        items.Insert(slotNum, item); //replace it with actual item

        items[slotNum].slotNum = slotNum; //save refrence to the slot it's been placed in
    }

    private Item ConvertToInventoryItem(Item item)
    {
        //if slotNum is null then it has not been in our inventory needs an instance
        //otherwise we do not create a new instance and simply use the item as it is
        if (item.slotNum == null)
        {
            item = Instantiate(item);
            Debug.Log("No slotNum found, instantiating new object (" + item.slotNum + ")");
        }

        return item;
    }

    private void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }
    #endregion
}
