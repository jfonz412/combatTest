using System.Collections.Generic;
using UnityEngine;

//probably should derive from shop inventory base class of some sort so that LoadShops can be called dynamically via it's base
public class DemoburghShops : MarketInventory {
	
	protected override void InstantiateItems()
    {
        townShops.Add(
            "Old Armsman",
            new Item[]
            {
                MasterItemList.IronChest(),
                MasterItemList.IronChest(),
                MasterItemList.ClothShirt(),
                MasterItemList.ClothShirt(),
                MasterItemList.ClothShirt(),
                MasterItemList.IronDagger()
            });
    }
}
