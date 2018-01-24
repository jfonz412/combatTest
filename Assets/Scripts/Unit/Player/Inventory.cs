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

    //creates a callback?
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged; 

    public int space = 10;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Inventory is full");
                return false;
            }
            items.Add(item);

            if (onInventoryChanged != null)
            {
                onInventoryChanged.Invoke(); //callback
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onInventoryChanged != null)
        {
            onInventoryChanged.Invoke(); //callback
        }
    }
}
