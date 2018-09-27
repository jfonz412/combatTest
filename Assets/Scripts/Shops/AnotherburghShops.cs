using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherburghShops : MarketInventory
{
    protected override void InstantiateItems()
    {
        townShops.Add(
            "Townie",
            new Item[]
            {
                MasterItemList.IronChest(),
                MasterItemList.ClothShirt(),
                MasterItemList.IronDagger(),
                MasterItemList.WoodenShield(),
                MasterItemList.HealthPotion(),
                MasterItemList.HealthPotion(),
                MasterItemList.Log(),
                MasterItemList.Log(),
                MasterItemList.Log()
            });
        townShops.Add(
            "Barbarian",
            new Item[]
            {
                MasterItemList.Log(),
                MasterItemList.Log(),
                MasterItemList.Log()
            });
    }
}