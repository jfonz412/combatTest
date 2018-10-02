using System.Collections.Generic;
using UnityEngine;

//probably should derive from shop inventory base class of some sort so that LoadShops can be called dynamically via it's base
public class DemoburghShops : MarketInventory {
	
	protected override void InstantiateItems()
    {
        townShops.Add(
            "NPC",
            new Item[]
            {
                MasterItemList.IronChest(),
                MasterItemList.ClothShirt(),
                MasterItemList.ClothShirt(),
                MasterItemList.ClothShirt(),
                MasterItemList.HealthPotion(),
                MasterItemList.HealthPotion(),
                MasterItemList.IronDagger(),
                MasterItemList.WoodenShield(),
                MasterItemList.IronShield(),
                MasterItemList.Log(),
                MasterItemList.Log(),
                MasterItemList.Log()
            });
    }
}
