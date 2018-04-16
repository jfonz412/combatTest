﻿using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int inventorySpace;
    public List<Item> items = new List<Item>();


    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    #region Singleton
    //insures that we can easily access the inventory, and that there is only one 
    //inventory at all times
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }
        instance = this;
    }
    #endregion

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
     
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);

        Callback();

    }

    public void RemoveAndDestroy(Item item)
    {
        Remove(item);
        Destroy(item); 
    }

    #region private methods
    int AttemptToStackItem(Item newItem)
    {
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

    int AddQuantityToSlot(int slot, int newItemQuantity)
    {
        int maxQ = items[slot].maxQuantity;

        while (items[slot].quantity != maxQ && newItemQuantity != 0)
        {
            items[slot].quantity++;
            newItemQuantity--;
        }

        return newItemQuantity;
    }

    bool AddToFirstEmptySlot(Item item)
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

    void InsertItemIntoEmptySlot(Item item, int slotNum)
    {
        items.RemoveAt(slotNum); //remove the null item
        items.Insert(slotNum, item); //replace it with actual item

        items[slotNum].slotNum = slotNum; //save refrence to the slot it's been placed in
    }

    Item ConvertToInventoryItem(Item item)
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

    void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }
    #endregion

    #region Check for open spaces

    //USED BY SlotClick.cs and SlotClickHelper.cs
    public bool CheckInventorySpace(Item item)
    {
        int leftovers = CheckOccupiedSpaces(item);

        if (leftovers == 0)
        {
            Debug.Log("Item will fit (CheckOccupiedSpaces()");
            return true;
        }
        else if (EmptySlots())
        {
            Debug.Log("Item will fit (EmpytSlots())");
            return true;
        }
        else
        {
            Debug.Log("Item will NOT fit");
            return false;
        }
    }

    bool EmptySlots()
    {
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    int CheckOccupiedSpaces(Item item)
    {
        int newItemQ = item.quantity;
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] != null)
            {
                if (items[i].name == item.name)
                {
                    int q = items[i].quantity;
                    int maxQ = items[i].maxQuantity;

                    while (q != maxQ && newItemQ != 0)
                    {
                        q++;
                        newItemQ--;
                    }
                }
            }
        }
        return newItemQ;
    }
    #endregion
}
