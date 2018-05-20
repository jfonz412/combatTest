using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public int inventorySpace;
    private LoadShop currentLoadedShop;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    public SoldSlot soldSlot;
    private Item lastItemSold;

    void Start()
    {
        //fill shop with null spaces
        for (int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }

    public void LoadShopInventory(List<Item> shopInventory, LoadShop npcShop)
    {
        for (int i = 0; i < shopInventory.Count; i++)
        {
            if (shopInventory[i] != null)
            {
                AddToFirstEmptySlot(shopInventory[i]);
            }
        }

        currentLoadedShop = npcShop;
    }

    public void AddToSoldSlot(Item item)
    {
        if (lastItemSold != null)
            AddToFirstEmptySlot(lastItemSold);

        int lastSlot = inventorySpace - 1; 
        lastItemSold = item;
        lastItemSold.slotNum = lastSlot;
        items.Insert(lastSlot, lastItemSold);
        soldSlot.AddItem(lastItemSold);
    }

    private bool AddToFirstEmptySlot(Item item)
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
 
    public void Remove(Item item)
    {
        int itemIndex = item.slotNum.GetValueOrDefault();
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);
        Callback();
    }

    public void ClearShopInventory()
    {
        UpdateNPCShopInventory();

        for(int i = 0; i<items.Count; i++)
        {
            if(items[i] != null)
            {
                Remove(items[i]);
                Destroy(items[i]);
            }
        }
    }

    //called by SoldSlot's button when it is clicked to clear last item sold
    public void ClearLastItemSold()
    {
        Debug.Log("Clearing last item sold");
        lastItemSold = null;
    }


    private Item CheckIfAlreadyInstantiated(Item item)
    {
        if (item.slotNum == null)
        {
            item = Instantiate(item);
            //Debug.Log("No slotNum found, instantiating new object (" + item.slotNum + ")");
        }

        return item;
    }

    private void InsertItemIntoEmptySlot(Item item, int slotNum)
    {
        items.RemoveAt(slotNum); //remove the null item
        items.Insert(slotNum, item); //replace it with actual item

        items[slotNum].slotNum = slotNum; //save refrence to the slot it's been placed in
    }

    private void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }

    private void UpdateNPCShopInventory()
    {
        if(currentLoadedShop != null)
            currentLoadedShop.UpdateInventory(items);
        currentLoadedShop = null;
    }
}
