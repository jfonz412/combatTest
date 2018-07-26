using System.Collections.Generic;
using UnityEngine;

//probably should derive from shop inventory base class of some sort so that LoadShops can be called dynamically via it's base
public class DemoburghShops : MonoBehaviour {

    private static Dictionary<string, Item[]> townShops = new Dictionary<string, Item[]>();

    public static Item[] GetInventory(string shopOwner)
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

    public Dictionary<string, Item[]> SaveShops()
    {
        return townShops;
    }
	
	private void InstantiateItems()
    {
        townShops.Add(
            "Old Armsman",
            new Item[]
            {
                MasterItemList.IronChest()
            });
    }
}
