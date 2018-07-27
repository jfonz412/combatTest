using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInventory : MonoBehaviour {
    protected Dictionary<string, Item[]> townShops = new Dictionary<string, Item[]>();

    public Item[] GetInventory(string shopOwner)
    {
        return townShops[shopOwner];
    }

    //someone needs to run this method and save the shops
    public void LoadShops(Dictionary<string, Item[]> savedShops = null)
    {
        if (savedShops == null)
        {
            InstantiateItems();
            Debug.Log("saved shops not found, instatiating items");
        }
        else
        {
            townShops = savedShops; //link these or make a new copy? if we leave them linked items will be pdated automatically for better or worse...
            Debug.Log("Saved shops found and loaded");
            Debug.LogWarning("You are linking these dictionaries");
        }
    }

    public void UpdateShop(string shopOwner, List<Item> updatedInventory)
    {
        townShops[shopOwner] = updatedInventory.ToArray();
    }

    public Dictionary<string, Item[]> SaveShops()
    {
        return townShops;
    }

    protected virtual void InstantiateItems()
    {
        //base
    }
}
