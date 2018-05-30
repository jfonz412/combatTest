using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public int inventorySpace = 18;
    private LoadShop currentLoadedShop;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

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

    public void ClearShopInventory()
    {
        UpdateNPCShopInventory();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                Remove(items[i]);
                Destroy(items[i]);
            }
        }
    }

    public void Remove(Item item)
    {
        int itemIndex = item.slotNum.GetValueOrDefault();
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);
        Callback();
    }

    //this assumes CheckSpaceAndGold() has been ran for this item, if it has slotClicks will use this to add items
    public void AddItem(Item item)
    {
        //item = ConvertToInventoryItem(item);

        int leftovers = AttemptToStackItem(item);

        if (leftovers == 0)
        {
            return;
        }
        else
        {
            AddToFirstEmptySlot(item);
        }
    }

    public bool CheckSpaceAndGold(Item item, int quantity, float price)
    {
        //if price <= shopGold
        return CheckForSpace(item, quantity);
    }

    #region Stacking stackables

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
                item = CheckIfAlreadyInstantiated(item);

                InsertItemIntoEmptySlot(item, i);

                Callback();
                return true;
            }
        }

        Debug.Log("Inventory is full");
        return false;
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

    #endregion

    #region CHECK IF WE CAN PURCHASE ITEM

    private bool CheckForSpace(Item item, int quantity)
    {
        int leftovers = CheckOccupiedSpaces(item, quantity);

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

    private bool EmptySlots()
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

    private int CheckOccupiedSpaces(Item item, int newItemQ)
    {
        if (!item.stackable)
            return newItemQ;

        //int newItemQ = item.quantity;
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


    //saves NPC Shop inventory when done shopping
    private void UpdateNPCShopInventory()
    {
        if (currentLoadedShop != null)
            currentLoadedShop.UpdateInventory(items);
        currentLoadedShop = null;
    }

    private void Callback()
    {
        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }
}
