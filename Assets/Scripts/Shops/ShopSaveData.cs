using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShopSaveData : DataController {
    MarketInventory shops;

    private string fileName = "/ShopData.dat"; //MAKE THIS DYNAMIC, GIVE DEMOSHOP A BASE SCRIPT THIS SCRIPT CAN GRAB

    protected override void GatherComponents()
    {
        base.GatherComponents();
        shops = GetComponent<MarketInventory>();
    }

    public override string SaveData()
    {
        base.SaveData();
        ShopData data = PackageShopData();
        SaveDataToFile(data, tempDirectory + fileName);
        return fileName;
    }

    //we attempt to load temp data first, because temp data is deleted after a manual save
    public override void LoadData()
    {
        base.LoadData();
        ShopData data;

        if (File.Exists(Application.persistentDataPath + tempDirectory + fileName))
        {
            data = (ShopData)LoadDataFromFile(tempDirectory + fileName);
        }
        else if (File.Exists(Application.persistentDataPath + permDirectory + fileName))
        {
            Debug.Log("Loading perm ShopData");
            data = (ShopData)LoadDataFromFile(permDirectory + fileName);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
            shops.LoadShops(); //run with nothing
            return;
        }

        ApplyData(data);
    }

    private ShopData PackageShopData()
    {
        ShopData data = new ShopData();
        data.shopInventory = shops.SaveShops();
        return data;
    }

    private void ApplyData(ShopData data)
    {
        shops.LoadShops(data.shopInventory);
    }
}

[Serializable]
public class ShopData : Data
{
    public Dictionary<string, Item[]> shopInventory;
}

