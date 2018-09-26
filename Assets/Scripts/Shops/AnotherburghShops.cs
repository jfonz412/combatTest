using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherburghShops : MarketInventory
{
    protected override void InstantiateItems()
    {
        townShops.Add(
            "TestB",
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
        townShops.Add(
            "TestB (1)",
            new Item[]
            {
                MasterItemList.Log(),
                MasterItemList.Log(),
                MasterItemList.Log()
            });
    }
}