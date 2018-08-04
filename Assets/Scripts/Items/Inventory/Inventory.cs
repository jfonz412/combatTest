using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int inventorySpace;
    public List<Item> items;

    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    void Start()
    {
        //fill inventory with null spaces, gives our list the correct # of spaces
        for(int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }

    //returns the amount of leftover items that could not be stacked
    public int AddItem(Item item)
    {
        if (item == null)
            return 0;

        int leftovers = AttemptToStackItem(item);

        if(leftovers == 0)
        {
            return 0;
        }
        else if(AddToFirstEmptySlot(item))
        {
            Debug.Log("adding to first empty slot");
            return 0;
        }
        else
        {
            return leftovers;
        }
    }

    public void AddToSpecificSlot(Item item)
    {
        int slotNum = item.mySlotNum.GetValueOrDefault();

        items.RemoveAt(slotNum);     
        items.Insert(slotNum, item);

        StartCoroutine(Callback());
    }

    //called from slotclick after an item is sold from inventory 
    public void SubtractQuantityFromItem(Item item, int amountRemoved)
    {
        if (item.quantity - amountRemoved < 1)
        {
            Remove(item);
        }
        else
        {
            item.quantity -= amountRemoved;
        }
    }

    //Removes but doesn't destroy the item
    public void Remove(Item item) 
    {
        int itemIndex = item.mySlotNum.GetValueOrDefault();
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);

        StartCoroutine(Callback());
    }

    //for PlayerSaveData to save inventory
    public List<Item> GetInventoryItems()
    {
        return new List<Item>(items);
    }

    public void LoadSavedItems(List<Item> savedItems)
    {
        items = savedItems;
        StartCoroutine(Callback());
    }

    #region private methods

    private int AttemptToStackItem(Item newItem)
    {
        if (!newItem.stackable)
            return newItem.quantity;

        for (int i = 0; i < inventorySpace; i++)
        {
            Debug.Log("searching space " + i + " for " + newItem.name);
            if (items[i] != null)
            {
                Debug.Log(items[i].name + " found, comparing it's name to the new item");
                if (items[i].name == newItem.name)
                {
                    Debug.Log("item of same type found, stacking " + newItem.name + " onto " + items[i].name);
                    newItem.quantity = AddQuantityToSlot(i, newItem.quantity);
                }
            }
            Debug.Log("Done with this space");
        }

        Debug.Log("Returning newItem.quantity for " + newItem.name + " " + newItem.quantity);
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
        Debug.Log(items[slot].quantity);
        return newItemQuantity;
    }

    private bool AddToFirstEmptySlot(Item item)
    {
        //check if there are any empty slots/items
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                //item = ConvertToInventoryItem(item);

                InsertItemIntoEmptySlot(item, i);

                StartCoroutine(Callback());
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
        Debug.Log("quantity for " + item.name + " " + item.quantity);
        items[slotNum].mySlotNum = slotNum; //save refrence to the slot it's been placed in
    }

    private Item ConvertToInventoryItem(Item item)
    {
        //if slotNum is null then it has not been in our inventory needs an instance
        //otherwise we do not create a new instance and simply use the item as it is
        if (item.mySlotNum == null)
        {
            //item = Instantiate(item);
            Debug.Log("Need to use Item Master List here");
            //item.Init();
            //Debug.Log("No slotNum found, instantiating new object (" + item.slotNum + ")");
        }

        return item;
    }

    private void CallbackOLD()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
        else
        {
            Debug.LogError("NULL");
        }
    }

    private IEnumerator Callback()
    {
        while (onInventoryChanged == null)
        {
            yield return new WaitForSeconds(0.02f); //wait for UI to be ready
        }
        onInventoryChanged.Invoke(); //callback
    }
    #endregion
}
