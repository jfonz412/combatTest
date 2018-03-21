﻿using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour {

    #region Singleton
    //insures that we can easily access the inventory, and that there is only one 
    //inventory at all times
    public static ShopInventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SHOP inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int inventorySpace;

    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    public SoldSlot soldSlot;
    Item lastItemSold;

    void Start()
    {
        //fill inventory with null spaces
        for (int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }

    public void AddToSoldSlot(Item item)
    {
        lastItemSold = item;
        soldSlot.AddItem(lastItemSold);
    }

    #region probably won't use these
    public bool AddToFirstEmptySlot(Item item)
    {
        //check if there are any empty slots/items
        for (int i = 0; i < inventorySpace; i++)
        {
            if (items[i] == null)
            {
                item = CheckIfAlreadyInstantiated(item);

                InsertItemIntoEmptySlot(item, i);

                Callback();
                return true;
            }
        }

        Debug.Log("Inventory is full");
        return false;
    }

    public void AddToSpecificSlot(Item item)
    {
        int slotNum = item.slotNum.GetValueOrDefault();

        items.RemoveAt(slotNum);
        items.Insert(slotNum, item);

        //Debug.Log("Instance ID of "+ items[i] + " is: " + items[i].GetInstanceID()); 

        Callback();
    }


    public void Remove(Item item)
    {
        int itemIndex = item.slotNum.GetValueOrDefault();

        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);

        Callback();

    }

    void InsertItemIntoEmptySlot(Item item, int slotNum)
    {
        items.RemoveAt(slotNum); //remove the null item
        items.Insert(slotNum, item); //replace it with actual item

        items[slotNum].slotNum = slotNum; //save refrence to the slot it's been placed in
    }

    Item CheckIfAlreadyInstantiated(Item item)
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
    #endregion

    void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }


}