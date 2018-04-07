using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public int inventorySpace;

    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    public SoldSlot soldSlot;
    Item lastItemSold;

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

    void Start()
    {
        //fill shop with null spaces
        for (int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }


    public void AddToSoldSlot(Item item)
    {
        int lastSlot = inventorySpace - 1; 
        lastItemSold = item;
        lastItemSold.slotNum = lastSlot;
        items.Insert(lastSlot, lastItemSold);
        soldSlot.AddItem(lastItemSold);
    }

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

    Item CheckIfAlreadyInstantiated(Item item)
    {
        if (item.slotNum == null)
        {
            item = Instantiate(item);
            Debug.Log("No slotNum found, instantiating new object (" + item.slotNum + ")");
        }

        return item;
    }
 
    public void Remove(Item item)
    {
        int itemIndex = item.slotNum.GetValueOrDefault();
        Debug.Log("removing from " + itemIndex);
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

    public void ClearShopInventory()
    {
        for(int i = 0; i<items.Count; i++)
        {
            if(items[i] != null)
            {
                Remove(items[i]);
                Destroy(items[i]);
            }
        }
    }

    void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }


}
