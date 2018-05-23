using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class NPCSaveData : DataController {
    private Health health;
    private Loadout loadOut;
    private LoadShop myShop;

    public override string SaveData()
    {
        base.SaveData();
        NPCData data = PackageNPCData();
        SaveDataToFile(data, GetFileName());
        return GetFileName();
    }

    public override void LoadData()
    {
        base.LoadData();
        if (File.Exists(Application.persistentDataPath + GetFileName()))
        {
            NPCData data = (NPCData)LoadDataFromFile(GetFileName());
            ApplyDataToNPC(data);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
        }

        loadOut.EquipLoadout();
    }

    protected override void GatherComponents()
    {
        base.GatherComponents();
        health = GetComponent<Health>();
        loadOut = GetComponent<Loadout>();
        myShop = GetComponent<LoadShop>(); //may be null
    }

    //MAKE THIS OVERRIDE?
    private NPCData PackageNPCData()
    {
        NPCData data = new NPCData();

        Vector3 pos = transform.position;
        data.currentPosition = new SavedPosition { x = pos.x, y = pos.y, z = pos.z };

        data.currentHealth = health.GetCurrentHealth();
        if (myShop != null)
        {
            data.currentShopInventory = myShop.GetCurrentInventory();
        }

        return data;
    }

    //MAKE THIS OVERRIDE?
    private void ApplyDataToNPC(NPCData data)
    {
        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);
        health.ApplyCurrentHealth(data.currentHealth);

        if (myShop != null)
        {
            myShop.UpdateInventory(UnpackSavedShopInventory(data.currentShopInventory));
        }
    }

    private string GetFileName()
    {
        string filePath = "/NPC_" + gameObject.name + ".dat";
        return filePath;
    }

    private List<Item> UnpackSavedShopInventory(SavedItem[] savedInventory)
    {
        //Debug.Log("Unpacking shop inventory for " + gameObject.name);
        List<Item> loadedItems = new List<Item>();

        if (savedInventory == null)
        {
            //Debug.Log("savedInventory is null for " + gameObject.name);
            return loadedItems;
        }
            

        for (int i = 0; i < savedInventory.Length; i++)
        {
            if (savedInventory[i].fileName == null)
                continue;

            Item item = (Item)Instantiate(Resources.Load(savedInventory[i].fileName)); 
            item.quantity = savedInventory[i].quantity;

            loadedItems.Add(item);
            //Debug.Log(item);
        }
        return loadedItems;
    }
}

[Serializable]
public class NPCData : Data
{
    public SavedPosition currentPosition;
    public float currentHealth;
    public SavedItem[] currentShopInventory;
}