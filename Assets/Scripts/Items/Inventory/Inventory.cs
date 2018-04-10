using System.Collections.Generic;
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

    //this returns the amount of leftover items that could not be added
    public int AddItem(Item item) //kind of more like pickup item
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

    int AttemptToStackItem(Item newItem)
    {
        for (int i = 0; i < inventorySpace; i++)
        {
            if(items[i] != null)
            {
                if (items[i].name == newItem.name)
                {
                    newItem.quantity = TransferQuantity(i, newItem.quantity);
                }
            }
        }

        return newItem.quantity;
    }

    int TransferQuantity(int slot, int newItemQuantity)
    {
        int maxQ = items[slot].maxQuantity;

        while (items[slot].quantity != maxQ && newItemQuantity != 0) //or?
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
            if(items[i] == null)
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

    public void AddToSpecificSlot(Item item)
    {
        int slotNum = item.slotNum.GetValueOrDefault();

        items.RemoveAt(slotNum);     
        items.Insert(slotNum, item);

        Callback();
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

    void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
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
}
