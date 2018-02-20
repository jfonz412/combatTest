using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

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

    //creates a callback
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged; 

    public List<Item> items = new List<Item>(); 
    public int inventorySpace;

    void Start()
    {
        //fill inventory with null spaces
        for(int i = 0; i < inventorySpace; i++)
        {
            items.Add(null);
        }
    }

    //change to Pickup()? maybe after equipment item slot functionality is set
    public bool Add(Item item)
    {
        //check if there are any empty slots/items
        for (int i = 0; i < inventorySpace; i++)
        {
            if(items[i] == null)
            {
                //if slotNum is null then it has not been in our inventory needs an instance
                //otherwise we do not create a new instance and simply use the item as it is
                if(item.slotNum == null)
                {
                    item = Instantiate(item);
                    Debug.Log("No slotNum found, instantiating new object (" + item.slotNum + ")");
                }
                    

                items.RemoveAt(i); //remove the null item
                items.Insert(i, item); //replace it with actual item

                items[i].slotNum = i; //save refrence to the slot it's been placed in

                //Debug.Log("Instance ID of "+ items[i] + " is: " + items[i].GetInstanceID()); 

                if (onInventoryChanged != null)
                {
                    onInventoryChanged.Invoke(); 
                }
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

        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke();
        }
    }

    public void Remove(Item item)
    {
        int itemIndex = item.slotNum.GetValueOrDefault();
     
        items.RemoveAt(itemIndex);
        items.Insert(itemIndex, null);

        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }

    }

    public void RemoveAndDestroy(Item item)
    {
        Remove(item);
        Destroy(item); //should eventually remove item from memory...
    }
}
